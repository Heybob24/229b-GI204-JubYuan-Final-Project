using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;

    private float timer;

    void Update()
    {
        if (firePoint == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Projectile proj = bullet.GetComponent<Projectile>();

        float dir = transform.localScale.x > 0 ? 1f : -1f;

        proj.direction = dir;
    }
}