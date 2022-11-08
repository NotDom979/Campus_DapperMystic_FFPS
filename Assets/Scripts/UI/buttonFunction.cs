using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour, IDataPersistence
{
	public void resume()
	{
		if (GameManager.instance.isPaused == true)
		{
			GameManager.instance.cursorUnLockUnPause();
		}
	}
	public void restart()
	{
		GameManager.instance.cursorUnLockUnPause();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void quit()
	{
		Application.Quit();
		 
	}
	public void respawn()
	{
		GameManager.instance.playerScript.respawn();
		GameManager.instance.cursorUnLockUnPause();
	}

	public void LoadData(GameData data)
	{
		
	}

	public void SaveData(GameData data)
	{
		
	}
}
