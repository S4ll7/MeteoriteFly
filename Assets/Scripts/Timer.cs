using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [SerializeField] private MenuController _gameController;
    [SerializeField] private float _updateTime;

    private int _score = 0;
    private float _curentTime = 0;

    public UnityAction<int> ScoreUpdated;
    public UnityAction SpeedIncreased;
    public int Score => _score;

    private void Start()
    {
        ScoreUpdated?.Invoke(_score);
    }

    private void OnEnable()
    {
        _gameController.GameRestarted += ResetTimer;   
    }

    private void OnDisable()
    {
        _gameController.GameRestarted -= ResetTimer;
    }

    public void ResetTimer()
    {
        _score = 0;
        _curentTime = 0;
        ScoreUpdated?.Invoke(_score);
    }

    private void Update()
    {
        _curentTime += Time.deltaTime;
        if (_curentTime >= _updateTime)
        {
            _curentTime = 0;
            _score++;
            ScoreUpdated?.Invoke(_score);
        }
    }

}
