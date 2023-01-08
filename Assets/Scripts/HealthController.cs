using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class HealthController : MonoBehaviour
{
    private Health healthComponent;
    [SerializeField] private string canBeHitByTag;
    public delegate void onHPChangeDelegate();
    public onHPChangeDelegate OnHPChange;
    [SerializeField] private UnityEvent eventOnDie;
    [SerializeField] private bool gameOverWhenDie;
    [SerializeField] private bool earnScoreByDestruct;


    void OnEnable()
    {
        healthComponent = gameObject.GetComponent<Health>();
        healthComponent.OnDie += HandleEventOnDie;
    }
    void OnDisable()
    {
        healthComponent.OnDie -= HandleEventOnDie;
    }

    void HandleEventOnDie()
    {
        if (gameOverWhenDie)
        {
            GameManager.instance.GameOver();
        }
        eventOnDie?.Invoke();
    }

    public void HandleDie()
    {
        if (earnScoreByDestruct) GameManager.instance.IncreaseScore(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(canBeHitByTag))
        {
            if (canBeHitByTag == "Weapon")
            {
                GameManager.instance.PlaySFX("player_attack");
            }
            Hit();
        }
    }

    public void Hit(int damage = 1)
    {
        healthComponent.Hit(damage);

        OnHPChange?.Invoke();
    }

    public void SetMaxHealth(int value)
    {
        healthComponent.MaxHealth = value;
    }

    public void RecoverAllHealth()
    {
        healthComponent.RecoverAllHealth();
        OnHPChange?.Invoke();
    }
}
