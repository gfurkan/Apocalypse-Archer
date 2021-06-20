using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private float cameraRotationSpeed = 0;

    Vector3 distance;

    void Awake()
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1, character.transform.position.z);
        distance = transform.position - character.transform.position;
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, character.transform.position + distance, 0.5f);
        // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,character.transform.eulerAngles,1* Time.deltaTime*cameraRotationSpeed);
        transform.eulerAngles = character.transform.eulerAngles;
    }
}
