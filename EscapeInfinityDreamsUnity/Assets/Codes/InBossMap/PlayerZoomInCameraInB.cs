using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoomInCameraInB : MonoBehaviour
{
	public CinemachineVirtualCamera defaultCamera;
	public CinemachineVirtualCamera playerCamera;
	public CinemachineVirtualCamera voidCamera;

	public void SwitchToDefaultCamera()
	{
		playerCamera.Priority = 0;
		defaultCamera.Priority = 1;
	}

	public void SwitchVoidCam()
	{
		voidCamera.Priority = 15;
		playerCamera.Priority = 0;
		defaultCamera.Priority = 0;
	}

	public IEnumerator SwitchZoomInCamera()
	{
		playerCamera.Priority = 1;
		defaultCamera.Priority = 0;

		yield return new WaitForSeconds(1);

		SwitchToDefaultCamera();
	}
}
