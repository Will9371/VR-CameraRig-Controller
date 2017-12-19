# VR-CameraRig-Controller
Drag, scale, and rotate a VR camera rig using the grip buttons on the handheld controllers.  Includes SteamVR.

Notes on use:

1) "RotationRod" is an empty game object used as a point of reference for rotating the camera.

2) A "Controller Input" script has been attached to both hand controllers.  Expand this script to add other input functionality, or integrate the method calls to RigController into your input-handling script.

3) The "RigControl" script, attached to the CameraRig, has several adjustable fields.  MinScale and MaxScale determine how much you can change your scale.  ScaleMultiplier determines how fast scale changes with hand motion (I recommend 50 or thereabouts).  

4) Check the "Use Y Offset on Scale" box to offset the Y position when scaling so that the center 
of the CameraRig stays in the same place (useful for space settings); leave the box unchecked for the bottom of the camera rig to stay in the same place when scaling (useful when player is on the ground).

Created by Will Petillo
