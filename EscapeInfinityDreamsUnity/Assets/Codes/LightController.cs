using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D GlobalLight; //�� ��ü�� ���� �Ѱ��ϴ� GlobalLight
    public Light2D RoomLight; //Room0�� ���� ����ϴ� �� ������Ʈ
    public float fadeDuration = 1.0f; //���� �����ų� ������ �ð�

    //���ϴ� ���� �°� �� ��������Ʈ�� ���� ������ ���� ����
    public SpriteRenderer Player;
    public SpriteRenderer Cat;
	public Color targetColor = Color.white;

    void Start()
    {
        StartCoroutine(FadeInLight()); //���� ���۽� ���� ������ �ڷ�ƾ ����
    }

    //���� ������ �ڷ�ƾ
    public IEnumerator FadeInLight()
    {
        float elapsedTime = 0f; //���� �ð� �ʱ�ȭ
        float initialIntensity = 0f;   //���� �ʱ�ȭ ��
        float GtargetIntensity = GlobalLight.intensity; //GlobalLight�� ��ǥ �� ��
        float RtargetIntensity = RoomLight.intensity; //RoomLight�� ��ǥ �� ��

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
        float GInitialIntensity = GlobalLight.intensity; //GlobalLight�� �ʱ�ȭ ��
        float RInitialIntensity = RoomLight.intensity; //RoomLight�� �ʱ�ȭ ��
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
}
