using UnityEngine;
using System.Collections;

public class Mobility : MonoBehaviour
{
    [Tooltip("Speed at which the player moves")]
    public float MovementSpeed = 5.0F;
    [Tooltip("Whether or not the player is able to move")]
    public bool Moveable = true;

    void Start()
    {
        PolyNavAgent polyNavAgent = GetComponent<PolyNavAgent>();
        if (polyNavAgent != null)
        {
            polyNavAgent.maxSpeed = MovementSpeed;
        }
    }

    /// <summary>
    /// Moves gameObject based on the parameters of horizontal axis, vertical axis, and speed
    /// </summary>
    public Vector3 MovePlayer(float horizontalAxis, float verticalAxis, float speed)
    {
        Vector3 movement = Vector3.zero;
        if (Moveable)
        {
            movement = new Vector3(horizontalAxis, verticalAxis, 0);
            movement *= speed;
        }
        rigidbody2D.velocity = movement;
        return movement;
    }

    /// <summary>
    /// Rotates gameObject based on the passed in movement
    /// </summary>
    public void RotatePlayerInDirectionOfMovement(Vector3 movement)
    {
        if (rigidbody2D.velocity.sqrMagnitude > 0.01f)
        {
            rigidbody2D.fixedAngle = false;
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            rigidbody2D.fixedAngle = true;
        }
    }
}
