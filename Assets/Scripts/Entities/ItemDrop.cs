using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsDrops;
    [SerializeField] public int randItem;
    private int grabItem;
    public GameObject enemy;

    private void Start()
    {
             randItem = Random.Range(0, 5);
        enemy = GameObject.FindGameObjectWithTag("enemy");
        Debug.Log(itemsDrops);
    }

    // Update is called once per frame
    void Update()
    {

        RandomItem();
    }

    public void RandomItem()
    {
 
        


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
