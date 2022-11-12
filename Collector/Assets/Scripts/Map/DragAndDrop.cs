using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    CustomEventHandler customEventHandler;

    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
    }

    private void OnMouseDown()
    {
        customEventHandler.UnitClicked(this.gameObject, transform.position);
    }
}
