using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string dataDirectoryPath = " ";
    private string dataFileName = " ";

    public FileDataHandler(string dataDirectoryPath, string dataFileName)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;


    }
    public void Save(GameData data)
    {

        PlayerPrefs.SetInt("moneySave", data.moneySave);


        ////use Path.combine to account for different OS's different paths
        //string fullP = Path.Combine(dataDirectoryPath, dataFileName);
        //try
        //{
        //    //create the directory the file will be written to if not existing already 

        //    Directory.CreateDirectory(fullP);
                
        //    //changin the c# game data into text/computer files 
        //    string dataBeingStored = JsonUtility.ToJson(data,true); 
               
        //    using(FileStream stream = new FileStream(fullP, FileMode.Create))
        //    {
        //        using(StreamWriter writer = new StreamWriter(stream))
        //        {
        //            writer.Write(dataBeingStored);
                    
        //        }
        //    }

        //}
        //catch (Exception error)
        //{

        //    Debug.LogError(" Error occured when trying to save file data: " + fullP + "\n" + error);
        //}
    }
    public GameData Load(GameData loadData)
    {


        //    string fullP = Path.Combine(dataDirectoryPath, dataFileName);
         loadData = null;

        if (PlayerPrefs.HasKey("moneySave"))
         loadData.moneySave = PlayerPrefs.GetInt("moneySave");
        
           return loadData; 


    }
        //    if (File.Exists(fullP))
        //    {
        //        try
        //        {
        //            //loads the data from Json File
        //            string dataBeingTransferBack = " ";
        //            using (FileStream stream = new FileStream(fullP, FileMode.Open))
        //            {
        //                using (StreamReader readIt = new StreamReader(stream))
        //                {
        //                   dataBeingTransferBack = readIt.ReadToEnd(); 
        //                }
        //            }

        //            //turning it back into C# code or Object
        //            loadData = JsonUtility.FromJson<GameData>(dataBeingTransferBack);
        //        }
        //        catch (Exception error)
        //        {

        //            Debug.LogError(" Error occured when trying to save file data: " + fullP + "\n" + error); ;
        //        }
        //    }
        //    return loadData;   
        //}

    }
