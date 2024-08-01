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

	//애니메이션 상태 변경을 위한 함수 선언
	public void Dead()
	{
		animator.SetBool("IsAlive", false);
	}

	private void Update()
	{
		//(임시) X키가 눌리면 플레이어는 사망한다.
		if (Input.GetKeyDown(KeyCode.X))
		{
			if (animator.GetBool("IsAlive") == true)
			{
				Dead(); //상태 변경 함수 호출
				//관련 코루틴 실행
				StartCoroutine(PlayerDeadRoutine());
			}
		}
	}

	//플레이어 사망 코루틴
	public IEnumerator PlayerDeadRoutine()
	{
		//빛 효과
		yield return StartCoroutine(GameManager.Instance.lightController.DeadLightOut());
	}
}
