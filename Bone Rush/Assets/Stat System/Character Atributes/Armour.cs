using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{
    [SerializeField]
    private int PlayerArmour;
    private int Modifier;

    void PlayerArmourLevel(ArmourModifier)
    {
        Modifier = PlayerArmour * ArmourModifier;
        PlayerArmour += Modifier;
    }
}
