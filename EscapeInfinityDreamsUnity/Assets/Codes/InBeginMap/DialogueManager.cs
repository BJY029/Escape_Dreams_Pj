using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;  //텍스트
    public GameObject dialoguePanel; //대화창
    public float autoAdvanceTime = 3.5f;  //자동으로 대화가 넘어가는 시간

    private Queue<string> sentences; //대사를 넣을 큐
    private bool isDialogueActive = false;  //대사가 나오고 있는지 여부
    private Coroutine autoAdvanceCoroutine;  //자동으로 대사가 넘어가는 코루틴

    public AudioSource audioSource;

    private void Start()
    {
        sentences = new Queue<string>();  //큐 초기화
        dialoguePanel.SetActive(false);   //시작 시 대화창 비활성화
    }

    public void StartDialogue(string[] dialogueLines)  //대화 시작
    {
        isDialogueActive = true;        //대사 출력 여부를 true로 설정
        dialoguePanel.SetActive(true);  //대화창 활성화

        sentences.Clear();              //이전에 남아있던 대사를 모두 삭제

        foreach (string sentence in dialogueLines)  //전달받은 대사를 큐에 추가
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()  //다음 대사를 화면에 표시하는 메서드
    {
        if (autoAdvanceCoroutine != null) //기존에 실행 중이던 자동 넘어가기 코루틴을 중지
        {
            StopCoroutine(autoAdvanceCoroutine);
        }

        if (sentences.Count == 0)  //더 이상 남은 대사가 없을 경우 대화 종료
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  //큐에서 다음 대사를 꺼내어 표시
        StopAllCoroutines();                    //모든 코루틴 중지
        StartCoroutine(TypeSentence(sentence)); //대사를 한 글자씩 타이핑하는 효과 시작

        //자동으로 다음 대사로 넘어가는 코루틴 시작
        autoAdvanceCoroutine = StartCoroutine(AutoAdvanceToNextSentence());
    }

    IEnumerator TypeSentence(string sentence)  //대사를 한 글자씩 타이핑하는 코루틴
    {
        dialogueText.text = "";                          //텍스트를 비움
        foreach (char letter in sentence.ToCharArray())  //문장을 한 글자씩 반복
        {
            dialogueText.text += letter;                 //텍스트에 글자를 추가\
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);      //다음 글자 타이핑 전에 대기
        }
	}

    IEnumerator AutoAdvanceToNextSentence()   //일정 시간 후에 자동으로 다음 대사로 넘어가는 코루틴
    {
        yield return new WaitForSeconds(autoAdvanceTime); //지정된 시간 대기
        DisplayNextSentence();                            //다음 대사 표시
    }

    void EndDialogue()                    //대화를 종료하는 메서드
    {
        isDialogueActive = false;         //대화 비활성화 상태로 변경
        dialoguePanel.SetActive(false);   //대화창 비활성화
    }

    private void Update()
    {
        //대화가 활성화된 상태에서 스페이스바를 누르면 다음 대사를 표시
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}