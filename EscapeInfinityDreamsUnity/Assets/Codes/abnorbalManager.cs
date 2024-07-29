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
		Text PaperText = abnormals[18].GetComponent<Text>();
		Light2D GlobalLight = abnormals[23].GetComponent<Light2D>();
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
		abnormals[15].SetActive(false) ;	//���ο� ��Ʈ ����
		abnormals[16].SetActive(true) ;     //���� ��Ʈ Ȱ��ȭ
		abnormals[17].SetActive(true);      //������ Ȱ��ȭ
		abnormals[19].SetActive(false);      //������2 ��Ȱ��ȭ
        abnormals[20].SetActive(false);      //���� ����ġ ��Ȱ��ȭ
        abnormals[21].SetActive(true);      //���� ���� ���� Ȱ��ȭ
        abnormals[22].SetActive(false);     //���� ������ ��Ȱ��ȭ

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
		else if(ranIdx >= 2 && ranIdx <= 14)
		{
			//�ش� ������Ʈ���� ���� ��ȭ
			abnormals[ranIdx].SetActive(true);
			originals[ranIdx].SetActive(false);
			Debug.Log(ranIdx + " has been changed");
		}
		else if(ranIdx == 15) //�繰�� �� ���ο� ��Ʈ ���� ����
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("New Vent has been actived");
		}
		else if (ranIdx == 16) //���Ǳ� �� ��Ʈ ��Ȱ��ȭ
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Vent has been unactived");
		}
		else if (ranIdx == 17) //���� ������ ��Ȱ��ȭ ����
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Posters has been unactived");
		}
		else if(ranIdx == 18) //���� �ؽ�Ʈ �ٲ�� ����
		{
			Text PaperText = abnormals[18].GetComponent<Text>();
			PaperText.text = "���ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ���";
			PaperText.color = Color.red;
			Debug.Log("Paper Text has been changed");
		}
		else if (ranIdx == 19) //�����͵��� �ٲ�� ����
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("Posters has been actived");
		}
        else if (ranIdx == 20) //�濡 ����ġ�� �����Ǵ� ����
        {
            abnormals[ranIdx].SetActive(true);
            Debug.Log("Switch has been actived");
        }
        else if (ranIdx == 21) //���� ���� ���ڰ� ������� ����
        {
            abnormals[ranIdx].SetActive(false);
            Debug.Log("Small chair has been actived");
        }
        else if (ranIdx == 22) //�濡 �����͸� �����ϴ� �̻�����
        {
            abnormals[ranIdx].SetActive(true);
            Debug.Log("room posters has been actived");
        }
        else if (ranIdx == 23) //�� �� �ٲ�� ����
		{
			Debug.Log("Lights has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 23;
			////�÷��װ� ���� �κ��� �����ؾ� �� ��, Player�� cat ��ũ��Ʈ�� OnTriggerEnter 2D�� ���� �ؾ� ��. 
			///�߰��� �ش� �÷��׷� �ڵ� �������� ���� ���� ��Ұ� ���� �� ����
		}
		else if (ranIdx == 24) //�¿� ���� ��Ʈ��
		{
			Debug.Log("a,d has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 24;
		}
		else if (ranIdx == 25) //�÷��̾� �̵� �ӵ� ��ȭ
		{
			Debug.Log("speed has been changed");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 25;
		}
		else if (ranIdx == 26) //�÷��̾�, ����� �׸��� ����
		{
			Debug.Log("Shadow deleted");
			//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
			//OnTriggerEnter 2D ���� ���ȴ�.
			flag = 26;
		}
		else if (ranIdx == 27) //����� ��������Ʈ ���� ����
		{
			Debug.Log("Change Cat Sprite");
			//catAnimationController ��ũ��Ʈ���� �����ȴ�.
			flag = 27;
		}
	}
}
