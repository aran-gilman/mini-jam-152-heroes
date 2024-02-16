using System.Collections.Generic;
using UnityEngine;

public class RuntimeAnimationPosition : StateMachineBehaviour
{
    [SerializeField]
    private string stateName;

    public Dictionary<string, Transform> StateTransformMap =
        new Dictionary<string, Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var kv in StateTransformMap)
        {
            if (stateInfo.IsName(kv.Key))
            {
                animator.transform.SetPositionAndRotation(kv.Value.position, kv.Value.rotation);
            }
        }
    }
}
