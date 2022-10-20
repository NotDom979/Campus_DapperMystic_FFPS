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
	public GameObject arMuzzle;
	public GameObject sniperMuzzle;
	public GameObject bazookaMuzzle;
	public GameObject pistolMuzzle;
	int HPOrigin;
	int ArmorOrigin;
    private Vector3 playerVelocity;
    private int timesJumped;

    [Header("-----GunStats-----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDmg;
	[SerializeField] GameObject bullet;
	[SerializeField] public GameObject missile;
    [SerializeField] List<gunStats> gunstats = new List<gunStats>();
	[SerializeField] GameObject Pistol;
	[SerializeField] GameObject model;
	[SerializeField] GameObject AR;
	[SerializeField] GameObject Sniper;
	[SerializeField] GameObject Bazooka;
	public GameObject hitEffect;
    public GameObject muzzleFlash;
    public AudioSource gunShot;
	public AudioSource reloadSound;
	public AudioSource weaponPickupSound;
	public GameObject pistolSp;
	public GameObject rifleSp;
	public GameObject SniperSp;
	public GameObject bazookaSp;
	public AudioClip emptyMag;

    bool isShooting;
    //bool isReloading = false;
    public int selectGun;

    public int maxAmmo;
    public int currentAmmo;
    public int reloadTime;
    private GameObject mfClone;
	private GameObject spClone;
	private GameObject hitEffClone;
	AudioClip stored;


    private void Start()
	{
		playerGrunt.pitch = 2;
		playerGrunt.volume = .598f;
        selectGun = 0;
		HPOrigin = HP;
		ArmorOrigin = Armor;
		respawn();
		currentAmmo = maxAmmo;
		stored = gunShot.clip;
		GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
		arMuzzle.SetActive(false);
		sniperMuzzle.SetActive(false);
		bazookaMuzzle.SetActive(false);
		pistolMuzzle.SetActive(false);
		anim.SetBool("ArBool", false);
		anim.SetBool("SniperBool", false);
		anim.SetBool("PistolBool", false);
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
	    if (controller.isGrounded)
	    {
		    //Crouch();
	    	if ( playerVelocity.y < 0)
	    	{
		    	playerVelocity.y = 0f;
				timesJumped = 0;
		    }    	
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
	        anim.SetTrigger("Jump");
	        //gameObject.GetComponent<Animator>().Play("Jump");
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
	        StartCoroutine("Wait");
	        anim.SetTrigger("Idle");
	        // gameObject.GetComponent<Animator>().Play("Idle");
        }
	    //anim.ResetTrigger("Jump");
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
            	if (maxAmmo == 1)
            	{
                   
            		 currentAmmo--;
            		 BazookaShoot();

                    
            	}
            	else {
            		
            	
            	Recoil();
	           	Muzzle();
	           	isShooting = true;
	           	gunShot.clip = stored;
	           	gunShot.Play();
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
		           	}
              
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
	           	mfClone.SetActive(false);
	            
	            //Destroy(hitEffClone);
	            
            }
	        else
	        {
	        	gunShot.clip = emptyMag;
	        	gunShot.Play();
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
    public void AddArmor(int armorAmount)
    {


        Armor += armorAmount;
        if (Armor > ArmorOrigin)
            Armor = ArmorOrigin;
        if (Armor >= 0)
            updatePLayerHud();


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
	    // shotPoint.transform.localPosition = stats.shotPoint.transform.localPosition;
	    hitEffect = stats.hitEffect;
	    gunShot.clip = stats.sound;
        hitEffect.SetActive(true);
	    gunstats.Add(stats);
	    stored = gunShot.clip;
	    GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
	    if (maxAmmo == 30)
	    {
		    gameObject.GetComponent<Animator>().Play("AR");
	    	Pistol.SetActive(false);
	    	Sniper.SetActive(false);
		    Bazooka.SetActive(false);
	    	AR.SetActive(true);
		    AR.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
		    AR.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 1)
	    {
		    gameObject.GetComponent<Animator>().Play("Bazooka");
	    	Pistol.SetActive(false);
	    	Sniper.SetActive(false);
	    	AR.SetActive(false);
	    	Bazooka.SetActive(true);
		    Bazooka.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Bazooka.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 5)
	    {
		    gameObject.GetComponent<Animator>().Play("Sniper");
	    	AR.SetActive(false);
	    	Pistol.SetActive(false);
		    Bazooka.SetActive(false);
	    	Sniper.SetActive(true);
		    Sniper.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Sniper.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 20)
	    {
		    gameObject.GetComponent<Animator>().Play("Pistol");
	    	AR.SetActive(false);
	    	Sniper.SetActive(false);
		    Bazooka.SetActive(false);
	    	Pistol.SetActive(true);
	    	Pistol.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Pistol.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
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
	    gunShot.clip = gunstats[selectGun].sound;
	    hitEffect = gunstats[selectGun].hitEffect;
        hitEffect.SetActive(true);
        model.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
	    model.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    stored = gunShot.clip;
	    if (maxAmmo == 30)
	    {
		    gameObject.GetComponent<Animator>().Play("AR");
	    	Pistol.SetActive(false);
	    	Sniper.SetActive(false);
		    Bazooka.SetActive(false);
	    	AR.SetActive(true);
		    AR.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
		    AR.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 1)
	    {
		    gameObject.GetComponent<Animator>().Play("Bazooka");
	    	Pistol.SetActive(false);
	    	Sniper.SetActive(false);
	    	AR.SetActive(false);
	    	Bazooka.SetActive(true);
		    Bazooka.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Bazooka.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 5)
	    {
		    gameObject.GetComponent<Animator>().Play("Sniper");
	    	AR.SetActive(false);
	    	Pistol.SetActive(false);
	    	Bazooka.SetActive(false);
	    	Sniper.SetActive(true);
		    Sniper.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Sniper.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
	    	
	    }
	    if (maxAmmo == 20)
	    {
		    gameObject.GetComponent<Animator>().Play("Pistol");
	    	AR.SetActive(false);
	    	Sniper.SetActive(false);
		    Bazooka.SetActive(false);
	    	Pistol.SetActive(true);
	    	Pistol.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
		    Pistol.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
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
	    if (GameManager.instance.checkPoint != null)
	    {
		    transform.position = GameManager.instance.checkPoint.transform.position;
	    }
	    else
		    transform.position = GameManager.instance.spawnPoint.transform.position;
        	
        GameManager.instance.playerDeadMenu.SetActive(false);
        controller.enabled = true;
    }
    IEnumerator reloadGun()
    {

	    if (Input.GetKey("r") && currentAmmo < maxAmmo)
	    {
		    anim.SetTrigger("Reload");
		    Reload();
        	reloadSound.Play(1);
            Debug.Log("Reload");
            yield return new WaitForSeconds(reloadTime);
		    currentAmmo = maxAmmo;
		    if (currentAmmo == maxAmmo)
		    {
		    	StartCoroutine("Wait");
		    	anim.SetTrigger("Idle");
			    WeaponIdle();
		    }
        }

    }
    IEnumerator muzzleWait()
    {
	    yield return new WaitForSeconds(0.01f);
	    mfClone.SetActive(false);
	    Destroy(mfClone);
    }
	IEnumerator bloodWait()
	{
		yield return new WaitForSeconds(.5f);
		hitEffect.SetActive(false);
	}
	IEnumerator StartRecoil()
	{
		yield return new WaitForSeconds(.5f);
	
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3f);
	
	}
	void Recoil()
	{
		
		if (maxAmmo == 1)
		{
			gameObject.GetComponent<Animator>().Play("BaShoot");
			StartCoroutine("StartRecoil");
			gameObject.GetComponent<Animator>().Play("Bazooka");
		}
		if (maxAmmo == 5)
		{
			gameObject.GetComponent<Animator>().Play("RifleShot");
			StartCoroutine("StartRecoil");
			gameObject.GetComponent<Animator>().Play("Sniper");
		}
		if (maxAmmo == 20)
		{
			gameObject.GetComponent<Animator>().Play("PistolShot");
			StartCoroutine("StartRecoil");
			gameObject.GetComponent<Animator>().Play("Pistol");
		}
		if (maxAmmo == 30)
		{
			gameObject.GetComponent<Animator>().Play("ARShot");
			StartCoroutine("StartRecoil");
			gameObject.GetComponent<Animator>().Play("AR");
		}
	}
	void Reload()
	{
		
		
			if (maxAmmo == 5)
			{
				gameObject.GetComponent<Animator>().Play("SniperReload");
			}
			if (maxAmmo == 1)
			{
				gameObject.GetComponent<Animator>().Play("BaReload");
			}
			if (maxAmmo == 20)
			{
				gameObject.GetComponent<Animator>().Play("PistolReload");
			}
			if (maxAmmo == 30)
			{
				gameObject.GetComponent<Animator>().Play("AReload");
			}
		
	}
	void WeaponIdle()
	{
		if (maxAmmo == 5)
		{
			gameObject.GetComponent<Animator>().Play("Sniper");
		}
		if (maxAmmo == 1)
		{
			gameObject.GetComponent<Animator>().Play("Bazooka");
		}
		if (maxAmmo == 20)
		{
			gameObject.GetComponent<Animator>().Play("Pistol");
		}
		if (maxAmmo == 30)
		{
			gameObject.GetComponent<Animator>().Play("AR");
		}
	}
	void Muzzle()
	{
		if (maxAmmo == 5)
		{
			mfClone = Instantiate(sniperMuzzle, SniperSp.transform.position, transform.rotation);
			mfClone.SetActive(true);
			StartCoroutine("muzzleWait");
		}
		if (maxAmmo == 1)
		{
			mfClone = Instantiate(bazookaMuzzle, bazookaSp.transform.position, transform.rotation);
			mfClone.SetActive(true);
			StartCoroutine("muzzleWait");
		}
		if (maxAmmo == 20)
		{
			mfClone = Instantiate(pistolMuzzle, pistolSp.transform.position, transform.rotation);
			mfClone.SetActive(true);
			StartCoroutine("muzzleWait");
		}
		if (maxAmmo == 30)
		{
			mfClone = Instantiate(arMuzzle, rifleSp.transform.position, transform.rotation);
			mfClone.SetActive(true);
			StartCoroutine("muzzleWait");
		}
	}
	void Crouch()
	{
		if (Input.GetKeyDown("c"))
		{
			//anim.SetTrigger("Crouch");
			//gameObject.GetComponent<Animator>().Play("Crouch");
		}
		//	else
		//anim.SetTrigger("Idle");
	}
	void BazookaShoot()
	{
		isShooting = true;

		Instantiate(missile, bazookaSp.transform.position, transform.rotation);
		

		gunShot.Play();

		gunShot.Stop();
		//Destroy(missile);
		isShooting = false;
	}
}
