using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowBrain : MonoBehaviour
{
    private Transform target;
    private Movement movementComponent;
    private Health healthComponent;
    private HealthController healthControllerComponent;
    [SerializeField] float minDistanceToAttack = 0.1f;
    [SerializeField] GameObject attackEffect;
    [SerializeField] float timeBetweenAttacks = 1f;
    bool canAttack = true;
    [SerializeField] float timeBeforeAttack = 0.3f;

    void Start()
    {
        movementComponent = gameObject.GetComponent<Movement>();
        healthComponent = gameObject.GetComponent<Health>();
        healthControllerComponent = gameObject.GetComponent<HealthController>();
        healthControllerComponent.OnHPChange += findNewTarget;
        findNewTarget();
    }

    void findNewTarget()
    {
        if (healthComponent.CurrentHealth == healthComponent.MaxHealth)
        {
            var plants = GameObject.FindGameObjectsWithTag("Plant");

            if (plants.Count() == 0)
            {
                target = GameObject.FindGameObjectWithTag("House")?.transform;
            }
            else
            {
                var randomPlantIndex = Random.Range(0, plants.Count());
                target = plants[randomPlantIndex].transform;
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;

        }
    }

    void Update()
    {
        if (target == null)
        {
            findNewTarget();
        }

        if (CheckAttack())
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(PrepareToAttack());
            }
        }
        else
        {
            var moveDirection = target.position - transform.position;
            moveDirection = moveDirection.normalized;
            movementComponent.Translate(moveDirection);
        }
    }

    IEnumerator RefreshAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(timeBeforeAttack);

        // Verifica se o alvo ainda está numa distância atacável
        if (target != null && Vector2.Distance(transform.position, target.transform.position) <= minDistanceToAttack)
        {
            // Attack (Polir o ataque pra ser mais bonito depois)
            Instantiate(attackEffect, target);
            GameManager.instance.PlaySFX("enemy_attack");
            target.gameObject.GetComponent<HealthController>()?.Hit(1);
        }

        StartCoroutine(RefreshAttack());
    }

    bool CheckAttack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= minDistanceToAttack)
        {
            return true;
        }

        return false;
    }
}
