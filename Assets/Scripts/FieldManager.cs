using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {

	public GameObject[] fielders;
	public GameObject ball;
	public bool hasReachedBall;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBallForFielders(GameObject ball)
	{
		hasReachedBall = false;
		for (int i = 0; i < fielders.Length; i++) {
			fielders[i].GetComponent<Fielder>().ball = ball.transform;
			fielders[i].GetComponent<Fielder>().ResetSpeed();
			//fielders[i].GetComponent<Fielder>().ResetStatus();
		}
	}

	public void ResetFielders()
	{
		for (int i = 0; i < fielders.Length; i++) {
			fielders[i].GetComponent<Fielder>().Reset();
		}
	}
}
