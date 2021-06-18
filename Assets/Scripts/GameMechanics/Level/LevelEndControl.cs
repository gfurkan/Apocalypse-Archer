using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using DG.Tweening;

public class LevelEndControl : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject cameraPosLeft,cameraPosRight,camera;

    GameObject nextLevel;
    Animator animator;

    private bool changeCameraPos = false;
    private bool _levelEnded = false;
    public bool levelEnded
    {
        get
        {
            return _levelEnded;
        }
        set
        {
            _levelEnded = value;
        }
    }
    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        if (changeCameraPos)
            CameraPosition();
    }
    private void Update()
    {
        StartCoroutine(nextLevelButton());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextLevel = GameObject.FindGameObjectWithTag("NextLevelButton");
            _levelEnded = true;
            Destroy(other.GetComponent<PlayerCombat>());
            Destroy(other.GetComponent<PlayerMovement>());
            Destroy(camera.GetComponent<CameraMovement>());
            Destroy(other.GetComponentInParent<SplineFollower>());
            GameObject.FindGameObjectWithTag("Bow").SetActive(false);

            changeCameraPos = true;

            // Rotating player to see dance animation.
            other.transform.LookAt(cameraPosLeft.transform);
            other.transform.eulerAngles=new Vector3(0,other.transform.eulerAngles.y,0);

            animator = other.GetComponent<Animator>();
            animator.applyRootMotion = true;
            animator.SetBool("Dance", true);
            Destroy(this.GetComponent<Collider>());
        }
    }
    void CameraPosition()
    {
        
        if (playerMovement.moveLeft)
        {
            
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPosLeft.transform.position, 0.25f * Time.deltaTime);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, cameraPosLeft.transform.rotation, 0.25f * Time.deltaTime);
        }
        else
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPosRight.transform.position, 0.25f * Time.deltaTime);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, cameraPosRight.transform.rotation, 0.25f * Time.deltaTime);
        }
    }
    IEnumerator nextLevelButton()
    {
        if (_levelEnded)
        {
            yield return new WaitForSeconds(2f);
            nextLevel.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 2;
            if (nextLevel.GetComponent<CanvasGroup>().alpha >= 0.5f)
            {
                nextLevel.GetComponent<Button>().interactable = true;
            }
        }
    }
}
