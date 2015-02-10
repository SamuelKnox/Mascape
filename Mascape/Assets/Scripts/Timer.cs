using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    [Tooltip("Time remaining on game timer")]
    public float TimeRemaining = 120.0f;
    [Tooltip("The starting game time")]
    public float GameTime = 120.0f;
    [Tooltip("Whether or not the timer is running")]
    public bool IsRunning = false;
	
	// Update is called once per frame
	void Update () {
        if (IsRunning)
        {
            TimeRemaining -= Time.deltaTime;
        }
	}

    public void StartTimer()
    {
        IsRunning = true;
    }

    public void PauseTimer()
    {
        IsRunning = false;
    }

    public void ResetTimer()
    {
        TimeRemaining = GameTime;
    }
}
