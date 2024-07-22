using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiSystem : MonoBehaviour
{
	[Header("# triggerTagObj")]
	public GameObject Paper;	//종이 UI
	public GameObject VandingMachine; //자판기 UI
	public GameObject Extensonmeter; //신장계 UI

	[Header("# UI Set")]
	public GameObject interactionUI; // 배경을 포함한 UI 오브젝트(text_background)
	public GameObject interactionUIforOut; //종이 UI에서 생성할 UI 오브젝트
	public Vector3 uiOffset; // UI 오브젝트의 출력 위치 조정
	public float displayDuration = 2.0f; //자판기, 신장계에서 UI를 출력할 시간


	private bool isInteracted;	//상호작용 UI를 띄우기 위한 Bool형 변수
	private bool isUiActived;  //종이 UI가 현재 띄어져있는지 구분하기 위한 Bool형 변수
	private int flag;	//각 UI를 구분하기 위한 플래그

	public LightController lightController; //FadeOut 효과를 적용하기 위해 해당 스크립트를 불러온다.
	public TextMeshProUGUI sleepText; //잠에 들 때, 상호작용 UI의 텍스트 내용을 변경하기 위해 불러온다.


	//초기화
	private void Awake()
	{
		if (interactionUI != null) 
		{ 
			interactionUI.SetActive(false); 
		}
		if(interactionUIforOut != null)
		{
			interactionUIforOut.SetActive(false);
		}

		Paper.SetActive(false);
		VandingMachine.SetActive(false);
		Extensonmeter.SetActive(false);


		isInteracted = false;
		isUiActived = false;
		flag = 0;
	}



	private void Update()
	{
		if (!isInteracted) return;	//UI와 상호작용 가능할 때만 실행
		
		//상호작용 UI의 위치 설정
		interactionUI.transform.position = transform.position + uiOffset;
		
		//상호작용 키가 눌렸을 때
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			switch (flag)
			{
				case 0:
					break;
				//종이 UI와 상호작용 한 경우
				case 1:
					if(isUiActived ==  false)	//만약 종이 UI가 활성화 되지 않은 상태면
					{	//종이 UI를 활성화 시키는 코루틴 호출
						StartCoroutine(PaperInRoutine());
					}
					else //종이 UI가 활성화된 상태면
					{
						//종이 UI를 비활성화 시키는 코루틴 호출
						StartCoroutine(PaperOutRoutine());
					}
					break;
				//자판기 UI와 상호작용 한 경우
				case 2:
					//자판기 UI를 활성화 시키는 코루틴 호출
					StartCoroutine(VandingRoutine());
					break;
				//신장계 UI와 상호작용 한 경우
				case 3:
					//신장계 UI를 활성화 시키는 코루틴 호출
					StartCoroutine(extRoutine());
					break;
				case 4:
					//잠에 들 때 씬 전환과 각종 효과들을 위한 코루틴 호출
					StartCoroutine(bedRoutine());
					break;
				default:
					break;
			}
		}
	}

	//종이 UI를 활성화 시키는 코루틴
	IEnumerator PaperInRoutine()
	{
		isUiActived = true; //종이 UI가 활성화 되었음을 선언
		Paper.SetActive(true);	//종이 UI 활성화
		interactionUI.SetActive(false); //상호작용 UI는 비활성화
		interactionUIforOut.SetActive(true); //종이 UI에서 나가는 UI를 활성화
		VandingMachine.SetActive(false ); //자판기에서 바로 종이 UI와 상호작용 한 경우, 자판기 UI 비활성화
		yield return new WaitForEndOfFrame();	//한 프레임 대기

		Time.timeScale = 0; //활성화 된 동안 게임 시간 정지
	}

	//종이 UI를 비활성화 시키는 코루틴
	IEnumerator PaperOutRoutine()
	{
		isUiActived = false; //종이 UI가 비활성화 되었음을 선언
		Paper.SetActive(false);	//종이 UI 비활성화
		interactionUI.SetActive(true);	//상호작용 UI 활성화
		interactionUIforOut.SetActive(false);	//종이 UI에서 나가기 위한 UI 비활성화
		yield return new WaitForEndOfFrame();	//한프레임 대기
		
		Time.timeScale = 1.0f; //시간 다시 흐르도록 설정
	}

	//자판기 UI를 일정시간 동안 활성화 시키는 코루틴
	IEnumerator VandingRoutine()
	{
		VandingMachine.SetActive(true); //해당 UI를 활성화
		yield return new WaitForSeconds(displayDuration); //설정한 시간만큼 대기
		VandingMachine.SetActive(false ); //비활성화
	}

	//신장계 UI를 일정시간 동아 활성화 시키는 코루틴
	IEnumerator extRoutine()
	{
		Extensonmeter.SetActive(true); //해당 UI를 활성화
		yield return new WaitForSeconds(displayDuration); //설정한 시간만큼 대기
		Extensonmeter.SetActive(false); //비활성화
	}

	//잠에 드는 코루틴 호출
	IEnumerator bedRoutine()
	{
		//해당 상호작용 텍스트를 다음과 같이 변경
		sleepText.text = "잠에 드는 중..";

		//LightController의 FadeOutLight() 코루틴 호출
		yield return lightController.FadeOutLight();

		//다음 씬 로드
		SceneManager.LoadScene("testScene");
	}


	//특정 콜라이더와 충돌한 경우
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Paper")) //충돌한 콜라이더의 태그가 종이인 경우
		{
			//함수 호출(flag ; 1로 설정)
			ShowUIInteraction(1);
		}
		else if (collision.CompareTag("vandingMachine")) //충돌한 콜라이더의 태그가 자판기
		{
			//함수 호출(flag : 2로 설정)
			ShowUIInteraction(2);
		}
		else if (collision.CompareTag("extensometer")) //충돌한 콜라이더의 태그가 신장계
		{
			//함수 호출(flag : 3로 설정)
			ShowUIInteraction(3);
		}
		else if (collision.CompareTag("bed")) //충돌한 콜라이더의 태그가 침대
		{
			//함수 호출(flag : 4로 설정)
			ShowUIInteraction(4);
		}
	}

	//해당 UI와 관련된 것들을 바꾸는 함수
	private void ShowUIInteraction(int interactionFlag)
	{
		//상호작용 UI 활성화
		interactionUI.SetActive(true);

		//상호작용 가능한 상태로 선언
		isInteracted = true;

		//플래그 설정
		flag = interactionFlag;
	}

	//콜라이더와 충돌 벗어난 경우
	private void OnTriggerExit2D(Collider2D collision)
	{
		//어떤 콜라이더를 벗어나든 상관없이
		if (collision.CompareTag("Paper") || collision.CompareTag("vandingMachine") || collision.CompareTag("extensometer") || collision.CompareTag("bed"))
		{
			//오류 방지 조건문
			if (interactionUI != null)
			{
				//상호작용 Ui 비활성화
				interactionUI.SetActive(false);
				//상호작용 불가능한 상태로 선언
				isInteracted = false;
				//플래그 초기화
				flag = 0;
			}
		}
	}
}
