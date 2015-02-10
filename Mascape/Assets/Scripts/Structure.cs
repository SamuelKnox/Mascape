using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Animator))]
public class Structure : MonoBehaviour
{
    public Builder Creator { get; set; }

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        transform.parent = GameObject.Find("Structures").transform;
        animator = GetComponent<Animator>();
        animator.speed = Creator.BuildSpeed / Creator.BuildSpeed * .3f; //TODO softcode this  should .3 be the clip speed? i dunno
        Invoke("EnableObstacleState", Creator.BuildSpeed);
    }

    private void EnableObstacleState()
    {
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    /// <summary>
    /// Destroys the structure
    /// </summary>
    public void DestroyStructure()
    {
        animator.SetTrigger("Destroy");
        Destroy(gameObject, 3.0f); //TODO softcode this to animation length
    }
}
