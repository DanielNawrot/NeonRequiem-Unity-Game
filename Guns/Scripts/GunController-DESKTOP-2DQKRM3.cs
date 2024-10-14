using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GunController : MonoBehaviour
{
    public bool activated = true;

    public Transform tr;
    public SpriteRenderer spr;
    public TextMeshProUGUI ammoText;
    public CameraShake cameraShake;
    public GameObject muzzleFlash;

    public Weapon[] weapons;
    public int currentWeaponIndex = 0;
    private Weapon currentWeapon;
    public enum FireMode { Semi, Auto, Shotgun}
    public FireMode fireMode;

    public GameObject bulletPrefab; // Bullet prefab to Instantiate
    public Transform firePoint;     // Point from which the bullets are instatiated
    public float bulletSpeed = 20f; // The speed of the bullet

    public float fireRate = 0.1f;   // The minimum time between shots
    private float fireCooldown;     // The live time left before the next shot can be fired
    private bool reloading = false;
    public int ammo;
    private float reloadTimer;

    public float bloomAmount = 2f;  // The maximum angle at which bullets can deviate from aiming direction

    public float cameraShakeIntesity;
    public float cameraShakeDuration;
    public float cameraShakeDecay;

    public GameObject bulletCasing;

    public int curWeaponDamage;

    public GameObject reloadIndicator;

    [SerializeField] public AudioClip shoutSound;
    [SerializeField] public AudioClip reloadSound;
    [SerializeField] public float ShotSFXVolume = 0.5f;

    void Start()
    {
        EquipWeapon(currentWeaponIndex);
        spr.sprite = currentWeapon.weaponSprite;
        ammo = currentWeapon.ammoCapacity;

        cameraShakeIntesity = currentWeapon.cameraShakeIntesity;
        cameraShakeDuration = currentWeapon.cameraShakeDuration;
        cameraShakeDecay = currentWeapon.cameraShakeDecay;

        curWeaponDamage = weapons[currentWeaponIndex].damage;

        reloadIndicator.SetActive(false);

        UpdateFirePoint();
        UpdateAmmoUI();
    }


    void Update()
    {
         if (reloading)
         {
             HandleReload();
         }
         else
         {
             handleShooting();
             reload();
         }
         pointToMouse();
    }

    void pointToMouse()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x += gameObject.transform.position.x;
        

       Vector3 aimDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        if (angle > 90 || angle < -90)
        {
            tr.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            tr.localScale = new Vector3(1, 1, 1);
        }
    }

    void handleShooting()
    {
        fireCooldown -= Time.deltaTime;

        if (currentWeapon.fireMode == FireMode.Semi && ammo > 0 && !reloading)
        {
            if (Input.GetButtonDown("Fire1") && fireCooldown <= 0)
            {
                shoot();
                fireCooldown = currentWeapon.fireRate;
            }
        }

        if (currentWeapon.fireMode == FireMode.Auto && ammo > 0 && !reloading)
        {
            if (Input.GetButton("Fire1") && fireCooldown <= 0f)
            {
                shoot();
                fireCooldown = currentWeapon.fireRate;
            }
        }

        if (currentWeapon.fireMode == FireMode.Shotgun && ammo > 0 && !reloading)
        {
            if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f)
            {
                ShootShotgun();
                fireCooldown = currentWeapon.fireRate;
            }
        }
    }
    void shoot()
    {
        float bloomOffset = Random.Range(-bloomAmount, bloomAmount);
        Quaternion bloomRotation = firePoint.rotation * Quaternion.Euler(0,0,bloomOffset);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // SFX
        SFXManager.instance.PlaySoundFXClip(shoutSound, transform, ShotSFXVolume);

        rb.velocity = bloomRotation * Vector2.right * currentWeapon.bulletSpeed;
        if (cameraShake != null)
        {
            cameraShake.Shake(cameraShakeDuration, cameraShakeIntesity, cameraShakeDecay);
        }
        ammo--;
        GameObject mFlash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);

        UpdateAmmoUI();
        ejectBulletCasing();
    }

    void ShootShotgun()
    {
        for (int i = 0; i < currentWeapon.shotgunPellets; i++)
        {
            float spreadOffset = Random.Range(-currentWeapon.shotgunSpread / 2f, currentWeapon.shotgunSpread / 2f);
            Quaternion pelletRotation = firePoint.rotation * Quaternion.Euler(0, 0, spreadOffset);

            // SFX
            SFXManager.instance.PlaySoundFXClip(shoutSound, transform, ShotSFXVolume);

            GameObject pellet = Instantiate(bulletPrefab, firePoint.position, pelletRotation);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = pelletRotation * Vector2.right * currentWeapon.bulletSpeed;
            if (cameraShake != null)
            {
                cameraShake.Shake(cameraShakeDuration, cameraShakeIntesity, cameraShakeDecay);
            }
            ammo--;
            GameObject mFlash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);

            UpdateAmmoUI();
            
        }
        ejectBulletCasing();
    }

    void reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammo < currentWeapon.ammoCapacity)
        {
            reloading = true;
            if (ammoText != null)
            {
                ammoText.text = $"-- / {currentWeapon.ammoCapacity}";
            }
            reloadIndicator.SetActive(true);
            reloadTimer = currentWeapon.reloadTime;
        }
    }

    void HandleReload()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0f)
        {
            SFXManager.instance.PlaySoundFXClip(reloadSound, transform, ShotSFXVolume);
            ammo = currentWeapon.ammoCapacity;
            reloadIndicator.SetActive(false);
            reloading = false;

            UpdateAmmoUI();
        }
    }

    /** void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        else if (Input.GetKeyDown (KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipWeapon(4);
        }
    }
    */
    public void EquipWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            currentWeaponIndex = index;
            currentWeapon = weapons[currentWeaponIndex];
            spr.sprite = currentWeapon.weaponSprite;
            ammo = currentWeapon.ammoCapacity;

            cameraShakeIntesity = currentWeapon.cameraShakeIntesity;
            cameraShakeDuration = currentWeapon.cameraShakeDuration;
            cameraShakeDecay = currentWeapon.cameraShakeDecay;

            curWeaponDamage = weapons[currentWeaponIndex].damage;

            UpdateFirePoint();
            UpdateAmmoUI();
        }
    }

    void UpdateFirePoint()
    {
        if (firePoint != null)
        {
            firePoint.localPosition = currentWeapon.firePointOffset;
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{ammo} / {currentWeapon.ammoCapacity}";
        }
    }


    void ejectBulletCasing()
    {
        GameObject bulletCasingPrefab = Instantiate(bulletCasing, transform.position, transform.rotation);
        bulletCasingPrefab.GetComponent<SpriteRenderer>().sprite = currentWeapon.bulletCasing;
        Rigidbody2D rb = bulletCasingPrefab.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Vector2.up.x + Random.Range(-0.5f,0.5f), Vector2.up.y) * Random.Range(2,5), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-10, 10), ForceMode2D.Impulse);
    }

    public GameObject enemyImpact;
    public GameObject terrainImpact;
    public void ImpactEffect(Transform tr, int layer)
    {
        if (layer == 8)
        {
            Instantiate(enemyImpact, tr);
        }
        else if (layer == 6)
        {
            Instantiate(terrainImpact, tr);
        }
    }
}
