
using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Dreamers.InventorySystem.UISystem
{
    public partial class DisplayMenu
    {

        ItemType DisplayItems;
        GameObject itemPanel { get; set; }

        GameObject itemsDisplayerPanel { get; set; }

        GameObject CreateItemPanel()
        {
            if (itemPanel)
                Object.Destroy(itemPanel);

            GameObject MainPanel = Manager.GetPanel(TabView.transform, new Vector2(1400, 300), new Vector2(0, 150));
            MainPanel.transform.SetSiblingIndex(1);
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            MainPanel.name = "Item Window";
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = true; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = "Inventory";
            titleGO.fontSize = 24;
            titleGO.name = "Inventory Title TextBox";
            HorizontalLayoutGroup InventoryPanel = Manager.GetPanel(MainPanel.transform, new Vector2(400, 900), new Vector2(0, 150)).AddComponent<HorizontalLayoutGroup>();
            InventoryPanel.name = " Control Display Buttons";
            InventoryPanel.childControlHeight = false;
            InventoryPanel.childForceExpandHeight = false;
            MainPanel.name = "Items Window";

            for (int i = 0; i < 7; i++)
            {
                int test = i;
                Button Temp;
                if (i == 0)
                {
                    Temp = Manager.UIButton(InventoryPanel.transform, "All");
                    Temp.name = "All";


                }
                else
                {
                    Temp = Manager.UIButton(InventoryPanel.transform, ((ItemType)i).ToString());

                    Temp.name = ((ItemType)i).ToString();
                }
                Temp.onClick.AddListener(() =>
                {
                    DisplayItems = (ItemType)test;
                    itemsDisplayerPanel = ItemsDisplayPanel(MainPanel.transform, Inventory, DisplayItems);
                });

            }
            itemsDisplayerPanel = ItemsDisplayPanel(MainPanel.transform, Inventory, DisplayItems);

            return MainPanel;
        }


        GameObject ItemsDisplayPanel(Transform Parent, InventoryBase inventory, ItemType Type)
        {
            if (itemsDisplayerPanel)
            {
                Object.Destroy(itemsDisplayerPanel);
            }

            GridLayoutGroup Main = Manager.GetPanel(Parent, new Vector2(1400, 300), new Vector2(0, 150)).AddComponent<GridLayoutGroup>();
            Main.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            Main.spacing = new Vector2(20, 20);


            for (int i = 0; i < inventory.ItemsInInventory.Count; i++)
            {
                ItemSlot Slot = inventory.ItemsInInventory[i];
                int IndexOf = i;

                if (DisplayItems == ItemType.None)
                {
                    Button temp = ItemButton(Main.transform, Slot);
                    temp.onClick.AddListener(() =>
                    {
                        GameObject pop = PopUpItemPanel(temp.GetComponent<RectTransform>().anchoredPosition
                             + new Vector2(575, -175)
                             , Slot, IndexOf);
                        // pop.AddComponent<PopUpMouseControl>();
                    });
                }
                else if (Slot.Item.Type == Type)
                {
                    Button temp = ItemButton(Main.transform, Slot);
                    temp.onClick.AddListener(() =>
                    {
                        GameObject pop = PopUpItemPanel(temp.GetComponent<RectTransform>().anchoredPosition
                             + new Vector2(575, -175)
                             , Slot, IndexOf);
                        // pop.AddComponent<PopUpMouseControl>();
                    });
                }
            }

            return Main.gameObject;
        }
        GameObject PopUpItemPanel(Vector2 Pos, ItemSlot Slot, int IndexOf)
        {
            GameObject PopUp = Manager.GetPanel(Manager.UICanvas().transform, new Vector2(300, 300), Pos);
            HorizontalLayoutGroup group = PopUp.AddComponent<HorizontalLayoutGroup>();
            PopUp.AddComponent<PopUpMouseControl>();

            group.childControlWidth = false;

            Text info = Manager.TextBox(PopUp.transform, new Vector2(150, 300));
            info.text = Slot.Item.ItemName + "\n";
            info.text += Slot.Item.Description;

            VerticalLayoutGroup ButtonPanel = Manager.GetPanel(PopUp.transform, new Vector2(150, 300), Pos).AddComponent<VerticalLayoutGroup>();

            switch (Slot.Item.Type)
            {
                case ItemType.General:
                    Button use = Manager.UIButton(ButtonPanel.transform, "Use Item");
                    use.onClick.AddListener(() =>
                    {
                        RecoveryItemSO temp = (RecoveryItemSO)Slot.Item;
                        temp.Use(CharacterInventory, IndexOf, Character);

                        itemsDisplayerPanel = ItemsDisplayPanel(itemPanel.transform, Inventory, DisplayItems);
                        Object.Destroy(PopUp);

                    });
                    info.text += "\nQuantity: " + Slot.Count;
                    break;
                case ItemType.Armor:
                case ItemType.Weapon:
                    Button Equip = Manager.UIButton(ButtonPanel.transform, "Equip");
                    Equip.onClick.AddListener(() =>
                    {
                        switch (Slot.Item.Type)
                        {
                            case ItemType.Armor:
                                ArmorSO Armor = (ArmorSO)Slot.Item;
                                Armor.EquipItem(CharacterInventory, IndexOf, Character);
                                break;
                            case ItemType.Weapon:
                                WeaponSO weapon = (WeaponSO)Slot.Item;
                                weapon.EquipItem(CharacterInventory, IndexOf, Character);

                                break;
                        }
                        itemsDisplayerPanel = ItemsDisplayPanel(itemPanel.transform, Inventory, DisplayItems);
                        playerStats = CreatePlayerPanel(MenuPanelParent.transform);
                        Object.Destroy(PopUp);
                    });
                    Button Mod = Manager.UIButton(ButtonPanel.transform, "Modify");
                    Mod.onClick.AddListener(() => Debug.LogWarning("Implentation to be added once Skill/Magic system designed"));
                    Button Dismantle = Manager.UIButton(ButtonPanel.transform, "Dismantle");

                    break;
                case ItemType.Quest:
                    Button View = Manager.UIButton(ButtonPanel.transform, "View Item");

                    break;
                case ItemType.Blueprint_Recipes:
                    break;
            }
            if (Slot.Item.Type != ItemType.Quest)
            {
                Button Drop = Manager.UIButton(ButtonPanel.transform, "Drop");
                Drop.onClick.AddListener(() => {
                    Slot.Item.RemoveFromInventory(CharacterInventory, IndexOf);
                    Object.Destroy(PopUp);
                });
            }

            return PopUp;
        }

        Button ItemButton(Transform Parent, ItemSlot Slot)
        {
            Button temp = Manager.UIButton(Parent, Slot.Item.ItemName);
            temp.name = Slot.Item.ItemName;
            Text texttemp = temp.GetComponentInChildren<Text>();
            texttemp.alignment = TextAnchor.LowerCenter;
            if (Slot.Item.Stackable)
                texttemp.text += Slot.Count;
            temp.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
            temp.GetComponent<Image>().sprite = Slot.Item.Icon;
            return temp;
        }

    }
}
