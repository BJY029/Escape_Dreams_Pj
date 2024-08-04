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

	//�ʱ�ȭ
	private void Awake()
	{
		istyping = false;
		canvas.SetActive(false);
		dialogueText.text = "";
	}

	//��ȭ â�� ���� �ԷµǴ� �ڷ�ƾ
	public IEnumerator dialogController()
	{
		//���� �ð����� ��ٸ� ��
		yield return new WaitForSeconds(activeTime);

		//��ȭâ�� Ȱ��ȭ�Ѵ�.
		canvas.SetActive(true);

		//���� ���� ������ ���� 0�̸�
		if(GameManager.level == 0)
		{
			//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
			StartTyping("0 ��° ���̴� ��~\n�ش� �޿��� �̻������� ���� ��~");
		}
		else //�ƴϸ�
		{
			//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
			StartTyping(GameManager.level + " ��° ���̴� ��~");
		}

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));
		//�ؽ�Ʈ�� �ʱ�ȭ �ϰ�
		dialogueText.text = "";
		//��Ȱ��ȭ �Ѵ�.
		canvas.SetActive(false);
	}

	//Ÿ������ �ϱ� ���� �Լ�
	public void StartTyping(string message)
	{
		//�ڷ�ƾ ȣ��
		StartCoroutine(TypeSentence(message));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		istyping  = true; //�ش� �ڷ�ƾ�� ����Ǵ� ���� ������ ���� �÷��� ����

		//�ʱ�ȭ
		dialogueText.text = "";
		//�ѱ��ھ� typingSpeed�� �ӵ��� ��µǵ��� ����
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
		//�÷��� �ʱ�ȭ
		istyping = false ;
	}
}
