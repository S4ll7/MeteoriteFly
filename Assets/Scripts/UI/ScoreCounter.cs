using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        _timer.ScoreUpdated += UpdatePointView;
    }

    private void OnDisable()
    {
        _timer.ScoreUpdated -= UpdatePointView;
    }

    private void UpdatePointView(int pointsValue)
    {
        _pointsText.text = pointsValue.ToString();
    }

}
