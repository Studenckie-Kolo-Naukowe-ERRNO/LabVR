using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRenderTexture : MonoBehaviour
{
    private Renderer boardRenderer;
    [SerializeField] private RenderTexture boardRenderTexture;
    private Mesh boardMesh;
    [SerializeField] private Camera drawingCamera;
    void Start()
    {
        boardRenderer = GetComponent<Renderer>();
        boardMesh = GetComponent<Mesh>();
        boardRenderer.material = GetComponent<Renderer>().material;
        //* (int)(boardMesh.bounds.size.x/boardMesh.bounds.size.z);
        boardRenderTexture = new RenderTexture(512, 256, 16, RenderTextureFormat.ARGB32);
        boardRenderer.material.SetTexture("_BaseMap", boardRenderTexture);
        drawingCamera.targetTexture = boardRenderTexture;
    }

    void Update()
    {
        drawingCamera.Render();
    }


}
