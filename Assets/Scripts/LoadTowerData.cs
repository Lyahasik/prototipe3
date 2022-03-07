using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class LoadTowerData : MonoBehaviour
{
    public GameObject TowerPrefab;

    public Image TowerImage;
    
    [Space] public Text TextSpeed;
    public Text TextDamage;
    public Text TextRadius;
    public Text TextEnergy;

    [Space] public Image FlyImage;
    public Sprite FlySprite;
    public Sprite NoFlySprite;

    private towerScript _towerScript;
    private bool _active = true;
    void Start()
    {
        _towerScript = TowerPrefab.GetComponent<towerScript>();

        TextSpeed.text = _towerScript.fireRate.ToString();
        TextDamage.text = _towerScript.damage.ToString();
        TextRadius.text = _towerScript.range.ToString();
        TextEnergy.text = _towerScript.energy.ToString();

        FlyImage.sprite = (_towerScript.type == towerScript.Type.canon) ? NoFlySprite : FlySprite;
    }

    public void CheckEnergy(int value)
    {
        if (value > _towerScript.energy)
        {
            TowerImage.color = Color.white;
            _active = true;
        }
        else
        {
            TowerImage.color = Color.red;
            _active = false;
        }
    }

    public bool IsActive()
    {
        return _active;
    }
}
