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
		//���� �ε��� ����
		randomIdx = Random.Range(0, grassWalkClips.Length);
		//������ �ε����� �ش�Ǵ� ����� Ŭ���� �÷��̾��� ����� �ҽ��� ����
		audioSource.clip = grassWalkClips[randomIdx];
		//���
		audioSource.Play();
		yield return null;
	}
}
