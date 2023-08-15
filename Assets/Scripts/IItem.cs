using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public ItemData GetItemData();
    public GameObject ThisObject();
    public bool IsHolded();
}
