using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shooter))]
public class Tower : MonoBehaviour
{
    [Tooltip("Enemy which the tower will target")]
    public Enemy Target;

    private Shooter shooter;

    // Use this for initialization
    void Start()
    {
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            FindNewTarget();
        }
        else
        {
            FaceTarget();
            shooter.FireWeapon();
        }
    }

    private void FindNewTarget()
    {
        Enemy[] possibleTargets = FindObjectsOfType<Enemy>();
        Enemy closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Enemy target in possibleTargets)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }
        Target = closestTarget;
    }

    private void FaceTarget()
    {
        if (Target != null)
        {
            shooter.DirectionOfAim = Target.transform.position - transform.position;
        }
    }
}
