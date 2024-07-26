using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abnorbalManager : MonoBehaviour
{
    public List<GameObject> abnormals; //이상현상용 오브젝트 리스트
    public List<GameObject> originals; //오리지날 오브젝트 리스트

	//초기화 함수
	public void Init()
	{ 
		//이상현상 오브젝트 리스트을 돌아보며
		for (int i = 0; i < originals.Count; i++)
		{
			if (abnormals[i] != null)
			{
				//어떤 이상현상 오브젝트가 활성화 되어있을 경우
				if (abnormals[i].activeSelf == true)
				{
					//해당 오브젝트 비활성화 후,
					abnormals[i].SetActive(false);
					//오리지날 오브젝트 활성화
					originals[i].SetActive(true);

				}
			}
		}
		abnormals[14].SetActive(false) ;	//새로운 밴트 삭제
		abnormals[15].SetActive(true) ;		//기존 벤트 활성화
	}

	//다음 스테이지 이동 함수
	public void nextStage()
	{
		//초기화 진행
		Init();

		//랜덤 인덱스 생성
		int ranIdx = Random.Range(0, abnormals.Count);

		//오류 방지 코드
		if (abnormals[ranIdx] == null)
		{
			return;
		}

		//아무것도 변하지 않은 경우
		if (ranIdx >= 0 && ranIdx <= 1) Debug.Log("Not Changed(Original Map");
		else if(ranIdx >= 2 && ranIdx <= 13)
		{
			//해당 오브젝트들의 상태 변화
			abnormals[ranIdx].SetActive(true);
			originals[ranIdx].SetActive(false);
			Debug.Log(ranIdx + " has been changed");
		}
		else if(ranIdx == 14) //사물함 위 새로운 벤트 생성 현상
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("New Vent has been actived");
		}
		else if (ranIdx == 15) //사물함 위 새로운 벤트 생성 현상
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Vent has been unactived");
		}
	}
}
