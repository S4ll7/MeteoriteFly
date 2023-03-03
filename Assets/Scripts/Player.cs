using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private RestartMenu _restartMenue;
    [SerializeField] private Shop _shop;
    [SerializeField] private GameObject _projectilleContainer;
    [SerializeField] private int _startSpaceshipId;
    [SerializeField] private MenuController _gameController;
    [SerializeField] private int _money;

    private int _maxDistance = 0;
    private Spaceship _curentSpaceship;
    private Camera _camera;
    private Vector3 _rightBorder;
    private Vector3 _leftBorder;
    
    public UnityAction<float> EnergyChanged;
    public UnityAction<int> MoneyChanged;
    public int Money => _money;
    public int MaxDistance => _maxDistance;

    private void Start()
    {
        _camera = Camera.main;
        _rightBorder = transform.position;
        _leftBorder = transform.position;
        _rightBorder.x = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        _leftBorder.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        _curentSpaceship = null;
        SetCurentSpaceship(_shop.Spaceships[_startSpaceshipId]);
    }

    private void OnEnable()
    {
        _gameController.GameRestarted += Restart;
        _restartMenue.GameRoundFinished += FinishRound;
    }

    private void OnDisable()
    {
        _gameController.GameRestarted -= Restart;
        _restartMenue.GameRoundFinished -= FinishRound;
    }

    public void UpdateBestScore(int score)
    {
        if (score > _maxDistance)
        {
            _maxDistance = score;
        }
    }

    private void AddMoney(int value)
    {
        _money += value;
        MoneyChanged?.Invoke(_money);
    }

    public void BuySpaceship(int price)
    {
        _money -= price;
        MoneyChanged?.Invoke(_money);
    }

    private void FinishRound(int curentScore)
    {
        UpdateBestScore(curentScore);
        AddMoney(curentScore);
    }

    private void Restart()
    {
        if (_curentSpaceship != null)
        {
            _curentSpaceship.RestoreValues();
        }
    }

    public void SetCurentSpaceship(Spaceship spaceship)
    {
        if (_curentSpaceship != null)
        {
            _curentSpaceship.EnergyChanged -= ChangeEnergyBar;
            _curentSpaceship.SpaceshipDied.RemoveListener(() => _gameController.OpenMenu(_restartMenue));
            _curentSpaceship.gameObject.SetActive(false);
        }
        _curentSpaceship = spaceship;
        _curentSpaceship.gameObject.SetActive(true);
        _curentSpaceship.Init(_projectilleContainer, _leftBorder, _rightBorder);
        _curentSpaceship.SpaceshipDied.AddListener(() => _gameController.OpenMenu(_restartMenue));
        _curentSpaceship.EnergyChanged += ChangeEnergyBar; 
    }
    
    private void ChangeEnergyBar(float value)
    {
        EnergyChanged?.Invoke(value);
    }

    private void Update()
    {
        _curentSpaceship.MoveSpaceship(Input.acceleration.x);
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            _curentSpaceship.Shoote();
        }

    }
}
