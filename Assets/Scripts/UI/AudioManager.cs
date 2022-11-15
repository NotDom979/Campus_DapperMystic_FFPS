using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
	public AudioMixer audioMixer;
	public TextMeshProUGUI masText;
	public TextMeshProUGUI musText;
	public TextMeshProUGUI sfxText;
	public float masterVol;
	public float SFXVol;
	public float musicVol;
	public Slider masterSlide;
	public Slider musicSlide;
	public Slider SFXSlide;
    // Start is called before the first frame update
	void Awake()
    {
	    masterVol = masterSlide.value;
	    musicVol = musicSlide.value;
	    SFXVol = SFXSlide.value;
    }
	void FixedUpdate()
	{
		MasterVolume();
		SFXVolume();
		MusicVolume();
	}
	
	public void MasterVolume()
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		if (masterSlide.value == 0)
		{
			masterVol = -80;
		}
		else
			masterVol = 20.0f * Mathf.Log10(masterSlide.value);

		audioMixer.SetFloat("MasterVol",masterVol);
		masText.text = (masterSlide.value * 100).ToString("F0");
		
	}
	public void SFXVolume()
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);

		if (SFXSlide.value == 0)
		{
			SFXVol = -80;
		}
		else
			SFXVol = 20.0f * Mathf.Log10(SFXSlide.value);
			
		

		sfxText.text = (SFXSlide.value * 100).ToString("F0");
		audioMixer.SetFloat("SFXVol",SFXVol);
	}
	public void MusicVolume()
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		if (musicSlide.value == 0)
		{
			musicVol = -80;
		}
		else
			musicVol = 20.0f * Mathf.Log10(musicSlide.value);

		audioMixer.SetFloat("MusicVol",musicVol);
		musText.text = (musicSlide.value * 100).ToString("F0");
		
	}
}
