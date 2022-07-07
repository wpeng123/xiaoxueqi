using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory mybag;
    public GameObject slotGrid;
    // public Slot slotprefab;
    public GameObject emptyslot;
    public Text iteminfo;
    public List<GameObject> slots = new List<GameObject>();
      void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.iteminfo.text = "";
    }
    public static void UpdateItemInfo(string itemDescription)
    {
        instance.iteminfo.text = itemDescription;
    }
  /*  public  static void CreateNewItem(Item item)
    {
        Slot newitem = Instantiate(instance.slotprefab,instance.slotGrid.transform.position ,Quaternion.identity);
        newitem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newitem.slotitem = item;
        newitem.slotImage.sprite = item.itemimage;
        newitem.slotnum.text = item.itemhiled.ToString();
         
    }*/
    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            Debug.Log(instance.slotGrid.transform.childCount);
            if (instance.slotGrid.transform.childCount == 0)
                break;

            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        for(int i = 0; i < instance.mybag.itemlist.Count; i++)
        {
            // CreateNewItem(instance.mybag.itemlist[i]);
            instance.slots.Add(Instantiate(instance.emptyslot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.mybag.itemlist[i]);
        }
    }
}
