using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveShake : MonoBehaviour
{

    [SerializeField] WaveSpawner wavespawn;
    [SerializeField] AudioSource crumbs;
    

    // Start is called before the first frame update
    void Awake()
    {
        wavespawn = GetComponent<WaveSpawner>();
        crumbs.Play();
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
        crumbs.Stop();
        Destroy(gameObject);
    }
}
