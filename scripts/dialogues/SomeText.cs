using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SomeText : MonoBehaviour
{

    dialogueShow dialogue;
    public TextAsset asset;
    //estas son las frases que se van a decir, hay que extraerlas de un fichero de texto
    /*public string[] s = new string[]
    {
        "joder:nacho",
        "estan pintando",
        "toda la casa"
    };*/
    public static string[] dialogo;

    void Awake()
    {

        //hay que asignar esto aqui porque si no explota ¯\ (ツ) /¯
        dialogo = asset.text.Split('\n');

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //instanciamos de la clase dialogueShow, todos los dialogos del juego van a derivar de esa clase
        dialogue = dialogueShow.instance;

    }

    int i = 0;
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            if(!dialogue.isSpeaking || dialogue.isWaitingForUserInput) //si nadie esta hablando y se espera que el jugador le de al raton, se pasa a la siguiente frase
            {
                if(i >= dialogo.Length)
                {
                    Debug.Log("xoriso");
                    dialogue.dialogueBackground.SetActive(false);
                    //aqui tocara cambiar de escena a la que proceda
                    return;
                }
                
                say(dialogo[i]);
                i++;
            }
        }
    }

    void say(string s) //coge la cadena que se pasa y la divide con el operador especial
    {
        string[] frases = s.Split(">");
        string text = frases[0]; //el texto siempre va a ser lo primero
        string character = (frases.Length >= 2) ? frases[1] : ""; //si hay persona hablando, se pasa su nombre, si no, cadena vacia

        dialogue.say(text, character);
    }
}
