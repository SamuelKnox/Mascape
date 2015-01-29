using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolyNavAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mobility))]
[RequireComponent(typeof(Vunerable))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Vunerable target which the enemy is focused on")]
    public Vunerable Target;
    [Tooltip("Range at which the enemy can attack")]
    public float AttackRange = 1.0f;
    [Tooltip("Number of seconds before enemy updates path")]
    public float UpdatePathTime = 1.0f;
    [Tooltip("Whether or not the enemy is stuck")]
    public bool IsStuck = false;

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

    private Vector2 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
        InvokeRepeating("UpdatePath", UpdatePathTime, UpdatePathTime);
        InvokeRepeating("CheckIfStuck", 1.0f, 1.0f);
    }

    void Update()
    {
        if (Target == null || IsPathBlocked())
        {
            FindNewTarget();
        }
        if (Target != null && Vector2.Distance(Target.transform.position, transform.position) <= AttackRange)
        {
            Target.Die();
        }
    }

    private void CheckIfStuck()
    {
        float distanceTraveled = Vector2.Distance(previousPosition, transform.position);
        previousPosition = transform.position;
        IsStuck = distanceTraveled < 0.01f;
    }

    private void UpdatePath()
    {
        if (Target != null && Agent.activePath.Count <= 1)
        {
            Agent.SetDestination(Target.transform.position);
        }
    }

    private void FindNewTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }
        Vunerable target = player.GetComponent<Vunerable>();
        Agent.SetDestination(target.transform.position);
        float targetDistance = Agent.remainingDistance;
        foreach (GameObject ghostGameObject in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            Vunerable ghost = ghostGameObject.GetComponent<Vunerable>();
            Agent.SetDestination(ghost.transform.position);
            float ghostDistance = Agent.remainingDistance;
            if (ghostDistance < targetDistance)
            {
                target = ghost;
                targetDistance = ghostDistance;
            }
        }
        Agent.SetDestination(target.transform.position);
        if (IsPathBlocked())
        {
            targetDistance = Mathf.Infinity;
            foreach (GameObject structureGameObject in GameObject.FindGameObjectsWithTag("Structure"))
            {
                Vunerable structure = structureGameObject.GetComponent<Vunerable>();
                Agent.SetDestination(structure.transform.position);
                float structureDistance = Agent.remainingDistance;
                if (structureDistance < targetDistance)
                {
                    target = structure;
                    targetDistance = structureDistance;
                }
            }
            PolyNavObstacle polyNavObstacle = target.gameObject.GetComponent<PolyNavObstacle>();
            if (polyNavObstacle != null)
            {
                polyNavObstacle.enabled = false;
            }
            Agent.SetDestination(target.transform.position);
        }
        Target = target;
    }

    private bool IsPathBlocked()
    {
        return IsStuck || !Agent.hasPath;
    }
}
