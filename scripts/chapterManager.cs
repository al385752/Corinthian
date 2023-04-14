using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class chapterManager : MonoBehaviour
{
    public TextAsset asset;
    public static string[] data; // donde estaran almacenadas las lineas del archivo
    int chapterProgress = 0; //referencia al numero de linea del archivo

    void Awake()
    {
        data = asset.text.Split('\n');
    }

    // Start is called before the first frame update
    void Start()
    {
        chapterProgress = 0; //el capitulo debe de empezar a leerse por el principio
        lastCharacterTalking = ""; //no hay nadie hablando antes de que comience el capitulo!!
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            readLine(data[chapterProgress]);
            chapterProgress++;
        }
    }

    void readLine(string line)
    {
        string[] fileLine = line.Split(':');

        if(fileLine.Length == 3)
        {
            //Debug.Log("tengo dialogo");
            itIsDialogue(fileLine[0], fileLine[1]);
            itIsAction(fileLine[2]);
        }
        else
        {
            //Debug.Log("no tengo dialogo");
            itIsAction(fileLine[0]);
        }
    }

    string lastCharacterTalking = "";
    void itIsDialogue(string firstObject, string secondObject)
    {
        string speaker = lastCharacterTalking;
        bool sameCharacterTalking = firstObject.Contains("+");

        if(sameCharacterTalking)
        {
            firstObject = firstObject.Remove(firstObject.Length-1);
        }
        
        if(firstObject.Length > 0)
        {
            speaker = firstObject;
            lastCharacterTalking = firstObject;
        }

        dialogueShow.instance.say(secondObject, speaker);
    }

    void itIsAction(string action)
    {
        string[] actionSplit = action.Split('(', ')');
        if(actionSplit[0] == "setBackground")
        {
            setLayerImage(actionSplit[1], GameObject.FindGameObjectsWithTag("fondo")[0], 2);
            return;
        }
    }

    void setLayerImage(string imageName, GameObject fondo, float transitionSpeed)
    {   
        /*if(fondo.GetComponent<Image>().sprite != null)
            StartCoroutine(FadeCanvasGroup(fondo.GetComponent<CanvasGroup>(), 1, 0, transitionSpeed));*/

        fondo.GetComponent<Image>().sprite = Resources.Load<Sprite>("imgs/fondos/" + imageName);
        StartCoroutine(FadeCanvasGroup(fondo.GetComponent<CanvasGroup>(), 0, 1, transitionSpeed));

        /*Vector4 initial = (1f, 1f, 1f, 1f);
        Vector4 final = (1f, 1f, 1f, 0f);
        //de 1 a 0
        fondo.GetComponent<Image>().color = Mathf.Lerp(initial, final, Time.deltaTime);
        //cambio de imagen y de 0 a 1
        fondo.GetComponent<Image>().sprite = Resources.Load<Sprite>("imgs/fondos/" + imageName);
        fondo.GetComponent<Image>().color = Mathf.Interp(final, initial, Time.deltaTime);
        while(fondo.GetComponent<Image>().color.a < 1)
        {
            Color transition = fondo.GetComponent<Image>().color;
            transition.a += 1f/255f;
            fondo.GetComponent<Image>().color = tempColor;
        }
        tempColor.a = 1f;
        fondo.GetComponent<Image>().color = tempColor;*/
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
	{
		float _timeStartedLerping = Time.time;
		float timeSinceStarted = Time.time - _timeStartedLerping;
		float percentageComplete = timeSinceStarted / lerpTime;

		while (true)
		{
			timeSinceStarted = Time.time - _timeStartedLerping;
			percentageComplete = timeSinceStarted / lerpTime;

			float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

			yield return null;
		}
	}
}
