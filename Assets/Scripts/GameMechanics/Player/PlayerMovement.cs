using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform leftPosition, rightPosition;
    [SerializeField]
    private float movementSpeed = 0;

    private bool _moveLeft=false, _moveRight = false;
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

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(transform.localPosition.x);
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveLeft = true;
            _moveRight = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
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
    
}
