using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class choiceMenu : MonoBehaviour
{
    public static choiceMenu instance;
    public GameObject root;
    public choiceButton buttonPrefab;
    static List<choiceButton> buttonList = new List<choiceButton>();
    public VerticalLayoutGroup buttonLayoutGroup;

    void Awake()
    {
        instance = this;
        notShow();
    }
    
    public static void show(params string[] choices)
    {
        instance.root.SetActive(true);

        if(isDisplayingChoices)
            instance.StopCoroutine(displayChoices);
        deleteCurrentChoiceButton();

        displayChoices = instance.StartCoroutine(displayChoicesCoroutine(choices));
    }

    public static void notShow()
    {
        if(isDisplayingChoices)
            instance.StopCoroutine(displayChoices);
        displayChoices = null;

        deleteCurrentChoiceButton();

        instance.root.SetActive(false);
    }

    static void deleteCurrentChoiceButton()
    {
        foreach(choiceButton button in buttonList)
        {
            DestroyImmediate(button.gameObject);
        }
        buttonList.Clear();
    }

    public static bool isWaitingForChoiceToBeMade {get{return isDisplayingChoices && !lastChoice.chosenOne;}}
    public static bool isDisplayingChoices {get{return displayChoices != null;}}
    static Coroutine displayChoices = null;
    public static IEnumerator displayChoicesCoroutine(string[] choices)
    {
        yield return new WaitForEndOfFrame();
        lastChoice.resetState();

        for(int i = 0; i < choices.Length; i++)
        {
            newChoice(choices[i]);
        }

        while(isWaitingForChoiceToBeMade)
            yield return new WaitForEndOfFrame();
        
        notShow();
    }

    static void newChoice(string text)
    {
        GameObject newButton = Instantiate(instance.buttonPrefab.gameObject, instance.buttonPrefab.transform.parent);
        newButton.SetActive(true);
        choiceButton tempButton = newButton.GetComponent<choiceButton>();

        tempButton.texto = text;
        tempButton.choiceIndex = buttonList.Count;
        buttonList.Add(tempButton);
    }

    public class Choice
    {
        public string text = "";
        public int index = -1;
        public bool chosenOne {get{return text != "" && index != -1;}}

        public void resetState()
        {
            text = "";
            index = -1;
        }
    }

    public Choice choice = new Choice();
    public static Choice lastChoice{get{return instance.choice;}}

    public void makeChoice(choiceButton someButton)
    {
        choice.index = someButton.choiceIndex;
        choice.text = someButton.texto;
    }
}
