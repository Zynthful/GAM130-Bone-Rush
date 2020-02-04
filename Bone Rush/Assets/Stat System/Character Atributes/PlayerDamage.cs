using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private int PlayersDamage;
    private int Modifier;

    void PlayerDamageLevel()//DamageModifier)
    {
        //Modifier = PlayersDamage * DamageModifier;
        PlayersDamage += Modifier;
    }
}
