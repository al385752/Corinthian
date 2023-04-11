using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeText : MonoBehaviour
{

    dialogueShow dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
        dialogue = dialogueShow.instance;

    }

    public string[] s = new string[]
    {
        "joder:nacho",
        "estan pintando",
        "toda la casa"
    };

    int i = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if(i >= s.Length)
                {
                    return;
                }
                
                say(s[i]);
                i++;
            }
        }
    }

    void say(string s)
    {
        string[] frases = s.Split(":");
        string text = frases[0];
        string character = (frases.Length >= 2) ? frases[1] : "";

        dialogue.say(text, character);
    }
}
