using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed at which the player moves")]
    public float Speed = 5.0F;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 movement = MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Speed);
        RotatePlayerInDirectionOfMovement(movement);
    }

    private Vector3 MovePlayer(float horizontalAxis, float verticalAxis, float speed)
    {
        Vector3 movement = new Vector3(horizontalAxis, verticalAxis, 0);
        movement *= speed;
        characterController.Move(movement * Time.deltaTime);
        return movement;
    }

    private void RotatePlayerInDirectionOfMovement(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}