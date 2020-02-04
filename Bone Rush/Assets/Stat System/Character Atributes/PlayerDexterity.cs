using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDexterity : MonoBehaviour
{
    [SerializeField]
    private int PlayersDexterity;
    private int Modifier;

    void PlayerDexterityLevel()//DexterityModifier)
    {
        //Modifier = PlayersDexterity * DexterityModifier;
        PlayersDexterity += Modifier;
    }
}
