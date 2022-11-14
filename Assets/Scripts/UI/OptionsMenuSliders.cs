using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuSliders : MonoBehaviour
{
	public Slider masterVol;
	public Slider musicVol;
	public Slider sfxVol;
    // Start is called before the first frame update
    void Start()
    {
	    GameManager.instance.SFXVol.value = sfxVol.value;
	    GameManager.instance.musicVol.value = musicVol.value;
	    GameManager.instance.masterVol.value = masterVol.value;
    }

    // Update is called once per frame
    void Update()
    {
	    GameManager.instance.SFXVol.value = sfxVol.value;
	    GameManager.instance.musicVol.value = musicVol.value;
	    GameManager.instance.masterVol.value = masterVol.value;
    }
}
