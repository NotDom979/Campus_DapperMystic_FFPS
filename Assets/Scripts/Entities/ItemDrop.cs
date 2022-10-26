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
        enemy = GameObject.FindGameObjectWithTag("enemy");
        randItem = Random.Range(0, 5);
        Debug.Log(itemsDrops);
    }

    // Update is called once per frame
    void Update()
    {

      
    }

   


    }





