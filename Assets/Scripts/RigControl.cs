using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigControl : MonoBehaviour 
{
	public bool leftGripped, rightGripped;
	[SerializeField] Transform leftHandLoc, rightHandLoc;
	[SerializeField] float maxScale, minScale;
	[SerializeField] float scaleMultiplier;				//Recommended setting: 50
	[SerializeField] bool useYOffsetOnScale;			//Set false if on ground, set true if in space

	Vector3 moveCur, movePrev, moveDelta;			  	//track single hand movements
	float distCur, distPrev, distDelta, scaleChange;	//track both hand movements
	float scaleCur, scalePrev, scaleDelta;				//used to offset y axis when scaling to maintain perceived position
	float angleYCur, angleYPrev, angleYChange;			//used to rotate CameraRig
	float angleYChangePrev, angleZChangePrev;

	void Update () 
	{
		if (leftGripped && rightGripped)
		{
			ScaleRig();
			RotateRig();
		}
		else if (leftGripped || rightGripped)
			MoveRig();
	}

	void ScaleRig()
	{
		distCur = (leftHandLoc.position - rightHandLoc.position).magnitude;

		if(distPrev == 0f)
			distPrev = distCur;
		else 
		{
			distDelta = (distPrev - distCur) / transform.localScale.x;
			scaleChange = 1 + (distDelta * scaleMultiplier * Time.deltaTime);

			if(scaleChange > 1.1 || scaleChange < 0.9)
				return;
			else if((scaleChange > 1 && transform.localScale.x < maxScale) || (scaleChange < 1 && transform.localScale.x > minScale))
			{
				scalePrev = transform.localScale.x;

				transform.localScale *= scaleChange;
				distPrev = distCur;

				scaleCur = transform.localScale.x;
				scaleDelta = scalePrev - scaleCur;

				if (useYOffsetOnScale)		//offsets Y position when scaling to keep center of CameraRig in same place
					transform.Translate(0, scaleDelta, 0);				
			}
		}
	}

	void MoveRig()
	{
		if (leftGripped)
			moveCur = leftHandLoc.position;	//use absolute position to account for rotation effects
		else if (rightGripped)
			moveCur = rightHandLoc.position;

		if (movePrev == Vector3.zero)		//initialize on starting frame
			movePrev = moveCur;
		else
		{
			moveDelta = movePrev - moveCur;
			transform.position += moveDelta; 

			if (leftGripped)				//resetting moveCur after moving accounts for scaling effects...somehow
				moveCur = leftHandLoc.position;	
			else if (rightGripped)
				moveCur = rightHandLoc.position;

			movePrev = moveCur;
		}
	}

	public void ResetMove()		//Called by ControllerInput class.  
	{							//Prevents camera from jumping on grip press.
		movePrev = Vector3.zero;
		distPrev = 0f;
		angleYPrev = 0f;
	}

	void RotateRig()
	{
		angleYCur = Span2Nodes(rotationRod, leftHandLoc, rightHandLoc);

		if(angleYPrev == 0f)
			angleYPrev = angleYCur;
		else 
		{
			angleYChange = angleYPrev - angleYCur;

			if(angleYChangePrev == 0f)	//patch fix: remove jitter by checking ignoring cases where angle change flips sign
				angleYChangePrev = angleYChange;
			else
				if(angleYChange * angleYChangePrev < 0)
				{
					angleYChange = 0;
					angleYChangePrev = 0;
				}

			transform.eulerAngles += new Vector3(0, angleYChange, 0);
			angleYPrev = angleYCur;
		}
	}


	Vector3 pos1, pos2;
	[SerializeField] Transform rotationRod;

	float Span2Nodes(Transform rod, Transform t1, Transform t2)	//this function can be expanded to stretch an object between two points in real time
	{
		pos1 = t1.position;
		pos2 = t2.position;

		midpoint = FindMidpoint(pos1, pos2);
		rod.position = midpoint;

		rod.LookAt(t2, Vector3.up);	
		rod.Rotate(0f, -90f, 0f);

		return rod.rotation.eulerAngles.y;
	}

	Vector3 midpoint;
	public Vector3 FindMidpoint(Vector3 t1, Vector3 t2)	//this function returns the midpoint between any two points
	{
		midpoint = new Vector3((float)((t1.x - t2.x)/2 + t2.x), (float)((t1.y - t2.y)/2 + t2.y), (float)((t1.z - t2.z)/2 + t2.z));
		return midpoint;
	}
}
