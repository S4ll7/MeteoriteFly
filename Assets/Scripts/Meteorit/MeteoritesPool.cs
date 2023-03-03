using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritesPool : MonoBehaviour
{
    [SerializeField] private int _numberOfMeteorites;
    [SerializeField] private List<Meteorit> _meteoritPrefabs;
    [SerializeField] private List<GameObject> _positions;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _startSpawnDelay;
    [SerializeField] private AnimationCurve _speed;
    [SerializeField] private AnimationCurve _spawnDelay;
    [SerializeField] private MenuController _gameController;

    private float _curentSpawnDelay;
    private float _curentSpeed; 
    private List<Meteorit> _meteorites = new List<Meteorit>();
    private float _curentTime;
    private int _previousPositionIndex;

    private void Start()
    {
        SetSpawnPointsPositions(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x);
        _previousPositionIndex = -1;
        _curentTime = 0;
        _curentSpeed = _startSpeed;
        _curentSpawnDelay = _startSpawnDelay;

        for (int i = 0; i < _numberOfMeteorites; i++)
        {
            Meteorit meteorite = Instantiate(_meteoritPrefabs[Random.Range(0, _meteoritPrefabs.Count)], transform);
            meteorite.transform.position = _positions[Random.Range(0, _positions.Count)].transform.position;
            meteorite.SetSpeed(_curentSpeed);
            meteorite.gameObject.SetActive(false);
            _meteorites.Add(meteorite);
        }
    }

    private void SetSpawnPointsPositions(float leftBorder, float rightBorder)
    {
        Vector3 place = _positions[0].transform.position;
        float shift = (-leftBorder + rightBorder) / (_positions.Count + 1);
        place.x = leftBorder + shift;
        foreach (GameObject position in _positions)
        {
            position.transform.position = place;
            place.x += shift;
        }
    }

    private void OnEnable()
    {
        _timer.ScoreUpdated += ChangeSpeed;
        _gameController.GameRestarted += Restart;
    }

    private void OnDisable()
    {
        _timer.ScoreUpdated -= ChangeSpeed;
        _gameController.GameRestarted -= Restart;
    }

    private void Restart()
    {
        _previousPositionIndex = -1;
        _curentTime = 0;
        _curentSpeed = _startSpeed;
        _curentSpawnDelay = _startSpawnDelay;
        foreach (Meteorit meteorite in _meteorites)
        {
            meteorite.gameObject.SetActive(false);
        }
    }

    private void ChangeSpeed(int value)
    {
        _curentSpawnDelay = _spawnDelay.Evaluate(value);
        _curentSpeed = _speed.Evaluate(_curentSpawnDelay);
        foreach (Meteorit meteorite in _meteorites)
        {
            meteorite.SetSpeed(_curentSpeed);
        }
    }

    private void SpawnMeteorite()
    {
        foreach (Meteorit meteorite in _meteorites)
        {
            if (!meteorite.gameObject.activeSelf)
            {
                int newPositionIndex = Random.Range(0, _positions.Count);
                while (newPositionIndex == _previousPositionIndex)
                {
                    newPositionIndex = Random.Range(0, _positions.Count);
                }
                meteorite.transform.position = _positions[newPositionIndex].transform.position;
                _previousPositionIndex = newPositionIndex;
                meteorite.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Update()
    {
        _curentTime += Time.deltaTime;
        if (_curentTime > _curentSpawnDelay)
        {
            _curentTime = 0;
            SpawnMeteorite();
        }
    }

}
