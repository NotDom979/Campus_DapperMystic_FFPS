using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	
	
public class WeaponHud: MonoBehaviour
{
	[SerializeField] int FacePlayerSpeed;
	Vector3 playerDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		playerDirection = transform.position - GameManager.instance.player.transform.position;
	    playerDirection.y = 0;
	    Quaternion rotation = Quaternion.LookRotation(playerDirection);
	    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);
    }
}
