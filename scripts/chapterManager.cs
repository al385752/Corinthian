using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class chapterManager : MonoBehaviour
{
    public static chapterManager instance;
    public TextAsset asset;
    public static string[] chapterData; // donde estaran almacenadas las lineas del archivo
    playerCharacterSheet player = new playerCharacterSheet(10, 10, 10, 10);

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadChapterFile("chapter1");
        lastCharacterTalking = ""; //no hay nadie hablando antes de que comience el capitulo!!
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            nextLine();
        }
    }

    bool nextLineBool = false;
    public void nextLine()
    {
        nextLineBool = true;
    }

    public void LoadChapterFile(string chapterName)
    {
        AssetDatabase.ImportAsset(chapterName + ".txt");
        asset = (TextAsset)Resources.Load(chapterName);
        chapterData = asset.text.Split('\n');

        if(chapterHandler != null)
            StopCoroutine(chapterHandler);
        chapterHandler = StartCoroutine(chapterHandlerCoroutine());
        nextLine();
    }

    public bool isHandlingChapter {get {return chapterHandler != null;}}
    Coroutine chapterHandler = null;
    int chapterProgress = 0;
    IEnumerator chapterHandlerCoroutine()
    {
        chapterProgress = 0;

        while(chapterProgress < chapterData.Length)
        {
            if(nextLineBool)
            {
                string line = chapterData[chapterProgress];

                if(line.StartsWith("choice"))
                {
                    yield return readChoicesCoroutine(line);
                    chapterProgress++;
                }
                else
                {
                    readLine(line);
                    chapterProgress++;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        chapterHandler = null;
    }

    IEnumerator readChoicesCoroutine(string line)
    {
        List<string> choices = new List<string>();
        List<string> actions = new List<string>();

        bool readingChoices = true;
        while(readingChoices)
        {
            chapterProgress++;
            line = chapterData[chapterProgress];

            if(line.Contains("{"))
            {
                continue;
            }

            if(line.Contains("}"))
            {
                Debug.Log("sacabao");
                readingChoices = false;
            }
            else
            {
                choices.Add(line.Split('"')[1]);
                actions.Add(chapterData[chapterProgress+1]);
                chapterProgress++;
            }
        }

        if(choices.Count > 0)
        {
            choiceMenu.show(choices.ToArray());
            yield return new WaitForEndOfFrame();
            while(choiceMenu.isWaitingForChoiceToBeMade)
                yield return new WaitForEndOfFrame();
            
            string actionChoice = actions[choiceMenu.lastChoice.index];
            readLine(actionChoice);
        }
        else
        {
            Debug.LogError("no detecto elecciones");
        }
    }

    void readLine(string line)
    {
        nextLineBool = false;
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

        switch(actionSplit[0])
        {
            case "setBackground":
                setLayerImage(actionSplit[1], GameObject.FindGameObjectsWithTag("fondo")[0], 2);
                break;
            
            case "Load":
                setNewChapter(actionSplit[1]);
                break;
            
            case "saveThrow":
                string[] details = actionSplit[1].Split(',');
                savingThrow(details[0], int.Parse(details[1]), details[2], details[3]);
                break;
            
            case "changeAbilityScore":
                string[] newDetails = actionSplit[1].Split(',');
                changeAbilityScore(newDetails[0], int.Parse(newDetails[1]));
                break;
        }
    }

    void setLayerImage(string imageName, GameObject fondo, float transitionSpeed)
    {
        fondo.GetComponent<Image>().sprite = Resources.Load<Sprite>("imgs/fondos/" + imageName);
        StartCoroutine(FadeCanvasGroup(fondo.GetComponent<CanvasGroup>(), 0, 1, transitionSpeed));
    }

    void setNewChapter(string newChapter)
    {
        chapterManager.instance.LoadChapterFile(newChapter);
    }

    void savingThrow(string abilitySave, int dc, string winChapter, string loseChapter)
    {
        bool playerHasWon = false;
        switch (abilitySave)
        {
            case "intimidacion":
                if(player.intimidacionCheck() >= dc)
                {
                    playerHasWon = true;
                }
                break;
            
            case "persuasion":
                if(player.persuasionCheck() >= dc)
                    playerHasWon = true;
                break;
            
            case "perspicacia":
                if(player.perspicaciaCheck() >= dc)
                    playerHasWon = true;
                break;
            
            default:
                if(player.mentiraCheck() >= dc)
                    playerHasWon = true;
                break;
        }

        if(playerHasWon)
            LoadChapterFile(winChapter);
        else
            LoadChapterFile(loseChapter);
    }

    void changeAbilityScore(string ability, int amount)
    {
        switch (ability)
        {
            case "intimidacion":
                player._intimidacion = (amount - 10) / 2;
                break;
            
            case "persuasion":
                player._persuasion = (amount - 10) / 2;
                break;
            
            case "perspicacia":
                player._perspicacia = (amount - 10) / 2;
                break;
            
            default:
                player._intimidacion = (amount - 10) / 2;
                break;
        }
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
