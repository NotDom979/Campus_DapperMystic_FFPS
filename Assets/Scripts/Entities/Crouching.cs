using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Crouching : MonoBehaviour
{
	public CharacterController PlayerHeight;
	//makes camera move with the whole body - for dom :)
	[SerializeField] private Transform PlayerCamera = null;

	[Range(0f, 3f)]
    [SerializeField] private float normalHeight;
    [SerializeField] private float crouchHeight;
    


	public float _speed;
     bool isCrouching;
    private bool duringCrouchAnimation;
	


    // Update is called once per frame

    private void Start()
	{
		normalHeight = PlayerHeight.height;
	}
	void Update()
    {

	    if(Input.GetKeyDown(KeyCode.C))
	    {
		  isCrouching = true;
			PlayerHeight.height = crouchHeight;
	    }
	   else if(Input.GetKeyUp(KeyCode.C))
	   {
            isCrouching = false;
            PlayerHeight.height = normalHeight;
       }
    }
}
