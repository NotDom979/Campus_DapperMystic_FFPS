using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
	public Animator creditSlide;
    // Start is called before the first frame update
    void Start()
    {
	    creditSlide.enabled = false;
	    creditSlide.enabled = true;
    }
	void Update()
	{
		if(creditSlide.enabled == false)
		{
			creditSlide.enabled = true;
		}
	}

}
