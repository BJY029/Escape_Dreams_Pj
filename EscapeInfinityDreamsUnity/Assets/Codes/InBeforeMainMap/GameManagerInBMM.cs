using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInBMM : MonoBehaviour
{
    public static GameManagerInBMM Instance;
	public LightControllerInBMM LightController;
	public DialogueControllerInBMM DialogueController;


	private void Awake()
	{
		Instance = this;
	}
}
