using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    private CubeState _cubeState;
    [SerializeField] private Transform up, down, left, right, front, back;
    
    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        UpdateMap(_cubeState.front, front);
        UpdateMap(_cubeState.back, back);
        UpdateMap(_cubeState.left, left);
        UpdateMap(_cubeState.right, right);
        UpdateMap(_cubeState.up, up);
        UpdateMap(_cubeState.down, down);
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
                //TODO: Check if the side is marked X or O, and activate mark on CubeMap
                
                /*if (face[i].transform.parent.gameObject.GetComponent<LittleCubeProperties>().isMarkedO[0])
                {
                    map.GetChild(0).gameObject.SetActive(true);
                }
                if (face[i].transform.parent.gameObject.GetComponent<LittleCubeProperties>().isMarkedX[0])
                {
                    map.GetChild(1).gameObject.SetActive(true);
                }*/
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.red;
            }

            i++;
        }
    }
}
