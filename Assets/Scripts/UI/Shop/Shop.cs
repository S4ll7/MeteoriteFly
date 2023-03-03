using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Menu
{

    [SerializeField] private float _maxControllability;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _maxRecovery;

    [SerializeField] private SpaceshipView _spaceshipToBuy;
    [SerializeField] private SpaceshipView _spaceshipBought;
    [SerializeField] private List<Spaceship> _spaceships;
    [SerializeField] private Transform _cardsContainer;
    [SerializeField] private Player _player;

    private Menu _menuOpenedShop;

    public List<Spaceship> Spaceships => _spaceships; 

    private void Start()
    {
        CreateSpaceshipList();
    }

    private void CreateSpaceshipList()
    {
        for (int i = 0; i < _cardsContainer.childCount; i++)
        {
            Destroy(_cardsContainer.GetChild(i).gameObject);
        }

        foreach (Spaceship spaceship in _spaceships)
        {
            if (!spaceship.IsBought)
            {
                CreateSpaceshipToBuy(spaceship);
            }
            else
            {
                CreateSpaceshipBought(spaceship);
            }
        }
    }

    private void CreateSpaceshipToBuy(Spaceship spaceship)
    {
        var spaceshipCard = Instantiate(_spaceshipToBuy, _cardsContainer);
        spaceshipCard.Init(_maxControllability, _maxEnergy, _maxRecovery, spaceship, this);
        spaceshipCard.ButtonClicked += TryToBuySpaceship;
    }

    private void CreateSpaceshipBought(Spaceship spaceship)
    {
        var spaceshipCard = Instantiate(_spaceshipBought, _cardsContainer);
        spaceshipCard.Init(_maxControllability, _maxEnergy, _maxRecovery, spaceship, this);
        spaceshipCard.ButtonClicked += EquipSpaceship;
    }

    private void TryToBuySpaceship(SpaceshipView spaceshipCard, Spaceship spaceship)
    {
        if (_player.Money >= spaceship.Price)
        {
            _player.BuySpaceship(spaceship.Price);
            spaceship.BuySpaceship();
            CreateSpaceshipList();
            spaceshipCard.ButtonClicked -= TryToBuySpaceship;
        }
    }

    private void EquipSpaceship(SpaceshipView spaceshipCard, Spaceship spaceship)
    {
        _player.SetCurentSpaceship(spaceship);
    }

    public void OpenShop(Menu menuOpenedShop)
    {
        _menuOpenedShop = menuOpenedShop;
        _menuOpenedShop.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        _menuOpenedShop.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.SetActive(false);
    }

}
