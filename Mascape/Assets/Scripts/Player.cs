using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Builder))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Mobility))]
public class Player : MonoBehaviour
{
    public FrameState CurrentFrameState { get; set; }

    protected List<FrameState> FrameStates = new List<FrameState>();

    private Builder builder;
    private Shooter shooter;
    private bool isApplicationQuitting = false;
    private Animator animator;
    private Mobility mobility;

    void Start()
    {
        animator = GetComponent<Animator>();
        builder = GetComponent<Builder>();
        shooter = GetComponent<Shooter>();
        mobility = GetComponent<Mobility>();
    }

    void FixedUpdate()
    {
        Vector3 movement = mobility.MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), mobility.MovementSpeed);
        mobility.RotatePlayerInDirectionOfMovement(movement);
    }

    void Update()
    {
        CurrentFrameState = new FrameState();
        animator.SetFloat("Movement Speed", rigidbody2D.velocity.sqrMagnitude);
        if (Input.GetButton("Build Barricade") && !builder.IsBuilding)
        {
            builder.BuildStructure("Barricade");
            CurrentFrameState.BuildBarricade = true;
        }
        if (Input.GetButton("Build Turret") && !builder.IsBuilding)
        {
            builder.BuildStructure("Turret");
            CurrentFrameState.BuildTurret = true;
        }
        if (Input.GetButton("Destroy") && !builder.IsBuilding)
        {
            builder.DestroyStructure();
            CurrentFrameState.DestroyStructure = true;
        }
        if (Input.GetButton("Shoot") && !builder.IsBuilding)
        {
            shooter.FireWeapon();
            CurrentFrameState.FireWeapon = true;
        }
    }

    void LateUpdate()
    {
        UpdateCurrentFrameState();
    }

    void OnDestroy()
    {
        if (!isApplicationQuitting)
        {
            GameManager.Instance.ResetCurrentFrame();
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
        if (CurrentFrameState == null)
        {
            CurrentFrameState = new FrameState();
        }
        CurrentFrameState.Position = transform.position;
        CurrentFrameState.Rotation = transform.rotation.eulerAngles;
        FrameStates.Add(CurrentFrameState);
    }
}