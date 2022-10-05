using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject player;
	public playerController playerScript;
	
	public GameObject pauseMenu;
	public GameObject currMenu;
	
	public bool isPaused;
    // Start is called before the first frame update
	void Awake()
    {
	    instance = this;
	    player = GameObject.FindGameObjectWithTag("Player");
	    playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetButtonDown("Cancel"))
	    {
	    	isPaused = !isPaused;
	    	pauseMenu.SetActive(isPaused);
	    	if (isPaused)
	    	{
	    		cursorLockPause();
	    	}
	    	else
	    	{
	    		cursorUnLockUnPause();
	    	}
	    }
    }
	public void	cursorLockPause()
	{
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}
	public void	cursorUnLockUnPause()
	{
		Time.timeScale = 1;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		isPaused = false;
		pauseMenu.SetActive(isPaused);
	}
}
