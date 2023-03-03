using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Menu _startMenu;
    [SerializeField] private GameObject _gameHUD;
    
    public UnityAction GameRestarted;

    private void Start()
    {
        OpenMenu(_startMenu);
    }
    
    public void OpenMenu(Menu menu)
    {
        Time.timeScale = 0;
        _gameHUD.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void CloseMenue(Menu menu)
    {
        Time.timeScale = 1;
        _gameHUD.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        GameRestarted?.Invoke();
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
