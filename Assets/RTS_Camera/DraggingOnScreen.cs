using RTS_Cam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingOnScreen : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public RTS_Camera rtsCam;

    public void OnEndDrag(PointerEventData eventData)
    {
        rtsCam.dragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rtsCam.dragging = true;
    }
}
