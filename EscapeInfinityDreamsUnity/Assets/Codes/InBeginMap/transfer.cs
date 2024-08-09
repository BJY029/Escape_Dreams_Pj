using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transfer : MonoBehaviour
{
    public string MainScene; // 이동할 씬의 이름
    private bool canMoveScene; //씬의 이동 가능 여부(오브젝트와 충돌한 상태인지)


    // Update is called once per frame
    void Update()
    {
        //E 버튼이 눌려있고, canMoveScene이 true면 씬을 전환한다.
        if (Input.GetKeyDown(KeyCode.E) && canMoveScene == true)
        {
            Debug.Log("Move");
            SceneManager.LoadScene(MainScene);
            canMoveScene = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 오브젝트와 충돌하면 canMoveScene을 true로 설정한다.
        if (collision.CompareTag("Player"))  
        {
            Debug.Log("Enter");
            canMoveScene = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어와 오브젝트의 충돌이 끝나면 canMoveScene을 false로 설정한다.
        if(collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            canMoveScene = false;
        }
    }
}
