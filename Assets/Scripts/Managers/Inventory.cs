using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public Equipment itemToEquip;
    public int space = 2;
    public List<Equipment> items = new List<Equipment>();

    public bool Add(Equipment item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);
        itemToEquip = item;
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(Equipment item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}

