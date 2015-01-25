using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Friendly
{
    [Tooltip("Speed at which the player moves")]
    public float MovementSpeed = 5.0F;
    [Tooltip("Whether or not the player is able to move")]
    public bool Moveable = true;

    protected Animator Animator;
        protected List<FrameState> FrameStates = new List<FrameState>();
        protected FrameState CurrentFrameState = new FrameState();

        private bool isApplicationQuitting = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Animator.SetFloat("Movement Speed", rigidbody2D.velocity.sqrMagnitude);
    }

    void LateUpdate()
    {
        UpdateCurrentFrameState();
    }

    void FixedUpdate()
    {
        Vector3 movement = MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), MovementSpeed);
        RotatePlayerInDirectionOfMovement(movement);
    }

    void OnDestroy()
    {
        if (!isApplicationQuitting)
        {
            GameObject ghosts = GameObject.Find("Ghosts");
            GameObject ghost = Instantiate(Resources.Load("Ghost")) as GameObject;
            ghost.transform.parent = ghosts.transform;
            Ghost ghostComponent = ghost.GetComponent<Ghost>();
            ghostComponent.FrameStates = this.FrameStates;
            GameObject gameManager = GameObject.Find("Game Manager");
            gameManager.AddComponent<LevelLoader0>();
        }
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }

    private void UpdateCurrentFrameState()
    {
        currentFrameState.Position = transform.position;
        currentFrameState.Rotation = transform.rotation.eulerAngles;
        FrameStates.Add(currentFrameState);
    }

    private Vector3 MovePlayer(float horizontalAxis, float verticalAxis, float speed)
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

    private void RotatePlayerInDirectionOfMovement(Vector3 movement)
    {
        if (movement != Vector3.zero && Moveable)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public override void Die()
    {
        Moveable = false;
        Animator.SetTrigger("Die");
        Destroy(gameObject, 3); //TODO softcode this...not 3, animation length
    }
}