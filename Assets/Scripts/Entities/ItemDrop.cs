using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsDrops;
    [SerializeField] public int randItem;
    [SerializeField] public int grabItem;
    [SerializeField] Transform deathPos;
    enemyAi enemy;
    void Start()
    {
        deathPos = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Randomitem() 
    {

        if (enemy.maxHealth <= 0 )
        {
            if (randItem == 2)
            {
                grabItem = 2;
                Instantiate(itemsDrops[1], deathPos.position, Quaternion.identity);

            }
            else if (randItem == 5)
            {
                grabItem = 5;
            }
        }
    
    
    }


}
