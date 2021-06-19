using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class ZombieCombat : MonoBehaviour

{
    [SerializeField]
    private Collider[] _bodyColliders;

    private int _zombieWarriorHealth = 2;
    private int _zombieHealth = 1;

    #region Get Set
    public Collider[] bodyColliders
    {
        get
        {
            return _bodyColliders;
        }
    }

    public int zombieWarriorHealth
    {
        get
        {
            return _zombieWarriorHealth;
        }
        set
        {
            _zombieWarriorHealth = value;
        }
    }

    public int zombieHealth
    {
        get
        {
            return _zombieHealth;
        }
        set
        {
            _zombieHealth = value;
        }
    }
    #endregion
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
