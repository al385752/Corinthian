using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueShow : MonoBehaviour
{

    public static dialogueShow instance;
    public elements hud;

    void Awake()
    {
        instance = this;
    }

    public void say(string text, string character = "")
    {
        stopSpeaking();
        speaking = StartCoroutine(speakingCoroutine(text, character));
    }

    public void stopSpeaking()
    {
        if(isSpeaking)
        {
            StopCoroutine(speaking);        
        }
        
        speaking = null;
    }

    public bool isSpeaking {get{return speaking != null;}}
    public bool isWaitingForUserInput = false;

    Coroutine speaking = null;

    IEnumerator speakingCoroutine(string targetText, string character = "")
    {
        dialogueBackground.SetActive(true);
        characterText.text = "";
        characterName.text = WhoTalking(character);
        isWaitingForUserInput = false;

        while(characterText.text != targetText)
        {
            characterText.text += targetText[characterText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }

        stopSpeaking();
    }

    string WhoTalking(string s)
    {
        string retVal = characterName.text;
        
        if(s != characterName.text && s != "")
        {
            retVal = (s.ToLower().Contains("Narrador")) ? "" : s;
        }

        return retVal;
    }

    public void EndText()
    {
        stopSpeaking();
        dialogueBackground.SetActive(false);
    }

    [System.Serializable]

    public class elements
    {
        public Text characterText;
        public Text characterName;
        public GameObject dialogueBackground;
    }

    public Text characterText {get{return hud.characterText;}}
    public Text characterName {get{return hud.characterName;}}
    public GameObject dialogueBackground {get{return hud.dialogueBackground;}}
}
