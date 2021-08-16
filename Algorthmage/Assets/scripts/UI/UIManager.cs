using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject SpellUI;
    
    private MenuControls menuControls;

    private void Awake() {
        menuControls = new MenuControls();
    }

    private void OnEnable() {
        //Map and Enable UI controls
        menuControls.Menu.Toggle.performed += toggleUI;
        menuControls.Menu.Toggle.Enable();
    }

    private void OnDisable() {
        //Demap and Disable UI controls to avoid memory leaks
        menuControls.Menu.Toggle.performed -= toggleUI;
        menuControls.Menu.Toggle.Disable();
    }

    public void toggleUI(InputAction.CallbackContext context) {
        print("Esc pressed");
        SpellUI.SetActive(!SpellUI.activeSelf);
    }
}
