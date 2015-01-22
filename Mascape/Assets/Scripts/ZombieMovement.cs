using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolyNavAgent))]
public class ZombieMovement : MonoBehaviour
{
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

    private Transform target;

    void Start()
    {
        target = GameObject.FindObjectOfType<ActivePlayer>().transform as Transform;
    }

    void Update()
    {
        MoveZombie(target.position);
    }

    private void MoveZombie(Vector2 target)
    {
        Agent.SetDestination(target);
    }
}
