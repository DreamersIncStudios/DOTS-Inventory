using Dreamers.InventorySystem.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dreamers.InventorySystem.UISystem
{
    public partial class DisplayMenu
    {
        GameObject currentEquipWindow { get; set; }
        GameObject CurrentEquipWindow(Transform Parent)
        {
            if (currentEquipWindow)
            { Object.Destroy(currentEquipWindow); }
            GridLayoutGroup CurrentEquips = Manager.GetPanel(Parent, new Vector2(400, 400), new Vector2(0, 150)).AddComponent<GridLayoutGroup>();
            CurrentEquips.transform.localScale = Vector3.one;
            CurrentEquips.padding = new RectOffset() { bottom = 15, top = 15, left = 15, right = 15 };
            CurrentEquips.childAlignment = TextAnchor.MiddleCenter;
            CurrentEquips.spacing = new Vector2(10, 10);


            for (int i = 0; i < System.Enum.GetValues(typeof(ArmorType)).Length; i++)
            {
                if (Equipment.EquippedArmor.TryGetValue((ArmorType)i, out ArmorSO value))
                {
                    ItemIconDisplay(CurrentEquips.transform, Equipment.EquippedArmor[(ArmorType)i]);

                }
                else
                {
                    ItemIconDisplay(CurrentEquips.transform, null);
                }
            }

            for (int i = 0; i < System.Enum.GetValues(typeof(WeaponSlot)).Length; i++)
            {
                if (Equipment.EquippedWeapons.TryGetValue((WeaponSlot)i, out WeaponSO value))
                {
                    ItemIconDisplay(CurrentEquips.transform, Equipment.EquippedWeapons[(WeaponSlot)i]);

                }
                else
                {
                    ItemIconDisplay(CurrentEquips.transform, null);
                }
            }

            return CurrentEquips.gameObject;
        }

        // Making a button to remove 
        Button ItemIconDisplay(Transform Parent, ItemBaseSO so)
        {
            Button temp = Manager.UIButton(Parent, "ItemName");
            if (so != null)
            {
                temp.image.sprite = so.Icon;
                if (!so.Icon)
                    temp.image.color = new Color() { a = 0.0f };
                if (so.Type == ItemType.Armor || so.Type == ItemType.Weapon)
                {
                    IEquipable equippedItem = (IEquipable)so;
                    temp.onClick.AddListener(() =>
                    {
                        equippedItem.Unequip(CharacterInventory, Character);
                        playerStats = CreatePlayerPanel(MenuPanelParent.transform);
                        GetInventoryPanel.Refresh();

                    });
                }
            }
            return temp;
        }


    }
}