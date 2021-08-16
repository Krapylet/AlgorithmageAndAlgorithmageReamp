using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SpellCursor : MonoBehaviour
{
    public float maxCursorDistance;
    [SerializeField] GameObject Range;

    // Update is called once per frame
    void Update()
    {
        FollowMouse();   
    }

    private void FollowMouse() {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        Physics.Raycast(ray, out RaycastHit hitInfo, maxCursorDistance);

        //Lift the sprite a little over the ground. This shoulæd be replaced by a shader that renders it on top of everything but the UI.
        Vector3 mouseWorldPos = hitInfo.point;

        Vector3 distanceVector = mouseWorldPos - Range.transform.position;

        //if marker is further way than max range, bind the marker
        // divide by 2 because scale = diameter but we need the radius
        if(distanceVector.magnitude > Range.transform.localScale.x / 2) {
            mouseWorldPos = Range.transform.position + distanceVector.normalized * Range.transform.localScale.x / 2;
        }

        // This overlap should be replaced with a shader that renders the sprite above everything but the UI
        Vector3 overlapOffset = new Vector3(0, 0.1f, 0);

        gameObject.transform.position = mouseWorldPos + overlapOffset;
    }
}
