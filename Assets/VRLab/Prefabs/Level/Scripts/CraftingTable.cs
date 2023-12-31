using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
namespace VRLabEssentials
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameOutput;
        [SerializeField] private TextMeshProUGUI descOutput;

        private List<IItem> items = new List<IItem>();
        [SerializeField] private float writeSpeed = 0.1f;

        private IEnumerator writtingProcess;

        [Header("Crafting")]
        [SerializeField] private List<CraftingRecipe> craftings = new List<CraftingRecipe>();
        [SerializeField] private Transform resultPos;
        [SerializeField] private UnityEvent onSuccessfulCraft;
        [SerializeField] private UnityEvent onUnsuccessfulCraft;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IItem>(out IItem i))
            {
                items.Add(i);

                if (writtingProcess != null) StopCoroutine(writtingProcess);
                writtingProcess = WriteOutItemData(i);
                StartCoroutine(writtingProcess);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IItem>(out IItem i))
            {
                items.Remove(i);
                if (writtingProcess != null) StopCoroutine(writtingProcess);

                if (items.Count > 0)
                {
                    writtingProcess = WriteOutItemData(items[items.Count - 1]);
                    StartCoroutine(writtingProcess);
                }
                else
                {
                    nameOutput.SetText("");
                    descOutput.SetText("");
                }
            }
        }

        IEnumerator WriteOutItemData(IItem item)
        {
            ItemData dataOfItem = item.GetItemData();
            if (dataOfItem != null)
            {
                nameOutput.SetText("");
                descOutput.SetText("");

                string itemName = dataOfItem.GetItemName();
                string itemData = dataOfItem.GetItemDesc();

                for (int i = 0; i < itemName.Length; i++)
                {
                    nameOutput.text += itemName[i];
                    yield return new WaitForSeconds(writeSpeed);
                }
                for (int i = 0; i < itemData.Length; i++)
                {
                    descOutput.text += itemData[i];
                    yield return new WaitForSeconds(writeSpeed);
                }
            }
        }

        [ContextMenu("Craft!")]
        public void ManageCrafting()
        {
            for (int i = 0; i < craftings.Count; i++)
            {
                if (craftings[i].CanAffordForCraft(ref items))
                {
                    Instantiate(craftings[i].CraftThisRecipe(ref items),
                        resultPos.position, resultPos.rotation, null);
                    onSuccessfulCraft.Invoke();
                    return;
                }
            }
            onUnsuccessfulCraft.Invoke();
        }
    }

    [System.Serializable]
    public class CraftingElement
    {
        [SerializeField] private ItemData item;
        [SerializeField] private int amout;

        public bool CanAfford(ref List<IItem> itemsList)
        {
            int c = 0;
            for (int i = 0; i < itemsList.Count(); i++)
            {
                if (itemsList[i].GetItemData() == item) c++;//XD
            }

            return c >= amout;
        }
        public void RemoveItemsFromList(ref List<IItem> itemsList)
        {
            //przegl�danie listy od ko�ca!
            for (int i = itemsList.Count() - 1, c = amout; i >= 0 && c > 0; i--)
            {
                if (itemsList[i].GetItemData() == item)
                {
                    GameObject.Destroy(itemsList[i].ThisObject());
                    itemsList.RemoveAt(i);
                    c--;
                }
            }
        }
    }
}