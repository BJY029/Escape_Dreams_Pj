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
		//���� �ð����� ��ٸ� ��
		yield return new WaitForSeconds(activeTime);

		//��ȭâ�� Ȱ��ȭ�Ѵ�.
		canvas.SetActive(true);

		StartTyping("���� ���� �پ� ����� �� ������ ������ û��");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("û���� ���� ���� ���ڸ��� ������ �Բ� �ٽ� �� ������ ã�Ҵ�.");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("������ ��°������ �� ������ �µ����� �������, �㸧�� �ǹ��� ������ ���߰� ���� ���̾���.");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartTyping("�׷��� ��Ż�ϰ� �ٽ� ���ư����� ����, û���� �� �ؿ� ������ �� ����...");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		dialogueText.fontSize = 90;
		dialogueText.color = Color.red;
		StartTyping("\"���� �� ������ �� ����?\"");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//�ش� ������ �Ŀ� ���� ȭ�� ������ ���ư��� ������ ��ü
		Application.Quit();
		Debug.Log("Game is exiting");
	}

	public void StartTyping(string message)
	{
		//�ڷ�ƾ ȣ��
		StartCoroutine(TypeSentence(message));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		istyping = true; //�ش� �ڷ�ƾ�� ����Ǵ� ���� ������ ���� �÷��� ����

		//�ʱ�ȭ
		dialogueText.text = "";
		//�ѱ��ھ� typingSpeed�� �ӵ��� ��µǵ��� ����
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
		//�÷��� �ʱ�ȭ
		istyping = false;
	}
}
