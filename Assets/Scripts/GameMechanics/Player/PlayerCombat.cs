using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private GameObject handArrow;
    [SerializeField]
    private GameObject cameraObject;

    [SerializeField]
    private Transform arrowPosition;
    GameObject arrow;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private AudioClip[] clips;

    Animator animatorCharacter;
    SplineFollower follower;
    Rigidbody rb;
    AudioSource audioSource;
    

    private bool aimandShoot = false, arrowHit = false,shootComplete=true;
    private int arrowCount = 1;

    void Start()
    {
        animatorCharacter = GetComponent<Animator>();
        follower = transform.GetComponentInParent<SplineFollower>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(shootComplete)
        {
            if (Input.GetMouseButtonDown(0))
            {

                aimandShoot = true;
                animatorCharacter.SetBool("AimandShoot", aimandShoot);
                follower.followSpeed /= 2; // Character speed decreased while shooting.
                handArrow.SetActive(true);
                shootComplete = false;
            }
        }    
    }
    #region Arrow
    void CreateArrow() // Called in character animation events.
    {
        audioSource.clip = clips[0];
        audioSource.Play();
        arrow = Instantiate(arrowPrefab, arrowPosition.transform.position, arrowPosition.transform.rotation);
        arrow.transform.name = "Arrow " + arrowCount;
        arrowCount++;
        handArrow.SetActive(false);
        follower.followSpeed *= 2;
        ArrowMovement();
    }

    void ArrowMovement()
    {
        rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = arrow.transform.forward * arrowSpeed;
        rb.useGravity = true;
        aimandShoot = false;
        animatorCharacter.SetBool("AimandShoot", aimandShoot);
        shootComplete = true;
    }
    #endregion
    #region Character Death
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "ZombieArm" || other.gameObject.tag == "Obstacle")
        {
            audioSource.clip = clips[1];
            audioSource.Play();
            animatorCharacter.SetBool("CharacterDeath", true);
            transform.GetComponentInParent<SplineFollower>().enabled = false;
            animatorCharacter.applyRootMotion = true;
            cameraObject.GetComponentInChildren<CameraMovement>().enabled = false;

            Destroy(this);
            Destroy(transform.GetComponent<PlayerMovement>());
        }
    }
    #endregion
}
