using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite itemimage;
    public int itemhiled;
    [TextArea]
    public string iteminfo;

    // Start is called before the first frame update
    
}
