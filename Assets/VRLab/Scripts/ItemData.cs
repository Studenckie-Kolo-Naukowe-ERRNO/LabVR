using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRLabEssentials
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField][Multiline] private string itemDesc;

        public string GetItemName()
        {
            return itemName;
        }

        public string GetItemDesc()
        {
            return itemDesc;
        }
    }
}