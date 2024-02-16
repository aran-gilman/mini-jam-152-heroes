using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeAnimationPosition : StateMachineBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;

    [SerializeField]
    private string stateName;

    public string StateName => stateName;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(stateName))
        {
            previousPosition = animator.transform.position;
            previousRotation = animator.transform.rotation;
            animator.transform.SetPositionAndRotation(Position, Rotation);
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(stateName))
        {
            animator.transform.SetPositionAndRotation(previousPosition, previousRotation);
        }
    }
}
