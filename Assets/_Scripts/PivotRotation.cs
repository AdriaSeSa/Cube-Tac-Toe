using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PivotRotation : MonoBehaviour
{

    private List<GameObject> _activeSide;
    private Vector3 _localForward;
    private Vector3 _mouseRef;
    private bool _isDragging = false;
    private bool _autoRotating = false;
    private float _sensitivity = 0.4f;
    private Vector3 _rotation;
    private Quaternion _targetQuaternion;
    private float _speed = 300f;    
    private ReadCube _readCube;
    private CubeState _cubeState;
    
    // Start is called before the first frame update
    void Start()
    {
        _readCube = FindObjectOfType<ReadCube>();
        _cubeState = FindObjectOfType<CubeState>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDragging)
        {
            SpinSide(_activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                RotateToRightAngle();
            }
        }

        if (_autoRotating)
        {
            AutoRotate();
        }
        
    }

    private void SpinSide(List<GameObject> side)
    {
        // Reset the rotation
        _rotation = Vector3.zero;
        
        // Current mouse position minus the last mouse position
        Vector3 mouseOffset = (Input.mousePosition - _mouseRef);
        if (side == _cubeState.front)
        {
            _rotation.x = (mouseOffset.x + mouseOffset.y) * _sensitivity * 1;
        }
        if (side == _cubeState.back)
        {
            _rotation.x = (mouseOffset.x + mouseOffset.y) * _sensitivity * -1;
        }
        if (side == _cubeState.up)
        {
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _sensitivity * -1;
        }
        if (side == _cubeState.down)
        {
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _sensitivity * 1;
        }
        if (side == _cubeState.left)
        {
            _rotation.z = (mouseOffset.x + mouseOffset.y) * _sensitivity * -1;
        }
        if (side == _cubeState.right)
        {
            _rotation.z = (mouseOffset.x + mouseOffset.y) * _sensitivity * 1;
        }
        
        // Rotate
        transform.Rotate(_rotation, Space.Self);
        
        // Store mouse position for the next time this method is called
        _mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side)
    {
        _activeSide = side;
        _mouseRef = Input.mousePosition;
        _isDragging = true;
        // We create a vector to rotate around
        _localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        
        // Round vector to the nearest 90 degrees

        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        _targetQuaternion.eulerAngles = vec;
        _autoRotating = true;

    }

    private void AutoRotate()
    {
        _isDragging = false;
        float step = _speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetQuaternion, step);
        
        // If within one degree, set angle to the target angle and end the rotation
        if (Quaternion.Angle(transform.localRotation, _targetQuaternion) <= 1)
        {
            transform.localRotation = _targetQuaternion;
            // Unparent the little cubes
            _cubeState.PutDown(_activeSide, transform.parent);
            
            _readCube.ReadState();

            _autoRotating = false;
            _isDragging = false;
        }
    }
}
