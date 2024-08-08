using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WolfZoomInCameraInB : MonoBehaviour
{
	public CinemachineVirtualCamera defaultCamera;
	public CinemachineVirtualCamera zoomCamera;

	public void SwitchToZoomCamera()
	{
		zoomCamera.Priority = 1;
		defaultCamera.Priority = 0;
	}

	public void SwitchToDefaultCamera()
	{
		zoomCamera.Priority = 0;
		defaultCamera.Priority = 1;
	}
}
