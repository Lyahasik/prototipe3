using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerData : MonoBehaviour
{
    public GameObject GameManager;
    public ManagerScene ManagerScene;
    
    [Space]public LoadTowerData Canon;
    public LoadTowerData Gatling;
    public LoadTowerData Rocket;

    [Space] public Text TextHealth;
    public Text TextEnergy;

    private gameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.GetComponent<gameManager>();
        
        TextHealth.text = _gameManager.playerMaxHp.ToString();
        TextEnergy.text = _gameManager.playerStartEnergy.ToString();
    }

    private void Update()
    {
        TextHealth.text = _gameManager.playerHp.ToString();
        if (_gameManager.playerHp <= 0)
        {
            ManagerScene.GameOver();
        }
        
        TextEnergy.text = _gameManager.playerEnergy.ToString();
        
        Canon.CheckEnergy(_gameManager.playerEnergy);
        Gatling.CheckEnergy(_gameManager.playerEnergy);
        Rocket.CheckEnergy(_gameManager.playerEnergy);
    }
}
