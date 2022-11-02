using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    [Header("-----PlayerStats-----")]
    [SerializeField] public CharacterController controller;
    [SerializeField] public Animator anim;
    [SerializeField] public float playerSpeed = 2.0f;
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
    [SerializeField] GameObject Shotgun;
    [SerializeField] GameObject SMG;
    [SerializeField] GameObject Burst;
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
    public GameObject shotPoint;
    public GameObject katana;

    bool isShooting;
    public bool purchased;
    //bool isReloading = false;
    public int selectGun;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public int currentAmmoReserved;
    public int ammountOfAmmoGunHas;
    private int swap;
    public int reloadTime;
    private GameObject mfClone;
    private GameObject spClone;
    private GameObject hitEffClone;
    AudioClip stored;

    public StatusManager statusManager;

    private void Start()
    {
        statusManager = GetComponent<StatusManager>();
        playerGrunt.pitch = 2;
        playerGrunt.volume = .598f;
        anim.enabled = false;
        selectGun = 0;
        HPOrigin = HP;
        ArmorOrigin = Armor;
        respawn();
        damage = 1;
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
	    anim.enabled = true;
	    GameManager.instance.AmmoClip.text = currentAmmoReserved.ToString("F0");
    }

    void Update()
	{
		GameManager.instance.AmmoClip.text = currentAmmoReserved.ToString("F0");
	    if (GameManager.instance.playerDeadMenu.activeSelf == false && GameManager.instance.winMenu.activeSelf == false && GameManager.instance.optionMenu.activeSelf == false && GameManager.instance.pauseMenu.activeSelf == false && GameManager.instance.playerLoseMenu.activeSelf == false)
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

    }
    void movement()
    {
        if (controller.isGrounded)
        {
            //Crouch();
            if (playerVelocity.y < 0)
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
            anim.Play("Jump");
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
            anim.SetBool("Idle", true);
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
                else
                {
                    if (WeaponDetection() == 5)
                    {
                        shotgunShoot();
                    }
                    else if (WeaponDetection() == 7)
                    {
                        StartCoroutine(BurstShot());
                    }
                    else
                    {
	                    Vector3 mousePos = Input.mousePosition;
                        mousePos.z = 2.0f;
	                    // Instantiate(bullet, Camera.main.ScreenToWorldPoint(mousePos), transform.rotation);
	                    Instantiate(bullet, shotPoint.transform.position, transform.rotation);
                    }
                    Recoil();
                    Muzzle();
                    isShooting = true;
                    gunShot.clip = stored;
                    gunShot.Play();
                    currentAmmo--;
                    GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
                    RaycastHit hit;
                    if ((Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist)))
                    {
                    	if (hit.collider.CompareTag("enemy"))
                    	{
	                    	hitEffClone = Instantiate(hitEffect, hit.point, transform.rotation);
	                    	hitEffClone.SetActive(true);
	                    	hitEffect.SetActive(false);
                    	}
                    }
                }
                Debug.Log("ZipBang");
                if (shootRate <= 1)
                {
                    Recoil();
                    InvokeRepeating("shoot", 0f, shootRate);
                }
                yield return new WaitForSeconds(shootRate);
                isShooting = false;
                gunShot.Stop();
                mfClone.SetActive(false);
                Destroy(mfClone);
                Destroy(hitEffClone);

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
        HP = Mathf.Min(HP, HPOrigin);
        if (HP >= 0)
        {
            updatePLayerHud();

        }

    }
    public void replenishHealth(int amount)
    {

        HP += amount;
        HP = Mathf.Min(HP, HPOrigin);
        if (HP > 0)
        {
            updatePLayerHud();


        }

    }

    public int AmmoReload(int reload)
    {
        reload = maxAmmo - currentAmmo;
        if (currentAmmoReserved > 0)
        {

            currentAmmo += currentAmmoReserved;
            currentAmmoReserved -= reload;
            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }

            if (currentAmmo - reload >= 0)
                return reload;
            if (reload > currentAmmoReserved || currentAmmoReserved < 0)
            {
                currentAmmoReserved = 0;
            }
        }
	    GameManager.instance.AmmoClip.text = currentAmmoReserved.ToString("F0");
        return currentAmmo;
    }


    public void payDay(int currency)
    {
        if (purchased == true)
        {
	        GameManager.instance.bankTotal -= currency;
	        GameManager.instance.CheckBankTotal();
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
            GameManager.instance.bankTotal *= 0;
            GameManager.instance.playerDeadMenu.SetActive(true);
            GameManager.instance.cursorLockPause();
        }
        else
        {
            playerGrunt.Play(1);
            StartCoroutine(GameManager.instance.playerDamage());
        }
    }
    public void GunPickup(gunStats stats)
    {
        if (GameManager.instance.bankTotal >= stats.weaponCost && Input.GetKey("f"))
        {
            if (gunstats.Count == 2)
            {
                gunstats.RemoveAt(selectGun);
            }
            weaponPickupSound.Play(1);
            shootRate = stats.shootRate;
            shootDist = stats.shootDist;
            shootDmg = stats.shootDamage;
            currentAmmo = stats.ammoCount;
            maxAmmo = stats.maxAmmo;
            currentAmmoReserved = stats.currentAmmoleft;
            ammountOfAmmoGunHas = stats.maxAmmoSizeForGun;
            muzzleFlash = stats.muzzleEffect;
            hitEffect = stats.hitEffect;
            gunShot.clip = stats.sound;
            hitEffect.SetActive(true);
            gunstats.Add(stats);
            stored = gunShot.clip;
            GameManager.instance.AmmoCount.text = currentAmmo.ToString("F0");
            WeaponPickup(stats);
            shotPoint.transform.localPosition = stats.shotPoint.transform.localPosition;
            purchased = true;
            payDay(stats.weaponCost);
        }
        else if (GameManager.instance.bankTotal < stats.weaponCost)
        {
            StartCoroutine(GetMoney());
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
        currentAmmoReserved = gunstats[selectGun].currentAmmoleft;
        ammountOfAmmoGunHas = gunstats[selectGun].maxAmmoSizeForGun;
        muzzleFlash = gunstats[selectGun].muzzleEffect;
        gunShot.clip = gunstats[selectGun].sound;
        hitEffect = gunstats[selectGun].hitEffect;
        hitEffect.SetActive(true);
        stored = gunShot.clip;
        changeWeapon();
        shotPoint.transform.localPosition = gunstats[selectGun].shotPoint.transform.localPosition;
        model.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        model.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
    }

    public void updatePLayerHud()
	{
		GameManager.instance.CheckBankTotal();
        GameManager.instance.playerHpBar.fillAmount = (float)HP / (float)HPOrigin;
        GameManager.instance.playerArmorBar.fillAmount = (float)Armor / (float)ArmorOrigin;
    }
    public void respawn()
    {

        statusManager.burnTicks.Clear();
        anim.Play("Idle");
        controller.enabled = false;
        HP = HPOrigin;
        Armor = ArmorOrigin;
        updatePLayerHud();
        if (GameManager.instance.checkPoint != null)
        {
            transform.position = GameManager.instance.checkPoint.transform.position;
        }
        else
            transform.position = GameManager.instance.spawnPoint.transform.position;

        GameManager.instance.playerDeadMenu.SetActive(false);
        anim.Play("Idle");
        anim.SetBool("Idle", true);
        controller.enabled = true;
    }
    IEnumerator reloadGun()
    {
        if (currentAmmoReserved != 0)
        {
            if (Input.GetKey("r") && currentAmmo < maxAmmo)
            {
                anim.SetTrigger("Reload");
                Reload();
                reloadSound.Play(1);
                Debug.Log("Reload");
                yield return new WaitForSeconds(reloadTime);
                AmmoReload(swap);
                StartCoroutine("Wait");
                anim.SetBool("Idle", true);
                WeaponIdle();
            }
        }

    }
    IEnumerator muzzleWait()
    {
        yield return new WaitForSeconds(0.01f);
        mfClone.SetActive(false);
        //Destroy(mfClone);
    }
    IEnumerator bloodWait()
    {
        yield return new WaitForSeconds(.5f);
        hitEffect.SetActive(false);
    }
    IEnumerator StartRecoil()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Recoil", false);

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);

    }
    #region WeaponAnimTriggers
    void Recoil()
    {

        if (WeaponDetection() == 1)
        {
            anim.Play("PistolShot");
            anim.SetBool("Recoil", true);
            StartCoroutine("StartRecoil");
            anim.SetBool("Recoil", false);
            anim.Play("Pistol");
        }
        if (WeaponDetection() == 2 || WeaponDetection() == 5)
        {
            anim.Play("ARShot");
            anim.SetBool("Recoil", true);
            StartCoroutine("StartRecoil");
            anim.SetBool("Recoil", false);
            anim.Play("AR");
        }
        if (WeaponDetection() == 3)
        {
            anim.Play("RifleShot");
            anim.SetBool("Recoil", true);
            StartCoroutine("StartRecoil");
            anim.SetBool("Recoil", false);
            anim.Play("Sniper");
        }
        if (WeaponDetection() == 4)
        {
            anim.Play("BaShoot");
            anim.SetBool("Recoil", true);
            StartCoroutine("StartRecoil");
            anim.SetBool("Recoil", false);
            anim.Play("Bazooka");
        }
        if (WeaponDetection() == 7)
        {
            anim.Play("BurstShot");
            anim.SetBool("Recoil", true);
            StartCoroutine("StartRecoil");
            anim.SetBool("Recoil", false);
            anim.Play("AR");
        }
    }
    void Reload()
    {
        if (WeaponDetection() == 1)
        {
            anim.Play("PistolReload");
            anim.SetTrigger("Reload");
        }
        else if (WeaponDetection() == 2 || WeaponDetection() == 5)
        {
            anim.Play("AReload");
            anim.SetTrigger("Reload");
        }

        else if (WeaponDetection() == 3)
        {
            anim.Play("SniperReload");
            anim.SetTrigger("Reload");
        }
        else if (WeaponDetection() == 4)
        {
            anim.Play("BaReload");
            anim.SetTrigger("Reload");
        }


    }
    void WeaponIdle()
    {
        if (WeaponDetection() == 1)
        {
            anim.Play("Pistol");
            AllBoolFalse();
            anim.SetBool("PistolBool", true);
            anim.SetBool("Idle", true);

        }
        else if (WeaponDetection() == 2 || WeaponDetection() == 5 || WeaponDetection() == 6 || WeaponDetection() == 7)
        {
            anim.Play("AR");
            AllBoolFalse();
            anim.SetBool("ArBool", true);
            anim.SetBool("Idle", true);
        }
        else if (WeaponDetection() == 3)
        {
            anim.Play("Sniper");
            AllBoolFalse();
            anim.SetBool("SniperBool", true);
            anim.SetBool("Idle", true);
        }
        else if (WeaponDetection() == 4)
        {
            anim.Play("Bazooka");
            AllBoolFalse();
            anim.SetBool("BaBool", true);
            anim.SetBool("Idle", true);
        }
    }
    void Muzzle()
    {
        if (WeaponDetection() == 1)
        {
            mfClone = Instantiate(pistolMuzzle, pistolSp.transform.position, transform.rotation);
            mfClone.SetActive(true);
            StartCoroutine("muzzleWait");
        }
        else if (WeaponDetection() == 2 || WeaponDetection() == 5 || WeaponDetection() == 6)
        {
            mfClone = Instantiate(arMuzzle, rifleSp.transform.position, transform.rotation);
            mfClone.SetActive(true);
            StartCoroutine("muzzleWait");
        }
        else if (WeaponDetection() == 3)
        {
            mfClone = Instantiate(sniperMuzzle, SniperSp.transform.position, transform.rotation);
            mfClone.SetActive(true);
            StartCoroutine("muzzleWait");
        }
        else if (WeaponDetection() == 4)
        {
            mfClone = Instantiate(bazookaMuzzle, bazookaSp.transform.position, transform.rotation);
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
    int WeaponDetection()
    {
        if (Pistol.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Pistol"))
        {
            return 1;
        }
        else if (AR.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Rifle"))
        {
            return 2;
        }
        else if (Sniper.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Sniper"))
        {
            return 3;
        }
        else if (Bazooka.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Bazooka"))
        {
            return 4;
        }
        else if (Shotgun.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Shotgun"))
        {
            return 5;
        }
        else if (SMG.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("SMG"))
        {
            return 6;
        }
        else if (Burst.activeSelf == true || gameObject.GetComponent<Collider>().CompareTag("Burst"))
        {
            return 7;
        }

        return 0;
    }
    void WeaponPickup(gunStats stats)
    {
        if (gameObject.GetComponent<Collider>().CompareTag("Pistol") || maxAmmo == 20 || stats.Tag == "Pistol")
        {
            anim.Play("Pistol");
            AllFalse();
            Pistol.SetActive(true);
            anim.SetBool("PistolBool", true);
            Pistol.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
            Pistol.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else if (gameObject.GetComponent<Collider>().CompareTag("Rifle") || stats.Tag == "Rifle" || stats.Tag == "Shotgun" || stats.Tag == "SMG" || stats.Tag == "Burst")
        {
            anim.Play("AR");
            AllFalse();
            anim.SetBool("ArBool", true);
            if (stats.Tag == "SMG")
            {
                SMG.SetActive(true);
                SMG.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
                SMG.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else if (stats.Tag == "Shotgun")
            {
                Shotgun.SetActive(true);
                Shotgun.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
                Shotgun.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else if (stats.Tag == "Burst")
            {
                Burst.SetActive(true);
                Burst.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
                Burst.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else
            {
                AR.SetActive(true);
                AR.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
                AR.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }

        }
        else if (gameObject.GetComponent<Collider>().CompareTag("Sniper") || maxAmmo == 5)
        {
            anim.Play("Sniper");
            AllFalse();
            anim.SetBool("SniperBool", true);
            Sniper.SetActive(true);
            Sniper.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
            Sniper.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        }
        else if (gameObject.GetComponent<Collider>().CompareTag("Bazooka") || maxAmmo == 1)
        {
            anim.Play("Bazooka");
            AllFalse();
            anim.SetBool("BaBool", true);
            Bazooka.SetActive(true);
            Bazooka.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
            Bazooka.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        }

    }
    void changeWeapon()
    {
        if (gunstats[selectGun].Tag == "Rifle" || gunstats[selectGun].Tag == "Shotgun" || gunstats[selectGun].Tag == "SMG" || gunstats[selectGun].Tag == "Burst")
        {
            anim.Play("AR");
            AllFalse();
            anim.SetBool("ArBool", true);
            AR.SetActive(true);
            if (gunstats[selectGun].Tag == "Shotgun")
            {
                AR.SetActive(false);
                Shotgun.SetActive(true);
                Shotgun.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
                Shotgun.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else if (gunstats[selectGun].Tag == "SMG")
            {
                AR.SetActive(false);
                SMG.SetActive(true);
                SMG.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
                SMG.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else if (gunstats[selectGun].Tag == "Burst")
            {
                AR.SetActive(false);
                Burst.SetActive(true);
                Burst.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
                Burst.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else
            {

                AR.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
                AR.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
            }

        }
        else if (gunstats[selectGun].Tag == "Bazooka")
        {
            anim.Play("Bazooka");
            AllFalse();
            anim.SetBool("BaBool", true);
            Bazooka.SetActive(true);
            Bazooka.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
            Bazooka.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        }
        else if (gunstats[selectGun].Tag == "Sniper")
        {
            anim.Play("Sniper");
            AllFalse();
            anim.SetBool("SniperBool", true);
            Sniper.SetActive(true);
            Sniper.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
            Sniper.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        }
        else if (gunstats[selectGun].Tag == "Pistol")
        {
            AllFalse();
            anim.Play("Pistol");
            anim.SetBool("PistolBool", true);
            Pistol.SetActive(true);
            Pistol.GetComponent<MeshFilter>().sharedMesh = gunstats[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
            Pistol.GetComponent<MeshRenderer>().sharedMaterial = gunstats[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

    #endregion

    IEnumerator GetMoney()
    {
        GameManager.instance.bankRupt.enabled = true;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.bankRupt.enabled = false;
    }

    void shotgunShoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2.0f;
        for (int i = 0; i < 1; i++)
        {
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(1, 2)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(0, 2)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(0, 1)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(1, 3)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(0, 3)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(2, 3)), transform.rotation);
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(1, 2)), transform.rotation);
        }
    }

    IEnumerator BurstShot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2.0f;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, (Camera.main.ScreenToWorldPoint(mousePos) * Random.Range(0, 1)), transform.rotation);
            yield return new WaitForSeconds(.2f);
            gunShot.Play();
        }
    }
    void AllFalse()
    {
        anim.SetBool("SniperBool", false);
        anim.SetBool("ArBool", false);
        anim.SetBool("PistolBool", false);
        anim.SetBool("BaBool", false);
        Pistol.SetActive(false);
        AR.SetActive(false);
        Sniper.SetActive(false);
        Burst.SetActive(false);
        Bazooka.SetActive(false);
        SMG.SetActive(false);
        Shotgun.SetActive(false);
    }
    void AllBoolFalse()
    {
        anim.SetBool("SniperBool", false);
        anim.SetBool("ArBool", false);
        anim.SetBool("PistolBool", false);
        anim.SetBool("BaBool", false);
    }






}
