using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    playerCharacterSheet player = new playerCharacterSheet(16, 15, 17, 8);

    void Start()
    {
        player.intimidacionCheck();
    }
}
