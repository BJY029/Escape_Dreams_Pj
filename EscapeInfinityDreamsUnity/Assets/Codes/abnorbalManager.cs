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
	}

	//���� �������� �̵� �Լ�
	public void nextStage()
	{
		//�ʱ�ȭ ����
		Init();

		//���� �ε��� ����
		int ranIdx = Random.Range(0, originals.Count - 1);

		//���� ���� �ڵ�
		if (abnormals[ranIdx] == null)
		{
			return;
		}

		//�ƹ��͵� ������ ���� ���
		if (ranIdx >= 0 && ranIdx <= 1) Debug.Log("Not Changed(Original Map");
		else
		{
			//�ش� ������Ʈ���� ���� ��ȭ
			abnormals[ranIdx].SetActive(true);
			originals[ranIdx].SetActive(false);
			Debug.Log(ranIdx + " has been changed");
		}
	}
}
