using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class characterClass : MonoBehaviour
{
    public string characterName;
    public RectTransform root;
    public bool charEnabled {get {return root.gameObject.activeInHierarchy;} set{root.gameObject.SetActive(value);}}

    public characterClass (string _name, bool enable = true)
    {
        characterManager characterManagerInstance = characterManager.instance;
        GameObject prefab = Resources.Load("characters/characterContainer" + _name) as GameObject;
        GameObject ob = Instantiate(prefab, characterManagerInstance.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;
        rendererInstance.bodyRenderer = ob.GetComponentInChildren<RawImage>();
        charEnabled = enable;
    }
    
    public class Renderers
    {
        public RawImage bodyRenderer;
    }

    public Renderers rendererInstance = new Renderers();

}