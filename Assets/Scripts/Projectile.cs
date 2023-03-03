using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Spaceship>(out Spaceship spaceship))
        {
            if (collision.transform.TryGetComponent<Meteorit>(out Meteorit meteorit))
            {
                meteorit.GetDamage(_damage);
            }
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
