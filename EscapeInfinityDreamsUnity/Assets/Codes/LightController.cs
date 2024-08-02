using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D GlobalLight; //�� ��ü�� ���� �Ѱ��ϴ� GlobalLight
    public Light2D RoomLight; //Room0�� ���� ����ϴ� �� ������Ʈ
    public Light2D PlayerLight; //�÷��̾� ���� ��
    public Light2D[] AllLights; //��� �� ������Ʈ
    public float fadeDuration = 1.0f; //���� �����ų� ������ �ð�
	public float AllLightOutDuration = 2.0f; //���� �����ų� ������ �ð�

	//���ϴ� ���� �°� �� ��������Ʈ�� ���� ������ ���� ����
	public SpriteRenderer Player;
    public SpriteRenderer Cat;
	public Color targetColor = Color.white;

    //�� ���� �ʱ�ȭ ��
    private float GlobalLightIntensity;
    private float RoomLightIntensity;
	private float[] LightsInit; //��� ������ ������ ����Ʈ(GlobalLight ����)

	private void Awake()
	{
		GlobalLightIntensity = GlobalLight.intensity;
        RoomLightIntensity = RoomLight.intensity;

        //��� ������ �ʱ�ȭ ���� �Է¹ޱ� ���� ���� �� ����
        LightsInit = new float[AllLights.Length];
        for(int i = 0; i < AllLights.Length; i++)
        {
            LightsInit[i] = AllLights[i].intensity;
        }
	}


	//���� ������ �ڷ�ƾ
	public IEnumerator FadeInLight()
    {
        float elapsedTime = 0f; //���� �ð� �ʱ�ȭ
        float initialIntensity = 0f;   //���� �ʱ�ȭ ��
        float GtargetIntensity = GlobalLightIntensity; //GlobalLight�� ��ǥ �� ��
        float RtargetIntensity = RoomLightIntensity; //RoomLight�� ��ǥ �� ��

        //ó�� ����(������)
        Color initialColor = Color.black;
        //��ǥ ����(���)
        Color finalColor = targetColor;

        //�� ���� ���� �ʱ�ȭ
        GlobalLight.intensity = initialIntensity;  
        RoomLight.intensity = initialIntensity;
        //�� ��������Ʈ ���� �ʱ�ȭ
        Player.color = initialColor;
        Cat.color = initialColor;

        //������ �ð��� �� �� ����
        while(elapsedTime < fadeDuration)
        {
            //�� ���� ���� ������Ʈ
            GlobalLight.intensity = Mathf.Lerp(initialIntensity, GtargetIntensity, elapsedTime / fadeDuration);
            RoomLight.intensity = Mathf.Lerp(initialIntensity, RtargetIntensity, elapsedTime / fadeDuration);

            //�� ��������Ʈ�� ���� ������Ʈ
            Player.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);
			Cat.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
            yield return null;
        }

        //������ �ð��� �Ǹ� �� ���� ��ǥ ������ ����
        GlobalLight.intensity = GtargetIntensity;
        RoomLight.intensity = RtargetIntensity;

        //������ �ð��� �Ǹ� �� ��������Ʈ ������ ��ǥ ������ ���� ����
        Player.color = finalColor;
        Cat.color = finalColor;
    }

    //���� ������ �ڷ�ƾ(ĳ���Ͱ� �ٽ� �ῡ �� �� �ߵ�)
    public IEnumerator FadeOutLight()
    {
        float elapsedTime = 0f; //���� �ð� �ʱ�ȭ
        float GInitialIntensity = GlobalLightIntensity; //GlobalLight�� �ʱ�ȭ ��
        float RInitialIntensity = RoomLightIntensity; //RoomLight�� �ʱ�ȭ ��
        float targetIntensity = 0f; //�� ���� ��ǥ ��

        //ó�� ����(�÷��̾� ����(���))
        Color initialColor = Player.color;
        //��ǥ ����(������)
        Color finalColor = Color.black;

        //������ �ð� ����
        while(elapsedTime < fadeDuration)
        {
            //�� ���� ���� ������Ʈ
            GlobalLight.intensity = Mathf.Lerp(GInitialIntensity, targetIntensity, elapsedTime / fadeDuration);
			RoomLight.intensity = Mathf.Lerp(RInitialIntensity, targetIntensity, elapsedTime / fadeDuration);

			//�� ��������Ʈ�� ���� ������Ʈ
			Player.color = Color.Lerp(initialColor, finalColor,elapsedTime / fadeDuration);
            Cat.color = Color.Lerp(initialColor, finalColor,elapsedTime/ fadeDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null;
		}

        //������ �ð��� �Ǹ�, �� ���� ��ǥ ������ ����
        GlobalLight.intensity = targetIntensity;
        RoomLight.intensity = targetIntensity;

		//������ �ð��� �Ǹ� �� ��������Ʈ ������ ��ǥ ������ ���� ����
		Player.color = finalColor;
		Cat.color = finalColor;
	}

    //�÷��̾� ����� ����Ǵ� �� ȿ��
	public IEnumerator DeadLightOut()
	{
        float elapsedTime = 0f; //���� �ð� �ʱ�ȭ
        float GInitialIntensity = GlobalLightIntensity; //GlobalLight �ʱ�ȭ ��
        float targetIntensity = 0f; //��ǥ ��
        float PlayerLightIntensity = 0f; //�÷��̾� ���� �� �ʱ�ȭ ��
        float PlayerLightTarget = RoomLightIntensity; //��ǥ ��

        //������ �ð�����
        while(elapsedTime < AllLightOutDuration)
        {
            //�� ������ ���� LERP�� ���� ������ ��ȭ
			GlobalLight.intensity = Mathf.Lerp(GInitialIntensity, targetIntensity, elapsedTime / fadeDuration);
            for(int i = 0; i < AllLights.Length; i++)
            {
                AllLights[i].intensity = Mathf.Lerp(LightsInit[i], targetIntensity, elapsedTime / fadeDuration);
            }
            PlayerLight.intensity = Mathf.Lerp(PlayerLightIntensity, PlayerLightTarget, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

        //���� �� ����
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

		//ó�� ����(������)
		Color initialColor = Color.black;
		//��ǥ ����(���)
		Color finalColor = targetColor;

		//�� �� �ʱ�ȭ
		GlobalLight.intensity = initialValue;
		for (int i = 0; i < AllLights.Length; i++)
		{
			AllLights[i].intensity = initialValue;
		}

		//�� ��������Ʈ ���� �ʱ�ȭ
		Player.color = initialColor;
		Cat.color = initialColor;

		while (elapsedTime < AllLightOutDuration)
		{
			GlobalLight.intensity = Mathf.Lerp(initialValue, GlobalLightIntensity, elapsedTime / fadeDuration);
			for (int i = 0; i < AllLights.Length; i++)
			{
				AllLights[i].intensity = Mathf.Lerp(initialValue, LightsInit[i], elapsedTime / fadeDuration);
			}

			//�� ��������Ʈ�� ���� ������Ʈ
			Player.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);
			Cat.color = Color.Lerp(initialColor, finalColor, elapsedTime / fadeDuration);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		//������ �ð��� �Ǹ� �� ���� ��ǥ ������ ����
		GlobalLight.intensity = GlobalLightIntensity;
		for (int i = 0; i < AllLights.Length; i++)
		{
			AllLights[i].intensity = LightsInit[i];
		}

		//������ �ð��� �Ǹ� �� ��������Ʈ ������ ��ǥ ������ ���� ����
		Player.color = finalColor;
		Cat.color = finalColor;
	}
}