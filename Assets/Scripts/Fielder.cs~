using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fielder : MonoBehaviour {
	public Material ballReachMat, normalMat;
	public Bat bat;
	public BallManager bm;
	public TestThrow throwScript;
	public FieldManager fm;
	public Batsman batsman;
	public CameraSettings cameraSet;

	public float speed, maxSpeed;
	public Transform ball, bowler;
	public float distance;
	public Vector3 startpos;

	[SerializeField]
	float startY, startSpeed;

	bool ballInRange = false, throwToBowler = false;

	// Use this for initialization
	void Start () {

		startSpeed = speed;
		startY = transform.position.y;
		startpos = transform.position;
		bowler = GameObject.FindGameObjectWithTag("Bowler").transform;
		bat = GameObject.FindGameObjectWithTag("Bat").GetComponent<Bat>();
		bm = GameObject.Find("BallManager").GetComponent<BallManager>();
		fm = GameObject.Find("FieldManager").GetComponent<FieldManager>();
		cameraSet = GameObject.Find("Cameras").GetComponent<CameraSettings>();

		//ball = bm.ballInstance.transform;
	}

	void FixedUpdate()
	{
		if(ball!=null && bat.hasBallbeenHit && !fm.hasReachedBall && this.gameObject.tag!="Bowler")
		{
			FollowBall();
			CheckDistanceFromBall();
		}

		if(fm.hasReachedBall)
		{
			ResetField();
		}

	}

	public void FollowBall(){
		transform.position = Vector3.MoveTowards(new Vector3(this.transform.position.x, startY, this.transform.position.z), new Vector3(ball.position.x, startY, ball.position.z) , speed * Time.deltaTime);

//		Vector3 temp = transform.position;
//		temp.y = startY;
//		transform.position = temp;
	}

	void OnTriggerEnter(Collider other){
		ballInRange = true;
	}

	void OnTriggerStay(Collider other) {
		if (ball != null && bat.hasBallbeenHit && ballInRange && other.gameObject.tag == "Ball") {
//			transform.position = Vector3.MoveTowards (transform.position, new Vector3(other.transform.position.x, startY, other.transform.position.z), 10f * Time.deltaTime);
		//	print("in range");
			if(speed<maxSpeed)
				speed += 7f * Time.deltaTime;
			//SetFielderSpeed();
		}
	}

	void OnTriggerExit(Collider other)
	{
		ballInRange = false;
		if(other.gameObject.tag == "Ball")
		{
			speed = startSpeed;
		}
	}

	public void CheckDistanceFromBall(){
		distance = Vector3.Distance(transform.position, ball.position);


		if(distance>60f){
			speed = 2f;
		}
		else if(distance>50f && distance<60f)
		{
			speed += 5f*Time.deltaTime;
		}
//		else if(distance>30f && distance<50f)
//		{
//			speed += 5f*Time.deltaTime;
//		}
//		else if(distance>20f && distance<45f)
//		{
//			speed = 7f;
//		}
		else if(distance < 5f) {
			ReachedBall();
			speed = 0f;
			//Reset();
			if(!throwToBowler){
				ThrowToBowler();
				throwToBowler = true;
			}
		}

	}


	void SetFielderSpeed()
	{
		var distRatio = (distance-5f)/40f;		//dRatio = (dist-minDist)/(maxDist-minDist)
		var diffSpeed  = 15f;

		speed = (distRatio*diffSpeed) + 5f;
	}


	/*

	public void CheckDistanceFromBall(){
		distance = Vector3.Distance(transform.position, ball.position);

		if(distance>maxDistance){
			speed += 0.5f*Time.deltaTime; //CalculateSpeed(maxSpeed, minSpeed, 150f, maxDistance);
		}
		if(distance < 4f) {
			speed = minSpeed;
			ReachedBall();
			Reset();
		
		}
		else if(distance>20 && distance<maxDistance)
		{
			speed = CalculateSpeed(maxSpeed, minSpeed, maxDistance, 20f);
		}
		else if (distance>10f && distance<20f)
		{
			speed = CalculateSpeed(maxSpeed, minSpeed, 20f, 10f);
		}

	}

	float CalculateSpeed(float maxSpeed, float minSpeed, float maxDist, float minDist)
	{
		var distanceRatio = (distance-minDist)/(maxDist-minDist);	
		var diffSpeed = maxSpeed - minSpeed;

		speed = (distanceRatio*diffSpeed) + (minSpeed);

		return speed;
	}

	*/

	public void Reset()
	{
		//ReachedBall();
		cameraSet.ResetCameraOrientation();
		//ResetField ();
		throwToBowler = false;
		ball.GetComponent<TestThrow>().DestroyBall();
	}

	public void ReachedBall(){
		this.GetComponent<Renderer>().material = ballReachMat;
		batsman.StopRunning();
	}

	public void ResetField()
	{
		transform.position = Vector3.MoveTowards(transform.position, startpos, 15f * Time.deltaTime);
		Invoke("ResetStatus", 1f);
		//speed = startSpeed;

	}

	public void ResetSpeed(){
		speed = startSpeed;
	}

	public void ResetStatus()
	{
		this.GetComponent<Renderer>().material = normalMat;
	}

	void ThrowToBowler()
	{
		print("Throw to bowler");
		//speed = 0;
		fm.hasReachedBall = true;
		//Reset();
		if(ball!=null && this.gameObject.tag!="Bowler")
		{
			ball.GetComponent<Rigidbody>().velocity = ball.GetComponent<TestThrow>().CalculateThrowSpeed(this.transform.position, bowler.position, 2f);
		}
	}


}
