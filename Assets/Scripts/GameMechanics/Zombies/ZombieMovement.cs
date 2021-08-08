using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEditor;

/*[CustomEditor(typeof(ZombieMovement))]
public class DropDownList : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ZombieMovement script = (ZombieMovement)target;
        GUIContent arrayLabel = new GUIContent("Movement Type");
        int tempmovInd = script.movementIndex;
        script.movementIndex = EditorGUILayout.Popup(arrayLabel, script.movementIndex, script.movementTypes);   ***DropDownList***
        if (tempmovInd != script.movementIndex)
        {
            EditorUtility.SetDirty((ZombieMovement)target);
            tempmovInd = script.movementIndex;
        }
    }

}*/
public class ZombieMovement : MonoBehaviour
{
    [SerializeField]
    private ZombieProperties zombieProperties;
    [SerializeField]
    private float zombieWalkSpeed = 0, zombieRunSpeed = 0;
  /*  [HideInInspector]
    public string[] movementTypes = new string[] { "Walk", "Run" };  ***DropDownList***
    [HideInInspector]
    public int movementIndex;*/

    Animator animatorZombie;
    SplineFollower splineFollower;

    void Start()
    {
        animatorZombie = GetComponent<Animator>();
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.motion.applyPositionY = false;

        ZombieFirstPosition();

        if (zombieProperties.walkOrRun)
        {
            ZombieWalk();
        }
        if(!zombieProperties.walkOrRun)
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
        if (zombieProperties.zombiePosition)
        {
            splineFollower.offset = new Vector2(-2.5f, 0);
        }
        else
        {
            splineFollower.offset = new Vector2(2.7f, 0);
        }
    }
}
