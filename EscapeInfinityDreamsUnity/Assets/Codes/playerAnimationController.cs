using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerAnimationController : MonoBehaviour
{
    Animator animator;
	public GameObject PlayerRespawnLocation;
	public GameObject CatRespawnLocation;
	public bool playerDeadCoroutine;
	public bool canRespawn;
	public bool isRespawning;

	private CinemachineBrain CinemachineBrain;
	public CinemachineVirtualCameraBase targetCamera;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		CinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
		canRespawn = false;
	}

	//�ִϸ��̼� ���� ������ ���� �Լ� ����
	public void Dead()
	{
		animator.SetBool("IsAlive", false);
	}

	private void Update()
	{
		//(�ӽ�) XŰ�� ������ �÷��̾�� ����Ѵ�.
		if (Input.GetKeyDown(KeyCode.X))
		{
			//���࿡ ħ��� ��ȣ ���̸�, ��� Ű �ߵ��� �����ϴ� ���ǹ�, �߰��� ���� UI�� ��ȣ�ۿ� ���϶��� �ߵ��� �����Ѵ�.
			if (GameManager.Instance.uiSystem.isBedCoroutineRunning == true || GameManager.Instance.uiSystem.isPaperisVisualable == true) return;

			if (animator.GetBool("IsAlive") == true)
			{
				GameManager.Instance.playerController.flag = 0f;
				Dead(); //���� ���� �Լ� ȣ��
						//���� �ڷ�ƾ ����
				StartCoroutine(PlayerDeadRoutine());

			}
		}

		//���� �÷��̾ ���� ����� �����̰�, �������� ������ ����(PlayerDeadRouine �ڷ�ƾ�� ���� ����)�� ��쿡 rŰ�� ������
		if (Input.GetKeyDown(KeyCode.R) && canRespawn == true)
		{
			StartCoroutine(RespawnRoutine());
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)  
    {
        if (collision.CompareTag("enemy"))     //���� �浹���� ��, ����Ѵ�.
        {
            if (animator.GetBool("IsAlive") == true)
            {
                GameManager.Instance.playerController.flag = 0f;
                Dead(); //���� ���� �Լ� ȣ��
                        //���� �ڷ�ƾ ����
                StartCoroutine(PlayerDeadRoutine());
            }
        }
    }

    //�÷��̾� ��� �ڷ�ƾ
    public IEnumerator PlayerDeadRoutine()
	{
		animator.SetFloat("Speed", 0f); 
		//�ش� �ڷ�ƾ�� ����Ǵ� ������ �˸��� �÷���
		playerDeadCoroutine = true;

		//��ȣ�ۿ� UI�� Ȱ��ȭ �Ǿ�������, ��Ȱ��ȭ
		if (GameManager.Instance.uiSystem.interactionUI.activeSelf == true)
		{
			GameManager.Instance.uiSystem.interactionUI.SetActive(false);
		}

		//�� ȿ��
		yield return StartCoroutine(GameManager.Instance.lightController.DeadLightOut());

		//���� �ð� ����
		Time.timeScale = 0f;

		//����� ���� UI ǥ�� 
		GameManager.Instance.uiSystem.DeadStateUi.SetActive(true);

		canRespawn = true;
		//playerDeadCoroutine = false;
	}

	public IEnumerator RespawnRoutine()
	{
		isRespawning = true;

		//������ ��ġ�� �÷��̾�� ����� �̵�
		GameManager.Instance.player.transform.position = PlayerRespawnLocation.transform.position;
		GameManager.Instance.cat.transform.position = CatRespawnLocation.transform.position;

		//���� �̻������� �߻����� ���� ���¿��� �ڻ��� ���ϸ�, ������ ����ϰ�, ���� �ܰ�� �����Ѵ�.
		if (GameManager.Instance.isAbnormal == false)
		{
			if (GameManager.level != 0) //���� 0�϶� �ڻ��� �õ��ϸ� ���� �ܰ�� �Ѿ �� ����.
			{
				GameManager.level += 1;
				GameManager.Instance.abnorbalManager.nextStage();
			}
		}
		else//�̻� ������ �߻��ߴµ�, �ڻ��� ���ϸ�, ���з� ����, ������ �ʱ�ȭ�Ѵ�.
		{
			GameManager.level = 0;
			GameManager.Instance.abnorbalManager.Init();
		}

		//������ ���� UI ��Ȱ��ȭ
		GameManager.Instance.uiSystem.DeadStateUi.SetActive(false);

		//���� Ȱ��ȭ�� ���� ī�޶� ��������
		CinemachineVirtualCameraBase activeVirtualCamera = CinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
		//Ȱ��ȭ�� ���� ī�޶��� �켱���� ���߱�
		activeVirtualCamera.Priority = 0;
		//Room_0 ī�޶� �켱���� �÷��� �������� ����
		targetCamera.Priority = 1;

		//�ִϸ��̼��� ���� �Ķ���� �ʱ�ȭ
		animator.SetBool("IsAlive", true);


		//�ð� ���� �۵�
		Time.timeScale = 1f;


		//�� ȿ�� �ڷ�ƾ ȣ��
		yield return(StartCoroutine(GameManager.Instance.lightController.InitAllLights()));

		//���� ������ ���� cat��ȭâ ������Ʈ Ȱ��ȭ
		GameManager.Instance.catDialogController.gameObject.SetActive(true);
		//��ȭ â�� ����ϴ� �ڷ�ƾ ȣ��
		yield return StartCoroutine(GameManager.Instance.catDialogController.dialogController());

		//���� �÷��� �ʱ�ȭ
		canRespawn = false;
		playerDeadCoroutine = false;
		isRespawning = false;

		yield return null;
	}
}
