using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ghost : Friendly
{
    public List<FrameState> FrameStates { get; set; }

    void Update()
    {
        if (FrameStates.Count > GameManager.Instance.CurrentFrame)
        {
            FrameState currentFrameState = FrameStates[GameManager.Instance.CurrentFrame];
            transform.position = currentFrameState.Position;
            transform.eulerAngles = currentFrameState.Rotation;
            if (currentFrameState.BuildStructure)
            {
                BuildStructure();
            }
            if (currentFrameState.DestroyStructure)
            {
                DestroyStructure();
            }
        }
    }

    private void BuildStructure()
    {

    }

    private void DestroyStructure()
    {

    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
