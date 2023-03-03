using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDied : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.TryGetComponent<Spaceship>(out Spaceship spaceship))
        {
            spaceship.Die();
        }
    }
}
