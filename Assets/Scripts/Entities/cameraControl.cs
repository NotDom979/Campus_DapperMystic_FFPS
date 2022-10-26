using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
	[SerializeField] int sensHort;
	[SerializeField] int sensVert;
	[SerializeField] int lockVertMin;
	[SerializeField] int lockVertMax;
	[SerializeField] int lockHortMin;
	[SerializeField] int lockHortMax;
	[SerializeField] bool invert;
	float xRotation;
	float yRotation;
	//[SerializeField] Transform playerArms;
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
		float mousex = Input.GetAxis("Mouse X") * Time.deltaTime * sensHort;
		float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;
	    
		if(invert)
			xRotation += mousey;
		else
			xRotation -= mousey;
		//clamp camera rotation
		xRotation = Mathf.Clamp(xRotation,lockVertMin,lockVertMax);
		yRotation = Mathf.Clamp(yRotation,lockHortMin,lockHortMax);
	    
		//rotate camera on x axis
		transform.localRotation = Quaternion.Euler(xRotation,yRotation,0);
	    
		//rotate the player
		transform.parent.Rotate(Vector3.up * mousex);
		//	Vector3 rotArms = playerArms.transform.rotation.eulerAngles;
		//rotArms.x -= rotArms.y;
		//rotArms.z = 0;
		//rotArms.y += xRotation;
		
		//playerArms.rotation = Quaternion.Euler(rotArms);
		
		
	    
	}
}
