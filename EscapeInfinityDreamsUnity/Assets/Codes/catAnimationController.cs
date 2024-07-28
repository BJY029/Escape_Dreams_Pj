using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class catAnimationController : MonoBehaviour
{
	public Animator animator; //교체에 사용될 애니메이터
	public RuntimeAnimatorController newController; //변경할 애니메이션 컨트롤러
	public RuntimeAnimatorController origController; //기존 애니메이션 컨트롤러
	public abnorbalManager manager; //플래그 참조를 위한 선언

	private SpriteRenderer spriteRenderer; //스프라이트 렌더러
	private BoxCollider2D boxCollider; //콜라이터 크기 조정을 위한 선언

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		BackAnimatorController(); //후에 오류를 방지하기 위한 초기화 
	}

	private void Update()
	{
		//콜라이터 크기를 업데이트
		UpdateColliderSize();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (manager.flag == 27) //만약 해당 플래그가 발동 시
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				SwitchAnimatorController(); //이상현상 맵에서 애니메이션 컨트롤러 변경
			}
			else if (collision.CompareTag("Room_0"))
			{
				BackAnimatorController(); //기존 맵은 애니메이션 컨트롤러 되돌리기
			}
		}
	}

	void SwitchAnimatorController()
	{
		//새로 적용할 애니메이션 컨트롤러로 변경
		if (animator != null && newController != null && origController != null)
		{
			animator.runtimeAnimatorController = newController;
		}
	}

	void BackAnimatorController()
	{
		//기존 고양이 애니메이션 컨트롤러로 변경
		if (animator != null && newController != null && origController != null)
		{
			animator.runtimeAnimatorController = origController;
		}
	}

	//콜라이더 사이즈를 변경하는 함수
	void UpdateColliderSize()
	{
		if(spriteRenderer != null && boxCollider != null)
		{
			//bounds.size = 경계 상자의 전체 크기를 나타내는 Vector3. max - min의 값과 같다.
			//콜라이더의 크기를 스프라이트 크기로 변경한다.
			boxCollider.size = spriteRenderer.bounds.size;
			//bounds.center = 경계 상자의 중심 위치를 나타내는 Vector3
			//콜라이더의 위치를 변경한다.
			boxCollider.offset = spriteRenderer.bounds.center - transform.position;
		}
	}
}
