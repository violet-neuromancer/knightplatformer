using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private CrystalType crystalType;
    [SerializeField] private float quantity;

    public CrystalType CrystalType
    {
        get => crystalType;
        set => crystalType = value;
    }

    public float Quantity
    {
        get => quantity;
        set => quantity = value;
    }
}
