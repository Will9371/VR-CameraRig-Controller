using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour 
{
	private SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	[SerializeField] GameObject leftController, rightController;
	[SerializeField] RigControl CameraRig;

	void Start()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update()
	{
		device = SteamVR_Controller.Input((int)trackedObj.index);

		if (gameObject == leftController)
		{
			if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
			{
				if(CameraRig.leftGripped == false)
					CameraRig.ResetMove();
				CameraRig.leftGripped = true;
			}
			else
				CameraRig.leftGripped = false;	
		}

		else if(gameObject == rightController)
		{
			if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
			{
				if(CameraRig.rightGripped == false)
					CameraRig.ResetMove();
				CameraRig.rightGripped = true;
			}
			else
				CameraRig.rightGripped = false;	
		}
	}
}
