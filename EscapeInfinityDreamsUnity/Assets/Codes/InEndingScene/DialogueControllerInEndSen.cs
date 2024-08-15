using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControllerInEndSen : MonoBehaviour
{
	public GameObject canvas;
	public Text dialogueText;
	public float typingSpeed = 0.05f;
	public float activeTime;
	public bool istyping;

	private void Awake()
	{
		istyping = false;
		canvas.SetActive(false);
		dialogueText.text = "";
	}

	public IEnumerator StartDialogeInScene()
	{
		//일정 시간동안 기다린 후
		yield return new WaitForSeconds(activeTime);

		//대화창을 활성화한다.
		canvas.SetActive(true);

		StartTyping("깊은 숲을 뛰어 어느덧 한 마을에 도착한 청년");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("청년은 다음 날이 되자마자 경찰과 함께 다시 그 병원을 찾았다.");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("하지만 어째서인지 그 병원은 온데간데 사라지고, 허름한 건물만 모양새를 갖추고 있을 뿐이었다.");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("그렇게 허탈하고 다시 돌아가려던 찰나, 청년의 발 밑에 밟히는 한 쪽지...");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		dialogueText.fontSize = 90;
		dialogueText.color = Color.red;
		StartTyping("\"내가 널 포기할 것 같아?\"");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//해당 설정은 후에 메인 화면 씬으로 돌아가는 것으로 교체
		Application.Quit();
		Debug.Log("Game is exiting");
	}

	public void StartTyping(string message)
	{
		//코루틴 호출
		StartCoroutine(TypeSentence(message));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		istyping = true; //해당 코루틴이 실행되는 동안 통제를 위해 플래그 설정

		//초기화
		dialogueText.text = "";
		//한글자씩 typingSpeed의 속도록 출력되도록 설정
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
		//플래그 초기화
		istyping = false;
	}
}
