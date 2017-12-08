using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidbody : MonoBehaviour {

	public Rigidbody playerBody;
	public float speed;
	// Use this for initialization
	void Start () {
		playerBody = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
		playerBody.AddTorque(new Vector3(0, 1, 0) * speed * Time.deltaTime);
	}


	public void GetAngularShit()
	{
		//print("This is angular velocity : " + playerBody.angularVelocity);
	}
}
