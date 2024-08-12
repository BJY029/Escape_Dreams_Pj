using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIControllerInB : MonoBehaviour
{
	public GameObject me;

	public GameObject DeadStateUI;

	public Vector3 UIOffset;
	public GameObject interactionUI;
	public GameObject Diary;
	public GameObject goOutUI;

	private AudioSource AudioSource;

	private bool canInteract;
	private bool interacting;

	private void Awake()
	{
		AudioSource = GetComponent<AudioSource>();

		DeadStateUI.SetActive(false);

		Diary.SetActive(false);
		goOutUI.SetActive(false);

		canInteract = false;
		interacting = false;
	}
	public void activeDeadStateUI()
	{
		DeadStateUI.SetActive(true);
	}
	public void nonAcviteDeadStateUI()
	{
		DeadStateUI.SetActive(false); 
	}

	private void Update()
	{
		//종이 UI와 상호작용이 가능할 때
		if(canInteract)
		{
			//상호작용 UI 위치 설정
			interactionUI.transform.position = me.transform.position + UIOffset;
			//E키가 눌리면
			if (Input.GetKeyDown(KeyCode.E))
			{
				//현재 종이 UI가 켜진 상태가 아니면
				if(interacting == false)
				{
					//종이 UI 켜는 코루틴 호출
					StartCoroutine(PaperInRoutine());
				}
				else
				{
					//켜진 상태면 종이 UI 닫는 코루틴 호출
					StartCoroutine(PaperOutRoutine());
				}
			}
		}
	}

	//종이 UI가 켜지는 코루틴
	IEnumerator PaperInRoutine()
	{
		//플래그 설정
		interacting = true;
		//종이 UI 활성화
		Diary.SetActive(true);
		//상호작용 UI 비활성화
		interactionUI.SetActive(false);
		//종이 UI 나가기 활성화
		goOutUI.SetActive(true);
		yield return null;

		//버그 방지를 위한 오디오 음소거
		AudioSource.mute = true;
		Time.timeScale = 0f;
	}

	//종이 UI가 꺼지는 코루틴
	IEnumerator PaperOutRoutine()
	{
		//플래그 설정
		interacting=false;
		//종이 UI 비활성화
		Diary.SetActive(false);
		//상호 작용 UI 활성화
		interactionUI.SetActive(true);
		//나가기 UI 비활성화
		goOutUI.SetActive(false);
		yield return null;

		//음소거 해제
		AudioSource.mute = false;
		Time.timeScale = 1.0f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//해당 범위에 들어왔을 때, 상호작용 UI를 노출
		//종이 UI 상호작용 플래그 설정
		if (collision.CompareTag("diary"))
		{
			interactionUI.SetActive(true);
			canInteract = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//해당 범위에서 벗어났을 때, 상호작용 UI 비노출
		//종이 UI 상호작용 플래그 설정
		if (collision.CompareTag("diary"))
		{
			interactionUI.SetActive(false);
			canInteract = false;
		}
	}
}
