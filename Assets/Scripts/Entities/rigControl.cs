using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigControl : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.playerDeadMenu.activeSelf == false && GameManager.instance.winMenu.activeSelf == false && GameManager.instance.optionMenu.activeSelf == false && GameManager.instance.pauseMenu.activeSelf == false && GameManager.instance.playerLoseMenu.activeSelf == false)
		{
			
			Vector3 mousePos = Input.mousePosition;
			transform.LookAt(Camera.main.ScreenToWorldPoint( new Vector3(mousePos.x, mousePos.y, 10)));
		}
	}
}
