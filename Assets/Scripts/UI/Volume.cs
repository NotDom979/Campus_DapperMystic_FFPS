using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class Volume: MonoBehaviour
{
	public AudioMixer audioMixer;
	public void MasterVolume(float volume)
	{
		audioMixer.SetFloat("MasterVol",volume);
		
	}
	public void SFXVolume(float volume)
	{
		audioMixer.SetFloat("SFXVol",volume);
	}
	public void MusicVolume(float volume)
	{
		audioMixer.SetFloat("MusicVol",volume);
	}
}
