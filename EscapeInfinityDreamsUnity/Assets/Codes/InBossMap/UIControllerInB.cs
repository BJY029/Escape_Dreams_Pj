using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIControllerInB : MonoBehaviour
{
	public GameObject me;

	public GameObject DeadStateUI;

	public Vector3 UIOffset;
	public GameObject interactionUI;
	public GameObject Diary;
	public GameObject goOutUI;

	private AudioSource AudioSource;

	private bool canInteract;
	private bool interacting;

	private void Awake()
	{
		AudioSource = GetComponent<AudioSource>();

		DeadStateUI.SetActive(false);

		Diary.SetActive(false);
		goOutUI.SetActive(false);

		canInteract = false;
		interacting = false;
	}
	public void activeDeadStateUI()
	{
		DeadStateUI.SetActive(true);
	}
	public void nonAcviteDeadStateUI()
	{
		DeadStateUI.SetActive(false); 
	}

	private void Update()
	{
		//���� UI�� ��ȣ�ۿ��� ������ ��
		if(canInteract)
		{
			//��ȣ�ۿ� UI ��ġ ����
			interactionUI.transform.position = me.transform.position + UIOffset;
			//EŰ�� ������
			if (Input.GetKeyDown(KeyCode.E))
			{
				//���� ���� UI�� ���� ���°� �ƴϸ�
				if(interacting == false)
				{
					//���� UI �Ѵ� �ڷ�ƾ ȣ��
					StartCoroutine(PaperInRoutine());
				}
				else
				{
					//���� ���¸� ���� UI �ݴ� �ڷ�ƾ ȣ��
					StartCoroutine(PaperOutRoutine());
				}
			}
		}
	}

	//���� UI�� ������ �ڷ�ƾ
	IEnumerator PaperInRoutine()
	{
		//�÷��� ����
		interacting = true;
		//���� UI Ȱ��ȭ
		Diary.SetActive(true);
		//��ȣ�ۿ� UI ��Ȱ��ȭ
		interactionUI.SetActive(false);
		//���� UI ������ Ȱ��ȭ
		goOutUI.SetActive(true);
		yield return null;

		//���� ������ ���� ����� ���Ұ�
		AudioSource.mute = true;
		Time.timeScale = 0f;
	}

	//���� UI�� ������ �ڷ�ƾ
	IEnumerator PaperOutRoutine()
	{
		//�÷��� ����
		interacting=false;
		//���� UI ��Ȱ��ȭ
		Diary.SetActive(false);
		//��ȣ �ۿ� UI Ȱ��ȭ
		interactionUI.SetActive(true);
		//������ UI ��Ȱ��ȭ
		goOutUI.SetActive(false);
		yield return null;

		//���Ұ� ����
		AudioSource.mute = false;
		Time.timeScale = 1.0f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�ش� ������ ������ ��, ��ȣ�ۿ� UI�� ����
		//���� UI ��ȣ�ۿ� �÷��� ����
		if (collision.CompareTag("diary"))
		{
			interactionUI.SetActive(true);
			canInteract = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//�ش� �������� ����� ��, ��ȣ�ۿ� UI �����
		//���� UI ��ȣ�ۿ� �÷��� ����
		if (collision.CompareTag("diary"))
		{
			interactionUI.SetActive(false);
			canInteract = false;
		}
	}
}
