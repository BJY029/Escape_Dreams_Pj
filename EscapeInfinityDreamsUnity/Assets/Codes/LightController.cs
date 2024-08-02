using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D GlobalLight; //맵 전체의 빛을 총괄하는 GlobalLight
    public Light2D RoomLight; //Room0의 빛을 담당하는 빛 오브젝트
    public Light2D PlayerLight; //플레이어 개인 빛
    public Light2D[] AllLights; //모든 빛 오브젝트
    public float fadeDuration = 1.0f; //빛이 꺼지거나 켜지는 시간
	public float AllLightOutDuration = 2.0f; //빛이 꺼지거나 켜지는 시간

	//변하는 조명에 맞게 각 스프라이트의 색상 변경을 위한 선언
	public SpriteRenderer Player;
    public SpriteRenderer Cat;
	public Color targetColor = Color.white;

    //각 빛의 초기화 값
    private float GlobalLightIntensity;
    private float RoomLightIntensity;
	private float[] LightsInit; //모든 빛들을 포함한 리스트(GlobalLight 제외)

	private void Awake()
	{
		GlobalLightIntensity = GlobalLight.intensity;
        RoomLightIntensity = RoomLight.intensity;

        //모든 빛들의 초기화 값을 입력받기 위한 선언 및 대입
        LightsInit = new float[AllLights.Length];
        for(int i = 0; i < AllLights.Length; i++)
        {
            LightsInit[i] = AllLights[i].intensity;
        }
	}


	//빛이 켜지는 코루틴
	public IEnumerator FadeInLight()
    {
        float elapsedTime = 0f; //기준 시간 초기화
        float initialIntensity = 0f;   //빛의 초기화 값
        float GtargetIntensity = GlobalLightIntensity; //GlobalLight의 목표 빛 값
        float RtargetIntensity = RoomLightIntensity; //RoomLight의 목표 빛 값

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
        float GInitialIntensity = GlobalLightIntensity; //GlobalLight의 초기화 값
        float RInitialIntensity = RoomLightIntensity; //RoomLight의 초기화 값
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

    //플레이어 사망시 재생되는 빛 효과
	public IEnumerator DeadLightOut()
	{
        float elapsedTime = 0f; //기준 시간 초기화
        float GInitialIntensity = GlobalLightIntensity; //GlobalLight 초기화 값
        float targetIntensity = 0f; //목표 값
        float PlayerLightIntensity = 0f; //플레이어 개인 빛 초기화 값
        float PlayerLightTarget = RoomLightIntensity; //목표 값

        //설정한 시간동안
        while(elapsedTime < AllLightOutDuration)
        {
            //각 빛들의 값을 LERP을 통해 서서히 변화
			GlobalLight.intensity = Mathf.Lerp(GInitialIntensity, targetIntensity, elapsedTime / fadeDuration);
            for(int i = 0; i < AllLights.Length; i++)
            {
                AllLights[i].intensity = Mathf.Lerp(LightsInit[i], targetIntensity, elapsedTime / fadeDuration);
            }
            PlayerLight.intensity = Mathf.Lerp(PlayerLightIntensity, PlayerLightTarget, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

        //최총 값 설정
        GlobalLight.intensity = targetIntensity;
		for (int i = 0; i < AllLights.Length; i++)
		{
            AllLights[i].intensity = 0f;
		}
        PlayerLight.intensity = PlayerLightTarget;
	}

	public IEnumerator InitAllLights()
	{
		float elapsedTime = 0f;
		float initialValue = 0f;

		PlayerLight.intensity = 0f;

		//처음 색상(검은색)
		Color initialColor = Color.black;
		//목표 색상(흰색)
		Color finalColor = targetColor;

		//빛 값 초기화
		GlobalLight.intensity = initialValue;
		for (int i = 0; i < AllLights.Length; i++)
		{
			AllLights[i].intensity = initialValue;
		}

		//각 스프라이트 색상 초기화
		Player.color = initialColor;
		Cat.color = initialColor;

		while (elapsedTime < AllLightOutDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(initialValue, GlobalLightIntensity, elapsedTime / fadeDuration);
			for (int i = 0; i < AllLights.Length; i++)
			{
				AllLights[i].intensity = Mathf.Lerp(initialValue, LightsInit[i], elapsedTime / fadeDuration);
			}

			//각 스프라이트의 값을 업데이트
			Player.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);
			Cat.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		//설정된 시간이 되면 각 빛을 목표 값으로 설정
		GlobalLight.intensity = GlobalLightIntensity;
		for (int i = 0; i < AllLights.Length; i++)
		{
			AllLights[i].intensity = LightsInit[i];
		}

		//설정된 시간이 되면 각 스프라이트 색상을 목표 값으로 최종 설정
		Player.color = finalColor;
		Cat.color = finalColor;
	}
}