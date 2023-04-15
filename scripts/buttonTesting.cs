using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTesting : MonoBehaviour
{
    public string[] choices;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            choiceMenu.show(choices);
            Debug.Log("soy tu ano");
        }
    }
}
