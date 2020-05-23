using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public HealthBar healthBar;
    private PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        healthBar.SetMaxHealth(maxHealth);
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(currentHealth);
    }

    public override void Die()
    {
        base.Die();
        FindObjectOfType<GameManager>().GameOver();
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

}
