using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolyNavAgent))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Minimum change in destinations to warrant a pathfinding recalculation")]
    public float PathfindingRefresh = 1.0F;
    [Tooltip("Whether or not the enemy is able to move")]
    public bool Moveable = true;
    [Tooltip("Target that the enemy is chasing and will attack")]
    public Friendly Target;

    private PolyNavAgent agent;
    public PolyNavAgent Agent
    {
        get
        {
            if (!agent)
                agent = GetComponent<PolyNavAgent>();
            return agent;
        }
    }

    private Vector2 oldTargetPosition = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);

    protected virtual void Update()
    {
        if (Target == null || !Target.gameObject.activeSelf)
        {
            FindNewTarget();
        }
        MoveZombie(Target.transform.position);
    }

    protected void FindNewTarget()
    {
        Friendly closestTarget = GameObject.FindObjectOfType<Player>() as Friendly;
        float closestDistance = Mathf.Infinity;
        foreach (Transform ghost in GameObject.Find("Ghosts").transform)
        {
            if (ghost.gameObject.activeSelf && Vector3.Distance(transform.position, ghost.position) < closestDistance)
            {
                closestTarget = ghost.GetComponent<Friendly>();
                closestDistance = Vector3.Distance(transform.position, ghost.transform.position);
            }
        }
        Target = closestTarget;
    }

    private void MoveZombie(Vector2 target)
    {
        if (Moveable)
        {
            rigidbody2D.fixedAngle = false;
            if (Vector2.Distance(oldTargetPosition, target) > PathfindingRefresh)
            {
                Agent.SetDestination(target);
                oldTargetPosition = target;
            }
        }
        else if (!Moveable)
        {
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.fixedAngle = true;
        }
    }
}
