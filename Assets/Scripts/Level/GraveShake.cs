using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveShake : MonoBehaviour
{

    [SerializeField] WaveSpawner wavespawn;

    // Start is called before the first frame update
    void Start()
    {
        wavespawn = GetComponent<WaveSpawner>();

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(rubble());
        }


    }

    public IEnumerator rubble()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
