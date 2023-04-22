using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showStats : MonoBehaviour
{
    public TMP_Text intimidacionText;
    public TMP_Text persuasionText;
    public TMP_Text perspicaciaText;
    public TMP_Text mentiraText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        intimidacionText.text = "Intimidación: +" + chapterManager.player._intimidacion;
        persuasionText.text = "Persuasión: +" + chapterManager.player._persuasion;
        perspicaciaText.text = "Perspicacia: +" + chapterManager.player._perspicacia;
        mentiraText.text = "Mentira: +" + chapterManager.player._mentira;
    }
}
