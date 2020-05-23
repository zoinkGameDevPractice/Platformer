using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    public float attackDistance = 1f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    Enemy enemy;

    Equipment myEquipment;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void PreAttack(Equipment weapon, Transform point)
    {

        if(weapon == null || weapon.isRanged == false)
        {
            //Play Attack animation
            RaycastHit2D ray = Physics2D.Raycast(point.position, Vector2.right, attackDistance);
            if (ray.collider.gameObject.GetComponent<CharacterStats>())
                Attack(ray.collider.gameObject.GetComponent<CharacterStats>());
        }

    }

    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            if (OnAttack != null)
                OnAttack();
            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
    }

}
