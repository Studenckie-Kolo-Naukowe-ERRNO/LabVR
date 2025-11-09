using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Plank
{
    public class PlankManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        Vector3 startPos = Vector3.zero;
        [SerializeField] private Transform plank;
        [Space]
        [SerializeField] private TextMeshProUGUI lengthText;
        [SerializeField] private TextMeshProUGUI widthText;
        [Space]
        [SerializeField] private Slider lengthSlider;
        [SerializeField] private Slider widthSlider;

        private void Start()
        {
            if(player) startPos = player.transform.position;
            if (plank)
            {
                if(lengthSlider)lengthSlider.value = plank.transform.localScale.z;
                if(widthSlider) widthSlider.value = plank.transform.localScale.x;
            }
        }

        public void ChangeLength(float f)
        {
            if (plank == null) return;
            Vector3 scale = plank.transform.localScale;
            scale.z = f;
            plank.transform.localScale = scale;
            UpdateDesc();
        }
        public void ChangeWidth(float f)
        {
            if (plank == null) return;
            Vector3 scale = plank.transform.localScale;
            scale.x = f;
            plank.transform.localScale = scale;
            UpdateDesc();
        }
        private void UpdateDesc()
        {
            if (plank == null) return;
            Vector3 scale = plank.transform.localScale;
            if (lengthText) lengthText.SetText($"{scale.z} m");
            if (widthText) widthText.SetText($"{scale.x} m");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CharacterController cc = other.GetComponent<CharacterController>();
                if (cc == null) return;
                cc.enabled = false;
                other.transform.position = startPos;
                cc.enabled = true;
            }
        }
    }
}