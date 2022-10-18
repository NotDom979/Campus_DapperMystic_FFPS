using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigControl : MonoBehaviour
{
	[SerializeField] int sensHort;
	[SerializeField] int sensVert;
	[SerializeField] int lockVertMin;
	[SerializeField] int lockVertMax;
	[SerializeField] bool invert;
	float xRotation;
	// Start is called before the first frame update
	void Start()
	{
		Cursor.lockState =CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		//get input 
		float mousex = Input.GetAxis("Mouse Y") * Time.deltaTime * sensHort;
		float mousey = Input.GetAxis("Mouse X") * Time.deltaTime * sensVert;
	    
		if(invert)
			xRotation += mousey;
		else
			xRotation -= mousey;
		//clamp camera rotation
		xRotation = Mathf.Clamp(xRotation,lockVertMin,lockVertMax);
	    
		//rotate camera on x axis
		transform.localRotation = Quaternion.Euler(xRotation,0,0);
	    
		//rotate the player
		transform.parent.Rotate(Vector3.up * mousex);
	    
	}
}
