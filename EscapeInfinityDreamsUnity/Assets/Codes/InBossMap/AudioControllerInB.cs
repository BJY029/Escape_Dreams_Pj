using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInB : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource forChased;

    //걷는 효과음 클립들을 저장할 리스트
    public AudioClip WareWolfRoar;
    public AudioClip[] walkClips;
	public AudioClip[] grassWalkClips;
	public AudioClip[] wolfWalkClips;
    public AudioClip[] attackClips;
    public AudioClip[] ObjClips;
    //걷는 효과음을 랜덤 재생 하기위한 선언
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

	//걷거나 뛸때 효과음을 재생하는 코루틴
	public IEnumerator playWalkSound()
    {
        //랜덤 인덱스 선정
        randomIdx = Random.Range(0, walkClips.Length);
        //선정된 인덱스에 해당되는 오디오 클립을 플레이어의 오디오 소스에 삽입
        GameManagerInB.instance.playerController.audioSource.clip = walkClips[randomIdx];
        //재생
        GameManagerInB.instance.playerController.audioSource.Play();
        yield return null;
	}

	public IEnumerator playGrassWalkSound()
	{
		//랜덤 인덱스 선정
		randomIdx = Random.Range(0, grassWalkClips.Length);
		//선정된 인덱스에 해당되는 오디오 클립을 플레이어의 오디오 소스에 삽입
		GameManagerInB.instance.playerController.audioSource.clip = grassWalkClips[randomIdx];
		//재생
		GameManagerInB.instance.playerController.audioSource.Play();
		yield return null;
	}

	public IEnumerator playWolfWalkSound()
	{
		//랜덤 인덱스 선정
		randomIdx = Random.Range(0, wolfWalkClips.Length);
        //각 콜라이더를 확인해서 늑대와 플레이어가 같은 방에 있을때만, 늑대 움직임 소리 재생하도록 설정
        if (GameManagerInB.instance.playerController.RoomFlag == GameManagerInB.instance.warewolfController.RoomFlag)
        {
            //선정된 인덱스에 해당되는 오디오 클립을 플레이어의 오디오 소스에 삽입
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
        //리스트 길이만큼
        while(idx < attackClips.Length)
        {
            //재생
            GameManagerInB.instance.warewolfController.audioSource.clip = attackClips[idx];
            GameManagerInB.instance.warewolfController.audioSource.Play();
            //해당 오디오 소스 길이만큼 대기
            yield return new WaitForSeconds(GameManagerInB.instance.warewolfController.audioSource.clip.length);
            //다음 재생
            idx++;
		}
        idx = 0;   
    }


}
