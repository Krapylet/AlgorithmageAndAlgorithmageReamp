using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    public InputField nameField;
    public InputField scriptField;
    private string standardFilePath = "C:/Users/askej/Documents/Algorithmage";
    public string currentFilePath = "C:/Users/askej/Documents/Algorithmage";
    public GameObject listContentParent;
    public GameObject listEntryTemplate;

    [SerializeField] private GameObject interpreter;

    private Compiler compiler;

    private SpellNode spell;
    private PlayerControls playerControls;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.PlayerKeyboard.SpellHotkey1.Enable();
        playerControls.PlayerKeyboard.SpellHotkey1.performed += delegate { StartCoroutine(interpreter.GetComponent<Interpreter>().SelectTargets(spell)); };
    }

    private void OnDisable() {
        playerControls.PlayerKeyboard.SpellHotkey1.Disable();
        playerControls.PlayerKeyboard.SpellHotkey1.performed -= delegate { StartCoroutine(interpreter.GetComponent<Interpreter>().SelectTargets(spell)); };
    }


    // Start is called before the first frame update
    void Start(){
        compiler = new Compiler();
        UpdateSpellList();
    }


    //Deletes the spell at currentFilePath and saves the currently open spell.
    public void QuickSaveSpell(){
        //make sure game save directory exists
        checkForDirectory();

        //check if the spell was renamed and update file name accordingly
        String newFilePath = (standardFilePath + "/" + nameField.text + ".txt").Trim();
        if (currentFilePath.Equals(newFilePath)){
            File.WriteAllText(currentFilePath, nameField.text + "\n" + scriptField.text);
        }
        else{
            //replace old spell with new spell
            if (File.Exists(currentFilePath)){
                File.Delete(currentFilePath);
            }
            currentFilePath = newFilePath;
            File.WriteAllText(currentFilePath, nameField.text + "\n" + scriptField.text);
        }

        //TEST TEST TEST 
        //Save compiler output to test file
        spell = compiler.CompileSpell("Name : " + nameField.text + "\n" + scriptField.text);

        // TEST TEST TEST

        UpdateSpellList();
    }

    //Saves the currently open spell.
    //If a spell already has the same name, the old spell is overwritten
    public void SaveSpellAsNew(){
        //make sure game save directory exists
        checkForDirectory();

        //Save spell
        currentFilePath = ("C:/Users/askej/Documents/Algorithmage/" + nameField.text + ".txt").Trim();
        File.WriteAllText(currentFilePath, nameField.text + "\n" + scriptField.text);

        //TEST TEST TEST 
        //Save compiler output to test file
        spell = compiler.CompileSpell("Name : " + nameField.text + "\n" + scriptField.text);
        // TEST TEST TEST

        UpdateSpellList();
    }

    //Opens a file at path and loads the text into the games text fields
    public void SelectSpell(string name){
        String filePath = standardFilePath + "/" + name + ".txt";
        if (!File.Exists(filePath)){
            print("invalid file path: " + filePath);
        }
        else{
            String fileContent = File.ReadAllText(filePath);
            nameField.text = fileContent.Split(Environment.NewLine.ToCharArray())[0];
            scriptField.text = fileContent.Substring(nameField.text.Length + 1, fileContent.Length - 1 - nameField.text.Length);
        }
    }

    //check if game data directory exists, and create one if it doesn't
    public void checkForDirectory(){
        if (!Directory.Exists("C:/Users/askej/Documents/Algorithmage")){
            Directory.CreateDirectory("C:/Users/askej/Documents/Algorithmage");
        }
    }

    public void UpdateSpellList(){
        int index = 0;

        //Delete all old list elements
        Transform[] transformList = listContentParent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transformList.Length; i++){
            //element 0 is the content parent, and should not be deleted
            if(i != 0){
                Destroy(transformList[i].gameObject);
            }
        }


        //get all files in directory
        foreach(String file in Directory.GetFiles("C:/Users/askej/Documents/Algorithmage")){
            //get the spell names
            String spellNameWithType = Path.GetFileName(file);
            String spellName = spellNameWithType.Substring(0, spellNameWithType.Length - 4);
            
            //create list entry
            createSpellListEntry(spellName, index);
            index++;
        }
    }

    public GameObject createSpellListEntry(string name, int index){
        //spawn as child of listContentParent in order to be scrollable
        GameObject MenuEntry = Instantiate(listEntryTemplate, listContentParent.transform);
        
        //set name of button
        MenuEntry.GetComponentInChildren<Text>().text = name;
        
        //calculate correct position in relation to the content parent
        Vector2 MenuPos = listContentParent.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, index * (-30) - 15);
        MenuEntry.GetComponent<RectTransform>().anchoredPosition = MenuPos;

        //set up button to load correct spell on press
        MenuEntry.GetComponent<Button>().onClick.AddListener( delegate{ SelectSpell(name); });
        
        return MenuEntry;
    }
}
