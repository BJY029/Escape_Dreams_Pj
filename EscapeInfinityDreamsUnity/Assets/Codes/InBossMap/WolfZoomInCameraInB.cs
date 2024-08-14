using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WolfZoomInCameraInB : MonoBehaviour
{
	//기본 카메라
	public CinemachineVirtualCamera defaultCamera;
	//줌인 되는 카메라
	public CinemachineVirtualCamera zoomCamera;

	public CinemachineVirtualCamera playerCamera;


	//줌인 카메라 우선순위 증가
	public void SwitchToZoomCamera()
	{
		playerCamera.Priority = 0;
		zoomCamera.Priority = 1;
		defaultCamera.Priority = 0;
	}

	public void SwitchToZoomCameraToPlayer()
	{
		playerCamera.Priority = 1;
		zoomCamera.Priority = 0;
		defaultCamera.Priority = 0;
	}

	//줌 아웃 카메라 우선순위 증가
	public void SwitchToDefaultCamera()
	{
		playerCamera.Priority = 0;
		zoomCamera.Priority = 0;
		defaultCamera.Priority = 1;
	}
}
