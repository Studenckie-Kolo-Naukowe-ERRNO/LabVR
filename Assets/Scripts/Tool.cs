using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Tool : XRGrabInteractable
{
    [Header("Tool")]
    [SerializeField] protected string layerMaskA = "Tools";
    [SerializeField] protected string layerMaskB = "ToolsInUse";

    [Header("Events")]
    [SerializeField] protected UnityEvent OnToggle;
    [SerializeField] protected UnityEvent OnUnToggle;
    [SerializeField] protected UnityEvent InHand;

    protected bool toggled = false;
    protected byte inHands = 0;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        inHands++; 
        LayerChanger.SetObjectLayer(this.gameObject, layerMaskB);

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        inHands--;
        if(inHands == 0)
        {
            LayerChanger.SetObjectLayer(this.gameObject, layerMaskA);
        }
        base.OnSelectExited(args);  
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        if (toggled) OnUnToggle.Invoke();
        else OnToggle.Invoke();
        toggled = !toggled;
        base.OnActivated(args);
    }

    private void Start()
    {
        LayerChanger.SetObjectLayer(this.gameObject, layerMaskA);
    }

    private void Update()
    {
        if (inHands>0)
        {
            InHand.Invoke();
        }
    }

}
