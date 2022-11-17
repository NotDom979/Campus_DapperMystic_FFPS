using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
public class Volume: MonoBehaviour
{
	public AudioMixer audioMixer;
	public TextMeshProUGUI Text;
	public Slider slider;
	public AudioSource gunShot;
	public float masterVol;
	public float SFXVol;
	public float musicVol;
	float txt;
	


	
	public void MasterVolume(float volume)
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		if (slider.value == 0)
		{
			volume = -80;
		}
		else
			volume = 20.0f * Mathf.Log10(slider.value);

		PlayerPrefs.SetFloat("MasterVolume", volume);         
		audioMixer.SetFloat("MasterVol",PlayerPrefs.GetFloat("MasterVolume"));
		Text.text = (slider.value * 100).ToString("F0");
		
	}
	public void SFXVolume(float volume)
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);

		if (slider.value == 0)
		{
			volume = -80;
		}
		else
			volume = 20.0f * Mathf.Log10(slider.value);
			
		
		PlayerPrefs.SetFloat("SFXVolume", volume);   
		Text.text = (slider.value * 100).ToString("F0");
		audioMixer.SetFloat("SFXVol",PlayerPrefs.GetFloat("SFXVolume"));
	}
	public void MusicVolume(float volume)
	{
		//txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		if (slider.value == 0)
		{
			volume = -80;
		}
		else
			volume = 20.0f * Mathf.Log10(slider.value);

		PlayerPrefs.SetFloat("MusicVolume", volume);   
		audioMixer.SetFloat("MusicVol",PlayerPrefs.GetFloat("MusicVolume"));
		Text.text = (slider.value * 100).ToString("F0");
		
	}
}
