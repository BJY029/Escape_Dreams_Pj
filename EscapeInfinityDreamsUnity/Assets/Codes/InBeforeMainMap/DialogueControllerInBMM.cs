using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControllerInBMM : MonoBehaviour
{
	public GameObject canvas;
	public Text dialogueText;

	public Image doctorIcon;
	public Image playerIcon;

	public float typingSpeed = 0.05f;
	public float activeTime;
	public bool istyping;

	public AudioSource audioSource;

	private void Awake()
	{
		istyping = false;
		canvas.SetActive(false);
		dialogueText.text = "";
	}

	public IEnumerator startSceneDialog()
	{
		//일정 시간동안 기다린 후
		yield return new WaitForSeconds(activeTime);

		//대화창을 활성화한다.
		canvas.SetActive(true);

		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("이거 진짜 안전한거 맞죠..?");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("고럼고럼~ 이 주사 한대 맞고 푸~~욱 자고 일어나면 돼!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("급여도 고지해주신 대로 주시는거 맞죠?");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("당연하지~ 밀린 네 월세는 앞으론 걱정하지 않아도 될거야~");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("....");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("제가 월세가 밀린건 어떻게 아시는거죠...?");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("......");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartCoroutine(GameManagerInBMM.Instance.LightController.HalfFadeOut());

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("저...기요..? 선생....님??");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("다시 깨어나고 싶으면 거기서 해야할 일을 해.");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(false);

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
			audioSource.Play();
			yield return new WaitForSeconds(typingSpeed);
		}
		//플래그 초기화
		istyping = false;
	}
}
