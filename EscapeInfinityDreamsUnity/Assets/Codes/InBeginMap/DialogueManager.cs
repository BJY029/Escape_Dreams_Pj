using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;  //�ؽ�Ʈ
    public GameObject dialoguePanel; //��ȭâ
    public float autoAdvanceTime = 3.5f;  //�ڵ����� ��ȭ�� �Ѿ�� �ð�

    private Queue<string> sentences; //��縦 ���� ť
    private bool isDialogueActive = false;  //��簡 ������ �ִ��� ����
    private Coroutine autoAdvanceCoroutine;  //�ڵ����� ��簡 �Ѿ�� �ڷ�ƾ

    public AudioSource audioSource;

    private void Start()
    {
        sentences = new Queue<string>();  //ť �ʱ�ȭ
        dialoguePanel.SetActive(false);   //���� �� ��ȭâ ��Ȱ��ȭ
    }

    public void StartDialogue(string[] dialogueLines)  //��ȭ ����
    {
        isDialogueActive = true;        //��� ��� ���θ� true�� ����
        dialoguePanel.SetActive(true);  //��ȭâ Ȱ��ȭ

        sentences.Clear();              //������ �����ִ� ��縦 ��� ����

        foreach (string sentence in dialogueLines)  //���޹��� ��縦 ť�� �߰�
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()  //���� ��縦 ȭ�鿡 ǥ���ϴ� �޼���
    {
        if (autoAdvanceCoroutine != null) //������ ���� ���̴� �ڵ� �Ѿ�� �ڷ�ƾ�� ����
        {
            StopCoroutine(autoAdvanceCoroutine);
        }

        if (sentences.Count == 0)  //�� �̻� ���� ��簡 ���� ��� ��ȭ ����
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  //ť���� ���� ��縦 ������ ǥ��
        StopAllCoroutines();                    //��� �ڷ�ƾ ����
        StartCoroutine(TypeSentence(sentence)); //��縦 �� ���ھ� Ÿ�����ϴ� ȿ�� ����

        //�ڵ����� ���� ���� �Ѿ�� �ڷ�ƾ ����
        autoAdvanceCoroutine = StartCoroutine(AutoAdvanceToNextSentence());
    }

    IEnumerator TypeSentence(string sentence)  //��縦 �� ���ھ� Ÿ�����ϴ� �ڷ�ƾ
    {
        dialogueText.text = "";                          //�ؽ�Ʈ�� ���
        foreach (char letter in sentence.ToCharArray())  //������ �� ���ھ� �ݺ�
        {
            dialogueText.text += letter;                 //�ؽ�Ʈ�� ���ڸ� �߰�\
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);      //���� ���� Ÿ���� ���� ���
        }
	}

    IEnumerator AutoAdvanceToNextSentence()   //���� �ð� �Ŀ� �ڵ����� ���� ���� �Ѿ�� �ڷ�ƾ
    {
        yield return new WaitForSeconds(autoAdvanceTime); //������ �ð� ���
        DisplayNextSentence();                            //���� ��� ǥ��
    }

    void EndDialogue()                    //��ȭ�� �����ϴ� �޼���
    {
        isDialogueActive = false;         //��ȭ ��Ȱ��ȭ ���·� ����
        dialoguePanel.SetActive(false);   //��ȭâ ��Ȱ��ȭ
    }

    private void Update()
    {
        //��ȭ�� Ȱ��ȭ�� ���¿��� �����̽��ٸ� ������ ���� ��縦 ǥ��
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}