using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform tUp, tDown, tLeft, tRight, tFront, tBack;

    private int _layerMask = 1 << 8; // This layermask is for the faces of the cube only
    
    private CubeState _cubeState;
    private CubeMap _cubeMap;

    public GameObject emptyGO;
    
    private List<GameObject> _frontRays = new List<GameObject>();
    private List<GameObject> _backRays = new List<GameObject>();
    private List<GameObject> _leftRays = new List<GameObject>();
    private List<GameObject> _rightRays = new List<GameObject>();
    private List<GameObject> _upRays = new List<GameObject>();
    private List<GameObject> _downRays = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();
        
        _cubeState = FindObjectOfType<CubeState>();
        _cubeMap = FindObjectOfType<CubeMap>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void ReadState()
    {
        
        // Set the state of each position in the list of sides so we know what color is in what position
        _cubeState.up = ReadFace(_upRays, tUp);
        _cubeState.down = ReadFace(_downRays, tDown);
        _cubeState.left = ReadFace(_leftRays, tLeft);
        _cubeState.right = ReadFace(_rightRays, tRight);
        _cubeState.front = ReadFace(_frontRays, tFront);
        _cubeState.back = ReadFace(_backRays, tBack);
        
        // Update the map with the found positions
        _cubeMap.Set();
        
    }
    
    void SetRayTransforms()
    {
        // Populate the ray lists with raycasts eminating from the transform, angled towards the cube
        _upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        _downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        _leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        _rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        _frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        _backRays = BuildRays(tBack, new Vector3(0, 270, 0));

    }
    
    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        // The ray count is used to name the rays so we can be sure they are in the right order
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        
        // This creates 9 rays in the shape of the side of the cube
        // with Ray 0  at the top left and Ray 8 at the bottom right
        // [0][1][2]
        // [3][4][5]
        // [6][7][8]
        
        // Nested loops to iterate through the entire face
        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x+x, rayTransform.localPosition.y +y,
                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays; 
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            // Does the ray intersect any objects in the layermask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.green );
                facesHit.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }
        
       

        return facesHit;
    }
}
