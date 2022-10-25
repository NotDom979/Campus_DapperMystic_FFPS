using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsDrops;
    [SerializeField] public int randItem;
    [SerializeField] public int grabItem;
   
    enemyAi enemy;
    private void Start()
    {
   
        Debug.Log(itemsDrops);
    }

    // Update is called once per frame
    void Update()
    {
        RandomItem();
    }

    void RandomItem()
    {
        if (enemy.maxHealth <= 0)
        {
            randItem = Random.Range(0, 6);
            if (randItem == 2)
            {
                grabItem = 2;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);

            }
            else if (randItem == 1)
            {
                grabItem = 1;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);

            }
            else if (randItem == 3)
            {

                grabItem = 3;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);
            }
        }


    }


}
