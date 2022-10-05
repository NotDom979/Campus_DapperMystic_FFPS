using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject player;
	public playerController playerScript;
    // Start is called before the first frame update
	void Awake()
    {
	    instance = this;
	    player = GameObject.FindGameObjectWithTag("Player");
	    playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
