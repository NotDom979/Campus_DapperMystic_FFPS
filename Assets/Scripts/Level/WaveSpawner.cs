using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class WaveSpawner : MonoBehaviour
{
	
	
	public enum SpawnState	{SPAWNING, WAITING, COUNTING}
	int WaveCounter;
	public TextMeshProUGUI WaveTracker;
	public TextMeshProUGUI Timer;
	public GameObject WaveAlert;
	public GameObject ShopAlert;
	bool won;
	public GameObject shop;
	[System.Serializable]
	public class Wave
	{
		public string name;
		//public GameObject enemy;
		public GameObject[] enemies;
		public int count;
		public float rate;

	}
	
	public Wave[] waves;
	private int nextWave = 0;
	
	public float timeBetween = 3f;
	public float waveCountdown;
	
	public GameObject[] spawners;
	
	float enemyCheck = 1f;
	
	private SpawnState state = SpawnState.COUNTING;
	
	void Start()
	{
		WaveCounter = 1;
		WaveTracker.text = (WaveCounter).ToString("F0");
		waveCountdown = timeBetween;
		won = false;
		//shop = gameObject.GetComponent<Shop>().gameObject;
	}
	void Update()
	{
		if (GameManager.instance.winMenu.active != true)
		{
			Timer.text = waveCountdown.ToString("F0");
			if (state == SpawnState.WAITING)
			{
				if (!EnemyIsAlive())
				{
					WaveCounter++;
					WaveComplete();
					StartCoroutine(checkWin());
					
				
				}
				else
				{
					return;
				}
			}
			if (waveCountdown <= 0)
			{
				if (state != SpawnState.SPAWNING)
				{
					ShopDeactivate();
					StartCoroutine(SpawnWave(waves[nextWave]));
				}
			}
			else
			{
				waveCountdown -= Time.deltaTime;
			}
		}
	}
	bool EnemyIsAlive()
	{
		
		enemyCheck -=Time.deltaTime;
		if (enemyCheck <= 0)
		{
			enemyCheck = 1f;
			if (GameObject.FindGameObjectWithTag("enemy") == null)
			{
				GameManager.instance.enemyNumber = 0;
				return false;
			}
		}
		return true;
	}
	IEnumerator SpawnWave(Wave _wave)
	{
		StartCoroutine(WaveA());
		WaveTracker.text = (WaveCounter).ToString("F0");
		state = SpawnState.SPAWNING;
		
		for (int i = 0; i < _wave.count; i++) {
			for (int j = 0; j < spawners.Length; j++) {
				Instantiate(_wave.enemies[Random.Range(0,_wave.enemies.Length)], spawners[j].transform.position, spawners[j].transform.rotation);
			}
			yield return new WaitForSeconds(1f/_wave.rate);
		}
		
		
		state = SpawnState.WAITING;
		
		yield break;
	}
	void WaveComplete()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetween;
		if ((nextWave + 1) > waves.Length - 1)
		{
			nextWave = 0;
			won = true;
			GameManager.instance.winMenu.SetActive(true);
			GameManager.instance.cursorLockPause();
		}
		else
			nextWave++;
			
		
	}
	IEnumerator Shop()
	{
		ShopAlert.SetActive(true);
		yield return new WaitForSeconds(3);
		ShopAlert.SetActive(false);
	}
	IEnumerator WaveA()
	{
		WaveAlert.SetActive(true);
		yield return new WaitForSeconds(3);
		WaveAlert.SetActive(false);
	}
	void ShopDeactivate()
	{
		if (GameManager.instance.shopScript.isSpawned == true)
		{
			shop.SetActive(false);
			GameManager.instance.shopScript.isSpawned = false;
			GameManager.instance.shopScript.gun1.SetActive(false);
			GameManager.instance.shopScript.gun2.SetActive(false);
			GameManager.instance.shopScript.gun3.SetActive(false);
		}
	}
	IEnumerator checkWin()
	{
		if (won == false)
		{
			shop.SetActive(true);
			StartCoroutine(Shop());
		}
		yield return new WaitForSeconds(.1f);
	}

	
}
