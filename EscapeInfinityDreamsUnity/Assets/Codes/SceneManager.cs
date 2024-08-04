using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public bool SceneisStarting;
    void Start()
    {
        //������ ���۵Ǹ� ȣ��ȴ�.
        StartCoroutine(SceneStart());
	}

    IEnumerator SceneStart()
    {
        //ĳ���� �̵� ������ ���� �÷��� ����
        SceneisStarting = true;
        //LightIn �ڷ�ƾ�� ȣ���Ͽ� ����Ʈ�� ������ ȿ�� ���
		yield return StartCoroutine(GameManager.Instance.lightController.FadeInLight());
        //�ٽ� �̵��� �����ϵ��� �÷��� �ʱ�ȭ
        SceneisStarting = false;

        //��ȭâ ���� �۾� ����
		GameManager.Instance.catDialogController.gameObject.SetActive(true);
		yield return StartCoroutine(GameManager.Instance.catDialogController.dialogController());
	}
}
