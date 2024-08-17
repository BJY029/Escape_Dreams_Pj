using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInBEGIN : MonoBehaviour
{
	public AudioClip[] grassWalkClips;
	public AudioSource audioSource;

	private int randomIdx;

	public IEnumerator playGrassWalkSound()
	{
		//랜덤 인덱스 선정
		randomIdx = Random.Range(0, grassWalkClips.Length);
		//선정된 인덱스에 해당되는 오디오 클립을 플레이어의 오디오 소스에 삽입
		audioSource.clip = grassWalkClips[randomIdx];
		//재생
		audioSource.Play();
		yield return null;
	}
}
