using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int moneySave;
   
    
    //values defined in this constructor will be default settings
    //game will start with default settings when no data is being loaded
    public GameData()
    {
       this.moneySave = 0;
 
    }
}
