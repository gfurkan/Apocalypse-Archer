using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab, handArrow, cameraObject;

    [SerializeField]
    private Transform arrowPosition;
    GameObject arrow;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private AudioClip[] clips;

    PlayerMovement playerMovement;
    Animator animatorCharacter;
    SplineFollower follower;
    Rigidbody rb;
    AudioSource audioSource;

    private int arrowCount = 1;
    
    private bool aimandShoot = false, arrowHit = false, shootComplete=true;
    private bool _characterDied = false,destroyScript=false;
    public bool characterDied
    {
        get
        {
            return _characterDied;
        }
    }
    void Start()
    {
        
        animatorCharacter = GetComponent<Animator>();
        follower = transform.GetComponentInParent<SplineFollower>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (shootComplete && !_characterDied)
        {
            if (playerMovement.click)
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

        arrow = Instantiate(arrowPrefab, arrowPosition.transform.position, arrowPosition.transform.rotation,transform.root);
        arrow.transform.name = "Arrow " + arrowCount;
        arrowCount++;

        handArrow.SetActive(false);
        follower.followSpeed *= 2;
        playerMovement.click = false;
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
            _characterDied = true;

            audioSource.clip = clips[1];
            audioSource.Play();
            
            animatorCharacter.SetBool("CharacterDeath", true);
            animatorCharacter.applyRootMotion = true;

            transform.GetComponentInParent<SplineFollower>().enabled = false;
            cameraObject.GetComponentInChildren<CameraMovement>().enabled = false;

            LevelManager.instance.levelFail = true;
            Destroy(this);

        }
    }
 
    #endregion
}
