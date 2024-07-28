using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class catAnimationController : MonoBehaviour
{
	public Animator animator; //��ü�� ���� �ִϸ�����
	public RuntimeAnimatorController newController; //������ �ִϸ��̼� ��Ʈ�ѷ�
	public RuntimeAnimatorController origController; //���� �ִϸ��̼� ��Ʈ�ѷ�
	public abnorbalManager manager; //�÷��� ������ ���� ����

	private SpriteRenderer spriteRenderer; //��������Ʈ ������
	private BoxCollider2D boxCollider; //�ݶ����� ũ�� ������ ���� ����

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		BackAnimatorController(); //�Ŀ� ������ �����ϱ� ���� �ʱ�ȭ 
	}

	private void Update()
	{
		//�ݶ����� ũ�⸦ ������Ʈ
		UpdateColliderSize();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (manager.flag == 27) //���� �ش� �÷��װ� �ߵ� ��
		{
			if (collision.CompareTag("mainMap") || collision.CompareTag("Room_1"))
			{
				SwitchAnimatorController(); //�̻����� �ʿ��� �ִϸ��̼� ��Ʈ�ѷ� ����
			}
			else if (collision.CompareTag("Room_0"))
			{
				BackAnimatorController(); //���� ���� �ִϸ��̼� ��Ʈ�ѷ� �ǵ�����
			}
		}
	}

	void SwitchAnimatorController()
	{
		//���� ������ �ִϸ��̼� ��Ʈ�ѷ��� ����
		if (animator != null && newController != null && origController != null)
		{
			animator.runtimeAnimatorController = newController;
		}
	}

	void BackAnimatorController()
	{
		//���� ����� �ִϸ��̼� ��Ʈ�ѷ��� ����
		if (animator != null && newController != null && origController != null)
		{
			animator.runtimeAnimatorController = origController;
		}
	}

	//�ݶ��̴� ����� �����ϴ� �Լ�
	void UpdateColliderSize()
	{
		if(spriteRenderer != null && boxCollider != null)
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
