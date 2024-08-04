using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public bool SceneisStarting;
    void Start()
    {
        //게임이 시작되면 호출된다.
        StartCoroutine(SceneStart());
	}

    IEnumerator SceneStart()
    {
        //캐릭터 이동 방지를 위한 플래그 설정
        SceneisStarting = true;
        //LightIn 코루틴을 호출하여 라이트가 들어오는 효과 재생
		yield return StartCoroutine(GameManager.Instance.lightController.FadeInLight());
        //다시 이동이 가능하도록 플래그 초기화
        SceneisStarting = false;

        //대화창 노출 작업 진행
		GameManager.Instance.catDialogController.gameObject.SetActive(true);
		yield return StartCoroutine(GameManager.Instance.catDialogController.dialogController());
	}
}
