using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int moneySave;
    public int Zombiewave;
    public int health;
    public int armor;
  

   
    //values defined in this constructor will be default settings
    //game will start with default settings when no data is being loaded
    public GameData()
    {
        this.Zombiewave = 0; 
        this.moneySave = 50;
        this.armor = 50;
        this.health = 50;
        
    }
}
