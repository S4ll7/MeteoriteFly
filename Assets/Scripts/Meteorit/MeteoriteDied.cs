using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteDied : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.TryGetComponent<Meteorit>(out Meteorit meteorite))
        {
            meteorite.DestroyMeteorit();
        }
    }
}
