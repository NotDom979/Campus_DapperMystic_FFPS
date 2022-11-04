using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistencesSaves;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found another did you break something - ed");
        }
        instance = this;    

    }
    private void Start()
    {
        this.dataPersistencesSaves = FindAllDataPersistenceSaves();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceSaves()
    {
       
        IEnumerable<IDataPersistence>  dataPersistencessaves = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencessaves);
    }

    public void NewGame() 
    { 
        this.gameData = new GameData();
    }
   public void LoadGame() 
    {
        //load any save data
        // if no load data, initialize new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found");
          NewGame();    
        }
        //push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceScript in dataPersistencesSaves)
        {
            dataPersistenceScript.LoadData(gameData);
        }
        Debug.Log("Loaded money = " + gameData.moneySave);
    }
   public void SaveGame() 
    {

        //pass the data to other scripts so they can update it 
        foreach (IDataPersistence dataPersistenceScript in dataPersistencesSaves)
        {
            dataPersistenceScript.SaveData(ref gameData);
        }
        //save that data to a file using the data handler

        Debug.Log("Saved money = " + gameData.moneySave);

    }
    private void OnApplicationQuit()
    {
        SaveGame(); 
    }
}
