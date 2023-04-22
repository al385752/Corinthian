using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterManager : MonoBehaviour
{
    public static characterManager instance;

    public RectTransform characterPanel;

    public List<characterClass> characterList = new List<characterClass>();

    public Dictionary<string, int> charDictionary = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;
    }

    public characterClass getCharacter(string characterName, bool charEnabled)
    {
        int index = -1;
        if(charDictionary.TryGetValue(characterName, out index))
        {
            return characterList[index]; 
        }
        else
        {
            return createCharacter(characterName, charEnabled);    
        }
    }

    public characterClass createCharacter(string characterName, bool charEnabled = true)
    {
        characterClass newCharacter = new characterClass(characterName, charEnabled);

        charDictionary.Add(characterName, characterList.Count);

        characterList.Add(newCharacter);

        return newCharacter;
    }

    public void showCharacter(string characterName)
    {
        int index = -1;
        if(charDictionary.TryGetValue(characterName, out index))
        {
            characterClass selectedCharacter = characterList[index];
            Color colorActual = selectedCharacter.rendererInstance.bodyRenderer.color;
            colorActual.r = 1.0f;
            colorActual.g = 1.0f;
            colorActual.b = 1.0f;
            selectedCharacter.rendererInstance.bodyRenderer.color = colorActual;
        }
    }

    public void hideCharacter(string characterName)
    {
        int index = -1;
        if(charDictionary.TryGetValue(characterName, out index))
        {
            characterClass selectedCharacter = characterList[index];
            Color colorActual = selectedCharacter.rendererInstance.bodyRenderer.color;
            colorActual.r = .2f;
            colorActual.g = .2f;
            colorActual.b = .2f;
            selectedCharacter.rendererInstance.bodyRenderer.color = colorActual;
        }
    }
}
