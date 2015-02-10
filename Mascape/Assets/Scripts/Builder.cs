using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Mobility))]
[RequireComponent(typeof(Animator))]
public class Builder : MonoBehaviour {
    [Tooltip("Multiplier for the distance at which the player will build")]
    public float BuildRange = 1.0f;
    [Tooltip("Distance at which the player can destroy")]
    public float DestroyRange = 2.0f;
    [Tooltip("Speed at which the player can build in seconds")]
    public float BuildSpeed = 1.0f;
    [Tooltip("Speed at which the player can destroy in seconds")]
    public float DestroySpeed = 1.0f;

    public bool IsBuilding { get; set; }

    private float BuildTimeRemaining;
    private float DestroyTimeRemaining;
    private Mobility mobility;
    private Animator animator;

    void Start()
    {
        mobility = GetComponent<Mobility>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateBuildStatus();
    }

    /// <summary>
    /// Builds a structure
    /// </summary>
    public void BuildStructure(string structureType)
    {
        GameObject structurePrefab = Resources.Load(structureType) as GameObject;
        Bounds buildBounds = new Bounds(transform.position + transform.up * BuildRange, structurePrefab.renderer.bounds.size);
        foreach (GameObject structureGameObject in GameObject.FindGameObjectsWithTag("Structure"))
        {
            if (structureGameObject.renderer.bounds.Intersects(buildBounds))
            {
                return;
            }
        }
        IsBuilding = true;
        mobility.Moveable = false;
        animator.SetTrigger("Build");
        GameObject structure = Instantiate(structurePrefab, transform.position + transform.up * BuildRange, transform.rotation) as GameObject;
        structure.GetComponent<Structure>().Creator = this;
        BuildTimeRemaining = BuildSpeed;
    }

    /// <summary>
    /// Destroys a structure
    /// </summary>
    public void DestroyStructure()
    {
        GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
        foreach (GameObject structure in structures)
        {
            if (Vector3.Dot(Vector3.up, transform.InverseTransformPoint(structure.transform.position)) > 0 && Vector3.Distance(transform.position, structure.transform.position) < DestroyRange)
            {
                IsBuilding = true;
                mobility.Moveable = false;
                animator.SetTrigger("Destroy");
                structure.GetComponent<Structure>().DestroyStructure();
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
                animator.SetTrigger("Stop Building");
                mobility.Moveable = true;
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
                animator.SetTrigger("Stop Destroying");
                mobility.Moveable = true;
            }
        }
    }
}
