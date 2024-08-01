using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject player;
	public abnorbalManager abnorbalManager;
	public AudioController audioController;
	public Player playerController;
	public LightController lightController;
	private void Awake()
	{
		Instance = this;
	}
}
