using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
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
			if (animator.GetBool("IsAlive") == true)
			{
				Dead(); //���� ���� �Լ� ȣ��
				//���� �ڷ�ƾ ����
				StartCoroutine(PlayerDeadRoutine());
			}
		}
	}

	//�÷��̾� ��� �ڷ�ƾ
	public IEnumerator PlayerDeadRoutine()
	{
		//�� ȿ��
		yield return StartCoroutine(GameManager.Instance.lightController.DeadLightOut());
	}
}
