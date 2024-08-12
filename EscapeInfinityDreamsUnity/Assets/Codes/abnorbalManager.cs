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
	public int ranIdx;
	public int flag;

	//초기화 함수
	public void Init()
	{
		flag = 0;

		//Player의 속성과 다른 것들의 모든 이상현상들을 제거
		GameManager.Instance.playerController.InitAll();
		//고양이 스프라이트 복귀
		GameManager.Instance.catAnimationController.BackAnimatorController();

		//텍스트 할당
		Text PaperText = abnormals[19].GetComponent<Text>();
		Light2D GlobalLight = abnormals[24].GetComponent<Light2D>();
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
		abnormals[16].SetActive(false) ;	//새로운 밴트 삭제
		abnormals[17].SetActive(true) ;     //기존 벤트 활성화
		abnormals[18].SetActive(true);      //포스터 활성화
		abnormals[20].SetActive(false);      //포스터2 비활성화
        abnormals[21].SetActive(false);      //방의 스위치 비활성화
        abnormals[22].SetActive(true);      //방의 작은 의자 활성화
        abnormals[23].SetActive(false);     //방의 포스터 비활성화
        abnormals[32].SetActive(false);     //방의 적 비활성화
        abnormals[33].SetActive(false);     //복도의 적 비활성화

        //텍스트 초기화 작업
        PaperText.text = "우리 병원은.... 환자들의 건강을 최우선으로 생각합니다.\n우리 병원은.... 최고의 시설을 자랑합니다.\n우리 병원은.... 웃음으로 가득 차 있습니다.";
		PaperText.color = Color.black;

		GlobalLight.color = Color.white;
	}

	//다음 스테이지 이동 함수
	public void nextStage()
	{
		//현재 레벨을 알려주는 디버그
		Debug.Log("Level : " + GameManager.level);


		//초기화 진행
		Init();

		//레벨 중복 방지 시스템
		do
		{
			//랜덤 인덱스 생성
			ranIdx = Random.Range(0, abnormals.Count);
		} while (!GameManager.Instance.LevelSystem(ranIdx));
		//만약 해당 함수에서 true가 반환되면 중복된 인덱스가 아닌것이므로, 반복문을 빠져나오고 게임이 진행된다.
		//만약 해당 함수에서 false가 반환되면 중복된 인덱스이므로, 반복문을 돌아 새로운 랜덤 인덱스를 다시 뽑는다.

		//레벨 8이면 아무것도 하지 않고 리턴한다.
		if (GameManager.level == 8) return;

		//ranIdx = 26; //이상현상 정상 작동 테스트 용

		//참고 : 이상현상 리스트의 33~38까지는 더미로, 아무 이상현상이 발생하지 않는 확률의 수를 높이기 위함임
		
		//오류 방지 코드
		if (abnormals[ranIdx] == null)
		{
			return;
		}

		//아무것도 변하지 않은 경우
		if (ranIdx >= 0 && ranIdx <= 1 || ranIdx >= 34 && ranIdx <= 39)
		{
			GameManager.Instance.isAbnormal = false;//이상 현상이 발생되지 않았기에 false로 초기화
			Debug.Log("Not Changed(Original Map");
		}
		else {
			//이상 현상 발생
			GameManager.Instance.isAbnormal = true; //이상현상 플래그 설정
			if (ranIdx >= 2 && ranIdx <= 15)
			{
				//해당 오브젝트들의 상태 변화
				abnormals[ranIdx].SetActive(true);
				originals[ranIdx].SetActive(false);
				Debug.Log(ranIdx + " has been changed");
			}
			else if (ranIdx == 16) //사물함 위 새로운 벤트 생성 현상
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("New Vent has been actived");
			}
			else if (ranIdx == 17) //자판기 위 벤트 비활성화
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Vent has been unactived");
			}
			else if (ranIdx == 18) //벽에 포스터 비활성화 현상
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Posters has been unactived");
			}
			else if (ranIdx == 19) //종이 텍스트 바뀌는 현상
			{
				Text PaperText = abnormals[19].GetComponent<Text>();
				PaperText.text = "정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아정신나갈것같아";
				PaperText.color = Color.red;
				Debug.Log("Paper Text has been changed");
			}
			else if (ranIdx == 20) //포스터들이 바뀌는 현상
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("Posters has been actived");
			}
			else if (ranIdx == 21) //방에 스위치가 생성되는 현상
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("Switch has been actived");
			}
			else if (ranIdx == 22) //방의 작은 의자가 사라지는 현상
			{
				abnormals[ranIdx].SetActive(false);
				Debug.Log("Small chair has been actived");
			}
			else if (ranIdx == 23) //방에 포스터를 생성하는 이상현상
			{
				abnormals[ranIdx].SetActive(true);
				Debug.Log("room posters has been actived");
			}
			else if (ranIdx == 24) //빛 색 바뀌는 현상
			{
				Debug.Log("Lights has been changed");
				//플래그 설정, 해당 값은 Player 스크립트의 
				//OnTriggerEnter 2D 에서 사용된다.
				flag = 24;
				////플래그가 사용된 부분을 수정해야 할 때, Player와 cat 스크립트의 OnTriggerEnter 2D를 수정 해야 함. 
				///추가로 해당 플래그로 코드 내에서도 구분 짓는 요소가 있을 수 있음
			}
			else if (ranIdx == 25) //좌우 반전 컨트롤
			{
				Debug.Log("a,d has been changed");
				//플래그 설정, 해당 값은 Player 스크립트의 
				//OnTriggerEnter 2D 에서 사용된다.
				flag = 25;
			}
			else if (ranIdx == 26) //플레이어 이동 속도 변화
			{
				Debug.Log("speed has been changed");
				//플래그 설정, 해당 값은 Player 스크립트의 
				//OnTriggerEnter 2D 에서 사용된다.
				flag = 26;
			}
			else if (ranIdx == 27) //플레이어, 고양이 그림자 삭제
			{
				Debug.Log("Shadow deleted");
				//플래그 설정, 해당 값은 Player 스크립트의 
				//OnTriggerEnter 2D 에서 사용된다.
				flag = 27;
			}
			else if (ranIdx == 28) //고양이 스프라이트 변경 현상
			{
				Debug.Log("Change Cat Sprite");
				//catAnimationController 스크립트에서 참조된다.
				flag = 28;
			}
			else if (ranIdx == 29) //창문 노크 소리 재생
			{
				Debug.Log("Window Audio Play");
				//AudioController에서 참조된다.
				flag = 29;
			}
			else if (ranIdx == 30) //문 노크 소리 재생
			{
				Debug.Log("Door Audio Play");
				//AudioController에서 참조된다.
				flag = 30;
			}
			else if (ranIdx == 31) //우는 소리 재생
			{
				Debug.Log("cry Audio Play");
				//AudioController에서 참조된다.
				flag = 31;
			}
			else if(ranIdx == 32) //방의 적을 생성
			{
                abnormals[32].SetActive(true);
                Debug.Log("R_enemy actived");
			}
            else if (ranIdx == 33) //방의 적을 생성
            {
                abnormals[33].SetActive(true);
                Debug.Log("enemy actived");
            }
        }
	}
}
