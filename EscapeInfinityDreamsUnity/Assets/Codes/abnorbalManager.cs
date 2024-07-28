using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class abnorbalManager : MonoBehaviour
{
    public List<GameObject> abnormals; //�̻������ ������Ʈ ����Ʈ
    public List<GameObject> originals; //�������� ������Ʈ ����Ʈ
	public GameObject player;

	public int flag;

	//�ʱ�ȭ �Լ�
	public void Init()
	{
		flag = 0;

		//�ؽ�Ʈ �Ҵ�
		Text PaperText = abnormals[17].GetComponent<Text>();
		Light2D GlobalLight = abnormals[19].GetComponent<Light2D>();
		//�̻����� ������Ʈ ����Ʈ�� ���ƺ���
		for (int i = 0; i < originals.Count; i++)
		{
			if (abnormals[i] != null)
			{
				//� �̻����� ������Ʈ�� Ȱ��ȭ �Ǿ����� ���
				if (abnormals[i].activeSelf == true)
				{
					//�ش� ������Ʈ ��Ȱ��ȭ ��,
					abnormals[i].SetActive(false);
					//�������� ������Ʈ Ȱ��ȭ
					originals[i].SetActive(true);

				}
			}
		}
		abnormals[14].SetActive(false) ;	//���ο� ��Ʈ ����
		abnormals[15].SetActive(true) ;     //���� ��Ʈ Ȱ��ȭ
		abnormals[16].SetActive(true);      //������ Ȱ��ȭ
		abnormals[18].SetActive(false);      //������2 ��Ȱ��ȭ

		 //�ؽ�Ʈ �ʱ�ȭ �۾�
		PaperText.text = "�츮 ������.... ȯ�ڵ��� �ǰ��� �ֿ켱���� �����մϴ�.\n�츮 ������.... �ְ��� �ü��� �ڶ��մϴ�.\n�츮 ������.... �������� ���� �� �ֽ��ϴ�.";
		PaperText.color = Color.black;

		GlobalLight.color = Color.white;
	}

	//���� �������� �̵� �Լ�
	public void nextStage()
	{
		//�ʱ�ȭ ����
		Init();

		//���� �ε��� ����
		int ranIdx = Random.Range(0, abnormals.Count);

		//���� ���� �ڵ�
		if (abnormals[ranIdx] == null)
		{
			return;
		}

		//�ƹ��͵� ������ ���� ���
		if (ranIdx >= 0 && ranIdx <= 1) Debug.Log("Not Changed(Original Map");
		else if(ranIdx >= 2 && ranIdx <= 13)
		{
			//�ش� ������Ʈ���� ���� ��ȭ
			abnormals[ranIdx].SetActive(true);
			originals[ranIdx].SetActive(false);
			Debug.Log(ranIdx + " has been changed");
		}
		else if(ranIdx == 14) //�繰�� �� ���ο� ��Ʈ ���� ����
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("New Vent has been actived");
		}
		else if (ranIdx == 15) //�繰�� �� ���ο� ��Ʈ ���� ����
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Vent has been unactived");
		}
		else if (ranIdx == 16) //�繰�� �� ���ο� ��Ʈ ���� ����
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Posters has been unactived");
		}
		else if(ranIdx == 17) //���� �ؽ�Ʈ �ٲ�� ����
		{
			Text PaperText = abnormals[17].GetComponent<Text>();
			PaperText.text = "���ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ���";
			PaperText.color = Color.red;
			Debug.Log("Paper Text has been changed");
		}
		else if (ranIdx == 18) //�����͵��� �ٲ�� ����
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("Posters has been actived");
		}
		else if (ranIdx == 19) //�� �� �ٲ�� ����
		{
			Debug.Log("Lights has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 19;
			////�÷��װ� ���� �κ��� �����ؾ� �� ��, Player�� cat ��ũ��Ʈ�� OnTriggerEnter 2D�� ���� �ؾ� ��. 
			///�߰��� �ش� �÷��׷� �ڵ� �������� ���� ���� ��Ұ� ���� �� ����
		}
		else if (ranIdx == 20) //�¿� ���� ��Ʈ��
		{
			Debug.Log("a,d has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 20;
		}
		else if (ranIdx == 21) //�÷��̾� �̵� �ӵ� ��ȭ
		{
			Debug.Log("speed has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 21;
		}
		else if (ranIdx == 22) //�÷��̾�, ����� �׸��� ����
		{
			Debug.Log("Shadow deleted");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 22;
		}
		else if (ranIdx == 23) //�÷��̾�, ����� �׸��� ����
		{
			Debug.Log("Change Cat Sprite");
			//catAnimationController ��ũ��Ʈ���� �����ȴ�.
			flag = 23;
		}
	}
}
