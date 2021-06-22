using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform leftPosition, rightPosition;
    [SerializeField]
    private float movementSpeed = 0;

    private bool _moveLeft = false, _moveRight = false;
    private Vector3 touchPos, touchPosNext, direction;

    private bool _drag = false, _click = false, _draggedLeft = false, _draggedRight = false;

    #region Get Set

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
    public bool moveLeft
    {
        get
        {
            return _moveLeft;
        }
    }
    public bool moveRight
    {
        get
        {
            return _moveRight;
        }
    }
    #endregion



    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TouchControls();
        MovePlayer();

        if (_moveLeft)
        {
            MoveLeft();
        }
        if (_moveRight)
        {
            MoveRight();
        }
    }
    void MovePlayer()
    {
            if (_draggedLeft)
            {
                _moveLeft = true;
                _moveRight = false;
            }
            if (_draggedRight)
            {
                _moveLeft = false;
                _moveRight = true;
            }
        

    }

    void MoveLeft()
    {
        rb.transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(leftPosition.localPosition.x,transform.localPosition.y,transform.localPosition.z), 0.1f* Time.deltaTime*movementSpeed);
    }
    void MoveRight()
    {
        rb.transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(rightPosition.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.1f*Time.deltaTime*movementSpeed);
    }
    void TouchControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _drag = false;
            touchPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            touchPosNext = Input.mousePosition;
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
