using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsLab
{
    public class Whiteboard : MonoBehaviour
    {
        [SerializeField] private Camera drawingCamera;
        [SerializeField] private GameObject whiteboardCanvas;

        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;

        private Material drawingMaterial;
        private RenderTexture drawingRenderTexture;
        private bool calculateMarkers = false;
        private int markerCount = 0;

        private const int RESOLUTION = 512;

        void Start()
        {
            drawingMaterial = whiteboardCanvas.GetComponent<Renderer>().material;

            //float canvasRealSizeX = Mathf.Abs(leftCorner.localPosition.x - rightCorner.localPosition.x) * whiteboardCanvas.transform.localScale.x;
            //float canvasRealSizeY = Mathf.Abs(leftCorner.localPosition.z - rightCorner.localPosition.z) * whiteboardCanvas.transform.localScale.z;

            float res = (whiteboardCanvas.transform.localScale.x * transform.localScale.x)
                / (whiteboardCanvas.transform.localScale.z * transform.localScale.y);

            drawingRenderTexture = new RenderTexture((int)(RESOLUTION* res), RESOLUTION, 8, RenderTextureFormat.ARGB32);

            drawingMaterial.SetTexture("_BaseMap", drawingRenderTexture);

            drawingCamera.enabled = false;
            drawingCamera.targetTexture = drawingRenderTexture;
            
            float distance = Mathf.Abs(leftCorner.transform.position.y - rightCorner.transform.position.y)/2;
            drawingCamera.orthographicSize = distance;
        }
        private void Update()
        {
            if(calculateMarkers)
            {
                drawingCamera.Render();
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Marker"))
            {
                markerCount++;
                calculateMarkers = (markerCount>0);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Marker"))
            {
                markerCount--;
                calculateMarkers = (markerCount > 0);
            }
        }
    }

}
