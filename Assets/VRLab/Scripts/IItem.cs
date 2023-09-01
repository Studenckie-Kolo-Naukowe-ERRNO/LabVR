using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRLabEssentials
{
    public interface IItem
    {
        public ItemData GetItemData();
        public GameObject ThisObject();
        public GameObject GetThisObjectMesh();
        public void SetThisObjectMesh(GameObject newMesh);
        public bool IsHolded();

        public bool CanBeSliced();
    }
}