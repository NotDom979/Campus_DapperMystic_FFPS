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
	float txt;
	public void MasterVolume(float volume)
	{
		audioMixer.SetFloat("MasterVol",volume);
		txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		Text.text = txt.ToString("F0");
		
	}
	public void SFXVolume(float volume)
	{
		audioMixer.SetFloat("SFXVol",volume);
		txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		Text.text = txt.ToString("F0");
		
	}
	public void MusicVolume(float volume)
	{
		audioMixer.SetFloat("MusicVol",volume);
		txt = (Mathf.Pow(10.0f, volume / 20.0f) * 100);
		
		Text.text = txt.ToString("F0");
		
	}
}
