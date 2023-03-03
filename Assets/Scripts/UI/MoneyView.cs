using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    private int _oldMoneyValue = 0;

    private void Awake()
    {
        SetMoneyView(_player.Money);
    }
    
    private void OnEnable()
    {
        SetMoneyView(_player.Money);
        _player.MoneyChanged += ChangeMoneyView;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= ChangeMoneyView;
    }

    private void SetMoneyView(int money)
    {
        _money.text = money.ToString();
        _oldMoneyValue = money;
    }

    private void ChangeMoneyView(int money)
    {
        
        StartCoroutine(ChangingMoneyView(money));
        
    }

    private IEnumerator ChangingMoneyView(int money)
    {
        if (_oldMoneyValue != money)
        {
            int addingValue = 0;
            if (_oldMoneyValue > money)
            {
                addingValue = -1;
            }
            else
            {
                addingValue = 1;
            }
            while (_oldMoneyValue != money)
            {
                _oldMoneyValue += addingValue;
                _money.text = _oldMoneyValue.ToString();
                yield return null;
            }
        }
        else
        {
            SetMoneyView(money);
        }
    } 
}
