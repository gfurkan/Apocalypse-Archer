using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="New Zombie Prop",menuName ="Zombie Property")]
public class ZombieProperties : ScriptableObject
{
    [SerializeField]
    [Tooltip("Ýf true zombie walks, if false runs.")] private bool _walkOrRun=false;
    [SerializeField]
    [Tooltip("Ýf true start position of zombie is left, if false right.")] private bool _zombiePosition = false;

    public bool walkOrRun => _walkOrRun;
    public bool zombiePosition => _zombiePosition;
}
