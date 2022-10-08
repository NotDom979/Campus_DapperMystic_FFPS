using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	[Header("-----PlayerStats-----")]
	[SerializeField] CharacterController controller;
	[SerializeField] CharacterController PlayerHeight;
	[SerializeField] float playerSpeed = 2.0f;
	[SerializeField]float jumpHeight = 1.0f;
	[SerializeField] float gravityValue = -9.81f;
	[SerializeField] int jumpsMax;
	[SerializeField] int normalHeight;
	[SerializeField] int crouchHeight;
	[SerializeField] int HP;
	
	int HPOrigin;
	private Vector3 playerVelocity;
	private int timesJumped;
	
	[Header("-----GunStats-----")]
	[SerializeField] float shootRate;
	[SerializeField] int shootDist;
	[SerializeField] int shootDmg;
	[SerializeField] GameObject bullet;
	[SerializeField] List<gunStats> gunstats = new List<gunStats>();
	[SerializeField] GameObject model;
	
	bool isShooting;
	//bool isReloading = false;
	public int selectGun;

	public int maxAmmo;
	public int currentAmmo;
	public int reloadTime;
	
	private void Start()
	{
		HPOrigin = HP;
		respawn();
		currentAmmo = maxAmmo;
		GameManager.instance.AmmoCount.text	= currentAmmo.ToString("F0");
	}

	void Update()
	{
		StartCoroutine(reloadGun());
		movement();
		StartCoroutine(shoot());
		gunselect();
		
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
		if (Input.GetKeyDown(KeyCode.C))
		{
			PlayerHeight.height = crouchHeight;
		}
		if(Input.GetKeyUp(KeyCode.C))
		{
			PlayerHeight.height = normalHeight;
		}
		
	}
	IEnumerator shoot()
	{
		if (Input.GetButton("Shoot") && !isShooting)
		{
			if (currentAmmo >= 1)
			{
				isShooting = true;
				currentAmmo--;
				GameManager.instance.AmmoCount.text	= currentAmmo.ToString("F0");
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f,0.5f)), out hit, shootDist))
				{
					//Instantiate(bullet, hit.point, transform.rotation);
					if(hit.collider.GetComponent<IDamage>() != null)
					{
						hit.collider.GetComponent<IDamage>().takeDamage(shootDmg);
					}
				}
			
				Debug.Log("ZipBang");
				yield return new WaitForSeconds(shootRate);
				isShooting = false;
			}
		}
		
	}
	
	public void takeDamage(int dmg)
	{
		HP	-= dmg;
		updatePLayerHud();
		StartCoroutine(GameManager.instance.playerDamage());
		if (HP <= 0)
		{
			GameManager.instance.playerDeadMenu.SetActive(true);
			GameManager.instance.cursorLockPause();
		}
	}
	public void gunPickup(gunStats stats)
	{
		shootRate = stats.shootRate;
		shootDist = stats.shootDist;
		shootDmg = stats.shootDamage;
		currentAmmo = stats.ammoCount;
		maxAmmo = stats.maxAmmo;
		
		model.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
		model.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
		
		gunstats.Add(stats);
		GameManager.instance.AmmoCount.text	= currentAmmo.ToString("F0");
	}
	void gunselect()
	{
		if (gunstats.Count > 1)
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectGun > gunstats.Count - 1)
			{
				selectGun++;
				changeGun();
			}	
			if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectGun > 0)
			{
				selectGun--;
				changeGun();
			}	
		}
		GameManager.instance.AmmoCount.text	= currentAmmo.ToString("F0");
	}
	void changeGun()
	{
		shootRate = gunstats[selectGun].shootRate;
		shootDist = gunstats[selectGun].shootDist;
		shootDmg = gunstats[selectGun].shootDamage;
		currentAmmo = gunstats[selectGun].ammoCount;
				
		model.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
		model.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;	
	}
	
	public void updatePLayerHud()
	{
		GameManager.instance.playerHpBar.fillAmount = (float)HP/(float)HPOrigin;
	}
	public void respawn()
	{
		controller.enabled = false;
		HP = HPOrigin;
		updatePLayerHud();
		transform.position = GameManager.instance.spawnPoint.transform.position;
		GameManager.instance.playerDeadMenu.SetActive(false);
		controller.enabled = true;
	}
	IEnumerator reloadGun (){
		
		if(Input.GetKeyDown("r"))
		{
			Debug.Log("Reload");
			yield return new WaitForSeconds(reloadTime);
			currentAmmo = maxAmmo;
		}
		
	}
}
