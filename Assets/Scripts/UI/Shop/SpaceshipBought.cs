using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBought : SpaceshipView
{
    public override void Init(float maxControllability, float maxEnergy, float maxRecovery, Spaceship spaceship, Shop shop)
    {
        SetParametrs(maxControllability, maxEnergy, maxRecovery, spaceship, shop);
    }
}
