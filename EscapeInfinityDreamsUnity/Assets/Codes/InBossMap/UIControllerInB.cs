using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerInB : MonoBehaviour
{
	public GameObject DeadStateUI;

	private void Awake()
	{
		DeadStateUI.SetActive(false);
	}
	public void activeDeadStateUI()
	{
		DeadStateUI.SetActive(true);
	}
	public void nonAcviteDeadStateUI()
	{
		DeadStateUI.SetActive(false); 
	}
}
