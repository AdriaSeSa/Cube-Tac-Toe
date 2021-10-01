using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateRubikCube : MonoBehaviour
{
    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;

    private Vector3 _previousMousePosition;
    private Vector3 _mouseDelta;
    
    public GameObject target;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float dragSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    private void Drag()
    {
        if (Input.GetMouseButton(1))
        {
           /* //Move the cube during pressing the button to provide visual feedback
            _mouseDelta = Input.mousePosition - _previousMousePosition;
            _mouseDelta *= dragSpeed;
            
            transform.rotation = Quaternion.Euler(_mouseDelta.y, -_mouseDelta.x, _mouseDelta.z) * transform.rotation;*/
        }
        else
        {
            // Move to the target position

            if (transform.rotation != target.transform.rotation)
            {
                var step = rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }

        _previousMousePosition = Input.mousePosition;
    }

    public void SwipeWithButton(int direction)
    {
        switch (direction)
        {
            // Left
            case 0:
                target.transform.Rotate(0,90,0,Space.World);
                break;
            // Right
            case 1:
                target.transform.Rotate(0,-90,0,Space.World);
                break;
            // Up
            case 2:
                target.transform.Rotate(0,0,-90,Space.World);
                break;
            // Down
            case 3:
                target.transform.Rotate(0,0,90,Space.World);
                break;
            // Rotate Left
            case 4:
                target.transform.Rotate(90,0,0,Space.World);
                break;
            // Rotate Right
            case 5:
                target.transform.Rotate(-90,0,0,Space.World);
                break;
        }
    }
    
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Get the 2D position of the first mouse click
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(1))
        {
            // Get the 2D position of the second right click
            _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            float totalDrag = Mathf.Abs(_secondPressPos.x - _firstPressPos.x) +
                              Mathf.Abs(_secondPressPos.y - _firstPressPos.y);
            
            if (totalDrag < 300.0f) return;
            
            // Create a vector from the first and second mouse click positions
            _currentSwipe = new Vector2(_secondPressPos.x - _firstPressPos.x, 
                                        _secondPressPos.y - _firstPressPos.y);
            
            // Normalize the result
            _currentSwipe.Normalize();

            if (LeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0,90,0,Space.World);
            }
            else if (RightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0,-90,0,Space.World);
            }
            else if (UpLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(90,0, 0, Space.World);
            }
            else if (UpRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0,0,-90,Space.World);
            }
            else if (DownLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0,0,90,Space.World);
            }
            else if (DownRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(-90, 0,0,Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return swipe.x < 0 && swipe.y > -0.5 && swipe.y < 0.5; 
    }
    
    bool RightSwipe(Vector2 swipe)
    {
        return swipe.x > 0 && swipe.y > -0.5 && swipe.y < 0.5; 
    }
    bool UpLeftSwipe(Vector2 swipe)
    {
        return swipe.x < 0 && swipe.y > 0; 
    }
    bool UpRightSwipe(Vector2 swipe)
    {
        return swipe.x > 0 && swipe.y > 0; 
    }
    bool DownLeftSwipe(Vector2 swipe)
    {
        return swipe.x < 0 && swipe.y < 0; 
    }
    bool DownRightSwipe(Vector2 swipe)
    {
        return swipe.x > 0 && swipe.y < 0; 
    }
    
}
