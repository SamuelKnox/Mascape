using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Builder : Player
{
    [Tooltip("Distance multiplier for which the player will build and destroy")]
    public float BuildRange = 1.0f;
    [Tooltip("Distance at which the player can destroy")]
    public float DestroyRange = 2.0f;
    [Tooltip("Speed at which the builder can build in seconds")]
    public float BuildSpeed = 1.0f;
    [Tooltip("Speed at which the builder can destroy in seconds")]
    public float DestroySpeed = 1.0f;

    private float BuildTimeRemaining;
    private float DestroyTimeRemaining;
    private bool IsBuilding = false;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetButton("Build") && !IsBuilding)
        {
            BuildStructure();
            CurrentFrameState.BuildStructure = true;
        }
        if (Input.GetButton("Destroy") && !IsBuilding)
        {
            DestroyStructure();
            CurrentFrameState.DestroyStructure = true;
        }
        UpdateBuildStatus();
    }

    private void BuildStructure()
    {
        IsBuilding = true;
        Moveable = false;
        Animator.SetTrigger("Build");
        GameObject barricadePrefab = Resources.Load("Barricade") as GameObject;
        GameObject barricade = Instantiate(barricadePrefab, transform.position + transform.up * BuildRange, transform.rotation) as GameObject;
        barricade.GetComponent<Structure>().Creator = this;
        BuildTimeRemaining = BuildSpeed;
    }

    private void DestroyStructure()
    {
        Structure[] structures = GameObject.FindObjectsOfType<Structure>();
        foreach (Structure structure in structures)
        {
            if (Vector3.Dot(Vector3.up, transform.InverseTransformPoint(structure.transform.position)) > 0 && Vector3.Distance(transform.position, structure.transform.position) < DestroyRange)
            {
                IsBuilding = true;
                Moveable = false;
                Animator.SetTrigger("Destroy");
                structure.TearDown();
                DestroyTimeRemaining = DestroySpeed;
            }
        }
    }

    private void UpdateBuildStatus()
    {
        if (BuildTimeRemaining > 0)
        {
            BuildTimeRemaining -= Time.deltaTime;
            if (BuildTimeRemaining <= 0)
            {
                IsBuilding = false;
            }
            if (!IsBuilding)
            {
                BuildTimeRemaining = 0;
                Animator.SetTrigger("Stop Building");
                Moveable = true;
            }
        }

        if (DestroyTimeRemaining > 0)
        {
            DestroyTimeRemaining -= Time.deltaTime;
            if (DestroyTimeRemaining <= 0)
            {
                IsBuilding = false;
            }
            if (!IsBuilding)
            {
                BuildTimeRemaining = 0;
                Animator.SetTrigger("Stop Destroying");
                Moveable = true;
            }
        }
    }
}
