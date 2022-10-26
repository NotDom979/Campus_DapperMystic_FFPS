using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject[] itemsDrops;
    [SerializeField] public int randItem;
    [SerializeField] public int grabItem;
    public GameObject enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag(gameObject.tag);
        Debug.Log(itemsDrops);
    }

    // Update is called once per frame
    void Update()
    {
        RandomItem();

    }

    public void RandomItem()
    {
        
             randItem = Random.Range(0, 5);

            if (randItem == 2)
            {
                grabItem = 2;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }
            else if (randItem == 1)
            {
                grabItem = 1;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);
                Destroy(gameObject);

            }
            else if (randItem == 3)
            {

                grabItem = 3;
                Instantiate(itemsDrops[grabItem], transform.position, Quaternion.identity);
                Destroy(gameObject);

            }
        
        
    }





}
