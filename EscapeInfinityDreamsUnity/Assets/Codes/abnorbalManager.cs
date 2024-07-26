using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abnorbalManager : MonoBehaviour
{
    public List<GameObject> abnormals; //�̻������ ������Ʈ ����Ʈ
    public List<GameObject> originals; //�������� ������Ʈ ����Ʈ

	//�ʱ�ȭ �Լ�
	public void Init()
	{ 
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
		abnormals[15].SetActive(true) ;		//���� ��Ʈ Ȱ��ȭ
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
	}
}
