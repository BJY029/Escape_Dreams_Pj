using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transfer : MonoBehaviour
{
    public GameObject targetObj;//�÷��̾�
    public string MainScene; // �̵��� ���� �̸�
    private bool canMoveScene; //���� �̵� ���� ����(������Ʈ�� �浹�� ��������)
    public GameObject interactionUI; // ����� ������ UI ������Ʈ(text_background)
    public Vector3 uiOffset; // UI ������Ʈ�� ��� ��ġ ����

	public string targetSceneName; // �˻��� ���� �̸��� ������ �� �ִ� ����
    public string currentSceneName;

	private void Start()
    {
        interactionUI.SetActive(false); //������ ��, �ؽ�Ʈ�� ����

		// ���� Ȱ��ȭ�� ���� �����ɴϴ�
		Scene currentScene = SceneManager.GetActiveScene();

		// ���� ���� �̸��� �����ɴϴ�
		currentSceneName = currentScene.name;
	}

    // Update is called once per frame
    void Update()
    {
        //E ��ư�� �����ְ�, canMoveScene�� true�� ���� ��ȯ�Ѵ�.
        if (Input.GetKeyDown(KeyCode.E) && canMoveScene == true)
        {
            interactionUI.SetActive(false); // �ؽ�Ʈ�� ����
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
        //�÷��̾ ������Ʈ�� �浹�ϸ� canMoveScene�� true�� �����Ѵ�.
        if (collision.CompareTag("Player"))  
        {
            targetObj = collision.gameObject;//�÷��̾��� ���� ������Ʈ�� ����
            canMoveScene = true;
            interactionUI.SetActive(true); // �ؽ�Ʈ�� ���̰� ��
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾�� ������Ʈ�� �浹�� ������ canMoveScene�� false�� �����Ѵ�.
        if(collision.CompareTag("Player") && currentSceneName == targetSceneName)
        {
            canMoveScene = false;
            interactionUI.SetActive(false); // �ؽ�Ʈ�� ����
        }
    }
}
