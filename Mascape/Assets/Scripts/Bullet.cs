using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Tooltip("The shooter who shot the buller")]
    public Shooter Shooter;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shooter != null && collider.gameObject == Shooter.gameObject)
        {
            return;
        }
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(gameObject);
            enemy.GetComponent<Vunerable>().Die();
            return;
        }
        Structure structure = collider.GetComponent<Structure>();
        if (structure != null)
        {
            Destroy(gameObject);
            return;
        }
        Wall wall = collider.GetComponent<Wall>();
        if (wall != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
