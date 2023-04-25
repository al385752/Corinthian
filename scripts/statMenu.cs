using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void menuPrincipal()
    {
        SceneManager.LoadScene("main menu");
    }
}
