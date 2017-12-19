# VR-CameraRig-Controller
Drag, scale, and rotate a VR camera rig using the grip buttons on the handheld controllers.  Built with SteamVR with the Vive in mind.

How to use:
1) Create a Unity project, import SteamVR, and add the CameraRig and SteamVR prefabs into your scene.
2) Attach the ControllerInput script to the Left and Right controller objects (children of Steam's CameraRig).  
Assign the CameraRig and the Left and Right controller objects in the inspector window.
3) Create an empty game object (call it "RotationRod" or whatever you like)
4) Attach the RigControl script to the CameraRig object.  Assign the left and right controllers to the "LeftHandLoc" and "RightHandLoc"
fields.  Assign the empty "RotataionRod" game object to RotationRod.  Type in desired fields for MinScale, MaxScale 
(determines how much you can change your scale), and ScaleMultiplier (Determines how fast scale changes with hand motion.  
I recommend 50 or thereabouts).  Check box marked "Use Y Offset on Scale" to offset the Y position when scaling so that the center 
of the CameraRig stays in the same place (useful for space settings); leave the box unchecked for the bottom of the camera rig
to stay in the same place when scaling (useful when player is on the ground).

Created by Will Petillo
