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
    public bool moveLeft => _moveLeft;
    public bool moveRight => _moveRight;

    InputManager inputManagar;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManagar = InputManager.Instance;
    }

    void Update()
    {
        ChangePosition();

        if (_moveLeft)
        {
            MoveCharacter(leftPosition);
        }
        if (_moveRight)
        {
            MoveCharacter(rightPosition);
        }
    }
    void ChangePosition()
    {
            if (inputManagar.draggedLeft)
            {
                _moveLeft = true;
                _moveRight = false;
            }
            if (inputManagar.draggedRight)
            {
                _moveLeft = false;
                _moveRight = true;
            }
    }

    void MoveCharacter(Transform characterTransform)
    {
        rb.transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(characterTransform.localPosition.x,transform.localPosition.y,transform.localPosition.z), 0.1f* Time.deltaTime*movementSpeed);
    }
}
