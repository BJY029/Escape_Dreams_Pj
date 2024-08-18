using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInB : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource forChased;

    //�ȴ� ȿ���� Ŭ������ ������ ����Ʈ
    public AudioClip WareWolfRoar;
    public AudioClip[] walkClips;
	public AudioClip[] grassWalkClips;
	public AudioClip[] wolfWalkClips;
    public AudioClip[] attackClips;
    public AudioClip[] ObjClips;
    //�ȴ� ȿ������ ���� ��� �ϱ����� ����
    private int randomIdx;
    private int idx = 0;

    public float fadeDuration = 3.0f;


    public void openingDoor()
    {
        for(int i = 0; i<audioSources.Length; i++) 
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = ObjClips[0];
                audioSources[i].Play();
                return;
			}
            
        }
    }

    public void openingAutoDoor()
    {
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (!audioSources[i].isPlaying)
			{
				audioSources[i].clip = ObjClips[1];
				audioSources[i].Play();
                return;
			}
			
		}
	}

	//�Ȱų� �۶� ȿ������ ����ϴ� �ڷ�ƾ
	public IEnumerator playWalkSound()
    {
        //���� �ε��� ����
        randomIdx = Random.Range(0, walkClips.Length);
        //������ �ε����� �ش�Ǵ� ����� Ŭ���� �÷��̾��� ����� �ҽ��� ����
        GameManagerInB.instance.playerController.audioSource.clip = walkClips[randomIdx];
        //���
        GameManagerInB.instance.playerController.audioSource.Play();
        yield return null;
	}

	public IEnumerator playGrassWalkSound()
	{
		//���� �ε��� ����
		randomIdx = Random.Range(0, grassWalkClips.Length);
		//������ �ε����� �ش�Ǵ� ����� Ŭ���� �÷��̾��� ����� �ҽ��� ����
		GameManagerInB.instance.playerController.audioSource.clip = grassWalkClips[randomIdx];
		//���
		GameManagerInB.instance.playerController.audioSource.Play();
		yield return null;
	}

	public IEnumerator playWolfWalkSound()
	{
		//���� �ε��� ����
		randomIdx = Random.Range(0, wolfWalkClips.Length);
        //�� �ݶ��̴��� Ȯ���ؼ� ����� �÷��̾ ���� �濡 ��������, ���� ������ �Ҹ� ����ϵ��� ����
        if (GameManagerInB.instance.playerController.RoomFlag == GameManagerInB.instance.warewolfController.RoomFlag)
        {
            //������ �ε����� �ش�Ǵ� ����� Ŭ���� �÷��̾��� ����� �ҽ��� ����
            GameManagerInB.instance.warewolfController.audioSource.clip = wolfWalkClips[randomIdx];
            GameManagerInB.instance.warewolfController.audioSource.Play();
        }
		yield return null;
	}

    public void PlayRoar()
    {
        GameManagerInB.instance.warewolfController.audioSource.clip = WareWolfRoar;
        GameManagerInB.instance.warewolfController.audioSource.Play();
	}

    public void PlayChased()
    {
        forChased.volume = 0.4f;
        forChased.Play();
    }

	public IEnumerator ChasedFadeOut()
	{
        float elaspedTime = 0;
        float Volume = forChased.volume;
		
        while(fadeDuration > elaspedTime)
        {
            forChased.volume = Mathf.Lerp(Volume, 0f, elaspedTime / fadeDuration);

            elaspedTime += Time.deltaTime;
            yield return null;
		}

        forChased.volume = 0f;
	}

	public IEnumerator attackPlay()
    {
        //����Ʈ ���̸�ŭ
        while(idx < attackClips.Length)
        {
            //���
            GameManagerInB.instance.warewolfController.audioSource.clip = attackClips[idx];
            GameManagerInB.instance.warewolfController.audioSource.Play();
            //�ش� ����� �ҽ� ���̸�ŭ ���
            yield return new WaitForSeconds(GameManagerInB.instance.warewolfController.audioSource.clip.length);
            //���� ���
            idx++;
		}
        idx = 0;   
    }


}
