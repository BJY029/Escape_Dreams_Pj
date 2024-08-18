using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
	public sceneController sceneManager;
	public catAnimationController catAnimationController;

	//이상현상 여부를 판단하는 플래그
	public bool isAbnormal;
	//레벨을 통제하기 위한 변수, static으로 선언
	public static int level = 0;
	//중복 방지 시스템을 위한 배열 선언
	private int[] randomLevel;

	private void Awake()
	{
		Instance = this;
		randomLevel = new int[7];
	}

	//중복 방지 함수
	public bool LevelSystem(int idx)
	{
		//레벨을 모두 클리어 하여 8이 되면
		if (level == 8)
		{
			
			//씬 전환
			SceneManager.LoadScene("BossScene");
			return true;
		}
		//현재 진행된 레벨 수 만큼 반복문을 돈다.
		for (int i = 0; i < level; i++)
		{
			//저장된 과거 이상현상 인덱스 중, 같은 인덱스를 발견하면
			if (randomLevel[i] == idx)
			{
				//false을 반환한다.
				return false;
			}
		}
		//중복된 랜덤 레벨 인덱스가 아니면
		//해당 레벨을 배열에 저장한 후
		randomLevel[level - 1] = idx;
		//true를 반환한다.
		return true;
	}
}
