using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] private float _energyForShote;
    [SerializeField] private float _energyRecoverSpeed;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _controllability;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _spaceshipIcon;
    [SerializeField] private bool _isBought;
    [SerializeField] private Projectile _projectille;
    [SerializeField] private ForceField _foceField;
    [SerializeField] private FakeShip _leftSpaceship;
    [SerializeField] private FakeShip _rightSpaceship;

    private Vector3 _leftBorder;
    private Vector3 _rightBorder;
    private Animator _animator;
    private float _curentEnergy;
    private GameObject _projectilleContainer;

    public int Price => _price;
    public Sprite SpaceshipIcon => _spaceshipIcon;
    public bool IsBought => _isBought;
    public float EnergyRecover => _energyRecoverSpeed;
    public float MaxEnergy => _maxEnergy;
    public float Controllability => _controllability;
    public UnityAction<float> EnergyChanged;
    public UnityEvent SpaceshipDied;
    public UnityAction ApliedDamage;
    public UnityAction ApliedToManyDamage;
    
    public void Init(GameObject projectilleContainer, Vector3 leftBorder, Vector3 rightBorder)
    {
        _animator = GetComponent<Animator>();
        _curentEnergy = _maxEnergy;
        EnergyChanged?.Invoke(_curentEnergy / _maxEnergy);
        _projectilleContainer = projectilleContainer;
        _leftBorder = leftBorder;
        _rightBorder = rightBorder;
        MakeSpaceShipsGap();
    }
    
    public void RestoreValues()
    {
        _curentEnergy = _maxEnergy;
        EnergyChanged?.Invoke(_curentEnergy / _maxEnergy);
        _animator.SetBool("Died", false);
    }

    private void MakeSpaceShipsGap()
    {
        Vector3 shift = _rightBorder - _leftBorder;
        _leftSpaceship.transform.position = transform.position - shift;
        _rightSpaceship.transform.position = transform.position + shift;
    }

    public void Shoote()
    {
        GetDamage(_energyForShote);
        Projectile projectile = Instantiate(_projectille, _projectilleContainer.transform);
        projectile.transform.position = transform.position;
    }

    public void GetDamage(float damage)
    {
        _curentEnergy -= damage;
        ApliedDamage?.Invoke();
        EnergyChanged?.Invoke(_curentEnergy / _maxEnergy);

        if (_curentEnergy <= 0)
        {
            ApliedToManyDamage?.Invoke();
            _animator.SetBool("Died", true);
        }
    }

    private void PeremutateShips()
    {
        if (transform.position.x > _rightBorder.x)
        {
            transform.position = _leftBorder;
        }
        if (transform.position.x < _leftBorder.x)
        {
            transform.position = _rightBorder;
        }
    }

    public void MoveSpaceship(float direction)
    {
        transform.Translate(Vector3.right * direction * Time.deltaTime * _controllability);
        PeremutateShips();
    }

    public void BuySpaceship()
    {
        _isBought = true;
    }

    public void Die()
    {
        _animator.SetBool("Died", false);
        SpaceshipDied?.Invoke();
    }

    private void Update()
    {
        if (_curentEnergy < _maxEnergy)
        {
            _curentEnergy += _energyRecoverSpeed * Time.deltaTime;
            EnergyChanged?.Invoke(_curentEnergy / _maxEnergy);
        }
    }

}
