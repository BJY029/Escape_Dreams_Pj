using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialoguePanel;
    public float autoAdvanceTime = 3.5f;

    private Queue<string> sentences;
    private bool isDialogueActive = false;
    private Coroutine autoAdvanceCoroutine;

    private void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string[] dialogueLines)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (autoAdvanceCoroutine != null)
        {
            StopCoroutine(autoAdvanceCoroutine);
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        autoAdvanceCoroutine = StartCoroutine(AutoAdvanceToNextSentence());
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator AutoAdvanceToNextSentence()
    {
        yield return new WaitForSeconds(autoAdvanceTime);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}