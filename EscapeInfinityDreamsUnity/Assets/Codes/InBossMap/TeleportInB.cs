using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;

public class TeleportInB : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject toObj;
    public CinemachineVirtualCamera thisRoomCamera;
    public CinemachineVirtualCamera nextRoomCamera;
    private Collider2D newConfiner;
    private Collider2D oldConfiner;

    public float teleportCooldown = 0.5f;
    
    private bool isTeleproting = false;
    private bool canTeleport = false;

    public GameObject interactionUI;
    public Vector3 UIOffset;

	private void Awake()
	{
		oldConfiner = GetComponent<Collider2D>();

	}

	private void Start()
	{
        interactionUI.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
        {
			targetObj = collision.gameObject;
            canTeleport = true;
            interactionUI.SetActive(true);

        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
        {
            canTeleport = false;
            if(interactionUI != null) interactionUI.SetActive(false);
        }
	}

	private void Update()
	{
        if (GameManagerInB.instance.warewolfController.isExecuting == true)
        {
            interactionUI.SetActive(false );
            return;
        }
            
        
        if (canTeleport && !isTeleproting && Input.GetKeyDown(KeyCode.E))
        {
            if (oldConfiner.CompareTag("autoDoor"))
            {
                GameManagerInB.instance.audioControllerInB.openingAutoDoor();
            }
            else
            {
                GameManagerInB.instance.audioControllerInB.openingDoor();
            }
            StartCoroutine(TeleportRoutine());
        }

        if (canTeleport) interactionUI.transform.position = targetObj.transform.position + UIOffset;
	}

    IEnumerator TeleportRoutine()
    {
        isTeleproting = true;

        thisRoomCamera.Priority = 0;

        targetObj.transform.position = toObj.transform.position;

        yield return new WaitForEndOfFrame();

        nextRoomCamera.Priority = 1;

        CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = newConfiner;
            confiner.InvalidatePathCache();
        }

        yield return new WaitForSeconds(teleportCooldown);

        isTeleproting = false;
        interactionUI.SetActive(false);
    }
}
