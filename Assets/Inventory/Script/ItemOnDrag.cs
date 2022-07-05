using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalparent;
    public Inventory mybag;
    public int currentItemID;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalparent = transform.parent;
        currentItemID = originalparent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            var temp = mybag.itemlist[currentItemID];
            mybag.itemlist[currentItemID] = mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
            mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalparent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.name== "Assembly groove")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform );
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            
              return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = mybag.itemlist[currentItemID];

            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID) mybag.itemlist[currentItemID] = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        //other position
        transform.SetParent(originalparent);
        transform.position = originalparent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }


}
