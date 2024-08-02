using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject player;
	public GameObject cat;
	public abnorbalManager abnorbalManager;
	public AudioController audioController;
	public Player playerController;
	public LightController lightController;
	public UiSystem uiSystem;
	public playerAnimationController playerAnimationController;

	private void Awake()
	{
		Instance = this;
	}
}
