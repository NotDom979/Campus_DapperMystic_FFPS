using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	
	
	public enum SpawnState	{SPAWNING, WAITING, COUNTING}
	int WaveCounter;
	public TextMeshProUGUI WaveTracker;
	public TextMeshProUGUI Timer;
	public GameObject WaveAlert;
	public GameObject ShopAlert;
	public GameObject shop;
	[System.Serializable]
	public class Wave
	{
		public string name;
		public GameObject enemy;
		public int count;
		public float rate;
	}
	
	public Wave[] waves;
	private int nextWave = 0;
	
	public float timeBetween = 3f;
	public float waveCountdown;
	
	float enemyCheck = 1f;
	
	private SpawnState state = SpawnState.COUNTING;
	
	void Start()
	{
		GameManager.instance.WaveCounter++;
		WaveTracker.text = (GameManager.instance.WaveCounter/4).ToString("F0");
		waveCountdown = timeBetween;
	}
	void Update()
	{
		Timer.text = waveCountdown.ToString("F0");
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveComplete();
				shop.SetActive(true);
				StartCoroutine(Shop());
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
				shop.SetActive(false);
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
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
				return false;
			}
		}
		return true;
	}
	IEnumerator SpawnWave(Wave _wave)
	{
		StartCoroutine(WaveA());
		WaveTracker.text = (WaveCounter/4).ToString("F0");
		state = SpawnState.SPAWNING;
		
		for (int i = 0; i < _wave.count; i++) {
			Instantiate(_wave.enemy, transform.position, _wave.enemy.transform.rotation);
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
			GameManager.instance.winMenu.SetActive(true);
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
	
	
}
