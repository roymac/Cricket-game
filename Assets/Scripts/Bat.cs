using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

	public bool hasBallbeenHit;
	public Vector3 hitForce;
	public float factor;
	public Rigidbody ball;
	public Batsman batsman;

	[SerializeField]
	BoxCollider b_collider;

	float startTime;
	Vector3 startPos, swipeForce;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		b_collider = this.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame

	void FixedUpdate()
	{
		if(CameraSettings.showBatCamera)
		{
			DetectSwipe(this.transform, Camera.main.transform);
		}
		else{
			DetectSwipe(Camera.main.transform, this.transform);
		}
	}
	

	public void InvokeMethods(string method, float time)
	{
		Invoke(method, time);
	}

	public void AfterBallHit()
	{
		b_collider.enabled = false;
		hasBallbeenHit = true;
		batsman.TimeToRun();
		//print("Set collision status to : " + b_collider.enabled);	
	}

	public void Nextball()
	{
		if(!b_collider.enabled){
			b_collider.enabled = true;
		}
		hasBallbeenHit = false;
	}


	public void Swing(string animParamater){	//Move this to a player script
		this.GetComponent<Animator>().SetTrigger(animParamater);
	}

	public Vector3 DetectSwipe( Transform transform1, Transform transform2)
	{
		if(Input.GetMouseButtonDown(0))
		{
			startTime = Time.time;
			startPos = Input.mousePosition;
			//print("Start : " + startPos);
			startPos.z = transform1.position.z - transform2.position.z; // - Camera.main.transform.position.z;
			startPos = Camera.main.ScreenToWorldPoint(startPos);
		}
		if(Input.GetMouseButtonUp(0))
		{
			//if((Input.mousePosition - startPos).magnitude > 100f)
			{
				print("Swipe");
				Vector3 endPos = Input.mousePosition;
				//print("end : " + endPos);
				endPos.z =  transform1.position.z - transform2.position.z; // - Camera.main.transform.position.z;
				endPos = Camera.main.ScreenToWorldPoint(endPos);

				hitForce = startPos - endPos;
				//transform.rotation = Quaternion.LookRotation(hitForce);
				hitForce.z = -hitForce.magnitude;
				hitForce /= Time.time - startTime;
				//ball.AddForce(force*factor);
				Debug.DrawRay(transform.position,hitForce, Color.red, 10f);
				Swing("Bat");
			}
		
		}

		return hitForce;
	}

	public Vector3 Swipe(Transform transform1, Transform transform2)
	{
		if(Input.touchCount>0)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase==TouchPhase.Began)
			{
				startTime = Time.time;
				startPos = touch.position;
				startPos.z = transform1.position.z - transform2.position.z;
				startPos = cam.ScreenToWorldPoint(startPos);
			}
			else if(touch.phase==TouchPhase.Moved)
			{
				//lastTouchPoint = touch.position;
			}
			else if(touch.phase==TouchPhase.Ended)
			{
				Vector3 endPos = touch.position;
				endPos.z = transform1.position.z - transform2.position.z;
				endPos = cam.ScreenToWorldPoint(endPos);

				swipeForce = CalculateSwipeForce(startPos, endPos);
				Swing("Bat");

			}
		}

		return swipeForce;
	}

	Vector3 CalculateSwipeForce(Vector3 fp, Vector3 lp)
	{
		Vector3 force = fp - lp;
		//transform.rotation = Quaternion.LookRotation(force);

		force.z = -force.magnitude;
		force /= Time.time - startTime;	//lesser the time, greater the force;

		Debug.DrawRay(transform.position,force, Color.red, force.magnitude);

		return force;
	}
}
