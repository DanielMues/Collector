using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragAndDrop : MonoBehaviour
{
    CustomEventHandler customEventHandler;
    bool active;
    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.DragAndDropSwitch += DeactivateDragAndDropTrigger;
        active = true;
    }

    private void OnMouseDown()
    {
        if (active)
        {
            customEventHandler.UnitClicked(this.gameObject, transform.position);
        }
    }

    private void DeactivateDragAndDropTrigger(object sender, EventArgs args)
    {
        active = false;
    }
}
