using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager _Instance = null;
    public static InputManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    #endregion

    private bool _drag = false, _click = false, _draggedLeft = false, _draggedRight = false;
    private Vector3 _touchPos, _touchPosNext, _direction;

    #region Get Set

    LevelManager levelManager;
    public bool drag 
    {
        get
        {
            return _drag;
        }
        set
        {
            _drag = value;
        }
    }
    public bool click
    {
        get
        {
            return _click;
        }
        set
        {
            _click = value;
        }
    }
    public Vector3 touchPos
    {
        get
        {
            return _touchPos;
        }
        set
        {
            _touchPos = value;
        }
    }
    public bool draggedLeft => _draggedLeft;
    public bool draggedRight => _draggedRight;

    #endregion
    private void Awake()
    {
        _Instance = transform.GetComponent<InputManager>();
    }
    private void Start()
    {
        levelManager = LevelManager.instance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _drag = false;
            _touchPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _touchPosNext= Input.mousePosition;
            _direction = _touchPosNext - _touchPos;
            if (_direction.x > 25)
            {
                _drag = true;
               // touchPos = touchPosNext;
                _draggedLeft = false;
                _draggedRight = true;
            }
            if (_direction.x < -25)
            {
                _drag = true;
               // touchPos = touchPosNext;
                _draggedRight = false; 
                _draggedLeft = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!_drag)
            {
                _click = true;
            }
        }
    }
}
