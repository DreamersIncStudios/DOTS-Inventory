using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem;
using Unity.Entities;

public class Testadder : MonoBehaviour,IConvertGameObjectToEntity
{
    public uint ChangeValue;
    public List<ItemBaseSO> addering;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SubtractWalletValue() { Change = ChangeValue });

    }

    // Start is called before the first frame update
    void Start()
    {
        addering[0].AddToInventory(this.GetComponent<CharacterInventory>().Inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
