using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayInWorld : MonoBehaviour
{
    Health healthComponent;
    HealthController healthControllerComponent; 
    [SerializeField] Slider healthBar;

    void Start()
    {
        healthComponent = gameObject.GetComponent<Health>();
        healthControllerComponent = gameObject.GetComponent<HealthController>();

        healthControllerComponent.OnHPChange += UpdateVisuals;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        healthBar.value = (float) healthComponent.CurrentHealth / healthComponent.MaxHealth;
    }
}
