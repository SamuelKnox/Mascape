using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    [Tooltip("Rate at which shooter can fire in seconds")]
    public float FireRate = 1.0f;
    [Tooltip("Velocity at which bullets are fired")]
    public float BulletVelocity = 250.0f;
    [Tooltip("Distance from shooter which the bullets spawn")]
    public float SpawnDistance = 1.0f;
    [Tooltip("Direction in which to fire.  If Vector2.zero, then shooter will fire in the direction it is facing")]
    public Vector2 DirectionOfAim = Vector2.zero;
    [Tooltip("Bullet which will be fired from shooter")]
    public Bullet Bullet;

    private float reloadTimeRemaining;

    void Start()
    {
        reloadTimeRemaining = FireRate;
    }

    void Update()
    {
        reloadTimeRemaining -= Time.deltaTime;
    }

    public bool FireWeapon()
    {
        if (reloadTimeRemaining <= 0)
        {
            reloadTimeRemaining = FireRate;
            Bullet bullet = Instantiate(Bullet, transform.position + transform.up * SpawnDistance, Quaternion.identity) as Bullet;
            bullet.Shooter = this;
            if (DirectionOfAim == Vector2.zero)
            {
                bullet.rigidbody2D.AddForce(transform.up * BulletVelocity);
            }
            else
            {
                bullet.rigidbody2D.AddForce(Vector3.Normalize(DirectionOfAim) * BulletVelocity);
            }
            bullet.transform.parent = GameObject.Find("Bullets").transform;
            return true;
        }
        return false;
    }
}
