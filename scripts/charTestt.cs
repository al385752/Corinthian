using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charTestt : MonoBehaviour
{
    public characterClass prueba;

    // Start is called before the first frame update
    void Start()
    {
        prueba = characterManager.instance.getCharacter("Chai", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
