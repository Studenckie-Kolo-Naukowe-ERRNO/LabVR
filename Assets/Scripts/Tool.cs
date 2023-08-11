using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Tool : XRGrabInteractable
{
    [SerializeField] private string layerMaskA = "Tools";
    [SerializeField] private string layerMaskB = "ToolsInUse";

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        ChangeLayer(true);
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        ChangeLayer(false);
        base.OnSelectExited(args);  
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        ChangeLayer(true);
        base.OnSelectEntered(interactor);  
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        ChangeLayer(false);
        base.OnSelectExited(interactor);
    }

    public void ChangeLayer(bool a)
    {
        transform.gameObject.layer = LayerMask.NameToLayer(a ? layerMaskA : layerMaskB);
    }

}
