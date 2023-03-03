using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ForceField : MonoBehaviour
{
    [SerializeField] private Spaceship _spaceship;
    [SerializeField] private float _pauseTime;

    private WaitForSeconds _pause;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Color _endColor;

    private void Start()
    {
        _pause = new WaitForSeconds(_pauseTime);
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _endColor = _spriteRenderer.color;
        _endColor.a = 0;
    }
    
    private void OnEnable()
    {
        _spaceship.ApliedDamage += EnergyLost;
        _spaceship.ApliedToManyDamage += DropBelowZero;
    }

    private void OnDisable()
    {
        _spaceship.ApliedDamage -= EnergyLost;
        _spaceship.ApliedToManyDamage -= DropBelowZero;
    }
    
    private void EnergyLost()
    {
        _animator.Play("Damaged");
    }

    private IEnumerator DisableForceField()
    {
        _animator.enabled = false;
        _spriteRenderer.color = _endColor;
        yield return _pause;
        _animator.enabled = true;
    }

    private void DropBelowZero()
    {
        StartCoroutine("DisableForceField");
    }

}
