using Dreamers.InventorySystem.Base;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dreamers.SkillMagicSystem
{
    public class UseItemSO : ActionScriptableObject
    {

        public void UseSkill(BaseCharacter character, ItemSlot itemSlot, InventoryBase inventoryFrom, int ItemIndex )
        {
            base.UseSkill(character);
            itemSlot.Item.Use(inventoryFrom,ItemIndex);

        }
    }
}