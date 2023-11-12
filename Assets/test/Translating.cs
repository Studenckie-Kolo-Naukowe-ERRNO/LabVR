using UnityEngine;
using System.Xml;
using TMPro;

public class Translating : MonoBehaviour {
    public TextAsset xmlFile;
    public string selectedLanguage = "Polish";

    private void Start() { 
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);

        XmlNode languageNode = xmlDoc.SelectSingleNode($"/languages/{selectedLanguage}");
        XmlNodeList stringNodes = languageNode.SelectNodes("string");

        foreach (XmlNode stringNode in stringNodes) {
            GameObject objectToTranslate = GameObject.Find(stringNode.Attributes["name"].Value);

            if (objectToTranslate.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text)) {
                text.SetText(stringNode.InnerText);
            }
        }
    }
}
