using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Tool : XRGrabInteractable
{
    [Header("Tool")]
    [SerializeField] private string layerMaskA = "Tools";
    [SerializeField] private string layerMaskB = "ToolsInUse";

    [Header("Toggle")]
    [SerializeField] private UnityEvent OnToggle;
    [SerializeField] private UnityEvent OnUnToggle;
    private bool toggled = false;

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

    protected override void OnActivated(ActivateEventArgs args)
    {
        if (toggled) OnUnToggle.Invoke();
        else OnToggle.Invoke();
        toggled = !toggled;
        base.OnActivated(args);
    }

    public void ChangeLayer(bool a)
    {
        transform.gameObject.layer = LayerMask.NameToLayer(a ? layerMaskA : layerMaskB);
    }

}
