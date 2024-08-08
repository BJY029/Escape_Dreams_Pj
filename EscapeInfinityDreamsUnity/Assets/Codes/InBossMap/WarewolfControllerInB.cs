using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarewolfControllerInB : MonoBehaviour
{
    public Transform wareWolf;
    public float moveSpeed;
	public float cameraChangeTime;
	public bool isActiveWolfRun;
	public bool isExecuting;
	public bool startChasing;

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

		//���۰� ���ÿ� �ش� ��������Ʈ�� Ȱ��ȭ ���� �Ѵ�.
		wareWolf.gameObject.SetActive(false);
		startChasing = false;
	}

	private void Update()
	{
		//�ֱ������� �����ΰ��� collider�� ������Ʈ ���ش�.
		UpdateColliderSize();

		if (startChasing == false) return;
	}

	//�÷��̾ Ư�� ��ġ�� ������ ��� ȣ��ȴ�.
	public void activeSprite()
	{
		//�����ΰ��� Ȱ��ȭ �Ѵ�.
		//�ڷ�ƾ�� ����Ϸ��� �ش� ��������Ʈ�� Ȱ��ȭ �Ǿ� �־�� �Ѵ�.
		wareWolf.gameObject.SetActive(true);
		//�ڷ�ƾ ȣ��
		StartCoroutine(activeWolf());
		startChasing = true;
	}

	//���� collider�� ��Ƽ� ó�����ϴ� ��Ȳ�� �� ȣ��ȴ�.
	public void Execute()
	{
		StartCoroutine(ExecutePlayer());
	}

	//���� Ȱ��ȭ �ڷ�ƾ
	IEnumerator activeWolf()
	{
		//�ڷ�ƾ�� ����Ǵ� ����, �÷��̾� �������� �����ϱ� ���� �÷���
		isActiveWolfRun = true;

		//ī�޶� ��ȯ
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//��ȯ �� ���
		yield return new WaitForSeconds(cameraChangeTime);

		//Ư�� �ִϸ��̼� ���
		animator.SetBool("Bark", true);
		//�ִϸ��̼� ��� �� ���
		yield return new WaitForSeconds(1.0f);

		//�ִϸ��̼� �ٽ� Idle�� ����
		animator.SetBool("Bark", false);
		//���� �ð� ���
		yield return new WaitForSeconds(1.0f);

		//�ٽ� ���� ī�޶�� ����
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToDefaultCamera();
		//���� �Ǵ� ���� ���
		yield return new WaitForSeconds(cameraChangeTime);

		//�÷��� ����
		isActiveWolfRun = false;
	}

	//�÷��̾� ��� ȿ�� �ڷ�ƾ
	IEnumerator ExecutePlayer()
	{
		//�ش� �ڷ�ƾ�� ���� �߿� �ٸ� �Լ��� �����ϱ� ���� �ڷ�ƾ
		isExecuting = true;

		//ī�޶� ��ȯ
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCamera();
		//��ȯ �� ���
		yield return new WaitForSeconds(cameraChangeTime);

		//Ư�� �ִϸ��̼� ���
		animator.SetBool("Bark", true);
		//�ִϸ��̼� ��� �� ���
		yield return new WaitForSeconds(1.0f);
		animator.SetBool("Bark", false);

		//�÷��̾�� ī�޶� ���� �ϴ� �Լ� ȣ��
		GameManagerInB.instance.wolfZoomInCameraInB.SwitchToZoomCameraToPlayer();
		//�÷��̾� ��� �ִϸ��̼� ����
		GameManagerInB.instance.playerController.animator.SetBool("IsAlive", false);
		GameManagerInB.instance.playerController.animator.Play("die_0");
		yield return new WaitForSeconds(2.0f);

		//����� �ȳ� UI ����
		GameManagerInB.instance.UIControllerInB.activeDeadStateUI();

		//�ð� ����
		Time.timeScale = 0f;

		//isExecuting = false;
	}

	//collider ����� ������Ʈ �ϴ� �Լ�
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
