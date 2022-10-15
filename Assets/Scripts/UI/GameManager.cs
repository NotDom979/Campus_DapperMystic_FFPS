using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("-----GameGoal------")]
    public int flag;
    public int enemyNumber;
    [Header("-----Player Relations------")]
    public GameObject player;
    public playerController playerScript;
    public GameObject spawnPoint;
    [Header("-----MENUS-----")]
    public GameObject pauseMenu;
    public GameObject currMenu;
    public GameObject winMenu;
    public GameObject playerDeadMenu;
    [Header("-----UI-----")]
    public GameObject damageFlash;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI flagCountText;
	public TextMeshProUGUI AmmoCount;
	public TextMeshProUGUI LethalCount;
	public Image playerHpBar;
	public Image playerArmorBar;

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
		checkWin();
        if (Input.GetButtonDown("Cancel") && !playerDeadMenu.activeSelf && !winMenu.activeSelf)
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
    public void cursorLockPause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void cursorUnLockUnPause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        pauseMenu.SetActive(isPaused);
    }
    public IEnumerator playerDamage()
	{
		if (playerScript.HP >= 2)
		{
			damageFlash.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			damageFlash.SetActive(false);
		}

    }
    public void CheckEnemyTotal()
    {
        enemyNumber--;
        enemyCountText.text = enemyNumber.ToString("F0");
    }
    public void WinCondition()
    {
        flag++;
        flagCountText.text = flag.ToString("F0");
	    if (flag == 1 && enemyNumber == 0)
        {
            winMenu.SetActive(true);
            cursorLockPause();
        }
    }
	public void checkWin()
	{
		if (flag == 1 && enemyNumber == 0)
		{
			winMenu.SetActive(true);
			cursorLockPause();
		}
	}
}
