using Cinemachine;
using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetObj;//플레이어
    public GameObject toObj;//이동할 문
    public GameObject catObj;//고양이 
    public CinemachineVirtualCamera thisRoomCamera; // 현재 방의 가상 카메라
    public CinemachineVirtualCamera nextRoomCamera; // 다음 방의 가상 카메라
    public Collider2D newConfiner; // 다음 방의 Collider2D
    public float teleportCooldown = 0.5f; // 텔레포트 쿨다운 시간

    private bool isTeleporting = false; // 텔레포트 중복 방지 플래그
    private bool canTeleport = false; // 텔레포트 가능 여부 플래그

    public GameObject interactionUI; // 배경을 포함한 UI 오브젝트(text_background)
    public Vector3 uiOffset; // UI 오브젝트의 출력 위치 조정

    private void Start()
    {
        interactionUI.SetActive(false); //시작할 때, 텍스트를 숨김
    }

    private void OnTriggerEnter2D(Collider2D collision)//문과 충돌 감지
    {
        if (collision.CompareTag("Player"))//부딪힌 대상이 플레이어인지 확인
        {
            targetObj = collision.gameObject;//플레이어의 게임 오브젝트를 받음
            canTeleport = true; // 텔레포트 가능 여부 설정
            interactionUI.SetActive(true); // 텍스트를 보이게 함
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//문과의 충돌이 종료
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = false; // 텔레포트 가능 여부 해제
            interactionUI.SetActive(false); // 텍스트를 숨김
        }
    }

    private void Update()
    {
        // 키 입력을 처리하여 텔레포트를 시작
        if (canTeleport && !isTeleporting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportRoutine());//텔레포트를 시작
        }
        // 플레이어 기준으로 UI 위치 업데이트
        if (canTeleport)
        {
            interactionUI.transform.position = targetObj.transform.position + uiOffset;
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true; // 텔레포트 시작

		// 현재 방의 가상 카메라 비활성화
		thisRoomCamera.Priority = 0;

		targetObj.transform.position = toObj.transform.position; //플레이어 이동
        catObj.transform.position = new Vector3(toObj.transform.position.x, toObj.transform.position.y - 1f, toObj.transform.position.z); //고양이 이동

        yield return new WaitForEndOfFrame(); // 한 프레임 대기

        

        // 다음 방의 가상 카메라 활성화
        nextRoomCamera.Priority = 1;

        // 가상 카메라의 경계 업데이트
        CinemachineConfiner confiner = nextRoomCamera.GetComponent<CinemachineConfiner>();
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = newConfiner;
            confiner.InvalidatePathCache(); // 캐시 무효화하여 즉시 업데이트
        }

        yield return new WaitForSeconds(teleportCooldown); // 쿨다운 시간 동안 대기

        isTeleporting = false; // 텔레포트 종료
        interactionUI.SetActive(false); //텔레포트 종료 후 텍스트를 숨김
    }
}