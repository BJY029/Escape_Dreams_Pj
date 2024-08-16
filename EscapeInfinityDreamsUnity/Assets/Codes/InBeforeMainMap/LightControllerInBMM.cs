using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControllerInBMM : MonoBehaviour
{
	public Light2D GlobalLight;
	public Light2D RoomLight;

	public float fadeDuration = 3.0f;

	public SpriteRenderer Player;
	public SpriteRenderer Doctor;
	public Color targetColor = Color.white;

	private float GlobalLightIntensity;
	private float RoomLightIntensity;

	private void Awake()
	{
		GlobalLightIntensity = GlobalLight.intensity;
		RoomLightIntensity = RoomLight.intensity;
	}

	public IEnumerator FadeIn()
	{
		float elaspedTime = 0f;
		float GlobalTargetIntensity = GlobalLightIntensity;
		float RoomTargetIntensity = RoomLightIntensity;

		Color initialColor = Color.black;
		Color finalColor = targetColor;

		GlobalLight.intensity = 0f;
		RoomLight.intensity = 0f;

		Player.color = initialColor;
		Doctor.color = initialColor;

		while(elaspedTime < fadeDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(0f, GlobalTargetIntensity, elaspedTime/fadeDuration);
			RoomLight.intensity = Mathf.Lerp(0f, RoomTargetIntensity, elaspedTime / fadeDuration);

			Player.color = Color.Lerp(initialColor, finalColor, elaspedTime/fadeDuration);
			Doctor.color = Color.Lerp(initialColor, finalColor,	elaspedTime/fadeDuration);

			elaspedTime += Time.deltaTime;
			yield return null;
		}

		GlobalLight.intensity = GlobalLightIntensity;
		RoomLight.intensity = RoomLightIntensity;

		Player.color = finalColor;
		Doctor.color = finalColor;
	}

	public IEnumerator FadeOut()
	{
		float elaspedTime = 0f;
		float TargetIntensity = 0f;

		float NowGIntensity = GlobalLight.intensity;
		float NowRIntensity = RoomLight.intensity;

		Color NowPlayerColor = Player.color;
		Color NowDoctorColor = Doctor.color;
		Color targetColor = Color.black;
		
		while (elaspedTime < fadeDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(NowGIntensity, TargetIntensity, elaspedTime/fadeDuration);
			RoomLight.intensity = Mathf.Lerp(NowRIntensity, TargetIntensity, elaspedTime / fadeDuration);

			Player.color = Color.Lerp(NowPlayerColor, targetColor, elaspedTime / fadeDuration);
			Doctor.color = Color.Lerp(NowDoctorColor, targetColor, elaspedTime/fadeDuration);

			elaspedTime += Time.deltaTime;
			yield return null;
		}

		GlobalLight.intensity = TargetIntensity;
		RoomLight.intensity = TargetIntensity;

		Player.color = targetColor;
		Doctor.color = targetColor;
	}

	public IEnumerator HalfFadeOut()
	{
		float elaspedTime = 0f;
		float GTargetIntensity = GlobalLightIntensity / 2;
		float RTargetIntensity = RoomLightIntensity / 2;

		Color NowPlayerColor = Player.color;
		Color NowDoctorColor = Doctor.color;
		Color targetColor = Color.gray;

		while (elaspedTime < fadeDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(GlobalLightIntensity, GTargetIntensity, elaspedTime / fadeDuration);
			RoomLight.intensity = Mathf.Lerp(RoomLightIntensity, RTargetIntensity, elaspedTime / fadeDuration);

			Player.color = Color.Lerp(NowPlayerColor, targetColor, elaspedTime / fadeDuration);
			Doctor.color = Color.Lerp(NowDoctorColor, targetColor, elaspedTime / fadeDuration);

			elaspedTime += Time.deltaTime;
			yield return null;
		}

		GlobalLight.intensity = GTargetIntensity;
		RoomLight.intensity = RTargetIntensity;

		Player.color = targetColor;
		Doctor.color = targetColor;
	}
}
