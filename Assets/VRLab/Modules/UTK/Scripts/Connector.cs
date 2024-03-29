using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UTKLab
{
    [CreateAssetMenu(fileName = "New connector", menuName = "ScriptableObjects/Components/Connector")]
    public class Connector : ScriptableObject
    {
        [SerializeField] private string connectorName;

        public string GetConnectorName()
        {
            return connectorName;
        }
    }
}