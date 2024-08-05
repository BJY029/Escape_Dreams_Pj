using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject player;
	public GameObject cat;
	public abnorbalManager abnorbalManager;
	public AudioController audioController;
	public Player playerController;
	public LightController lightController;
	public UiSystem uiSystem;
	public playerAnimationController playerAnimationController;
	public CatDialogController catDialogController;
	public EventSystem eventSystem;
	public SceneManager sceneManager;
	public catAnimationController catAnimationController;

	//�̻����� ���θ� �Ǵ��ϴ� �÷���
	public bool isAbnormal;
	//������ �����ϱ� ���� ����, static���� ����
	public static int level = 0;
	//�ߺ� ���� �ý����� ���� �迭 ����
	private int[] randomLevel;

	private void Awake()
	{
		Instance = this;
		randomLevel = new int[7];
	}

	//�ߺ� ���� �Լ�
	public bool LevelSystem(int idx)
	{
		//���� ����� ���� �� ��ŭ �ݺ����� ����.
		for (int i = 0; i < level; i++)
		{
			//����� ���� �̻����� �ε��� ��, ���� �ε����� �߰��ϸ�
			if (randomLevel[i] == idx)
			{
				//false�� ��ȯ�Ѵ�.
				return false;
			}
		}
		//�ߺ��� ���� ���� �ε����� �ƴϸ�
		//�ش� ������ �迭�� ������ ��
		randomLevel[level - 1] = idx;
		//true�� ��ȯ�Ѵ�.
		return true;
	}
}
