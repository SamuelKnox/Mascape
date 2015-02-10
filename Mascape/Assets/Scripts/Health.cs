using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [Tooltip("Current Hit Points")]
    public float CurrentHP = 2.0f;
    [Tooltip("Maximum Hit Points")]
    public float MaxHP = 2.0f;

    public bool IsAlive { get { return CurrentHP > 0; } }

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool DealDamage(float damage)
    {
        if (CurrentHP > 0)
        {
            CurrentHP -= damage;
            if (!IsAlive)
            {
                Die();
            }
            return true;
        }
        return false;
    }

    public bool Heal(int heal)
    {
        if (CurrentHP < MaxHP)
        {
            CurrentHP += heal;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            return true;
        }
        return false;
    }

    public void Die()
    {
        if (GetComponent<Player>() != null)
        {
            Timer timer = GameObject.FindGameObjectWithTag("Timer").gameObject.GetComponent<Timer>();
            timer.PauseTimer();
            animator.SetTrigger("Die");
            gameObject.GetComponent<Mobility>().Moveable = false;
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
