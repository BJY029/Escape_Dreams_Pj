using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class abnorbalManager : MonoBehaviour
{
    public List<GameObject> abnormals; //이상현상용 오브젝트 리스트
    public List<GameObject> originals; //오리지날 오브젝트 리스트
	public GameObject player;

	public int flag;

	//초기화 함수
	public void Init()
	{
		flag = 0;

		//텍스트 할당
		Text PaperText = abnormals[18].GetComponent<Text>();
		Light2D GlobalLight = abnormals[23].GetComponent<Light2D>();
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
		abnormals[15].SetActive(false) ;	//새로운 밴트 삭제
		abnormals[16].SetActive(true) ;     //기존 벤트 활성화
		abnormals[17].SetActive(true);      //포스터 활성화
		abnormals[19].SetActive(false);      //포스터2 비활성화
        abnormals[20].SetActive(false);      //방의 스위치 비활성화
        abnormals[21].SetActive(true);      //방의 작은 의자 활성화
        abnormals[22].SetActive(false);     //방의 포스터 비활성화

        //텍스트 초기화 작업
        PaperText.text = "우리 병원은.... 환자들의 건강을 최우선으로 생각합니다.\n우리 병원은.... 최고의 시설을 자랑합니다.\n우리 병원은.... 웃음으로 가득 차 있습니다.";
		PaperText.color = Color.black;

		GlobalLight.color = Color.white;
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
		else if(ranIdx >= 2 && ranIdx <= 14)
		{
			//해당 오브젝트들의 상태 변화
			abnormals[ranIdx].SetActive(true);
			originals[ranIdx].SetActive(false);
			Debug.Log(ranIdx + " has been changed");
		}
		else if(ranIdx == 15) //사물함 위 새로운 벤트 생성 현상
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("New Vent has been actived");
		}
		else if (ranIdx == 16) //자판기 위 벤트 비활성화
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Vent has been unactived");
		}
		else if (ranIdx == 17) //벽에 포스터 비활성화 현상
		{
			abnormals[ranIdx].SetActive(false);
			Debug.Log("Posters has been unactived");
		}
		else if(ranIdx == 18) //종이 텍스트 바뀌는 현상
		{
			Text PaperText = abnormals[18].GetComponent<Text>();
			PaperText.text = "정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아";
			PaperText.color = Color.red;
			Debug.Log("Paper Text has been changed");
		}
		else if (ranIdx == 19) //포스터들이 바뀌는 현상
		{
			abnormals[ranIdx].SetActive(true);
			Debug.Log("Posters has been actived");
		}
        else if (ranIdx == 20) //방에 스위치가 생성되는 현상
        {
            abnormals[ranIdx].SetActive(true);
            Debug.Log("Switch has been actived");
        }
        else if (ranIdx == 21) //방의 작은 의자가 사라지는 현상
        {
            abnormals[ranIdx].SetActive(false);
            Debug.Log("Small chair has been actived");
        }
        else if (ranIdx == 22) //방에 포스터를 생성하는 이상현상
        {
            abnormals[ranIdx].SetActive(true);
            Debug.Log("room posters has been actived");
        }
        else if (ranIdx == 23) //빛 색 바뀌는 현상
		{
			Debug.Log("Lights has been changed");
			//플래그 설정, 해당 값은 Player 스크립트의 
			//OnTriggerEnter 2D 에서 사용된다.
			flag = 23;
			////플래그가 사용된 부분을 수정해야 할 때, Player와 cat 스크립트의 OnTriggerEnter 2D를 수정 해야 함. 
			///추가로 해당 플래그로 코드 내에서도 구분 짓는 요소가 있을 수 있음
		}
		else if (ranIdx == 24) //좌우 반전 컨트롤
		{
			Debug.Log("a,d has been changed");
			//플래그 설정, 해당 값은 Player 스크립트의 
			//OnTriggerEnter 2D 에서 사용된다.
			flag = 24;
		}
		else if (ranIdx == 25) //플레이어 이동 속도 변화
		{
			Debug.Log("speed has been changed");
			//플래그 설정, 해당 값은 Player 스크립트의 
			//OnTriggerEnter 2D 에서 사용된다.
			flag = 25;
		}
		else if (ranIdx == 26) //플레이어, 고양이 그림자 삭제
		{
			Debug.Log("Shadow deleted");
			//플래그 설정, 해당 값은 Player 스크립트의 
			//OnTriggerEnter 2D 에서 사용된다.
			flag = 26;
		}
		else if (ranIdx == 27) //고양이 스프라이트 변경 현상
		{
			Debug.Log("Change Cat Sprite");
			//catAnimationController 스크립트에서 참조된다.
			flag = 27;
		}
	}
}
