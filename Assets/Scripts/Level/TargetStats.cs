﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetStats : MonoBehaviour, IDamage
{
    public Image enemyHpBar;
    [SerializeField]float currentHP;
	[SerializeField]float MaxHP;

    public bool destroyed;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
    }


    // Update is called once per frame
    void Update()
    {
        //if (round is over)
        //{
        // payDay(150)
        //}
    }
    public void payDay(int currency)
    {
       
        GameManager.instance.bankTotal += currency;
       
    }

    public void takeDamage(float damage)
    {
        currentHP -= damage;
        enemyHpBar.fillAmount = currentHP / MaxHP;
        if (currentHP <= 0)
        {

           Destroy(gameObject);
            destroyed = true;
        }
    }

    
}
