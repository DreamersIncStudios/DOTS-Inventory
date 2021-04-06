using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem;
using Stats;

public class EquipTesting
{
    // A Test behaves as an ordinary method
    [Test]
    public void OnlyAllowOneChestPiece()
    {
        //Arrange
        InventoryBase inventory = new InventoryBase();
        EquipmentBase equipment = new EquipmentBase();
        PlayerCharacter PC = new PlayerCharacter();
        ArmorSO chest1 = (ArmorSO)ItemDatabase.GetItem(5);
        ArmorSO chest2 = (ArmorSO)ItemDatabase.GetItem(5);

        chest1.AddToInventory(inventory);
        chest2.AddToInventory(inventory);


        //Act
        chest1.EquipItem(inventory, equipment, 0, PC);

        //Assert



    }

}
