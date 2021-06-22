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
         /*   if (_Instance == null)
            {
                _Instance = new GameObject("InputManager").AddComponent<InputManager>();
            }*/
            return _Instance;
        }
    }
    #endregion
    #region Get Set
    private bool _drag = false, _click = false,_draggedLeft=false,_draggedRight=false;
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
    public bool draggedLeft
    {
        get
        {
            return _draggedLeft;
        }

    }
    public bool draggedRight
    {
        get
        {
            return _draggedRight;
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
    #endregion
    private Vector3 touchPos, touchPosNext, direction;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _drag = false;
            touchPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            touchPosNext= Input.mousePosition;
            direction = touchPosNext - touchPos;
            if (direction.x > 25)
            {
                _drag = true;
               // touchPos = touchPosNext;
                _draggedLeft = false;
                _draggedRight = true;
            }
            if (direction.x < -25)
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
