using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	public GameObject ball, bat;
	public Transform bowler;
	public Bat batScript;
	public FieldManager fm;
	public GameObject ballInstance;
	public bool bowlSpin = false;


	// Use this for initialization
	void Start () {
		//InvokeRepeating("NewBall", 3f, 7f);
		Invoke("NewBall", 3f);
		fm = GameObject.Find("FieldManager").GetComponent<FieldManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewBall(){
		batScript.Nextball();
		ballInstance = Instantiate(ball, bowler.position, Quaternion.identity) as GameObject;
		//fm.ball = ballInstance;
		fm.SetBallForFielders(ballInstance);

	}
//
//	public void Swing(string animParamater){	//Move this to a player script
//		bat.GetComponent<Animator>().SetTrigger(animParamater);
//	}
}
