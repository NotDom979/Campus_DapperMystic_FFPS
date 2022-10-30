using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
	int objCount;
	AudioSource audioSource;
    // Start is called before the first frame update
	void Awake()
	{
		objCount = FindObjectsOfType<Music>().Length;
	    DontDestroyOnLoad(transform.gameObject);
	    audioSource = gameObject.GetComponent<AudioSource>();
	    
		if (objCount != 1)
	    {
			Destroy(gameObject);
	    }
    }

    // Update is called once per frame
	public void PLay(){
		if (audioSource.isPlaying == true)
		{
			return;
		}
		audioSource.Play();
	}
	public void StopMusic()
	{
		audioSource.Stop();
	}
}
