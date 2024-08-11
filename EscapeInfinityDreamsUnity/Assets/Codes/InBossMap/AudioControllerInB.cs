using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInB : MonoBehaviour
{
    //걷는 효과음 클립들을 저장할 리스트
    public AudioClip[] walkClips;
    public AudioClip[] wolfWalkClips;
    //걷는 효과음을 랜덤 재생 하기위한 선언
    private int randomIdx;

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
}
