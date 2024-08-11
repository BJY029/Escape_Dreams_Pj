using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public AudioClip[] walkClips;
	public AudioClip[] clips; //����� ���ҽ� ����Ʈ
	public AudioSource[] m_AudioSources; //����� ����ϴ� audioSource ����Ʈ

	private int randIdx;

	public void PlayDoorOpenSound() //�� ���� �� ȣ���ϴ� �Լ�
	{
		//�ش� �ݺ����� ���� ��������� �ʴ� AudioSource�� ã�´�.
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//��������� ���� AudioSource�� �ش� Ŭ���� ���� ��
				m_AudioSources[i].clip = clips[0];
				m_AudioSources[i].Play(); //����Ѵ�.
				break;
			}
		}
	}

	public void PlayWindoeKnocking() //â�� ���� �̻����� ȣ�� �Լ�
	{
		//�ش� �ݺ����� ���� ��������� �ʴ� AudioSource�� ã�´�.
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//��������� ���� AudioSource�� �ش� Ŭ���� ���� ��
				m_AudioSources[i].clip = clips[1];
				m_AudioSources[i].Play();//����Ѵ�.
				break;
			}
		}
	}

	//�� ���� �Ÿ��� ȿ���� ��� �Լ�
	public void PlayDoorKnocking()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//��������� ���� AudioSource�� �ش� Ŭ���� ���� ��
				m_AudioSources[i].clip = clips[2];
				m_AudioSources[i].Play();//����Ѵ�.
				break;
			}
		}
	}

	//���� ��� ȿ���� ��� �Լ�
	public void PlayCryAudio()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//��������� ���� AudioSource�� �ش� Ŭ���� ���� ��
				m_AudioSources[i].clip = clips[4];
				m_AudioSources[i].Play();//����Ѵ�.
				break;
			}
		}
	}

	//���� ����� �� ����ϴ� �Լ�
	public void PlayDoorLocked()
	{
		for (int i = 0; i < m_AudioSources.Length; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				//��������� ���� AudioSource�� �ش� Ŭ���� ���� ��
				m_AudioSources[i].clip = clips[3];
				m_AudioSources[i].Play();//����Ѵ�.
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
				//��� ���� ��� ����� ȿ�� ����
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
