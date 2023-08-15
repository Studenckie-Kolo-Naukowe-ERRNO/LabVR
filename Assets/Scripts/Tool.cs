using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Tool : XRGrabInteractable, IItem
{
    [Header("Item")]
    [SerializeField] private ItemData data;
    [Header("Events")]
    [SerializeField] protected UnityEvent OnToggle;
    [SerializeField] protected UnityEvent OnUnToggle;
    [SerializeField] protected UnityEvent InHand;

    protected bool toggled = false;
    protected byte inHands = 0;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        inHands++; 

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        inHands--;
        base.OnSelectExited(args);  
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        if (toggled) OnUnToggle.Invoke();
        else OnToggle.Invoke();
        toggled = !toggled;
        base.OnActivated(args);
    }

    private void Update()
    {
        if (IsHolded())
        {
            InHand.Invoke();
        }
    }

    public ItemData GetItemData()
    {
        return data;
    }

    public GameObject ThisObject()
    {
        return this.gameObject;
    }

    public bool IsHolded()
    {
        return inHands > 0;
    }
}
