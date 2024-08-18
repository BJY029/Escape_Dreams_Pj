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

	//�ʱ�ȭ
	private void Awake()
	{
		istyping = false;
		canvas.SetActive(false);
		dialogueText.text = "";
	}

	public IEnumerator startSceneDialog()
	{
		//���� �ð����� ��ٸ� ��
		yield return new WaitForSeconds(activeTime);

		//��ȭâ�� Ȱ��ȭ�Ѵ�.
		canvas.SetActive(true);

		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("��... ��� �Ȱ���..?");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�������̴� ��!");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("����?! �ʰ� ��� ���⿡... �� 2�� ���� ��...");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		//yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�� ���� �� �ӿ� ������ ��! �� ���� ���ؼ� �� ���� �����ϼ̴� ���̴� ��!");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�ǻ簡 �ῡ ��� �� �ؾ� �� ���� �϶�� �ߴµ� � �� �ؾ� ��?");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�����ϴ� ��~ �̻������� ã���� �ٽ� ���� �ڰ�, �̻������� ������ �� �ָӴϿ� �ִ� �๰�� ���ø� �ȴ� ��!");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�̻������̶�� � ��...");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("����� �̻������� ���� ���´� ��~ �װ� ���� �ڱ� �����ϸ� ���������� �̻������� �߻��Ǵ�, ���� ���¸� ������ �����϶��~");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		catIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�˰ھ�.. �����༭ ����!");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�׸��� EŰ�� ��ȣ�ۿ�, ShiftŰ�� �޸���, XŰ�� �๰���� Ű�� ��~");
		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);

		//�ؽ�Ʈ�� �ʱ�ȭ �ϰ�
		dialogueText.text = "";
		//��Ȱ��ȭ �Ѵ�.
		canvas.SetActive(false);
	}

	//��ȭ â�� ���� �ԷµǴ� �ڷ�ƾ
	public IEnumerator dialogController()
	{
		//���� �ð����� ��ٸ� ��
		yield return new WaitForSeconds(activeTime);

		//��ȭâ�� Ȱ��ȭ�Ѵ�.
		canvas.SetActive(true);

		playerIcon.gameObject.SetActive(false);
		catIcon.gameObject.SetActive(true);

		//���� ���� ������ ���� 0�̸�
		if (GameManager.level == 0)
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
			audioSource.Play();
			yield return new WaitForSeconds(typingSpeed);
		}
		//�÷��� �ʱ�ȭ
		istyping = false ;
	}
}
