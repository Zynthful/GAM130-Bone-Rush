using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmour : MonoBehaviour
{
    [SerializeField]
    private int PlayersArmour;
    private int Modifier;

    void PlayerArmourLevel()
    {
        Modifier = PlayersArmour;
        PlayersArmour += Modifier;
    }
}
