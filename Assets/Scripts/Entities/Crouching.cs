using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
	public CharacterController PlayerHeight;
	public float normalHeight;
	public float crouchHeight;
	
    // Update is called once per frame
    void Update()
    {
	    if(Input.GetKeyDown(KeyCode.C))
	    {
	    	PlayerHeight.height = crouchHeight;
	    }
	    else if(Input.GetKeyUp(KeyCode.C))
	    {
	    	PlayerHeight.height = normalHeight;
	    }
    }
}
