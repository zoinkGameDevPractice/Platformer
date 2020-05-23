using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;
    PlayerManager pm;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = Inventory.instance;
        pm = PlayerManager.instance;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        if(newItem.isConsumable == false)
        {
            ShowSprite(newItem);
        }

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);

        currentEquipment[slotIndex] = newItem;
    }

    void ShowSprite(Equipment item)
    {
        if (item.sprite != null)
        {
            SpriteRenderer pointRenderer = pm.point.gameObject.GetComponent<SpriteRenderer>();
            pointRenderer.sprite = item.sprite;
        }
    }
}
