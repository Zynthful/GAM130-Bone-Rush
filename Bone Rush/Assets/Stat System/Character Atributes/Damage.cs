using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private int PlayerDamage;
    private int Modifier;

    void PlayerDamageLevel(DamageModifier)
    {
        Modifier = PlayerDamage * DamageModifier;
        PlayerDamage += Modifier;
    }
}
