using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBeginGameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject mainCanvas;

    private void Start()
    {
        Time.timeScale = 0f; //시작버튼 누르기 전까지 멈춤
    }
    

    public void GameStart()
    {
        Time.timeScale = 1f; //시작버튼 눌러서 멈춤 해제
        
        //시작 전 딜레이 있어야 자연스러울듯

        string[] introDialogue = new string[]
        {
            "(헥헥)무슨 병원이 이렇게 외진 곳에 있지?",
            "임상시험 알바치곤 급여가 높던데, 이유가 있었네",
            "그래도 당장 밀린 월세를 내려면 이 방법 뿐이야.",
            "(허름한 병원을 보며) 나 장기 털리는건 아니겠지...?"
        };

        dialogueManager.StartDialogue(introDialogue);
    }

    public void GameExit() //QUit 버튼을 눌렀을 때 게임 종료
    {
    #if UNITY_EDITOR // 유니티 에디터에서
            UnityEditor.EditorApplication.isPlaying = false;
    #else //실제 종료 스크립트
                Application.Quit();
    #endif
    }

    public void OnSettingBtn()
    {

    }
}
