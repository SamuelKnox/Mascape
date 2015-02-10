using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class TimerUIUpdater : MonoBehaviour {
    private Timer timer;
    private UILabel uiLabel;

	// Use this for initialization
	void Start () {
        timer = GameObject.FindGameObjectWithTag("Timer").gameObject.GetComponent<Timer>();
        uiLabel = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        uiLabel.text = "" + (int) timer.TimeRemaining;
	}
}
