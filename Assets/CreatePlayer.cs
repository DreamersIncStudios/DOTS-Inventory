using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Dreamers.InventorySystem;
using Stats.Entities;
using Dreamers.InventorySystem.Base;
using Sirenix.OdinInspector;

public class CreatePlayer : SerializedMonoBehaviour
{

    public InventoryBase inventory;
    public EquipmentBase equipment;
    public CharacterClass Info;

    class Baking : Baker<CreatePlayer>
    {
        public override void Bake(CreatePlayer authoring)
        {
            var data = new CharacterInventory();
        //    data.Setup(authoring.inventory, authoring.equipment);
            AddComponentObject(data);
            AddComponentObject(new AnimatorComponent());
            BaseCharacterComponent character = new();
            character.SetupDataEntity(authoring.Info);
            AddComponentObject(character);
            AddComponent(new PlayerTag());
        }
    }
}
