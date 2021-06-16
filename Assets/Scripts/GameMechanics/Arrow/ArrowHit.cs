using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class ArrowHit : MonoBehaviour
{
    Collider[] bodyColliders;
    Animator animatorZombie;
    Rigidbody rb;
    AudioSource audioSource;

    bool hit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        if (!hit)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Sticking Arrow
        if (other.gameObject.tag != "ZombieAttackZone" && other.gameObject.tag != "ZombieArm")
        {
            hit = true;
            rb.isKinematic = true;

            Destroy(transform.GetComponent<Collider>());
            Destroy(transform.GetComponent<TrailRenderer>());
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        if (other.gameObject.tag == "Zombie")
        {
            transform.parent = other.transform;
            audioSource.Play();
        }
        #endregion

        #region Zombie Death 
        if (other.gameObject.tag == "Zombie")
        {
            
            GameObject enemy = other.gameObject.transform.parent.gameObject;
            bodyColliders = enemy.GetComponentInChildren<ZombieCombat>().bodyColliders;

            foreach (Collider col in bodyColliders)
            {
                Destroy(col);
            }

            audioSource =enemy.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            animatorZombie = enemy.GetComponent<Animator>();
            animatorZombie.SetBool("ZombieDeath", true); 
            animatorZombie.applyRootMotion = true; // Added this line because zombies weren't grounded when death animation work.
            enemy.GetComponent<SplineFollower>().enabled = false;
        }
        #endregion
    }
}

