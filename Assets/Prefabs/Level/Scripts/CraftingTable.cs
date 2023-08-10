using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameOutput;
    [SerializeField] private TextMeshProUGUI descOutput;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Item>(out Item i))
        {
            nameOutput.SetText(i.GetItemData().GetItemName());
            descOutput.SetText(i.GetItemData().GetItemDesc());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Item>(out Item i))
        {
            nameOutput.SetText("");
            descOutput.SetText("");
        }
    }
}
