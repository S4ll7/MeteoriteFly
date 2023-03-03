using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class RestartMenu : Menu
{
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _curentValue;
    [SerializeField] private TMP_Text _bestValue;

    public UnityAction<int> GameRoundFinished;

    private void OnEnable()
    {
        int curentScore = _timer.Score;
        GameRoundFinished?.Invoke(curentScore);
        _curentValue.text = curentScore.ToString();
        _bestValue.text = _player.MaxDistance.ToString();
    } 
}
