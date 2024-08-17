using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transfer : MonoBehaviour
{
    public GameObject targetObj;//플레이어
    public string MainScene; // 이동할 씬의 이름
    private bool canMoveScene; //씬의 이동 가능 여부(오브젝트와 충돌한 상태인지)
    public GameObject interactionUI; // 배경을 포함한 UI 오브젝트(text_background)
    public Vector3 uiOffset; // UI 오브젝트의 출력 위치 조정

	public string targetSceneName; // 검사할 씬의 이름을 설정할 수 있는 변수
    public string currentSceneName;

	private void Start()
    {
        interactionUI.SetActive(false); //시작할 때, 텍스트를 숨김

		// 현재 활성화된 씬을 가져옵니다
		Scene currentScene = SceneManager.GetActiveScene();

		// 현재 씬의 이름을 가져옵니다
		currentSceneName = currentScene.name;
	}

    // Update is called once per frame
    void Update()
    {
        //E 버튼이 눌려있고, canMoveScene이 true면 씬을 전환한다.
        if (Input.GetKeyDown(KeyCode.E) && canMoveScene == true)
        {
            interactionUI.SetActive(false); // 텍스트를 숨김
            SceneManager.LoadScene(MainScene);
            canMoveScene = false;
        }
        if (canMoveScene == true)
        {
            interactionUI.transform.position = targetObj.transform.position + uiOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 오브젝트와 충돌하면 canMoveScene을 true로 설정한다.
        if (collision.CompareTag("Player"))  
        {
            targetObj = collision.gameObject;//플레이어의 게임 오브젝트를 받음
            canMoveScene = true;
            interactionUI.SetActive(true); // 텍스트를 보이게 함
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어와 오브젝트의 충돌이 끝나면 canMoveScene을 false로 설정한다.
        if(collision.CompareTag("Player") && currentSceneName == targetSceneName)
        {
            canMoveScene = false;
            interactionUI.SetActive(false); // 텍스트를 숨김
        }
    }
}
