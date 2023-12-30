using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhysicsLab
{
    public class Slides : MonoBehaviour
    {
        [SerializeField] private Image output;
        [SerializeField] private Sprite[] slides;
        private byte pointer = 0;

        [ContextMenu("Next slide")]
        public void NextSlide()
        {
            pointer++;
            output.sprite = slides[pointer% slides.Length];
        }
        
    }
}