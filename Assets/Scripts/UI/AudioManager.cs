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
    }
	void FixedUpdate()
	{
		if (PlayerPrefs.HasKey("MasterVolume"))
		{
			masterSlide.value = PlayerPrefs.GetFloat("MasterVolume");
			masText.text = PlayerPrefs.GetFloat("MasterVolume").ToString();
		}
		if (PlayerPrefs.HasKey("MusicVolume"))
		{
			musicSlide.value = PlayerPrefs.GetFloat("MusicVolume");
			musText.text = PlayerPrefs.GetFloat("MusicVolume").ToString();
			
		}
		if (PlayerPrefs.HasKey("SFXVolume"))
		{
			SFXSlide.value = PlayerPrefs.GetFloat("SFXVolume");
			sfxText.text = PlayerPrefs.GetFloat("SFXVolume").ToString();
		}
		else
		{
			masterSlide.value = 1;
			musicSlide.value = 1;
			SFXSlide.value = 1;
			sfxText.text = (SFXSlide.value * 100).ToString();
			masText.text = (masterSlide.value * 100).ToString();
			musText.text = (musicSlide.value * 100).ToString();
		}
		
	}
	
}
