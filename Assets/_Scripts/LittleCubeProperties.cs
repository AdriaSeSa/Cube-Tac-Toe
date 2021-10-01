using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleCubeProperties : MonoBehaviour
{
    
    // 0 = Front
    // 1 = Back
    // 2 = Left
    // 3 = Right
    // 4 = Up
    // 5 = Down
    private Color[] facesColor;

    public MeshRenderer[] faces;

    private SelectLittleCube _selectLittleCube;

    public bool[] isMarkedO = new bool[6];
    public bool[] isMarkedX = new bool[6];

    public GameObject[] xMarks = new GameObject[6];
    public GameObject[] oMarks = new GameObject[6];   
    private void Start()
    {

        _selectLittleCube = FindObjectOfType<SelectLittleCube>();
        
        facesColor = new Color[6];
        
        facesColor[0] = new Color(1, 0.5f, 0, 1);
        facesColor[0].a = 0.5f;
        facesColor[1] =  Color.blue;
        facesColor[1].a = 0.5f;
        facesColor[2] = Color.yellow;
        facesColor[2].a = 0.5f;
        facesColor[3] = Color.white;
        facesColor[3].a = 0.5f;
        facesColor[4] = Color.green;
        facesColor[4].a = 0.5f;
        facesColor[5] = Color.red;
        facesColor[5].a = 0.5f;
    }

    private void Update()
    {
        if (!_selectLittleCube.isHighlighting)
        {
            UnHighLightFaces();
        }
        
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].material.color = facesColor[i];
        }
    }
    
    
    public void HighlightFace(char face, int highlight = 1)
    {
        switch (face)
        {
            case 'F':
                facesColor[0].a = highlight;
               UnHighLightFaces(0);
                break;
            case 'B':
                facesColor[1].a = highlight;
                UnHighLightFaces(1);
                break;
            case 'L':
                facesColor[2].a = highlight;
                UnHighLightFaces(2);
                break;
            case 'R':
                facesColor[3].a = highlight;
                UnHighLightFaces(3);
                break;
            case 'U':
                facesColor[4].a = highlight;
                UnHighLightFaces(4);
                break;
            case 'D':
                facesColor[5].a = highlight;
                UnHighLightFaces(5);
                break;
            case 'N':
                UnHighLightFaces();
                break;
        }
    }

    private void UnHighLightFaces(int exception = 99)
    {
        for (int i = 0; i < facesColor.Length; i++)
        {
            if (i != exception)
            {
                facesColor[i].a = 0.5f;
            }
        }
    }

    /// <summary>
    /// 0 = X 1 = O
    /// </summary>
    /// <param name="xo"></param>
    public bool MarkFace(char face, int xo)
    {
        int currentFace = 0;
        bool ret = false;
        switch (face)
        {
            case 'F':
                currentFace = 0;
                break;
            case 'B':
                currentFace = 1;
                break;
            case 'L':
                currentFace = 2;
                break;
            case 'R':
                currentFace = 3;
                break;
            case 'U':
                currentFace = 4;
                break;
            case 'D':
                currentFace = 5;
                break;
        }
        if (xo == 0 && !isMarkedO[currentFace] && !isMarkedX[currentFace] )
        {
            isMarkedX[currentFace] = true;
            xMarks[currentFace].SetActive(true);
            ret = true;
        }
        if (xo == 1 && !isMarkedX[currentFace] && !isMarkedO[currentFace])
        {
            isMarkedO[currentFace] = true;
            oMarks[currentFace].SetActive(true);
            ret = true;
        }

        return ret;
    }
}
