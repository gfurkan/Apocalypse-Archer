using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class ZombieMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Ýf true zombie walks, if false zombie runs.")] private bool walkOrRun=false;
    [SerializeField]
    [Tooltip("Ýf true start position of zombie is left, if false right.")] private bool zombiePosition = false;

    [SerializeField]
    private float zombieWalkSpeed = 0, zombieRunSpeed = 0;
    
    
    Animator animatorZombie;
    SplineFollower splineFollower;

    void Start()
    {
        animatorZombie = GetComponent<Animator>();
        splineFollower = GetComponent<SplineFollower>();
        ZombieFirstPosition();

        if (walkOrRun)
        {
            ZombieWalk();
        }
        else
        {
            ZombieRun();
        }

    }
    void ZombieWalk()
    {
        animatorZombie.SetBool("ZombieRun", false);
        animatorZombie.SetBool("ZombieWalk", true);
        splineFollower.followSpeed = zombieWalkSpeed;
    }
    void ZombieRun()
    {
        animatorZombie.SetBool("ZombieRun", true);
        animatorZombie.SetBool("ZombieWalk", false);
        splineFollower.followSpeed = zombieRunSpeed;
    }
    void ZombieFirstPosition()
    {
        if (zombiePosition)
        {
            splineFollower.offset = new Vector2(-2.5f, 0);
        }
        else
        {
            splineFollower.offset = new Vector2(2.7f, 0);
        }
    }

}
