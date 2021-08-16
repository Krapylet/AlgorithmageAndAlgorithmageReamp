using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float lerpArg;

    private Vector2 currSpeed;

    private PlayerControls playerControls;

    public void Awake() {
        playerControls = new PlayerControls();
        currSpeed = new Vector2(0, 0);
    }

    private void OnEnable() {
        playerControls.PlayerKeyboard.Move.Enable();
    }

    private void OnDisable() {
        playerControls.PlayerKeyboard.Move.Disable();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        //get intended move direction
        Vector2 inputDir = playerControls.PlayerKeyboard.Move.ReadValue<Vector2>().normalized;
        
        //Lerp previous move direction towards intended move direction
        currSpeed = Vector2.Lerp(currSpeed, inputDir * speed, lerpArg);

        //transform vector2 into vector3 before applying
        Vector3 moveDistance = new Vector3(currSpeed.x, 0, currSpeed.y);
        gameObject.transform.Translate(moveDistance);
    }
}
