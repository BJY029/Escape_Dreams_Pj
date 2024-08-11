using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInB : MonoBehaviour
{
    public AudioClip[] walkClips;
    private int randomIdx;

    public IEnumerator playWalkSound()
    {
        randomIdx = Random.Range(0, walkClips.Length);
        GameManagerInB.instance.playerController.audioSource.clip = walkClips[randomIdx];
        GameManagerInB.instance.playerController.audioSource.Play();
        yield return null;
	}
}
