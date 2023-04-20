using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTesting : MonoBehaviour
{
    public string[] choices;
    // Update is called once per frame
    void Start()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            choiceMenu.show(choices);
            Debug.Log("soy tu ano");
        }*/
        StartCoroutine(DynamicStoryExample());
    }

    IEnumerator DynamicStoryExample()
    {
        chapterManager.instance.LoadChapterFile("chapter1");
        yield return new WaitForEndOfFrame();

        choiceMenu.show("poop1", "poop2");
        while(chapterManager.instance.isHandlingChapter)
            yield return new WaitForEndOfFrame();
        
        if(choiceMenu.lastChoice.index == 0)
            chapterManager.instance.LoadChapterFile("chapter_1a");
        else
            chapterManager.instance.LoadChapterFile("chapter_1b");
        
        yield return new WaitForEndOfFrame();

        while(chapterManager.instance.isHandlingChapter)
            yield return new WaitForEndOfFrame();
    }
}
