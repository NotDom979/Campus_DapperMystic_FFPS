using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
public class VolumeText : MonoBehaviour
{
	public TextMeshProUGUI MasterVol;
	public TextMeshProUGUI MusicVol;
	public TextMeshProUGUI SFXVol;
	public AudioMixer audioMix;
	
    // Start is called before the first frame update
    void Start()
	{
    	
    }

    // Update is called once per frame
    void Update()
    {
	    //    MasterVol.text = audioMix.GetFloat("MasterVol").ToString("F0");
	    //   SFXVol.text = audioMix.GetFloat("SFXVol").ToString("F0");
	    // MusicVol.text = audioMix.GetFloat("MusicVol").ToString("F0");
    }
}
