using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
	public void resume()
	{
		GameManager.instance.cursorUnLockUnPause();
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
}
