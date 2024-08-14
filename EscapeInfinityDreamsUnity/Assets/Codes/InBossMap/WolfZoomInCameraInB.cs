using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WolfZoomInCameraInB : MonoBehaviour
{
	//�⺻ ī�޶�
	public CinemachineVirtualCamera defaultCamera;
	//���� �Ǵ� ī�޶�
	public CinemachineVirtualCamera zoomCamera;

	public CinemachineVirtualCamera playerCamera;


	//���� ī�޶� �켱���� ����
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

	//�� �ƿ� ī�޶� �켱���� ����
	public void SwitchToDefaultCamera()
	{
		playerCamera.Priority = 0;
		zoomCamera.Priority = 0;
		defaultCamera.Priority = 1;
	}
}
