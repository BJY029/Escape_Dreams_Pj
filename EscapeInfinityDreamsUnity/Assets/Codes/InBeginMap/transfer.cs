using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transfer : MonoBehaviour
{
    public string MainScene; // �̵��� ���� �̸�
    private bool canMoveScene; //���� �̵� ���� ����(������Ʈ�� �浹�� ��������)


    // Update is called once per frame
    void Update()
    {
        //E ��ư�� �����ְ�, canMoveScene�� true�� ���� ��ȯ�Ѵ�.
        if (Input.GetKeyDown(KeyCode.E) && canMoveScene == true)
        {
            Debug.Log("Move");
            SceneManager.LoadScene(MainScene);
            canMoveScene = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ ������Ʈ�� �浹�ϸ� canMoveScene�� true�� �����Ѵ�.
        if (collision.CompareTag("Player"))  
        {
            Debug.Log("Enter");
            canMoveScene = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾�� ������Ʈ�� �浹�� ������ canMoveScene�� false�� �����Ѵ�.
        if(collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            canMoveScene = false;
        }
    }
}
