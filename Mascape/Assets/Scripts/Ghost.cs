using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Builder))]
[RequireComponent(typeof(Shooter))]
public class Ghost : MonoBehaviour
{
    public List<FrameState> FrameStates { get; set; }

    private Builder builder;
    private Shooter shooter;

    void Start()
    {
        builder = GetComponent<Builder>();
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        if (FrameStates.Count > GameManager.Instance.CurrentFrame)
        {
            FrameState currentFrameState = FrameStates[GameManager.Instance.CurrentFrame];
            transform.position = currentFrameState.Position;
            transform.eulerAngles = currentFrameState.Rotation;
            if (currentFrameState.BuildBarricade)
            {
                builder.BuildStructure("Barricade");
            }
            if (currentFrameState.BuildTurret)
            {
                builder.BuildStructure("Turret");
            }
            if (currentFrameState.DestroyStructure)
            {
                builder.DestroyStructure();
            }
            if (currentFrameState.FireWeapon)
            {
                shooter.FireWeapon();
            }
        }
        else
        {
            ShootRandomly();
        }
    }

    private void ShootRandomly()
    {
        if (shooter.FireWeapon())
        {
            transform.RotateOverTime(new Vector3(0, 0, Random.Range(-360, 360)), shooter.FireRate);
        }
    }
}
