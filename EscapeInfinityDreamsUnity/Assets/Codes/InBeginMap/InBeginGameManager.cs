using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBeginGameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void Start()
    {
        string[] introDialogue = new string[]
        {
            "(헥헥)무슨 병원이 이렇게 외진 곳에 있지?",
            "임상시험 알바치곤 급여가 높던데, 이유가 있었네",
            "그래도 당장 밀린 월세를 내려면 이 방법 뿐이야.",
            "(허름한 병원을 보며) 나 장기 털리는건 아니겠지...?"
        };

        dialogueManager.StartDialogue(introDialogue);
    }
}
