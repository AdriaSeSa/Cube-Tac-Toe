using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    private CubeState _cubeState;
    private ReadCube _readCube;
    private int layermask = 1 << 8;
    
    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindObjectOfType<CubeState>();
        _readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Read the current state of the cube
            _readCube.ReadState();
            Debug.Log("PickUp");
            // Raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layermask))
            {
                // Save the face we hit
                GameObject face = hit.collider.gameObject;

                // If face hit is on the center or on a corner
                if (!face.transform.parent.gameObject.CompareTag("CenterOrCorners"))
                {
                    for (int i = 0; i < face.transform.parent.childCount; i++)
                    {
                        if (face != face.transform.parent.GetChild(i).gameObject && face.transform.parent.GetChild(i).gameObject.activeSelf)
                        {
                            face = face.transform.parent.GetChild(i).gameObject;
                            break;
                        }
                    }
                }
             
                // Make a list of all the sides
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    _cubeState.up,
                    _cubeState.down,
                    _cubeState.left,
                    _cubeState.right,
                    _cubeState.front,
                    _cubeState.back
                };
                // if the face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        // Pick it up
                        _cubeState.PickUp(cubeSide);
                        Debug.Log("PickUp");
                    }
                }
            }
        }
    }
}
