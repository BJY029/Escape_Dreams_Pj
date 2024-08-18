using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class UiSystem : MonoBehaviour
{
	[Header("# triggerTagObj")]
	public GameObject Paper;	//���� UI
	public GameObject VandingMachine; //���Ǳ� UI
    public GameObject Ab_VandingMachine; //�̻����� ���Ǳ� UI
    public GameObject Extensonmeter; //����� UI
    public GameObject Ab_Extensonmeter; //����� �̻����� UI
	public GameObject DeadStateUi;
	public GameObject PlayerBedLocation;
	public GameObject PlayerRespawnLocation;
	public GameObject CatRespawnLocation;
	public SpriteRenderer CatSpriteRen;

	[Header("# UI Set")]
	public GameObject interactionUI; // ����� ������ UI ������Ʈ(text_background)
	public GameObject interactionUIforOut; //���� UI���� ������ UI ������Ʈ
	public Vector3 uiOffset; // UI ������Ʈ�� ��� ��ġ ����
	public float displayDuration = 2.0f; //���Ǳ�, ����迡�� UI�� ����� �ð�


	private bool isInteracted;	//��ȣ�ۿ� UI�� ���� ���� Bool�� ����
	private bool isUiActived;  //���� UI�� ���� ������ִ��� �����ϱ� ���� Bool�� ����
	private int flag;	//�� UI�� �����ϱ� ���� �÷���

	public LightController lightController; //FadeOut ȿ���� �����ϱ� ���� �ش� ��ũ��Ʈ�� �ҷ��´�.
	public abnorbalManager abnorbalManager;
	public TextMeshProUGUI sleepText; //�ῡ �� ��, ��ȣ�ۿ� UI�� �ؽ�Ʈ ������ �����ϱ� ���� �ҷ��´�.

	public GameObject slotItem; //���Ծ����� ����, ������ �ݶ� UI
    public GameObject Ab_slotItem; //���Ծ����� ����, ������ ��ü�Ҹ��� ��ü UI

    //�ߺ� Ű ������ ���� ���� �÷��� ����
    public bool isBedCoroutineRunning = false;
	public bool isPlayerSleeping = false;
	public bool isPaperisVisualable = false;
	public EventSystem eventSystem;

	//�ִϸ��̼�
	Animator animator;

    //�ʱ�ȭ
    public void Awake()
	{
		if (interactionUI != null) 
		{ 
			interactionUI.SetActive(false); 
		}
		if(interactionUIforOut != null)
		{
			interactionUIforOut.SetActive(false);
		}

		Paper.SetActive(false);
		VandingMachine.SetActive(false);
        Ab_VandingMachine.SetActive(false);
        Extensonmeter.SetActive(false);
        Ab_Extensonmeter.SetActive(false);
		DeadStateUi.SetActive(false);
		animator = GetComponent<Animator>();

		animator.SetBool("IsAlive", true);
        sleepText.text ="EŰ�� ��ȣ�ۿ�";

		isInteracted = false;
		isUiActived = false;
		//isBedCoroutineRunning = false;
		flag = 0;
	}



    private void Update()
	{
		if (!isInteracted) return;	//UI�� ��ȣ�ۿ� ������ ���� ����
		
		//��ȣ�ۿ� UI�� ��ġ ����
		interactionUI.transform.position = transform.position + uiOffset;

		//ħ�� �ڷ�ƾ�� ����ǰ� �ִ� ���
		if (isBedCoroutineRunning == true || GameManager.Instance.playerAnimationController.playerDeadCoroutine == true)
		{
			eventSystem.enabled = false; //�̺�Ʈ �ý����� ��Ȱ��ȭ �ؼ� �Է��� �����Ѵ�.
			return;
		}

		eventSystem.enabled = true;
		//��ȣ�ۿ� Ű�� ������ ��
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			switch (flag)
			{
				case 0:
					break;
				//���� UI�� ��ȣ�ۿ� �� ���
				case 1:
					if(isUiActived ==  false)	//���� ���� UI�� Ȱ��ȭ ���� ���� ���¸�
					{	//���� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
						StartCoroutine(PaperInRoutine());
					}
					else //���� UI�� Ȱ��ȭ�� ���¸�
					{
						//���� UI�� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
						StartCoroutine(PaperOutRoutine());
					}
					break;
				//���Ǳ� UI�� ��ȣ�ۿ� �� ���
				case 2:
					//���Ǳ� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
					StartCoroutine(VandingRoutine());
					//�κ��丮�� �ݶ� ȹ��
					Inventory inven = GetComponent<Inventory>();
					for (int i = 0; i < inven.slots.Count; i++) {
						if (inven.slots[i].isEmpty) {
							Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
							inven.slots[i].isEmpty = false;
							break;
						}
					}
                    break;

				//����� UI�� ��ȣ�ۿ� �� ���
				case 3:
					//����� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
					StartCoroutine(extRoutine());
					break;
                case 4:
					if (GameManager.Instance.sceneManager.SceneisStarting == false)
					{
						//�ῡ �� �� �� ��ȯ�� ���� ȿ������ ���� �ڷ�ƾ ȣ��
						StartCoroutine(bedRoutine());
					}
					break;
				case 6:
                    //����� �̻����� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
                    StartCoroutine(Ab_extRoutine());
                    break;
                case 7:
                    //���Ǳ� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ ȣ��
                    StartCoroutine(Ab_VandingRoutine());
                    //�κ��丮�� �ݶ� ȹ��
                    inven = GetComponent<Inventory>();
                    for (int i = 0; i < inven.slots.Count; i++)
                    {
                        if (inven.slots[i].isEmpty)
                        {
                            Instantiate(Ab_slotItem, inven.slots[i].slotObj.transform, false);
                            inven.slots[i].isEmpty = false;
                            break;
                        }
                    }
                    break;
                default:
					break;
			}
		}
	}

	//���� UI�� Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator PaperInRoutine()
	{
		isPaperisVisualable = true; //�ش� �ڷ�ƾ Ȱ��ȭ �Ǵ� �߿� �ڻ� ������ ���� �÷��� ����
		isUiActived = true; //���� UI�� Ȱ��ȭ �Ǿ����� ����
		Paper.SetActive(true);	//���� UI Ȱ��ȭ
		interactionUI.SetActive(false); //��ȣ�ۿ� UI�� ��Ȱ��ȭ
		interactionUIforOut.SetActive(true); //���� UI���� ������ UI�� Ȱ��ȭ
		VandingMachine.SetActive(false ); //���Ǳ⿡�� �ٷ� ���� UI�� ��ȣ�ۿ� �� ���, ���Ǳ� UI ��Ȱ��ȭ
		yield return new WaitForEndOfFrame();	//�� ������ ���

		Time.timeScale = 0; //Ȱ��ȭ �� ���� ���� �ð� ����
	}

	//���� UI�� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator PaperOutRoutine()
	{
		isUiActived = false; //���� UI�� ��Ȱ��ȭ �Ǿ����� ����
		Paper.SetActive(false);	//���� UI ��Ȱ��ȭ
		interactionUI.SetActive(true);	//��ȣ�ۿ� UI Ȱ��ȭ
		interactionUIforOut.SetActive(false);	//���� UI���� ������ ���� UI ��Ȱ��ȭ
		yield return new WaitForEndOfFrame();	//�������� ���
		
		Time.timeScale = 1.0f; //�ð� �ٽ� �帣���� ����
		isPaperisVisualable = false;//�ش� �ڷ�ƾ Ȱ��ȭ �Ǵ� �߿� �ڻ� ������ ���� �÷��� ����
	}

	//���Ǳ� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
	IEnumerator VandingRoutine()
	{
		VandingMachine.SetActive(true); //�ش� UI�� Ȱ��ȭ
		yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
		VandingMachine.SetActive(false ); //��Ȱ��ȭ
	}

    IEnumerator Ab_VandingRoutine() //�̻����� ���Ǳ� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
    {
        Ab_VandingMachine.SetActive(true); //�ش� UI�� Ȱ��ȭ
        yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
        Ab_VandingMachine.SetActive(false); //��Ȱ��ȭ
    }

    //����� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
    IEnumerator extRoutine()
	{
		Extensonmeter.SetActive(true); //�ش� UI�� Ȱ��ȭ
		yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
		Extensonmeter.SetActive(false); //��Ȱ��ȭ
	}

    //�̻����� ����� UI�� �����ð� ���� Ȱ��ȭ ��Ű�� �ڷ�ƾ
    IEnumerator Ab_extRoutine()
    {
        Ab_Extensonmeter.SetActive(true); //�ش� UI�� Ȱ��ȭ
        yield return new WaitForSeconds(displayDuration); //������ �ð���ŭ ���
        Ab_Extensonmeter.SetActive(false); //��Ȱ��ȭ
    }

    //�ῡ ��� �ڷ�ƾ ȣ��
    IEnumerator bedRoutine()
	{
		isBedCoroutineRunning = true; //�̺�Ʈ �ý��� ��Ȱ��ȭ�� ���� �÷��� ����
		isPlayerSleeping = true;

		GameManager.Instance.player.transform.position = PlayerBedLocation.transform.position;
		
		animator.SetBool("isSleeping", true);

		//������� ��� ȿ���� ����
		GameManager.Instance.audioController.StopPlayAudio();

		//�ش� ��ȣ�ۿ� �ؽ�Ʈ�� ������ ���� ����
		sleepText.text = "�ῡ ��� ��..";

		//LightController�� FadeOutLight() �ڷ�ƾ ȣ��
		yield return StartCoroutine(lightController.FadeOutLight());

	

		//���� �̻������� �߻��� ���¿��� ħ��� ��ȣ�ۿ� �ϸ� ������ ��½�Ų��.
		//�߰��� ���� ������ 0�� ���¿����� �̻������� �߻����� �ʾƵ� ���� �ܰ�� �����ؾ� �ϹǷ�, �߰��� ������ �����Ѵ�.
		if (GameManager.Instance.isAbnormal == true || GameManager.Instance.isAbnormal == false && GameManager.level == 0)
		{
			GameManager.level += 1;
			//���� �� �ε�
			abnorbalManager.nextStage();
		}
		else //�̻������� �߻����� �ʾҴµ�, ħ��� ��ȣ�ۿ��ϸ� ����, ������ �ʱ�ȭ�Ѵ�.
		{
			GameManager.level = 0;
			GameManager.Instance.abnorbalManager.Init();
		}

		

		this.Awake();

		isPlayerSleeping = false;

		GameManager.Instance.player.transform.position = PlayerRespawnLocation.transform.position;
		GameManager.Instance.cat.transform.position = CatRespawnLocation.transform.position;
		CatSpriteRen.flipX = false;
		animator.SetBool("isSleeping", false);
		

		yield return lightController.FadeInLight();

		GameManager.Instance.playerController.init(); 

		

		//���� ������ ���� �ش� ��ȭâ ������Ʈ�� Ȱ��ȭ
		GameManager.Instance.catDialogController.gameObject.SetActive(true);
		//��ȭâ�� ����ϴ� �ڷ�ƾ ȣ��
		yield return StartCoroutine(GameManager.Instance.catDialogController.dialogController());

		isBedCoroutineRunning = false; //�̺�Ʈ �ý��� �ٽ� ���
	}


	//Ư�� �ݶ��̴��� �浹�� ���
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (GameManager.Instance.playerAnimationController.playerDeadCoroutine == false)
		{
			if (collision.CompareTag("Paper")) //�浹�� �ݶ��̴��� �±װ� ������ ���
			{
				//�Լ� ȣ��(flag ; 1�� ����)
				ShowUIInteraction(1);
			}
			else if (collision.CompareTag("vandingMachine")) //�浹�� �ݶ��̴��� �±װ� ���Ǳ�
			{
				//�Լ� ȣ��(flag : 2�� ����)
				ShowUIInteraction(2);
			}
			else if (collision.CompareTag("extensometer")) //�浹�� �ݶ��̴��� �±װ� �����
			{
				//�Լ� ȣ��(flag : 3�� ����)
				ShowUIInteraction(3);
			}
			else if (collision.CompareTag("bed")) //�浹�� �ݶ��̴��� �±װ� ħ��
			{
				//�Լ� ȣ��(flag : 4�� ����)
				ShowUIInteraction(4);
			}
			else if (collision.CompareTag("Ab_extensometer")) //�浹�� �ݶ��̴��� �±װ� �̻����� �����
			{
				//�Լ� ȣ��(flag : 6�� ����)
				ShowUIInteraction(6);
			}
			else if (collision.CompareTag("Ab_vandingMachine")) //�浹�� �ݶ��̴��� �±װ� �̻����� ���Ǳ�
			{
				//�Լ� ȣ��(flag : 7�� ����)
				ShowUIInteraction(7);
			}
		}
    }

	//�ش� UI�� ���õ� �͵��� �ٲٴ� �Լ�
	private void ShowUIInteraction(int interactionFlag)
	{
		//��ȣ�ۿ� UI Ȱ��ȭ
		interactionUI.SetActive(true);

		//��ȣ�ۿ� ������ ���·� ����
		isInteracted = true;

		//�÷��� ����
		flag = interactionFlag;
	}

	//�ݶ��̴��� �浹 ��� ���
	private void OnTriggerExit2D(Collider2D collision)
	{
		//� �ݶ��̴��� ����� �������
		if (collision.CompareTag("Paper") || collision.CompareTag("vandingMachine") || collision.CompareTag("extensometer") || collision.CompareTag("bed"))
		{
			//���� ���� ���ǹ�
			if (interactionUI != null)
			{
				//��ȣ�ۿ� Ui ��Ȱ��ȭ
				interactionUI.SetActive(false);
				//��ȣ�ۿ� �Ұ����� ���·� ����
				isInteracted = false;
				//�÷��� �ʱ�ȭ
				flag = 0;
			}
		}
	}
}
