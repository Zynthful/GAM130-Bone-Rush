using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dexterity : MonoBehaviour
{
    [SerializeField]
    private int PlayerDexterity;
    private int Modifier;

    void PlayerDexterityLevel(DexterityModifier)
    {
        Modifier = PlayerDexterity * DexterityModifier;
        PlayerDexterity += Modifier;
    }
}
