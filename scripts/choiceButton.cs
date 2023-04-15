using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class choiceButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public string texto {get{return buttonText.text;} set{buttonText.text = value;}}

    public int choiceIndex = -1;
}
