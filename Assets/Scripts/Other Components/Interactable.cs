using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Equipment equipment;

    public void PickUp()
    {
        //Debug.Log("Picking up " + equipment.name + " and its damage modifier is " + equipment.damageModifier);
        bool wasPickedUp = Inventory.instance.Add(equipment);
        if(wasPickedUp)
            Destroy(gameObject);
    }

}
