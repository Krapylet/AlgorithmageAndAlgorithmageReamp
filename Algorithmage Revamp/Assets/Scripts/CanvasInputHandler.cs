using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInputHandler : MonoBehaviour
{
    public float nodeInverval;
    public GameObject CanvasNode;   
    
    
    
    Camera mainCamera;
    CanvasNode prevStrokeNode;
    List<CanvasNode> nodes;

    int IDCounter = 0;


    private void Awake() {
        //Finds the camera when the game loads
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Initialize the empty list of nodes
        nodes = new List<CanvasNode>();
    }


    // Update is called once every frame
    void Update()
    {
        DrawHandler();
    }


    private void DrawHandler() {
        if (Input.GetButton("Fire1")) {

            //Convert the mouse Coords from screen space to World space
            Vector3 mouseWorldCoords = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            //we have add the plane distance of the canvas to the camera to the z position to place the node on the canvas.
            float planeDist = GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance;
            mouseWorldCoords = new Vector3(mouseWorldCoords.x, mouseWorldCoords.y, planeDist);

            //If first node in stroke, always place it
            if (prevStrokeNode == null) {
                PlaceNode(mouseWorldCoords);
            }
            else {
                // otherwise calculate the distance to the previously drawn node.
                float distToprevStrokeNode = (prevStrokeNode.gameObject.transform.position - mouseWorldCoords).magnitude;

                //If distance from last point is greater than nodeInverval, place pont on normalized direction vector times nodeInterval.
                if (distToprevStrokeNode > nodeInverval) {
                    PlaceNode(mouseWorldCoords);
                }
            }
        }

        //if we end the stroke, we don't need to remeber the previous node of the stroke.
        if (Input.GetButtonUp("Fire1")) {
            prevStrokeNode = null;
        }
    }


    private void PlaceNode(Vector3 pos) {

        //spawn node as child of CanvasInputHandler at corrected mouse pos
        CanvasNode newNode = Instantiate(CanvasNode, pos, transform.rotation, transform).GetComponent<CanvasNode>();
        newNode.ID = IDCounter++;

        //only set previous neighbor if this node isn't the first in a stroke.
        if(prevStrokeNode != null) {
            newNode.prevNeighbor = prevStrokeNode;

            prevStrokeNode.neighbors.Add(newNode);
        }

        //find mergable nodes
        List<CanvasNode> mergables = findMergeables(pos);

        //Merge all nodes into the current node
        Merge(newNode, mergables);

        //add the node ot the list of nodes
        nodes.Add(newNode);

        //lastly, set this node to be the previous node in the stroke
        prevStrokeNode = newNode;
    }


    //looks through all canvas nodes and checks if they are close enough to be merged with the new node.
    private List<CanvasNode> findMergeables(Vector3 pos){
        
        //Create empty list to hold the found mergable nodes
        List<CanvasNode> mergables = new List<CanvasNode>();

        //run though all the nodes and see if they are within the nodeInterval
        foreach (CanvasNode node in nodes) {

            //We don't want to merge with the previously drawn node in this stroke
            if (prevStrokeNode == node) { continue; }

            //calculate the distance to the newly placed node.
            float dist = (node.gameObject.transform.position - pos).magnitude;

            // add to mergeable set if inside reach of the new node.
            if(dist < nodeInverval) {
                mergables.Add(node);
            }
        }

        return mergables;
    }

    //Merge all the mergables into the newNode by collecting all the neighbors into one node.
    private void Merge(CanvasNode newNode, List<CanvasNode> mergeables) {

        //Initalize a holder for all the neighbors to be added to the new node.
        HashSet<CanvasNode> uniqueNeighbors = new HashSet<CanvasNode>();



        //For each node to be merged
        foreach (CanvasNode mergeable in mergeables) {

            if (mergeable.ID == 3) {
                ;
            }

            //Initialize holder for mergable neighbors.
            List<CanvasNode> mergeableNeighbors = new List<CanvasNode>();

            //replace all references to the node who is getting merged with the new node.
            //and note all neighbors who are also mergeable.
            foreach (CanvasNode neighbor in mergeable.neighbors) {
                int i = neighbor.neighbors.IndexOf(mergeable);
                
                //The new node might already be added to the neigbor
                //if that is the case we can just skip the loop
                bool neighborIsNewNode = i == -1;
                if (neighborIsNewNode) {
                    continue;
                }

                //replace mergable with newNode;
                neighbor.neighbors[i] = newNode;

                //add the neighboring mergeables to the collection
                if (mergeables.Contains(neighbor)){
                    mergeableNeighbors.Add(neighbor);
                }
            }

            //remove all mergable neighbors from the neighbors list.
            //We cannot do this earlier, as that would screw with the enumration of the foreach loop.
            foreach(CanvasNode mergeableNeighbor in mergeableNeighbors) {
                mergeable.neighbors.Remove(mergeableNeighbor);
            }
            

            //Then remove the merged node from the node list
            nodes.Remove(mergeable);

            //And add all its neighbors to the set of unique nodes (repeats are ignored)
            uniqueNeighbors.UnionWith(mergeable.neighbors);        
        }

        //We then destroy all mergeable nodes
        foreach (CanvasNode mergeable in mergeables) {
            Destroy(mergeable.gameObject);
        }

        //And lastly add all the unique neighbors found to the new node
        newNode.neighbors.AddRange(uniqueNeighbors);
    }
}
