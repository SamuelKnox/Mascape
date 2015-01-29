using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Vunerable : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Die()
    {
        if (GetComponent<Player>() != null)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 3); //TODO softcode this...not 3, animation length
        }
        else if (GetComponent<Ghost>() != null)
        {
            gameObject.SetActive(false);
            GameObject zombie = Instantiate(Resources.Load("Zombie"), transform.position, Quaternion.identity) as GameObject;
            zombie.transform.parent = GameObject.Find("Enemies").transform;
        }
        else if (GetComponent<Structure>() != null)
        {
            GetComponent<Structure>().DestroyStructure();
        }
        else if (GetComponent<Enemy>() != null)
        {
            Destroy(gameObject);
        }
    }
}
