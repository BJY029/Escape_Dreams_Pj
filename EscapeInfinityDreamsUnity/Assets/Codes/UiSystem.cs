using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSystem : MonoBehaviour
{
	[Header("# triggerTagObj")]
	public GameObject Paper;	//���� UI
	public GameObject VandingMachine; //���Ǳ� UI
	public GameObject Extensonmeter; //����� UI

	[Header("# UI Set")]
	public GameObject interactionUI; // ����� ������ UI ������Ʈ(text_background)
	public GameObject interactionUIforOut; //���� UI���� ������ UI ������Ʈ
	public Vector3 uiOffset; // UI ������Ʈ�� ��� ��ġ ����
	public float displayDuration = 2.0f; //���Ǳ�, ����迡�� UI�� ����� �ð�


	private bool isInteracted;	//��ȣ�ۿ� UI�� ���� ���� Bool�� ����
	private bool isUiActived;  //���� UI�� ���� ������ִ��� �����ϱ� ���� Bool�� ����
	private int flag;	//�� UI�� �����ϱ� ���� �÷���


	//�ʱ�ȭ
	private void Awake()
	{
		if (interactionUI != null) 
		{ 
			interactionUI.SetActive(false); 
		}
		if(interactionUIforOut != null)
		{
			interactionUIforOut.SetActive(false);
		}

		Paper.SetActive(false);
		VandingMachine.SetActive(false);
		Extensonmeter.SetActive(false);


		isInteracted = false;
		isUiActived = false;
		flag = 0;
	}



	private void Update()
	{
		if (!isInteracted) return;	//UI�� ��ȣ�ۿ� ������ ���� ����
		
		//��ȣ�ۿ� UI�� ��ġ ����
		interactionUI.transform.position = transform.position + uiOffset;
		
		//��ȣ�ۿ� Ű�� ������ ��
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			switch (flag)
			{
				case 0:
					break;
				//���� UI�� ��ȣ�ۿ� �� ���
				case 1:
					if(isUiActived ==  false)	//���� ���� UI�� Ȱ��ȭ ���� ���� ���¸�
					{	//���� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
						StartCoroutine(PaperInRoutine());
					}
					else //���� UI�� Ȱ��ȭ�� ���¸�
					{
						//���� UI�� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
						StartCoroutine(PaperOutRoutine());
					}
					break;
				//���Ǳ� UI�� ��ȣ�ۿ� �� ���
				case 2:
					//���Ǳ� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
					StartCoroutine(VandingRoutine());
					break;
				//����� UI�� ��ȣ�ۿ� �� ���
				case 3:
					//����� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
					StartCoroutine(extRoutine());
					break;
				default:
					break;
			}
		}
	}

	//���� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator PaperInRoutine()
	{
		isUiActived = true; //���� UI�� Ȱ��ȭ �Ǿ����� ����
		Paper.SetActive(true);	//���� UI Ȱ��ȭ
		interactionUI.SetActive(false); //��ȣ�ۿ� UI�� ��Ȱ��ȭ
		interactionUIforOut.SetActive(true); //���� UI���� ������ UI�� Ȱ��ȭ
		VandingMachine.SetActive(false ); //���Ǳ⿡�� �ٷ� ���� UI�� ��ȣ�ۿ� �� ���, ���Ǳ� UI ��Ȱ��ȭ
		yield return new WaitForEndOfFrame();	//�� ������ ���

		Time.timeScale = 0; //Ȱ��ȭ �� ���� ���� �ð� ����
	}

	//���� UI�� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator PaperOutRoutine()
	{
		isUiActived = false; //���� UI�� ��Ȱ��ȭ �Ǿ����� ����
		Paper.SetActive(false);	//���� UI ��Ȱ��ȭ
		interactionUI.SetActive(true);	//��ȣ�ۿ� UI Ȱ��ȭ
		interactionUIforOut.SetActive(false);	//���� UI���� ������ ���� UI ��Ȱ��ȭ
		yield return new WaitForEndOfFrame();	//�������� ���
		
		Time.timeScale = 1.0f; //�ð� �ٽ� �帣���� ����
	}

	//���Ǳ� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator VandingRoutine()
	{
		VandingMachine.SetActive(true); //�ش� UI�� Ȱ��ȭ
		yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
		VandingMachine.SetActive(false ); //��Ȱ��ȭ
	}

	//����� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator extRoutine()
	{
		Extensonmeter.SetActive(true); //�ش� UI�� Ȱ��ȭ
		yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
		Extensonmeter.SetActive(false); //��Ȱ��ȭ
	}


	//Ư�� �ݶ��̴��� �浹�� ���
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Paper")) //�浹�� �ݶ��̴��� �±װ� ������ ���
		{
			//�Լ� ȣ��(flag ; 1�� ����)
			ShowUIInteraction(1);
		}
		else if (collision.CompareTag("vandingMachine")) //�浹�� �ݶ��̴��� �±װ� ���Ǳ�
		{
			//�Լ� ȣ��(flag : 2�� ����)
			ShowUIInteraction(2);
		}
		else if (collision.CompareTag("extensometer")) //�浹�� �ݶ��̴��� �±װ� �����
		{
			//�Լ� ȣ��(flag : 3�� ����)
			ShowUIInteraction(3);
		}
	}

	//�ش� UI�� ���õ� �͵��� �ٲٴ� �Լ�
	private void ShowUIInteraction(int interactionFlag)
	{
		//��ȣ�ۿ� UI Ȱ��ȭ
		interactionUI.SetActive(true);

		//��ȣ�ۿ� ������ ���·� ����
		isInteracted = true;

		//�÷��� ����
		flag = interactionFlag;
	}

	//�ݶ��̴��� �浹 ��� ���
	private void OnTriggerExit2D(Collider2D collision)
	{
		//� �ݶ��̴��� ����� �������
		if (collision.CompareTag("Paper") || collision.CompareTag("vandingMachine") || collision.CompareTag("extensometer"))
		{
			//���� ���� ���ǹ�
			if (interactionUI != null)
			{
				//��ȣ�ۿ� Ui ��Ȱ��ȭ
				interactionUI.SetActive(false);
				//��ȣ�ۿ� �Ұ����� ���·� ����
				isInteracted = false;
				//�÷��� �ʱ�ȭ
				flag = 0;
			}
		}
	}
}
