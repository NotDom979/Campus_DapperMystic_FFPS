using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetStats : MonoBehaviour, IDamage
{

    [SerializeField]float currentHP;
	[SerializeField]float MaxHP;

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

        if (currentHP <= 0)
        {

           Destroy(gameObject);
        }
    }

    
}
