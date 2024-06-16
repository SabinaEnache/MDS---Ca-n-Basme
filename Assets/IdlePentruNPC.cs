using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyGameNamespace
{
public class IdlePentruNPC : NPCbarman, ITalkable 
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        //start conversation 
        dialogueController.DisplayNextParagraph(dialogueText);
        
    }
}
}

