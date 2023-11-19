using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRLabEssentials
{
    [CreateAssetMenu(fileName = "new GameSettings", menuName = "ScriptableObjects/new GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private string language;

        public string GetLanguage()
        {
            return language;
        }
    }
}