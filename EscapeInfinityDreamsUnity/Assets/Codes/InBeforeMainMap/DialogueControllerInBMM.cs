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
		//���� �ð����� ��ٸ� ��
		yield return new WaitForSeconds(activeTime);

		//��ȭâ�� Ȱ��ȭ�Ѵ�.
		canvas.SetActive(true);

		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�̰� ��¥ �����Ѱ� ����..?");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("����~ �� �ֻ� �Ѵ� �°� Ǫ~~�� �ڰ� �Ͼ�� ��!");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�޿��� �������ֽ� ��� �ֽô°� ����?");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�翬����~ �и� �� ������ ������ �������� �ʾƵ� �ɰž�~");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("....");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("���� ������ �и��� ��� �ƽô°���...?");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("......");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		StartCoroutine(GameManagerInBMM.Instance.LightController.HalfFadeOut());

		//----
		playerIcon.gameObject.SetActive(true);
		doctorIcon.gameObject.SetActive(false);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("��...���..? ����....��??");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));

		//----
		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(true);
		//�ش� �ؽ�Ʈ�� StartTyping �Լ��� ���� ���
		StartTyping("�ٽ� ����� ������ �ű⼭ �ؾ��� ���� ��.");

		//Ÿ������ �� �ɶ����� ��ٸ� ��
		yield return new WaitUntil(() => !istyping);
		//�����̽� Ű�� ,eŰ�� ���� �� ���� ��ٸ� ��
		yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.E));


		playerIcon.gameObject.SetActive(false);
		doctorIcon.gameObject.SetActive(false);

		//�ؽ�Ʈ�� �ʱ�ȭ �ϰ�
		dialogueText.text = "";
		//��Ȱ��ȭ �Ѵ�.
		canvas.SetActive(false);
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
			audioSource.Play();
			yield return new WaitForSeconds(typingSpeed);
		}
		//�÷��� �ʱ�ȭ
		istyping = false;
	}
}
