using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem;
using Unity.Entities;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.UISystem;

public class EquipTesting : MonoBehaviour
{
    public GameObject Player;
    CharacterInventory PlayerInventory => Player.GetComponent<CharacterInventory>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerInventory.Inventory.AddToInventory(5));
        Debug.Log(PlayerInventory.Inventory.AddToInventory(5));
        Debug.Log(PlayerInventory.Inventory.AddToInventory(0));

        ArmorSO test =(ArmorSO)PlayerInventory.Inventory.ItemsInInventory[2].Item;
       Debug.Log(test.Equals(ItemDatabase.GetItem(0)));
        EquipItemFromInventory(0);

    }

    public bool EquipItemFromInventory(int ItemID) {
        bool check = false;
   
        ArmorSO test = (ArmorSO)PlayerInventory.Inventory.ItemsInInventory[0].Item;
        test.EquipItem(PlayerInventory, Player.GetComponent<PlayerCharacter>());

        return check;
    }

    //public bool EquipOne
    
}
