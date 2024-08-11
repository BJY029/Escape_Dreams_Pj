using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInB : MonoBehaviour
{
    //�ȴ� ȿ���� Ŭ������ ������ ����Ʈ
    public AudioClip[] walkClips;
    public AudioClip[] wolfWalkClips;
    //�ȴ� ȿ������ ���� ��� �ϱ����� ����
    private int randomIdx;

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
}
