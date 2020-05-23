using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite sprite = null;
    public bool isConsumable;

    public virtual void Use()
    {
        //Debug.Log("Using " + name);
    }

}
