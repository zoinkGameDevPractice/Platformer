using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Items/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public int damageModifier;
    public int armorModifier;
    public bool isRanged;
    public float projectileSpeed = 0;
    public GameObject projectilePrefab = null;
    
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }

}

public enum EquipmentSlot { Weapon, Armor }