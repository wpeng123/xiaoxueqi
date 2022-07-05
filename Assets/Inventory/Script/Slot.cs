using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotitem;
    public Image slotImage;

    public Text slotnum;
    public string slotInfo;
    public GameObject itemInSlot;
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }
    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemimage;
        slotnum.text = item.itemhiled.ToString();
        slotInfo = item.iteminfo;
    }
    //  private void Start()
    //   {
    //      this.slotImage = this.GetComponent<Image>();
    //  } 

}
