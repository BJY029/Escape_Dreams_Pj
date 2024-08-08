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
			//bounds.size = ��� ������ ��ü ũ�⸦ ��Ÿ���� Vector3. max - min�� ���� ����.
			//�ݶ��̴��� ũ�⸦ ��������Ʈ ũ��� �����Ѵ�.
			boxCollider.size = spriteRenderer.bounds.size;
			//bounds.center = ��� ������ �߽� ��ġ�� ��Ÿ���� Vector3
			//�ݶ��̴��� ��ġ�� �����Ѵ�.
			boxCollider.offset = spriteRenderer.bounds.center - transform.position;
		}
	}
}
