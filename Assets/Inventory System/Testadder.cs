using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem;
using Unity.Entities;
using Test.CharacterStats;

public class Testadder : MonoBehaviour,IConvertGameObjectToEntity
{
    public uint ChangeValue;
    public List<ItemBaseSO> addering;
    public InventoryBase Inventory;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SubtractWalletValue() { Change = ChangeValue });

    }

    // Start is called before the first frame update
    void Start()
    {
        Inventory = this.GetComponent<CharacterInventory>().Inventory;
        addering[0].AddToInventory(Inventory);
        addering[0].AddToInventory(Inventory);
        addering[0].AddToInventory(Inventory);

        Inventory.ItemsInInventory[0].Item.Use(Inventory, 0, this.GetComponent<PlayerCharacter>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
