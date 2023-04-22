using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueShow : MonoBehaviour
{

    public static dialogueShow instance;
    public elements hud;
    public GameObject continueButton;

    void Awake()
    {
        instance = this;
    }

    public void say(string text, string character = "")
    {
        stopSpeaking(character);
        speaking = StartCoroutine(speakingCoroutine(text, character));
    }

    public void stopSpeaking(string characterSpeaking = "")
    {
        if(isSpeaking) //si hay alguien hablando, para de hablar
        {
            StopCoroutine(speaking);
        }
        
        speaking = null;
    }

    public bool isSpeaking {get{return speaking != null;}} //asegura que nadie este hablando,si alguien habla devuelve true
    public bool isWaitingForUserInput = false; //interaccion con el jugador, regula la salida de texto

    Coroutine speaking = null;

    IEnumerator speakingCoroutine(string targetText, string character = "") //se ejecuta mientras alguien dice algo
    {
        dialogueBackground.SetActive(true);
        characterText.text = "";
        characterName.text = WhoTalking(character);
        continueButton.SetActive(false);
        isWaitingForUserInput = false;

        /*
        esto escribe el texto letra a letra, hasta que se ha escrito entero (se comprueba comparando las cadenas objetivo y actual)
        tambien evita que se solape texto si el jugador pulsa para continuar antes de que se termine de escribir
        */
        while(characterText.text != targetText)
        {
            characterText.text += targetText[characterText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        /*
        el texto ya ha sido escrito entero
        el juego esperara a que el jugador pulse el boton asignado para continuar
        */
        continueButton.SetActive(true);
        isWaitingForUserInput = true;
        while(isWaitingForUserInput) //
        {
            yield return new WaitForEndOfFrame();
        }

        stopSpeaking();
    }

    string WhoTalking(string s)
    {
        string nombreSalida = characterName.text;
        
        if(s != characterName.text && s != "")
        {
            nombreSalida = (s == "narrador") ? "" : s;
        }

        return nombreSalida;
    }

    public void EndText()
    {
        stopSpeaking();
        dialogueBackground.SetActive(false);
    }

    [System.Serializable]

    public class elements //tener controlada la interfaz como un grupo
    {
        public Text characterText;
        public Text characterName;
        public GameObject dialogueBackground;
    }

    public Text characterText {get{return hud.characterText;}}
    public Text characterName {get{return hud.characterName;}}
    public GameObject dialogueBackground {get{return hud.dialogueBackground;}}
}
