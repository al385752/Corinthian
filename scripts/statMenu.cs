using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statMenu : MonoBehaviour
{
    public GameObject statUI;

    public void showStats()
    {
        if(statUI.activeInHierarchy == false)
            statUI.SetActive(true);
        else
            statUI.SetActive(false);
    }
}
