using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerInBMM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startScene());
    }

    IEnumerator startScene()
    {
        yield return StartCoroutine(GameManagerInBMM.Instance.LightController.FadeIn());

        GameManagerInBMM.Instance.DialogueController.gameObject.SetActive(true);
        yield return StartCoroutine(GameManagerInBMM.Instance.DialogueController.startSceneDialog());

        yield return StartCoroutine(GameManagerInBMM.Instance.LightController.FadeOut());

        SceneManager.LoadScene("MainScene");
	}
}
