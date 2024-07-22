using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D GlobalLight; //맵 전체의 빛을 총괄하는 GlobalLight
    public Light2D RoomLight; //Room0의 빛을 담당하는 빛 오브젝트
    public float fadeDuration = 1.0f; //빛이 꺼지거나 켜지는 시간

    //변하는 조명에 맞게 각 스프라이트의 색상 변경을 위한 선언
    public SpriteRenderer Player;
    public SpriteRenderer Cat;
	public Color targetColor = Color.white;

    void Start()
    {
        StartCoroutine(FadeInLight()); //게임 시작시 빛이 켜지는 코루틴 실행
    }

    //빛이 켜지는 코루틴
    public IEnumerator FadeInLight()
    {
        float elapsedTime = 0f; //기준 시간 초기화
        float initialIntensity = 0f;   //빛의 초기화 값
        float GtargetIntensity = GlobalLight.intensity; //GlobalLight의 목표 빛 값
        float RtargetIntensity = RoomLight.intensity; //RoomLight의 목표 빛 값

        //처음 색상(검은색)
        Color initialColor = Color.black;
        //목표 색상(흰색)
        Color finalColor = targetColor;

        //각 빛의 값을 초기화
        GlobalLight.intensity = initialIntensity;  
        RoomLight.intensity = initialIntensity;
        //각 스프라이트 색상 초기화
        Player.color = initialColor;
        Cat.color = initialColor;

        //설정된 시간이 될 때 까지
        while(elapsedTime < fadeDuration)
        {
            //각 빛의 값을 업데이트
            GlobalLight.intensity = Mathf.Lerp(initialIntensity, GtargetIntensity, elapsedTime / fadeDuration);
            RoomLight.intensity = Mathf.Lerp(initialIntensity, RtargetIntensity, elapsedTime / fadeDuration);

            //각 스프라이트의 값을 업데이트
            Player.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);
			Cat.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
            yield return null;
        }

        //설정된 시간이 되면 각 빛을 목표 값으로 설정
        GlobalLight.intensity = GtargetIntensity;
        RoomLight.intensity = RtargetIntensity;

        //설정된 시간이 되면 각 스프라이트 색상을 목표 값으로 최종 설정
        Player.color = finalColor;
        Cat.color = finalColor;
    }

    //빛이 꺼지는 코루틴(캐릭터가 다시 잠에 들 때 발동)
    public IEnumerator FadeOutLight()
    {
        float elapsedTime = 0f; //기준 시간 초기화
        float GInitialIntensity = GlobalLight.intensity; //GlobalLight의 초기화 값
        float RInitialIntensity = RoomLight.intensity; //RoomLight의 초기화 값
        float targetIntensity = 0f; //각 빛의 목표 값

        //처음 색상(플레이어 색상(흰색))
        Color initialColor = Player.color;
        //목표 색상(검은색)
        Color finalColor = Color.black;

        //설정한 시간 동안
        while(elapsedTime < fadeDuration)
        {
            //각 빛의 값을 업데이트
            GlobalLight.intensity = Mathf.Lerp(GInitialIntensity, targetIntensity, elapsedTime / fadeDuration);
			RoomLight.intensity = Mathf.Lerp(RInitialIntensity, targetIntensity, elapsedTime / fadeDuration);

			//각 스프라이트의 값을 업데이트
			Player.color = Color.Lerp(initialColor, finalColor,elapsedTime / fadeDuration);
            Cat.color = Color.Lerp(initialColor, finalColor,elapsedTime/ fadeDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
		}

        //설정한 시간이 되면, 각 빛을 목표 값으로 설정
        GlobalLight.intensity = targetIntensity;
        RoomLight.intensity = targetIntensity;

		//설정된 시간이 되면 각 스프라이트 색상을 목표 값으로 최종 설정
		Player.color = finalColor;
		Cat.color = finalColor;
	}
}
