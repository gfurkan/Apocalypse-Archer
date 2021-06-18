using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using DG.Tweening;

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

    GameObject replay;
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
    }

    void Update()
    {
        if (_characterDied)
        {
            StartCoroutine(playAgainButton());
        }

        if (shootComplete && !_characterDied)
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
        arrow = Instantiate(arrowPrefab, arrowPosition.transform.position, arrowPosition.transform.rotation,transform.root);
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
            replay = GameObject.FindGameObjectWithTag("ReplayButton");
            _characterDied = true;
            audioSource.clip = clips[1];
            audioSource.Play();
            animatorCharacter.SetBool("CharacterDeath", true);
            transform.GetComponentInParent<SplineFollower>().enabled = false;
            animatorCharacter.applyRootMotion = true;
            cameraObject.GetComponentInChildren<CameraMovement>().enabled = false;

            if (destroyScript)
            {
                Destroy(this);
            }
            Destroy(transform.GetComponent<PlayerMovement>());
        }
    }
    IEnumerator playAgainButton()
    {
        yield return new WaitForSeconds(0.5f);
        replay.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 2;

        if (replay.GetComponent<CanvasGroup>().alpha >= 0.5f)
        {
           replay.GetComponent<Button>().interactable = true;
           destroyScript = true;

        }
    }
    #endregion
}
