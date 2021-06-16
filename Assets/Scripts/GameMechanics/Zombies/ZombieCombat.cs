using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class ZombieCombat : MonoBehaviour

{
    [SerializeField]
    private Collider[] _bodyColliders;

    public Collider[] bodyColliders
    {
        get
        {
            return _bodyColliders;
        }
    }
    Animator animatorZombie;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            animatorZombie = transform.GetComponentInParent<Animator>();
            animatorZombie.SetBool("ZombieAttack", true);
            transform.GetComponentInParent<SplineFollower>().enabled = false;
        }
    }
}
