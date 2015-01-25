using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Animator))]
public class Structure : Friendly {
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TearDown()
    {
        animator.speed *= -1;
        animator.SetTrigger("Destroy");
        Destroy(gameObject, Creator.DestroySpeed);
    }
}
