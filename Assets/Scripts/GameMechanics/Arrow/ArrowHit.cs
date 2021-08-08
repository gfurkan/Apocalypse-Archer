using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class ArrowHit : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    ZombieCombat zombieCombat;

    [SerializeField]
    private ParticleSystem bloodEffectPrefab;

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
        #region Arrow Sticking
        if (other.gameObject.tag != "ZombieAttackZone" && other.gameObject.tag != "ZombieArm" && other.gameObject.tag != "LevelEnd")
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
            zombieCombat = other.gameObject.transform.parent.gameObject.GetComponentInChildren<ZombieCombat>();
            var bloodEffect = Instantiate(bloodEffectPrefab, transform.position, Quaternion.Euler(0, other.gameObject.transform.eulerAngles.y, 0), transform.parent = other.gameObject.transform);
            bloodEffect.Play();

            zombieCombat.zombieHealth--;
            if (zombieCombat.zombieHealth == 0)
            {
                KillZombie(other.gameObject);
            }

        }
        if(other.gameObject.tag == "ZombieWarrior")
        {
            zombieCombat = other.gameObject.transform.parent.gameObject.GetComponentInChildren<ZombieCombat>();
            var bloodEffect = Instantiate(bloodEffectPrefab, transform.position, Quaternion.Euler(0, other.gameObject.transform.eulerAngles.y, 0), transform.parent = other.gameObject.transform);
            bloodEffect.Play();

            zombieCombat.zombieWarriorHealth--;
            if (zombieCombat.zombieWarriorHealth == 0)
            {
                KillZombie(other.gameObject);
            }
            
        }
        void KillZombie(GameObject enemy)
        {
            enemy = other.gameObject.transform.parent.gameObject;
            Collider[] bodyColliders = enemy.GetComponentInChildren<ZombieCombat>().bodyColliders;

            foreach (Collider col in bodyColliders)
            {
                Destroy(col);
            }

            audioSource = enemy.gameObject.GetComponent<AudioSource>();
            audioSource.Play();

            Animator animatorZombie;
            animatorZombie = enemy.GetComponent<Animator>();
            animatorZombie.SetBool("ZombieDeath", true);

            enemy.GetComponent<SplineFollower>().enabled = false;
        }
        #endregion
    }

}

