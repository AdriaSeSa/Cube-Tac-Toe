using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectLittleCube : MonoBehaviour
{
    private int layermask = 1 << 8;

    public bool isHighlighting;

    public LittleCubeProperties currentHit;
    private char _currentHitChar;
    private LittleCubeProperties _lastHit;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLittleCubeSelect();

        if (Input.GetMouseButtonDown(1))
        {
            if (currentHit != null)
            {
                if (currentHit.MarkFace(_currentHitChar, _gameManager.currentPlayerTurn))
                {
                    _gameManager.NextTurn();
                }
            }
        }
        
    }

    private void CheckLittleCubeSelect()
    {
        // Raycast from the mouse towards the cube to see if a cube is hit
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, layermask))
        {
            isHighlighting = true;
          
            //Change little cube color
            LittleCubeProperties _properties = hit.collider.transform.parent.gameObject.GetComponent<LittleCubeProperties>();
            currentHit = _properties;
            _currentHitChar = hit.collider.gameObject.name[0];
            
            _properties.HighlightFace(_currentHitChar);
            
            // UnHihglight the last littlecube
            if (_lastHit != null && _lastHit != currentHit)
            {
                _lastHit.HighlightFace('N');
            }

            _lastHit = currentHit;

        }
        else
        {
            isHighlighting = false;
        }
    }
}
