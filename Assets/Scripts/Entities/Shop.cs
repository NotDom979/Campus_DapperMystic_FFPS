using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] public List<GameObject> spawners;
	[SerializeField] public List<GameObject> Guns;
	public GameObject gun1;
	public GameObject gun2;
	public GameObject gun3;
	public bool isSpawned;
    // Start is called before the first frame update
    void Start()
    {
		isSpawned = false;
    }

	// Update is called once per frame.
    void Update()
    {
		if (gameObject.activeSelf == true)
		{
			if (isSpawned == false)
			{
				for (int i = 0; i < 1; i++)
				{
					isSpawned = true;
					gun1 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i].transform.position, spawners[i].transform.rotation);
					gun2 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i + 1].transform.position, spawners[i + 1].transform.rotation);
					gun3 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i + 2].transform.position, spawners[i + 2].transform.rotation);
					if (gun1 == gun2)
					{
						gun1 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i].transform.position, spawners[i].transform.rotation);
					}
					else if (gun1 == gun3)
					{
						gun1 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i].transform.position, spawners[i].transform.rotation);
					}
					else if (gun2 == gun3)
					{
						gun2 = Instantiate(Guns[(int)Random.Range(0, Guns.Count)], spawners[i + 1].transform.position, spawners[i + 1].transform.rotation);
					}
				}
				gun1.SetActive(true);
				gun2.SetActive(true);
				gun3.SetActive(true);
				StartCoroutine(Despawn());

			}
		}
		else
			isSpawned = false;
    }
	IEnumerator Despawn()
    {
	    yield return new WaitForSeconds(15f);
		gun1.SetActive(false);
		gun2.SetActive(false);
		gun3.SetActive(false);
		isSpawned = false;
    }
}
