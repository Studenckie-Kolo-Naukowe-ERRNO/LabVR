using UnityEngine;
using System.Xml;
using TMPro;

namespace VRLabEssentials
{
    public class Translating : MonoBehaviour
    {
        [SerializeField] private TextAsset xmlFile;
        [SerializeField] private GameSettings gameSettings;
        private string selectedLanguage;

        private void Start()
        {
            LoadLanguage();
        }

        public void LoadLanguage()
        {
            selectedLanguage = gameSettings.GetLanguage();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlFile.text);

            XmlNode languageNode = xmlDoc.SelectSingleNode($"/languages/{selectedLanguage}");
            XmlNodeList stringNodes = languageNode.SelectNodes("string");

            foreach (XmlNode stringNode in stringNodes)
            {
                GameObject objectToTranslate = GameObject.Find(stringNode.Attributes["name"].Value);

                if (objectToTranslate != null && objectToTranslate.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                {
                    text.SetText(stringNode.InnerText);
                }
            }
        }
    }
}