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
	public SceneControllerInB sceneControllerInB;
	public AudioControllerInB audioControllerInB;
	public DialogeControllerInB dialogeControllerInB;
	public PlayerZoomInCameraInB playerZoomInCameraInB;
	private void Awake()
	{
		instance = this;
	}
}
