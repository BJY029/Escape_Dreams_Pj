using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateControllerInB : MonoBehaviour
{
    Animator animator;
    public GameObject PlayerRespawnLocation;

    private CinemachineBrain CinemachineBrain;
    public CinemachineVirtualCameraBase targetCamera;
	public bool isRespawning;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		CinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

		isRespawning = false;
	}

	private void Update()
	{
		//�÷��̾ ������ �� �� �ִ� �����϶��� �۵��Ѵ�.
		if(Input.GetKeyDown(KeyCode.R) && GameManagerInB.instance.warewolfController.canRespawn == true)
		{
			StartCoroutine(Respawn());
		}
	}

	//������ �ڷ�ƾ
	IEnumerator Respawn()
	{
		//�÷��� ����
		isRespawning = true;

		//�÷��̾� ��ġ �̵�
		GameManagerInB.instance.player.transform.position = PlayerRespawnLocation.transform.position;

		//UI ��Ȱ��ȭ
		GameManagerInB.instance.UIControllerInB.nonAcviteDeadStateUI();

		//ī�޶� �켱���� ����
		CinemachineVirtualCameraBase activeVituralCamera = CinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
		activeVituralCamera.Priority = 0;
		targetCamera.Priority = 1;

		//�ִϸ��̼� �ʱ�ȭ
		animator.SetBool("IsAlive", true);

		//�ð� �ٽ� �帣��
		Time.timeScale = 1f;

		//���� �۾� �ʱ�ȭ
		GameManagerInB.instance.warewolfController.InitAll();
		GameManagerInB.instance.playerController.InitAll();

		//fade in ����Ʈ �ڷ�ƾ �� ����
		yield return StartCoroutine(GameManagerInB.instance.lightControllerInB.InitLights());

		//���� �÷��� �ʱ�ȭ
		GameManagerInB.instance.warewolfController.isExecuting = false;
		GameManagerInB.instance.warewolfController.canRespawn = false;
		isRespawning = false;

		yield return null;
	}
}
