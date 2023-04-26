using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectScene : MonoBehaviour
{
    public void LoadPrologue()
    {
        SceneManager.LoadScene("intro");
    }

    public void LoadAct1()
    {
        SceneManager.LoadScene("primerActo");
    }

    public void LoadAct2()
    {
        SceneManager.LoadScene("segundoActo");
    }

    public void LoadAct3()
    {
        SceneManager.LoadScene("tercerActo");
    }
}
