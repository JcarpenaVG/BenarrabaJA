using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    private Queue<string> sentences;
    [SerializeField] private TMP_Text dialogueText;
    public Dialogue dialogue;

    public static DialogueManager Instance;

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
        StartDialogue(dialogue); //TODO this should be a trigger
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.N))
        {
            DisplayNextSentence();
        }
    }

    /// <summary>
    /// Activates the text box, pauses the game, saves the sentences that should be displayed and calls the function to display them
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        //GameController.Instance.Paused = true;
        textBox.SetActive(true);
        Time.timeScale = 0f;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    /// <summary>
    /// Gets a sentence out of the queue and calls a coroutine to display it, if there is one
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Displays one more letter of the sentence every 1/40 of a second
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.025f);
            if (IsSymbol(letter))
            {
                yield return new WaitForSecondsRealtime(0.4f);
            }
        }
    }

    /// <summary>
    /// Disables the dialogue box and unpauses the game
    /// </summary>
    public void EndDialogue()
    {
        textBox.SetActive(false);
        Time.timeScale = 1f;
        //GameController.Instance.Paused = false;
    }

    private bool IsSymbol(char letter)
    {
        string symbols = ",.;:!?-)]";
        return symbols.Contains(letter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartDialogue(dialogue);
        }
    }

}
