using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarewolfControllerInB : MonoBehaviour
{
    public Transform wareWolf;
    public float moveSpeed;
	public float cameraChangeTime;
	public bool isActiveWolfRun;

    private Rigidbody2D rb;
    private Vector2 movement;
	Animator animator;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();

		wareWolf.gameObject.SetActive(false);
	}

	private void Update()
	{
		UpdateColliderSize();
	}

	public void activeSprite()
	{
		wareWolf.gameObject.SetActive(true);
		StartCoroutine(activeWolf());
	}

	IEnumerator activeWolf()
	{
		isActiveWolfRun = true;

		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		yield return new WaitForSeconds(cameraChangeTime);

		animator.SetBool("Bark", true);
		yield return new WaitForSeconds(1.0f);

		animator.SetBool("Bark", false);
		yield return new WaitForSeconds(2.0f);

		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToDefaultCamera();
		yield return new WaitForSeconds(cameraChangeTime);

		isActiveWolfRun = false;
	}

	void UpdateColliderSize()
	{
		if (boxCollider != null && spriteRenderer != null)
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
