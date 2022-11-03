using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    CustomEventHandler customEventHandler;
    Vector3 mousePositionOffset;
    bool selected;

    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        selected = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        if (selected)
        {
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
            customEventHandler.DeleteUnitOnField(this.gameObject, transform.position);
            selected = true;
        }
        else
        {
            customEventHandler.SendPlaceUnit(this.gameObject, transform.position);
            selected = false;
        }
        
    }
}
