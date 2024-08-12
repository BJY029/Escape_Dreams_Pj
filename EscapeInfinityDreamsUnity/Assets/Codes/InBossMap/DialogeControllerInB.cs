using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeControllerInB : MonoBehaviour
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

		StartTyping("드디어 꿈에서 탈출한건가??!!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("루이는......");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("일단 여기서 나가야겠어.");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//텍스트를 초기화 하고
		dialogueText.text = "";
		//비활성화 한다.
		canvas.SetActive(false);
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
