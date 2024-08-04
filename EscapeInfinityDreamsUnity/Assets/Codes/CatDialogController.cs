using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatDialogController : MonoBehaviour
{
	public GameObject canvas;
	public Text dialogueText;
	public float typingSpeed = 0.05f;
	public float activeTime;
	public bool istyping;

	//초기화
	private void Awake()
	{
		istyping = false;
		canvas.SetActive(false);
		dialogueText.text = "";
	}

	//대화 창에 글이 입력되는 코루틴
	public IEnumerator dialogController()
	{
		//일정 시간동안 기다린 후
		yield return new WaitForSeconds(activeTime);

		//대화창을 활성화한다.
		canvas.SetActive(true);

		//만약 게임 레벨이 현재 0이면
		if(GameManager.level == 0)
		{
			//해당 텍스트를 StartTyping 함수를 통해 출력
			StartTyping("0 번째 꿈이다 냥~\n해당 꿈에는 이상현상이 없다 냥~");
		}
		else //아니면
		{
			//해당 텍스트를 StartTyping 함수를 통해 출력
			StartTyping(GameManager.level + " 번째 꿈이다 냥~");
		}

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));
		//텍스트를 초기화 하고
		dialogueText.text = "";
		//비활성화 한다.
		canvas.SetActive(false);
	}

	//타이핑을 하기 위한 함수
	public void StartTyping(string message)
	{
		//코루틴 호출
		StartCoroutine(TypeSentence(message));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		istyping  = true; //해당 코루틴이 실행되는 동안 통제를 위해 플래그 설정

		//초기화
		dialogueText.text = "";
		//한글자씩 typingSpeed의 속도록 출력되도록 설정
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
		//플래그 초기화
		istyping = false ;
	}
}
