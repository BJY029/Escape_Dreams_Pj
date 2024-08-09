using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInB : MonoBehaviour
{
	public static GameManagerInB instance;
	public GameObject player;
	

	public PlayerControllerInB playerController;
	public WarewolfControllerInB warewolfController;
	public WolfZoomInCameraInB wolfZoomInCameraInB;
	public UIControllerInB UIControllerInB;
	public LightControllerInB lightControllerInB;
	public PlayerStateControllerInB playerStateControllerInB;

	private void Awake()
	{
		instance = this;
	}
}
