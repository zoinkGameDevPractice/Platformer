using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEquip : MonoBehaviour
{
    Inventory inventory;
    Equipment equipment;
    PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += Equip;
        pm = PlayerManager.instance;
    }

    void Equip()
    {
        equipment = inventory.itemToEquip;
        pm.player.GetComponent<PlayerController>().GetWeapon(inventory.itemToEquip);
        equipment.Use();
    }
}
