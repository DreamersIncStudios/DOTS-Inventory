using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Dreamers.InventorySystem
{
    public class InventoryAuthoring : MonoBehaviour
    {


        class Baking : Baker<InventoryAuthoring>
        {
            public override void Bake(InventoryAuthoring authoring)
            {
                var data = new CharacterInventory();

                AddComponentObject(data);   
            }
        }

    }
}