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
    public int bankTotal;
	public GameObject shop;
	public Shop shopScript;
    [Header("-----Player Relations------")]
    public GameObject player;
    public playerController playerScript;
    public GameObject spawnPoint;
    public GameObject checkPoint;
    [Header("-----MENUS-----")]
    public GameObject pauseMenu;
    public GameObject currMenu;
	public GameObject optionMenu;
    public GameObject winMenu;
    public GameObject playerDeadMenu;
	public GameObject playerLoseMenu;
    [Header("-----UI-----")]
    public GameObject damageFlash;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI flagCountText;
    public TextMeshProUGUI AmmoCount;
	public TextMeshProUGUI AmmoClip;
    public TextMeshProUGUI bankAccount;
    public TextMeshProUGUI LethalCount;
    public TextMeshProUGUI bankRupt;
    public TextMeshProUGUI pressFtoInteract;
    public Image playerHpBar;
    public Image playerArmorBar;
	public bool isPaused;
	public int WaveCounter;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(pressF());
        bankRupt.enabled = false;
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
	    spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
	    bankTotal = 50;
	    shop = GameObject.FindGameObjectWithTag("Shop");
	    shopScript = shop.GetComponent<Shop>();
	    shop.SetActive(false);
	    
	    

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

    public void CheckBankTotal()
    {
        if (bankTotal < 0)
        {
            bankTotal = 0;
        }

        bankAccount.text = bankTotal.ToString("F0");

    }
    public void CheckEnemyTotal()
    {
        enemyNumber--;
	    enemyCountText.text = enemyNumber.ToString("F0");
	    if (enemyNumber < 0)
	    {
	    	enemyNumber = 0;
		    enemyNumber++;
	    }
    }
    public void WinCondition()
    {
        flag++;
        flagCountText.text = flag.ToString("F0");
        if (flag == 3 && enemyNumber == 0)
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

    IEnumerator pressF()
    {

        pressFtoInteract.enabled = true;
        yield return new WaitForSeconds(2);
        pressFtoInteract.enabled = false;
    }

}
