using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatDialogController : MonoBehaviour
{
	public GameObject canvas;
	public Text dialogueText;

	public Image catIcon;
	public Image playerIcon;

	public float typingSpeed = 0.05f;
	public float activeTime;
	public bool istyping;

	public AudioSource audioSource;

	//초기화
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
		catIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("음... 어떻게 된거지..?");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("오랜만이다 냥!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("루이?! 너가 어떻게 여기에... 넌 2년 전에 죽...");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		//yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("넌 지금 꿈 속에 갇혔다 냥! 널 돕기 위해서 이 몸이 행차하셨단 말이다 냥!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("의사가 잠에 들기 전 해야 할 일을 하라고 했는데 어떤 걸 해야 해?");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("간단하다 냥~ 이상현상을 찾으면 다시 잠을 자고, 이상현상이 없으면 네 주머니에 있는 약물을 마시면 된다 냥!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("이상현상이라면 어떤 걸...");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("현재는 이상현상이 없는 상태다 냥~ 네가 잠을 자기 시작하면 본격적으로 이상현상이 발생되니, 지금 상태를 유심히 관찰하라냥~");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("알겠어.. 도와줘서 고마워!");

		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//해당 텍스트를 StartTyping 함수를 통해 출력
		StartTyping("그리고 E키가 상호작용, Shift키가 달리기, X키가 약물복용 키다 냥~");
		//타이핑이 다 될때까지 기다린 후
		yield return new WaitUntil(() => !istyping);
		//스페이스 키나 ,e키가 눌릴 때 까지 기다린 후
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);

		//텍스트를 초기화 하고
		dialogueText.text = "";
		//비활성화 한다.
		canvas.SetActive(false);
	}

	//대화 창에 글이 입력되는 코루틴
	public IEnumerator dialogController()
	{
		//일정 시간동안 기다린 후
		yield return new WaitForSeconds(activeTime);

		//대화창을 활성화한다.
		canvas.SetActive(true);

		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);

		//만약 게임 레벨이 현재 0이면
		if (GameManager.level == 0)
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
			audioSource.Play();
			yield return new WaitForSeconds(typingSpeed);
		}
		//플래그 초기화
		istyping = false ;
	}
}
