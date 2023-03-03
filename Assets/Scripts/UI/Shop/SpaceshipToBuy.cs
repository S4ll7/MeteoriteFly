using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceshipToBuy : SpaceshipView
{
    [SerializeField] private TMP_Text _price;

    public override void Init(float maxControllability, float maxEnergy, float maxRecovery, Spaceship spaceship, Shop shop)
    {
        _price.text = spaceship.Price.ToString();
        SetParametrs(maxControllability, maxEnergy, maxRecovery, spaceship, shop);
    }
}
