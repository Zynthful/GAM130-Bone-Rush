using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int CurrentHealth;
    private int MaxHealth;
    private int Modifier;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void EnemyAttack(EnemyDamage)
    {
        CurrentHealth -= EnemyDamage;
        if (CurrentHealth <= 0)
        {

        }
    }
    
    void PlayerHealthLevel(HealthModifier)
    {
        Modifier = MaxHealth * HealthModifier;
        MaxHealth += Modifier;
    }
}
