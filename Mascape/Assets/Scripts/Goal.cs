using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("YOU WIN!");
        }
    }
}