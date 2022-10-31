using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
	public void BackButton()
	{
		
		GameManager.instance.pauseMenu.SetActive(true);
		GameManager.instance.optionMenu.SetActive(false);
	}
	public void MenuButton()
	{
		GameManager.instance.pauseMenu.SetActive(false);
		SceneManager.LoadScene("Menu");
	}
	public void OptionButton()
	{
		GameManager.instance.pauseMenu.SetActive(false);
		GameManager.instance.optionMenu.SetActive(true);
	}
}
