using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThrow : MonoBehaviour {


	public float duration;
	public Transform target;
	public Bat bat;
	public CameraSettings cameraSet;
	public FieldManager fm;
	public BallManager bm;
	public GameObject batsman, wicket;

	bool changeDir, dropOnPitch = false;


	void Awake(){
		bat = GameObject.FindGameObjectWithTag("Bat").GetComponent<Bat>();
		target = GameObject.FindGameObjectWithTag("BounceTarget").transform;
		cameraSet =GameObject.Find("Cameras").GetComponent<CameraSettings>();
		fm = GameObject.Find("FieldManager").GetComponent<FieldManager>();
		bm = GameObject.Find("BallManager").GetComponent<BallManager>();
		wicket = GameObject.FindGameObjectWithTag("Wicket");
		batsman = GameObject.FindGameObjectWithTag("Batsman");
	}

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().velocity = CalculateThrowSpeed(this.transform.position, target.position, duration);
		Physics.gravity = new Vector3(0, -20, 0);
		//Invoke("DestroyBall", 15f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		if(changeDir)
//			ChangeBallTrajectory();
	}

	public Vector3 CalculateThrowSpeed(Vector3 start, Vector3 target, float duration){

		Vector3 totarget = target - start;
		Vector3 toTargetXZ = totarget;
		toTargetXZ.y = 0;

		float y = totarget.y;
		float xz = toTargetXZ.magnitude;

		float t = duration;

		//s = u*t + (1/2)*a*t*t* : for y plane : a = -g.
		//for xz plane : a = 0; 

		float vy = (y/t) + (0.5f)*(Physics.gravity.magnitude)*t;			
		float vxz = xz/t;

		Vector3 result = toTargetXZ.normalized;
		result *= vxz;
		result.y  = vy;

		return result;
	}

	void OnCollisionEnter(Collision col){
		//print("Collided with : " + col.gameObject.name);
//		if(col.gameObject.tag=="Bat"){
//			print(col.gameObject.GetComponent<Rigidbody>().angularVelocity );
//			BallHitBat(col.gameObject);
//			//bat.InvokeMethods("AfterBallHit", 0.01f);
//		}
		switch (col.gameObject.tag)
		{
		case "Bat" : 
			BallHitBat(col.gameObject);
			break;

		case "Boundary" :
			fm.ResetFielders();
			bm.NewBall();
			break;

		case "Pitch" : 
			if(!dropOnPitch && bm.bowlSpin)
				ChangeBallTrajectory();
			break;
		case "Wicket" :
			fm.ResetFielders();
			bm.NewBall();
			break;
		case "Bowler" : 
			Debug.Log("Ball reached bowler");
			fm.ResetFielders();
			bm.NewBall();
			break;

		default:
			break;
		}
	}

	void BallHitBat(GameObject batCollider)
	{
		bat.AfterBallHit();

		//this.GetComponent<Rigidbody>().velocity = col.transform.up * 45f;// Vector3.SqrMagnitude(col.gameObject.GetComponent<Rigidbody>().angularVelocity *  col.gameObject.GetComponent<Rigidbody>().mass);
		//this.GetComponent<Rigidbody>().AddForce(batCollider.transform.up * 65f, ForceMode.Impulse);
		Vector3 force = (bat.hitForce + batCollider.transform.up)*0.5f;

		this.GetComponent<Rigidbody>().AddForce(force * bat.factor, ForceMode.Impulse);
	}

	public void DestroyBall()
	{
		//cameraSet.ResetCameraOrientation();

		Destroy(this.gameObject);

	}

	void ChangeBallTrajectory()
	{
		Debug.DrawRay(transform.position,(wicket.transform.position-transform.position), Color.red, 5f);
		this.GetComponent<Rigidbody>().velocity = CalculateThrowSpeed(this.transform.position, wicket.transform.position, duration);
		//transform.LookAt(batsman.transform.position-transform.position);
//		Vector3.MoveTowards(transform.position,batsman.transform.position, 10*Time.deltaTime  ); //CalculateThrowSpeed(transform.position, batsman.transform.position, 0.1f).sqrMagnitude
		dropOnPitch = true;

	}

}
