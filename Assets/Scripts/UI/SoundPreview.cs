using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundPreview : MonoBehaviour
{
	public AudioSource gunShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void playSound()
	{
		gunShot.Play();
	}
}
