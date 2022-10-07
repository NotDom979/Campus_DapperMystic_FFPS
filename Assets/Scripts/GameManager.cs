using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	public int enemyNumber;
	
	public GameObject player;
	public playerController playerScript;
	public GameObject spawnPoint;
	
	public GameObject pauseMenu;
	public GameObject currMenu;
	public GameObject winMenu;
	public GameObject playerDeadMenu;
	public GameObject damageFlash;
	public TextMeshProUGUI enemyCountText;
	public Image playerHpBar;
	
	public bool isPaused;
    // Start is called before the first frame update
	void Awake()
    {
	    instance = this;
	    player = GameObject.FindGameObjectWithTag("Player");
	    playerScript = player.GetComponent<playerController>();
	    spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
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
		public IEnumerator playerDamage()
	{
		damageFlash.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		damageFlash.SetActive(false);
	}
	public void CheckEnemyTotal()
	{
		enemyNumber--;
		enemyCountText.text = enemyNumber.ToString("F0");
		if (enemyNumber <= 0)
		{
			winMenu.SetActive(true);
			cursorLockPause();
		}
	}
}
