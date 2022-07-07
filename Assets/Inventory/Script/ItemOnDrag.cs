using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalparent;
    public Item thisitem;
    public Inventory mybag;
    public int currentItemID;
    public static int a;
    public static bool ispush = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        originalparent = transform.parent;
        currentItemID = originalparent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        thisitem = mybag.itemlist[currentItemID];
        Debug.Log(thisitem.name);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
       // Debug.Log(currentItemID);
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
            
            {
                ispush = true ;
                if (thisitem.name == "upgrade1") {a = 1;}
                if (thisitem.name == "upgrade2") a = 2;
                if (thisitem.name == "upgrade3") a = 3;
                if (thisitem.name == "upgrade4") a = 4;
                if (thisitem.name == "upgradehealth") a = 5;
                if (thisitem.name == " funnel") a = 6;
                if (thisitem.name == " sheild") a = 7;
                 transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position; 
            }
            
            GetComponent<CanvasGroup>().blocksRaycasts = true;
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
