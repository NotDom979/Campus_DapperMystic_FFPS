using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{	
	public Transform targetObj;
	private Vector3 initalOff;
	private Vector3 cameraPos;

	void Start()
	{  
		initalOff = transform.position - targetObj.position;
	}

	void FixedUpdate()
	{
		cameraPos = targetObj.position + initalOff;
		Vector3 rot = new Vector3(targetObj.localRotation.z, targetObj.localRotation.y, targetObj.localRotation.x);
		transform.position = Vector3.Lerp(transform.position, cameraPos, 3 * Time.fixedDeltaTime);
		transform.localRotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
}
