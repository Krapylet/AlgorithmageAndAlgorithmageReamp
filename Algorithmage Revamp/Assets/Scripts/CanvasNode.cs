using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNode : MonoBehaviour
{ 
    public List<CanvasNode> neighbors;
    public CanvasNode prevNeighbor { 
        get { return neighbors[0]; }
        set { neighbors.Insert(0, value);
        }
    }
    public int ID;

    private void Update() {
        foreach (CanvasNode neighbor in neighbors) {
            Vector3 nbPos = neighbor.gameObject.transform.position;

            Debug.DrawLine(nbPos, transform.position, Color.black);
        }
    }
}
