using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    // Start is called before the first frame update
    public Item thisitem;
    public Inventory playerinventory;

    private void Update()
    {
       if (Crabed.i == true)
        {
            Crabed.i = false;
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         AddNewItem();
            Destroy(gameObject);
    }
    public void AddNewItem()
    {
        if (!playerinventory.itemlist.Contains(thisitem))
        {
           // playerinventory.itemlist.Add(thisitem);
            //InventoryManager.CreateNewItem(thisitem);
            for(int i = 0; i < playerinventory.itemlist.Count; i++)
            {
                if (playerinventory.itemlist[i] == null)
                {
                    playerinventory.itemlist[i] = thisitem;
                    break;
                }
            }
        }
        else
        {
            thisitem.itemhiled += 1;
        }
       // InventoryManager.RefreshItem();
    }
}
