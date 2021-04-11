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
        Debug.Log(AddItemsToInventory(5));
        Debug.Log(AddItemsToInventory(5));
        Debug.Log(AddItemsToInventory(0));

        ArmorSO test =(ArmorSO)PlayerInventory.Inventory.ItemsInInventory[2].Item;
       Debug.Log(test.Equals(ItemDatabase.GetItem(0)));
        EquipItemFromInventory(0);

       Vector3 temp = this.transform.forward * .5f;
    }

    public bool AddItemsToInventory(int ID)
    {
        bool check = new bool();
        //Arrange
        ItemDatabase.GetItem(ID).AddToInventory(PlayerInventory);
        check = ID == PlayerInventory.Inventory.FirstIndexOfItem(ID).ItemID;

        return check;
    }
    public bool EquipItemFromInventory(int ItemID) {
        bool check = false;
   
        ArmorSO test = (ArmorSO)PlayerInventory.Inventory.ItemsInInventory[0].Item;
        test.EquipItem(PlayerInventory, 0, Player.GetComponent<PlayerCharacter>());

        return check;
    }

    //public bool EquipOne
    
}
