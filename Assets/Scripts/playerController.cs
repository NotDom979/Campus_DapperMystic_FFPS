using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	[SerializeField] CharacterController controller;
	[SerializeField] float playerSpeed = 2.0f;
	[SerializeField]float jumpHeight = 1.0f;
	[SerializeField] float gravityValue = -9.81f;
	
	[SerializeField] int jumpsMax;
	[SerializeField] float shootRate;
	[SerializeField] int shootDist;
	[SerializeField] int shootDmg;
	[SerializeField] GameObject bullet;
	private Vector3 playerVelocity;
	private int timesJumped;
	bool isShooting;

	private void Start()
	{
	}

	void Update()
	{
		movement();
		StartCoroutine(shoot());
		Destroy(bullet,3);

	}
	void movement()
	{
		if (controller.isGrounded && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
			timesJumped = 0;
		}

		Vector3 move = (transform.right * Input.GetAxis("Horizontal")) + 
		(transform.forward * Input.GetAxis("Vertical"));
		controller.Move(move * Time.deltaTime * playerSpeed);


		// Changes the height position of the player..
		if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
		{
			timesJumped++;
			playerVelocity.y = jumpHeight;
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
	IEnumerator shoot()
	{
		if (Input.GetButton("Shoot") && !isShooting)
		{
			isShooting = true;
			
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f,0.5f)), out hit, shootDist))
			{
				Instantiate(bullet, hit.point, transform.rotation);
				if(hit.collider.GetComponent<IDamage>() != null)
				{
					hit.collider.GetComponent<IDamage>().takeDamage(shootDmg);
				}
				Destroy(bullet,2);
			}
			
			Debug.Log("ZipBang");
			yield return new WaitForSeconds(shootRate);
			isShooting = false;
		}
		
	}
}
