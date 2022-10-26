using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	
	
	public enum SpawnState	{SPAWNING, WAITING, COUNTING}
	
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
		waveCountdown = timeBetween;
	}
	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveComplete();
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
	
	
}
