using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private string layerMaskA;
    [SerializeField] private string layerMaskB;
    public void ChangeLayer(bool a)
    {
        transform.gameObject.layer = LayerMask.NameToLayer(a? layerMaskA: layerMaskB);
    }
}
