using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("-----PlayerStats-----")]
    [SerializeField] public CharacterController controller;
	[SerializeField] public Animator anim;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] int jumpsMax;
	[SerializeField] public int HP;
	[SerializeField] public int Armor;
	public AudioSource playerGrunt;
	public AudioSource playerJumpNoise;
	public AudioSource playerFootSteps;
	int HPOrigin;
	int ArmorOrigin;
    private Vector3 playerVelocity;
    private int timesJumped;

    [Header("-----GunStats-----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDmg;
    [SerializeField] GameObject bullet;
    [SerializeField] List<gunStats> gunstats = new List<gunStats>();
	[SerializeField] GameObject model;
	public GameObject hitEffect;
    public GameObject muzzleFlash;
    public AudioSource gunShot;
	public AudioSource reloadSound;
	public AudioSource weaponPickupSound;
    public GameObject shotPoint;

    bool isShooting;
    //bool isReloading = false;
    public int selectGun;

    public int maxAmmo;
    public int currentAmmo;
    public int reloadTime;
    private GameObject mfClone;
	private GameObject spClone;
	private GameObject hitEffClone;


    private void Start()
	{
		playerGrunt.pitch = 2;
		playerGrunt.volume = .598f;
        selectGun = 0;
		HPOrigin = HP;
		ArmorOrigin = Armor;
        respawn();
        currentAmmo = maxAmmo;
	    GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
    }

    void Update()
    {
        StartCoroutine(reloadGun());
	    movement();
        StartCoroutine(shoot());
	    gunselect();
	    if (GameManager.instance.playerDeadMenu.activeSelf == true)
	    {
	    	GameManager.instance.damageFlash.SetActive(false);
	    }
	  
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
	    if (move != null)
	    {
	    	anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), move.magnitude, Time.deltaTime * 3));
	    }
        if (Input.GetButton("Sprint"))
        {
            playerFootSteps.pitch = 2.5f;
	        controller.Move(move * Time.deltaTime * (playerSpeed * 1.10f));
        }
        else
        {
            playerFootSteps.pitch = 1.6f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            timesJumped++;
	        playerVelocity.y = jumpHeight;
	        anim.SetBool("Jump", true);
	        anim.SetTrigger("Hop");
	        if (timesJumped == 1)
	        {
		        playerJumpNoise.pitch = 1;
		        playerJumpNoise.Play(1);
	        }
	        else if (timesJumped == 2)
	        {
	        	playerJumpNoise.pitch = 1.1f;
	        	playerJumpNoise.Play();
	        }
        }
	    anim.SetBool("Jump", false);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && timesJumped == 0)
        {
            playerFootSteps.enabled = true;
            
        }
        else
        {
            playerFootSteps.enabled = false;
        }

    }
    IEnumerator shoot()
    {
        if ((Input.GetButtonDown("Shoot") || Input.GetButton("Shoot")) && !isShooting)
        {
            if (currentAmmo >= 1)
            {
                isShooting = true;
                //mfClone = Instantiate(muzzleFlash, shotPoint.transform.position, transform.rotation);
	            //mfClone.SetActive(true);
	           // gunShot.Play();
	           // StartCoroutine(StartRecoil());
               // StartCoroutine(muzzleWait());
                currentAmmo--;
                GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
                {
	                //Instantiate(bullet, hit.point, transform.rotation);
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
    	                hitEffClone = Instantiate(hitEffect, hit.point, transform.rotation);
	                    hitEffClone.SetActive(true);

	                    hit.collider.GetComponent<IDamage>().takeDamage(shootDmg);
                        hitEffect.SetActive(false);
                        //StartCoroutine(bloodWait());
                    }
                }
                

                Debug.Log("ZipBang");
                if (shootRate <= 1)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(shootRate);
                isShooting = false;
                gunShot.Stop();
	            //Destroy(mfClone);
	            //Destroy(hitEffClone);
            }
        }

    }
	public void AddHealth(int amount)
	{
		
		HP += amount;
		HP = Mathf.Min(HP,HPOrigin);
		if (HP >= 0)
		{
			updatePLayerHud();
			
		}
		
	}

    public void takeDamage(int dmg)
	{
		if (Armor <= 0)
		{
			HP -= dmg;
		}
		else
			Armor -= dmg;
	    updatePLayerHud();
	    if (HP <= 0)
	    {
		    playerGrunt.volume = 1;
		    playerGrunt.pitch = 1;
		    playerGrunt.Play(1);
		    GameManager.instance.playerDeadMenu.SetActive(true);
		    GameManager.instance.cursorLockPause();
	    }
	    else 
	    {
	    	playerGrunt.Play(1);
		    StartCoroutine(GameManager.instance.playerDamage());
        }
    }
    public void gunPickup(gunStats stats)
    {
	    weaponPickupSound.Play(1);
        shootRate = stats.shootRate;
        shootDist = stats.shootDist;
        shootDmg = stats.shootDamage;
        currentAmmo = stats.ammoCount;
        maxAmmo = stats.maxAmmo;
        muzzleFlash = stats.muzzleEffect;
	    shotPoint.transform.localPosition = stats.shotPoint.transform.localPosition;
	    hitEffect = stats.hitEffect;
        hitEffect.SetActive(true);
        model.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
	    model.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    //gameObject.GetComponent<MeshRenderer>().sharedMaterial =
        gunstats.Add(stats);
	    GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
	    if (maxAmmo == 20)
	    {
	    	anim.SetTrigger("Pistol");
	    }
    }
    void gunselect()
    {
        if (gunstats.Count > 1)
        {
            if ((Input.GetKeyDown("q") || Input.GetAxis("Mouse ScrollWheel") > 0) && selectGun < gunstats.Count - 1)
            {
                selectGun++;
                changeGun();
            }
            if ((Input.GetKeyDown("e") || Input.GetAxis("Mouse ScrollWheel") < 0) && selectGun > 0)
            {
                selectGun--;
                changeGun();
            }
        }
        GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
    }
    void changeGun()
    {
	    weaponPickupSound.Play(1);
        shootRate = gunstats[selectGun].shootRate;
        shootDist = gunstats[selectGun].shootDist;
        shootDmg = gunstats[selectGun].shootDamage;
	    currentAmmo = gunstats[selectGun].ammoCount;
	    maxAmmo = gunstats[selectGun].maxAmmo;
        muzzleFlash = gunstats[selectGun].muzzleEffect;
	    shotPoint.transform.localPosition = gunstats[selectGun].shotPoint.transform.localPosition;
	    hitEffect = gunstats[selectGun].hitEffect;
        hitEffect.SetActive(true);
        model.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
	    model.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    if (maxAmmo == 20)
	    {
	    	anim.SetTrigger("Pistol");
	    }
    }

    public void updatePLayerHud()
    {
	    GameManager.instance.playerHpBar.fillAmount = (float)HP / (float)HPOrigin;
	    GameManager.instance.playerArmorBar.fillAmount = (float)Armor / (float)ArmorOrigin;
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
    IEnumerator reloadGun()
    {

        if (Input.GetKeyDown("r"))
        {
        	reloadSound.Play(1);
            Debug.Log("Reload");
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
        }

    }
    IEnumerator muzzleWait()
    {
        yield return new WaitForSeconds(0.01f);
        mfClone.SetActive(false);
    }
	IEnumerator bloodWait()
	{
		yield return new WaitForSeconds(.5f);
		hitEffect.SetActive(false);
	}
	IEnumerator StartRecoil()
	{
		model.GetComponent<Animator>().Play("Recoil");
		yield return new WaitForSeconds(.2f);
		model.GetComponent<Animator>().Play("New State");
	}
	
	
}
