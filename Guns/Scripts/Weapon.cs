using UnityEngine;

[System.Serializable]
public class Weapon 
{
    public string weaponName;             // Name of the weapon
    public Sprite weaponSprite;
    public GunController.FireMode fireMode; // The fire mode (semi, auto, shotgun)
    public int damage;
    public int ammoCapacity;
    public float reloadTime;
    public float bulletSpeed;             // The Speed of the bullets fired
    public float fireRate;                // Time between shots in seconds
    public float bloomAmount;             // Maximum bullet spread
    public int shotgunPellets = 0;        // Number of pellets for shotgun mode
    public float shotgunSpread = 0f;      // Spread angle for shotgun pellets

    public float detectionRange;

    public float cameraShakeIntesity;
    public float cameraShakeDuration;
    public float cameraShakeDecay;

    public Vector2 firePointOffset;

    public Sprite bulletCasing;
    public Transform bulletCasingEjectionPoint;

    public int weaponPrice;
}
