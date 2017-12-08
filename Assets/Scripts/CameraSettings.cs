using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour {

	public Bat batScript;
	public BallManager bm;
	public FieldManager fm;
	public Transform ball;
	public GameObject batCamera, bowlCamera;
	public static bool showBatCamera = false;
	public bool batCam = false;

	Vector3 startPosBat, startPosBowl;
	Quaternion  startRotBat, startRotBowl;
	GameObject cam;

	void Awake(){

		batScript = GameObject.FindGameObjectWithTag("Bat").GetComponent<Bat>();
		bm = GameObject.Find("BallManager").GetComponent<BallManager>();
		fm = GameObject.Find("FieldManager").GetComponent<FieldManager>();

		startRotBat = batCamera.transform.localRotation;
		startPosBat = batCamera.transform.localPosition;

		startRotBowl = bowlCamera.transform.localRotation;
		startPosBowl = bowlCamera.transform.localPosition;
	}

	// Use this for initialization
	void Start () {
		if(batCam)
			showBatCamera = true;
		else
			showBatCamera = false;
		SetCameras();
	}

	
	// Update is called once per frame
	void Update () {
		if(batScript.hasBallbeenHit && bm.ballInstance!=null){
			FollowBall(bm.ballInstance.transform, cam);
		}
	}


	void SetCameras(){
		if(showBatCamera){
			batCamera.SetActive(true);
			bowlCamera.SetActive(false);
			cam = batCamera;
		}else{
			batCamera.SetActive(false);
			bowlCamera.SetActive(true);
			cam = bowlCamera;
		}
	}


	public void FollowBall(Transform target, GameObject camera){
		camera.transform.LookAt(target);
	}

	public void ResetCameraOrientation(){
		batCamera.transform.localRotation = startRotBat;
		batCamera.transform.localPosition = startPosBat;

		bowlCamera.transform.localRotation = startRotBowl;
		bowlCamera.transform.localPosition = startPosBowl;

		//bm.NewBall();
		fm.hasReachedBall = true;
	}
}
