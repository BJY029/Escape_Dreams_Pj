using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public AudioClip[] walkClips;
	public AudioClip[] clips; //오디오 리소스 리스트
	public AudioSource[] m_AudioSources; //오디오 재생하는 audioSource 리스트

	private int randIdx;

	public void PlayDoorOpenSound() //문 열릴 때 호출하는 함수
	{
		//해당 반복문은 현재 재생중이지 않는 AudioSource를 찾는다.
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//재생중이지 않은 AudioSource에 해당 클립을 삽입 후
				m_AudioSources[i].clip = clips[0];
				m_AudioSources[i].Play(); //재생한다.
				break;
			}
		}
	}

	public void PlayWindoeKnocking() //창문 관련 이상현상 호출 함수
	{
		//해당 반복문은 현재 재생중이지 않는 AudioSource를 찾는다.
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//재생중이지 않은 AudioSource에 해당 클립을 삽입 후
				m_AudioSources[i].clip = clips[1];
				m_AudioSources[i].Play();//재생한다.
				break;
			}
		}
	}

	//문 쾅쾅 거리는 효과음 재생 함수
	public void PlayDoorKnocking()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//재생중이지 않은 AudioSource에 해당 클립을 삽입 후
				m_AudioSources[i].clip = clips[2];
				m_AudioSources[i].Play();//재생한다.
				break;
			}
		}
	}

	//여자 우는 효과음 재생 함수
	public void PlayCryAudio()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//재생중이지 않은 AudioSource에 해당 클립을 삽입 후
				m_AudioSources[i].clip = clips[4];
				m_AudioSources[i].Play();//재생한다.
				break;
			}
		}
	}

	//문이 잠겼을 때 재생하는 함수
	public void PlayDoorLocked()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//재생중이지 않은 AudioSource에 해당 클립을 삽입 후
				m_AudioSources[i].clip = clips[3];
				m_AudioSources[i].Play();//재생한다.
				break;
			}
		}
	}

	public void StopPlayAudio()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (m_AudioSources[i].isPlaying)
			{
				//재생 중인 모든 오디오 효과 정지
				m_AudioSources[i].Stop();
			}
		}
	}

	public IEnumerator playWalkSound()
	{
		randIdx = Random.Range(0, walkClips.Length);
		GameManager.Instance.playerController.AudioSource.clip = walkClips[randIdx];
		GameManager.Instance.playerController.AudioSource.Play();
		yield return null;
	}
}
