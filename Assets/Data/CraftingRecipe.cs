using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "ScriptableObjects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] private GameObject result;
    [SerializeField] private List<CraftingElement> ingredients = new List<CraftingElement>();

    public bool CanAffordForCraft(ref List<Item> itemsList)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (!ingredients[i].CanAfford(ref itemsList)) return false;
        }
        return true;
    }

    public GameObject CraftThisRecipe(ref List<Item> itemsList)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].RemoveItemsFromList(ref itemsList);
        }
        Debug.Log($"Crafted {result}");
        return result;
    }
}
