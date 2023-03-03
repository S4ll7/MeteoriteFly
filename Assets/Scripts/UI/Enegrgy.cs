using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enegrgy : MonoBehaviour
{
    [SerializeField] private Image _energyIcon;
    [SerializeField] private Player _player;

    private CanvasGroup _canvaseGroup;

    private void Start()
    {
        _canvaseGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.EnergyChanged += ChangeEnergy;
    }

    private void OnDisable()
    {
        _player.EnergyChanged -= ChangeEnergy;
    }
    
    private void ChangeEnergy(float value)
    {
        _energyIcon.fillAmount = value;
    }
}
