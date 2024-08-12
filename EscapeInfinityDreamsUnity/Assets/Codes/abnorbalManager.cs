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
	public int ranIdx;
	public int flag;

	//�ʱ�ȭ �Լ�
	public void Init()
	{
		flag = 0;

		//Player�� �Ӽ��� �ٸ� �͵��� ��� �̻�������� ����
		GameManager.Instance.playerController.InitAll();
		//����� ��������Ʈ ����
		GameManager.Instance.catAnimationController.BackAnimatorController();

		//�ؽ�Ʈ �Ҵ�
		Text PaperText = abnormals[19].GetComponent<Text>();
		Light2D GlobalLight = abnormals[24].GetComponent<Light2D>();
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
		abnormals[16].SetActive(false) ;	//���ο� ��Ʈ ����
		abnormals[17].SetActive(true) ;     //���� ��Ʈ Ȱ��ȭ
		abnormals[18].SetActive(true);      //������ Ȱ��ȭ
		abnormals[20].SetActive(false);      //������2 ��Ȱ��ȭ
        abnormals[21].SetActive(false);      //���� ����ġ ��Ȱ��ȭ
        abnormals[22].SetActive(true);      //���� ���� ���� Ȱ��ȭ
        abnormals[23].SetActive(false);     //���� ������ ��Ȱ��ȭ
        abnormals[32].SetActive(false);     //���� �� ��Ȱ��ȭ
        abnormals[33].SetActive(false);     //������ �� ��Ȱ��ȭ

        //�ؽ�Ʈ �ʱ�ȭ �۾�
        PaperText.text = "�츮 ������.... ȯ�ڵ��� �ǰ��� �ֿ켱���� �����մϴ�.\n�츮 ������.... �ְ��� �ü��� �ڶ��մϴ�.\n�츮 ������.... �������� ���� �� �ֽ��ϴ�.";
		PaperText.color = Color.black;

		GlobalLight.color = Color.white;
	}

	//���� �������� �̵� �Լ�
	public void nextStage()
	{
		//���� ������ �˷��ִ� �����
		Debug.Log("Level : " + GameManager.level);


		//�ʱ�ȭ ����
		Init();

		//���� �ߺ� ���� �ý���
		do
		{
			//���� �ε��� ����
			ranIdx = Random.Range(0, abnormals.Count);
		} while (!GameManager.Instance.LevelSystem(ranIdx));
		//���� �ش� �Լ����� true�� ��ȯ�Ǹ� �ߺ��� �ε����� �ƴѰ��̹Ƿ�, �ݺ����� ���������� ������ ����ȴ�.
		//���� �ش� �Լ����� false�� ��ȯ�Ǹ� �ߺ��� �ε����̹Ƿ�, �ݺ����� ���� ���ο� ���� �ε����� �ٽ� �̴´�.

		//���� 8�̸� �ƹ��͵� ���� �ʰ� �����Ѵ�.
		if (GameManager.level == 8) return;

		//ranIdx = 26; //�̻����� ���� �۵� �׽�Ʈ ��

		//���� : �̻����� ����Ʈ�� 33~38������ ���̷�, �ƹ� �̻������� �߻����� �ʴ� Ȯ���� ���� ���̱� ������
		
		//���� ���� �ڵ�
		if (abnormals[ranIdx] == null)
		{
			return;
		}

		//�ƹ��͵� ������ ���� ���
		if (ranIdx >= 0 && ranIdx <= 1 || ranIdx >= 34 && ranIdx <= 39)
		{
			GameManager.Instance.isAbnormal = false;//�̻� ������ �߻����� �ʾұ⿡ false�� �ʱ�ȭ
			Debug.Log("Not Changed(Original Map");
		}
		else {
			//�̻� ���� �߻�
			GameManager.Instance.isAbnormal = true; //�̻����� �÷��� ����
			if (ranIdx >= 2 && ranIdx <= 15)
			{
				//�ش� ������Ʈ���� ���� ��ȭ
				abnormals[ranIdx].SetActive(true);
				originals[ranIdx].SetActive(false);
				Debug.Log(ranIdx + " has been changed");
			}
			else if (ranIdx == 16) //�繰�� �� ���ο� ��Ʈ ���� ����
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("New Vent has been actived");
			}
			else if (ranIdx == 17) //���Ǳ� �� ��Ʈ ��Ȱ��ȭ
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Vent has been unactived");
			}
			else if (ranIdx == 18) //���� ������ ��Ȱ��ȭ ����
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Posters has been unactived");
			}
			else if (ranIdx == 19) //���� �ؽ�Ʈ �ٲ�� ����
			{
				Text PaperText = abnormals[19].GetComponent<Text>();
				PaperText.text = "���ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ������ų����Ͱ���";
				PaperText.color = Color.red;
				Debug.Log("Paper Text has been changed");
			}
			else if (ranIdx == 20) //�����͵��� �ٲ�� ����
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("Posters has been actived");
			}
			else if (ranIdx == 21) //�濡 ����ġ�� �����Ǵ� ����
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("Switch has been actived");
			}
			else if (ranIdx == 22) //���� ���� ���ڰ� ������� ����
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Small chair has been actived");
			}
			else if (ranIdx == 23) //�濡 �����͸� �����ϴ� �̻�����
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("room posters has been actived");
			}
			else if (ranIdx == 24) //�� �� �ٲ�� ����
			{
				Debug.Log("Lights has been changed");
				//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
				//OnTriggerEnter 2D ���� ���ȴ�.
				flag = 24;
				////�÷��װ� ���� �κ��� �����ؾ� �� ��, Player�� cat ��ũ��Ʈ�� OnTriggerEnter 2D�� ���� �ؾ� ��. 
				///�߰��� �ش� �÷��׷� �ڵ� �������� ���� ���� ��Ұ� ���� �� ����
			}
			else if (ranIdx == 25) //�¿� ���� ��Ʈ��
			{
				Debug.Log("a,d has been changed");
				//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
				//OnTriggerEnter 2D ���� ���ȴ�.
				flag = 25;
			}
			else if (ranIdx == 26) //�÷��̾� �̵� �ӵ� ��ȭ
			{
				Debug.Log("speed has been changed");
				//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
				//OnTriggerEnter 2D ���� ���ȴ�.
				flag = 26;
			}
			else if (ranIdx == 27) //�÷��̾�, ����� �׸��� ����
			{
				Debug.Log("Shadow deleted");
				//�÷��� ����, �ش� ���� Player ��ũ��Ʈ�� 
				//OnTriggerEnter 2D ���� ���ȴ�.
				flag = 27;
			}
			else if (ranIdx == 28) //����� ��������Ʈ ���� ����
			{
				Debug.Log("Change Cat Sprite");
				//catAnimationController ��ũ��Ʈ���� �����ȴ�.
				flag = 28;
			}
			else if (ranIdx == 29) //â�� ��ũ �Ҹ� ���
			{
				Debug.Log("Window Audio Play");
				//AudioController���� �����ȴ�.
				flag = 29;
			}
			else if (ranIdx == 30) //�� ��ũ �Ҹ� ���
			{
				Debug.Log("Door Audio Play");
				//AudioController���� �����ȴ�.
				flag = 30;
			}
			else if (ranIdx == 31) //��� �Ҹ� ���
			{
				Debug.Log("cry Audio Play");
				//AudioController���� �����ȴ�.
				flag = 31;
			}
			else if(ranIdx == 32) //���� ���� ����
			{
                abnormals[32].SetActive(true);
                Debug.Log("R_enemy actived");
			}
            else if (ranIdx == 33) //���� ���� ����
            {
                abnormals[33].SetActive(true);
                Debug.Log("enemy actived");
            }
        }
	}
}
