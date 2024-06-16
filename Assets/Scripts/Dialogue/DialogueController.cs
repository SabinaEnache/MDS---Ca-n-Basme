using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText; 
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;
    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        //if there is noting in the queue
        if(paragraphs.Count == 0)
        {
            if(!conversationEnded)
            {
                //start a consersation 
                StartConversation(dialogueText);
            }

            else if(conversationEnded && !isTyping)
            {
                //end the consersation 
                EndConversation();
                return;
            }
        }

        //if there is something in the queue
        if(!isTyping) 
        { 
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }

        //conversation is being typed out 
        else 
        {
            FinishParagraphEarly(); 
        }


        //update conversationEnded bool
        if(paragraphs.Count == 0)
        {
            conversationEnded = true;
        }

    }


    private void StartConversation(DialogueText dialogueText)
    {
        //activate gameObject 
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        //update the speaker name 
        NPCNameText.text = dialogueText.speakerName;

        //add dialogue text to the quoue 
        for(int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation() 
    {
        //clear the quoue

        //return bool to false 
        conversationEnded = false; 

        //deactivate gameobject 
        if(gameObject.activeSelf) 
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true; 

        NPCDialogueText.text = ""; 

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach(char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText; 

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false; 
    }

    private void FinishParagraphEarly() 
    {
        //stop the coroutine 
        StopCoroutine(typeDialogueCoroutine);

        //finish displaying text 
        NPCDialogueText.text = p; 

        //update isTyping bool
        isTyping = false; 
    }

}
