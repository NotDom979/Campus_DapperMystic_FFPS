using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] public List<GameObject> spawners;
	[SerializeField] public List<GameObject> Guns;
    // Start is called before the first frame update
    void Start()
    {
	    for (int i = 0; i < spawners.Count; i++) {
	    	Instantiate(Guns[Random.Range(0,Guns.Count)], spawners[i].transform.position, spawners[i].transform.rotation);
	    }
    }

	// Update is called once per frame.
    void Update()
    {
        
    }
}
