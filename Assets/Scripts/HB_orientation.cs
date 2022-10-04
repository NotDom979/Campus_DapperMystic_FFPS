using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HB_orientation : MonoBehaviour
{
	public Transform cam;

    // Update is called once per frame
    void Update()
    {
	    transform.LookAt(cam);
    }
}
