using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerInEndSen : MonoBehaviour
{
    public DialogueControllerInEndSen DEC;

	private void Start()
	{
		DEC.gameObject.SetActive(true);
		StartCoroutine(DEC.StartDialogeInScene());
	}
}
