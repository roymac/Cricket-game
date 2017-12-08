using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batsman : MonoBehaviour {
	//This will have running script

	public Vector3 pointB, pointA;
	public Transform to;
	public FieldManager fm;
	public Bat batScript;

	void Start()
	{
		pointB =  to.position;
	//	TimeToRun();

	}

	public void TimeToRun()
	{
		StartCoroutine(StartRunning());
	}

	public void StopRunning()
	{
		print("StopRunning");
		StopCoroutine(StartRunning());
	}


	IEnumerator MoveObject(Transform thisTranform, Vector3 startPos, Vector3 endPos, float time)
	{
		float i = 0f;
		float rate  = 1f/time;
		while(i<1.0f)
		{
			i+=Time.deltaTime * rate;
			thisTranform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}

	}

	IEnumerator StartRunning()
	{
		pointA = transform.position;
		while(!fm.hasReachedBall)
		{
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}
}
