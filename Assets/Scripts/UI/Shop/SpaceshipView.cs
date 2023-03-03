using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class SpaceshipView : MonoBehaviour
{
    private Spaceship _spaceship;
    private Shop _shop;

    public Image _spaceshipIcon;
    public Slider _controllability;
    public Slider _energy;
    public Slider _recovery;
    public Button _actionButton;
    public UnityAction<SpaceshipView, Spaceship> ButtonClicked;

    public void SetParametrs(float maxControllability, float maxEnergy, float maxRecovery, Spaceship spaceship, Shop shop)
    {
        _spaceshipIcon.sprite = spaceship.SpaceshipIcon;
        _controllability.value = spaceship.Controllability / maxControllability;
        _energy.value = spaceship.MaxEnergy / maxEnergy;
        _recovery.value = spaceship.EnergyRecover / maxRecovery;
        _spaceship = spaceship;
        _shop = shop;
        _actionButton.onClick.AddListener(ButtonAction);
    }

    public abstract void Init(float maxControllability, float maxEnergy, float maxRecovery, Spaceship spaceship, Shop shop);
    
    public void ButtonAction()
    {
        ButtonClicked?.Invoke(this, _spaceship);
    }
}
