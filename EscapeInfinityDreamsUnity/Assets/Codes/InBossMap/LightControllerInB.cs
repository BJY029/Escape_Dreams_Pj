using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControllerInB : MonoBehaviour
{
    public Light2D GlobalLight;
    public Light2D[] AllLights;
    public Light2D PlayerLight;

    public float fadeDuration = 2.0f;
    public float GlobalFadeDuration = 4.0f;

    public SpriteRenderer Player;

    private float GlobalLightIntensity;
    private float PlayerLightIntensity;
    private float[] AllLightsIntensity;

	private void Awake()
	{
	    GlobalLightIntensity = GlobalLight.intensity;
        PlayerLightIntensity = PlayerLight.intensity;

        AllLightsIntensity = new float[AllLights.Length];
        for(int i = 0; i < AllLightsIntensity.Length; i++)
        {
            AllLightsIntensity[i] = AllLights[i].intensity;
        }
	}

    public IEnumerator FadeGlobalLight()
    {
        float elaspedTime = 0f;
		float TargetIntensity = 0f;

		GlobalLight.intensity = GlobalLightIntensity;

		while (elaspedTime < GlobalFadeDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(GlobalLightIntensity, TargetIntensity, elaspedTime / fadeDuration);

			elaspedTime += Time.deltaTime;
			yield return null;
		}

		GlobalLight.intensity = TargetIntensity;
	}

    public IEnumerator PlayerDeadLight()
    {
        float elaspedTime = 0f;
        float TargetIntensity = 0f;
        float PlayerLightTargetIntensity = 1.0f;

        GlobalLight.intensity = GlobalLightIntensity;
        for(int i = 0;i < AllLightsIntensity.Length;i++)
        {
            AllLights[i].intensity = AllLightsIntensity[i];
        }
        PlayerLight.intensity = PlayerLightIntensity;


        while(elaspedTime < fadeDuration)
        {
            GlobalLight.intensity = Mathf.Lerp(GlobalLightIntensity, TargetIntensity, elaspedTime / fadeDuration);
            for(int i = 0; i < AllLightsIntensity.Length; i++)
            {
                AllLights[i].intensity = Mathf.Lerp(AllLightsIntensity[i], TargetIntensity, elaspedTime / fadeDuration);
            }
            PlayerLight.intensity = Mathf.Lerp(PlayerLightIntensity, PlayerLightTargetIntensity, elaspedTime / fadeDuration);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        GlobalLight.intensity = TargetIntensity;
        for(int i = 0; i < AllLightsIntensity.Length; i++)
        {
            AllLights[i].intensity = TargetIntensity;
        }
        PlayerLight.intensity = PlayerLightTargetIntensity;
    }

    public IEnumerator InitLights()
    {
        float elapsedTime = 0f;
        float initialValue = 0f;
        PlayerLight.intensity = 0f;

        Color initialColor = Color.black;
        Color finalColor = Color.white;

        GlobalLight.intensity = initialValue;
        for(int i = 0;i < AllLightsIntensity.Length; i++)
        {
            AllLights[i].intensity = initialValue;
        }

        Player.color = initialColor;


        while(elapsedTime < fadeDuration)
        {
            GlobalLight.intensity = Mathf.Lerp(initialValue, GlobalLightIntensity, elapsedTime / fadeDuration);
            for(int i = 0;i<AllLightsIntensity.Length; i++)
            {
                AllLights[i].intensity = Mathf.Lerp(initialValue, AllLightsIntensity[i], elapsedTime / fadeDuration);
            }

            Player.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GlobalLight.intensity = GlobalLightIntensity;
        for(int i = 0; i < AllLightsIntensity.Length; i++)
        {
            AllLights[i].intensity = AllLightsIntensity[i];
        }

        Player.color = finalColor;
    }
}
