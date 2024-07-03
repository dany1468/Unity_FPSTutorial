using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Shooting
    public bool isShooting;
    public bool readyToShoot;
    private bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletPerBurst = 3;
    public int burstBulletsLeft;

    // Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f; // 3秒後に弾が消える

    public GameObject muzzleEffect;
    public Animator animator;
    
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletPerBurst;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            // Hold down the mouse button to shoot
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode is ShootingMode.Single or ShootingMode.Burst)
        {
            // Press the mouse button to shoot
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");
        
        SoundManager.Instance.shootingSound1911.Play();
        
        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        
        // レイキャストする方法等もあるが、ここでは実際に弾丸を作成する
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.transform.forward = shootingDirection;

        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        
        bulletRigidbody.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke(nameof(ResetShot), shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke(nameof(FireWeapon) , 0.1f);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        var ray = Camera.main!.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out var hit))
        {
            // hitting something
            targetPoint = hit.point;
        }
        else
        {
            // shooting at the air
            targetPoint = ray.GetPoint(100);
        }
        
        Vector3 direction = targetPoint - bulletSpawn.position;
        
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        
        // Returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}