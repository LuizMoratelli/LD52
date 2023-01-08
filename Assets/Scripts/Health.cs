using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField]
    public int MaxHealth {
        get { return maxHealth; }
        set {
            maxHealth = value;
            CurrentHealth = value;
        }
    }
    [SerializeField] public int CurrentHealth { get; private set; }

    public delegate void OnDieDelegate();
    public OnDieDelegate OnDie;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {

    }

    public void Hit(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0 && OnDie != null)
        {
            OnDie();
        }
    }

    public void RecoverAllHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
