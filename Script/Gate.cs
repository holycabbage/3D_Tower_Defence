using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// gate
/// </summary>
public class Gate : Singleton<Gate>
{
    public Image img_HP;

    [HideInInspector] public float HP; 
    private float MaxHP; 

    private void Awake()
    {
        MaxHP = 5;
        HP = 5;
        img_HP.fillAmount = 1;
    }

    private void Start()
    {
        GameManager.Instance.City.text = "City HP:" + HP;
        GameManager.Instance.SetCityHP(HP, MaxHP);
    }

    private void Update()
    {
        img_HP.transform.parent.LookAt(GameManager.Instance.Player.transform);
    }

    /// <summary>
    /// get damage
    /// </summary>
    public void ReceivedDamage()
    {
        HP--;
        img_HP.fillAmount = HP / MaxHP;
        GameManager.Instance.City.text = "City HP:" + HP;
        GameManager.Instance.SetCityHP(HP, MaxHP);
        Debug.Log("City HP:" + HP);
        if (HP <= 0)
        {
            GameManager.Instance.GameOverGameFail();
        }
    }
}