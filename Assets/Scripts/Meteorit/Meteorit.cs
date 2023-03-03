using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(CircleCollider2D))]
public class Meteorit : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    
    private float _speed;
    private int _curentHealth;
    private Vector3 _downBorder;
    private Animator _animator;
    private CircleCollider2D _colider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colider = GetComponent<CircleCollider2D>();
        _colider.enabled = true;
        _curentHealth = _maxHealth;
        _downBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }
    
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Spaceship>(out Spaceship spaceship))
        {
            spaceship.GetDamage(_damage);
            GetDamage(_maxHealth);
        }
    }

    public void GetDamage(int damage)
    {
        _curentHealth -= damage;
        if (_curentHealth <= 0)
        {
            _animator.SetBool("Died", true);
            _colider.enabled = false;
        }
    }

    public void DestroyMeteorit()
    {
        _curentHealth = _maxHealth;
        gameObject.SetActive(false);
        _animator.SetBool("Died", false);
        _colider.enabled = true;
    }

    private void OutOfScreen()
    {
        if (transform.position.y < _downBorder.y)
        {
            DestroyMeteorit();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        OutOfScreen();
    }

}
