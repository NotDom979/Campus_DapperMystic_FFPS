using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[SerializeField] int timer;
	[SerializeField] int maxEnemies;
	[SerializeField] GameObject enemy;

	int enemiesSpawned;
	bool isSpawning;
	bool startSpawning;
	// Start is called before the first frame update
	void Start()
	{
		GameManager.instance.enemyNumber += maxEnemies;
	}

	// Update is called once per frame
	void Update()
	{
		if (startSpawning == true && !isSpawning && enemiesSpawned < maxEnemies)
		{
			StartCoroutine(spawn());
		}
	}


	IEnumerator spawn()
	{
		isSpawning = true;
		Instantiate(enemy, transform.position, enemy.transform.rotation);
		enemiesSpawned++;
		yield return new WaitForSeconds(timer);
		isSpawning = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			startSpawning = true;
		}
	}
}
