using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsLab
{
    public class Whiteboard : MonoBehaviour
    {
        private Material drawingMaterial;
        [SerializeField] private RenderTexture drawingRenderTexture;
        [SerializeField] private Camera drawingCamera;
        public bool checkForMarker = false;

        void Start()
        {
            drawingMaterial = GetComponent<Renderer>().material;
            drawingRenderTexture = new RenderTexture(512, 256, 8, RenderTextureFormat.ARGB32);
            drawingMaterial.SetTexture("_BaseMap", drawingRenderTexture);
            drawingCamera.enabled = false;
            drawingCamera.targetTexture = drawingRenderTexture;
            drawingCamera.orthographicSize = 1.5f;
            drawingCamera.farClipPlane = 0.18f;
            drawingCamera.nearClipPlane = 0.09f;
        }
        private void Update()
        {
            if(checkForMarker)
            {
                drawingCamera.Render();
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Marker")
            {
                checkForMarker = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Marker")
            {
                checkForMarker = false;
            }
        }
    }

}
