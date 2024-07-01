using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f; // 3秒後に弾が消える

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
        
    }

    private void FireWeapon()
    {
        // レイキャストする方法等もあるが、ここでは実際に弾丸を作成する
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // bulletRigidbody.velocity = bulletSpawn.forward * bulletVelocity;
        // forward はZ軸方向 (Viewport では青) になるので、normalized で正規化する
        bulletRigidbody.AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        
        // Destroy(bullet, bulletPrefabLifeTime);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
