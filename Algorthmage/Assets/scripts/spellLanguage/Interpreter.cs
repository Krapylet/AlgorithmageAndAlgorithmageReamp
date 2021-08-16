using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class Interpreter : MonoBehaviour
{
    [SerializeField] private GameObject SpellRangeMarker;
    [SerializeField] private GameObject SpellCursor;

    [SerializeField] private Sprite Circle;

    private PlayerControls playerControls;
    private bool waitingForClick;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.PlayerKeyboard.LeftClick.Enable();
        playerControls.PlayerKeyboard.LeftClick.performed += delegate { waitingForClick = false; };
    }

    private void OnDisable() {
        playerControls.PlayerKeyboard.LeftClick.Disable();
        playerControls.PlayerKeyboard.LeftClick.performed -= delegate { waitingForClick = false; };
    }

    public void Cast(SpellNode spell) {
        string target = spell.GetTargetNode().GetTargetType();
        if(target == "Circle") {
            //Write a method that takes scale, range and targetType and who returns all targets
            //use yield to wait for target selection
        }
    }

    public IEnumerator SelectTargets(SpellNode spell) {
        int range = spell.GetRange().GetValue();
        int scale = spell.GetScale().GetValue();
        string targetType = spell.GetTargetNode().GetTargetType();
        if(targetType == "Circle") {
            SpellRangeMarker.SetActive(true);
            //Scale = diameter but range = radius, so range*2 = scale
            SpellRangeMarker.transform.localScale = new Vector3(range*2, range*2, range*2);

            SpellCursor.GetComponent<SpriteRenderer>().sprite = Circle;
            SpellCursor.transform.localScale = new Vector3(scale, scale, scale);
            
            waitingForClick = true;
            while (waitingForClick) {
                yield return null;
            }

            MethodNode methodNode = (MethodNode)spell.GetBody().GetContents()[0];
            print(methodNode.GetContents()[2].GetType());
            string damageType = ((ElementNode) methodNode.GetContents()[2]).GetValue();
            int damage = ((NumberNode)((MethodNode)spell.GetBody().GetContents()[0]).GetContents()[1]).GetValue();
            print("BANG, a large " + damageType + "ball dealing " + damage);

            SpellRangeMarker.SetActive(false);
            SpellCursor.GetComponent<SpriteRenderer>().sprite = null;

        }
        else {
            throw new Exception("Unknown target type - this shouldn't have been allowed to compile");
        }
    }
}
