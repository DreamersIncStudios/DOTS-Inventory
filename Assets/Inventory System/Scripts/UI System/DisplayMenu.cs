using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamers.Global;
using Stats;
using Dreamers.InventorySystem.Base;

namespace Dreamers.InventorySystem.UISystem
{
    public class DisplayMenu 
    {
        readonly UIManager Manager;
        public DisplayMenu(PlayerCharacter player, EquipmentBase equipment, InventoryBase inventory) {
            Manager = UIManager.instance;
            MenuPanelParent = CreateMenu(
                new Vector2(0,0),
                new Vector2(0,0));
            playerStats = CreatePlayerPanel(player,equipment);
            CreateItemPanel(inventory);
        }
       // PlayerCharacter PC;

        GameObject MenuPanelParent { get; set; }
        GameObject CreateMenu(Vector2 Size, Vector2 Position) {
            if (MenuPanelParent) 
                Object.Destroy(MenuPanelParent);

            GameObject Parent = Manager.UICanvas();
            GameObject MainPanel = Manager.Panel(Parent.transform, Size, Position);
            MainPanel.transform.localScale = Vector3.one;
            RectTransform PanelRect = MainPanel.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(1, 1);
            PanelRect.anchorMin = new Vector2(.0f, .0f);

            HorizontalLayoutGroup HLG = MainPanel.AddComponent<HorizontalLayoutGroup>();


            HLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            HLG.spacing = 10;
            HLG.childAlignment = TextAnchor.UpperLeft;
            HLG.childControlHeight = true; HLG.childControlWidth = false;
            HLG.childForceExpandHeight = true; HLG.childForceExpandWidth = true;

            return MainPanel;
        }
        GameObject playerStats { get;  set; }
        GameObject CreatePlayerPanel(PlayerCharacter Player , EquipmentBase equipment) {
            if (playerStats)
                Object.Destroy(playerStats);

            GameObject MainPanel = Manager.Panel(MenuPanelParent.transform, new Vector2(400, 300), new Vector2(0, 150));
            MainPanel.name = "Player Window";

            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = " Player";
            titleGO.fontSize = 24;
            VerticalLayoutGroup PlayerStatsWindow = Manager.Panel(MainPanel.transform, new Vector2(400, 450), new Vector2(0, 150)).AddComponent<VerticalLayoutGroup>();
            PlayerStatsWindow.name = "Player Stats Window";
            PlayerStatsWindow.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            PlayerStatsWindow.childAlignment = TextAnchor.UpperCenter;
            PlayerStatsWindow.childControlHeight = true; PlayerStatsWindow.childControlWidth = true;
            PlayerStatsWindow.childForceExpandHeight = true; PlayerStatsWindow.childForceExpandWidth = true;
            
            Text statsText= Manager.TextBox(PlayerStatsWindow.transform, new Vector2(400, 50)).GetComponent<Text>();
            statsText.alignment = TextAnchor.UpperLeft;
            statsText.text = " Player";
            statsText.fontSize = 24;

            statsText.text = Player.Name + " Lvl: " + Player.Level;
            statsText.text += "\nHealth:\t\t" + Player.CurHealth+"/" +Player.MaxHealth;
            statsText.text += "\nMana:\t\t\t" + Player.CurMana + "/" + Player.MaxMana+"\n";

            for (int i = 0; i < System.Enum.GetValues(typeof(AttributeName)).Length; i++)
            {
                statsText.text += "\n"+((AttributeName)i).ToString() + ":\t\t\t\t\t" + Player.GetPrimaryAttribute(i).AdjustBaseValue;
        }
            currentEquipWindow = CurrentEquipWindow(MainPanel.transform, equipment);

            return MainPanel;


        }
        GameObject currentEquipWindow { get; set; }
        GameObject CurrentEquipWindow(Transform Parent,EquipmentBase equipment) {
            if (currentEquipWindow) 
             { Object.Destroy(currentEquipWindow); }
            GridLayoutGroup CurrentEquips = Manager.Panel(Parent, new Vector2(400, 400), new Vector2(0, 150)).AddComponent<GridLayoutGroup>();
            CurrentEquips.transform.localScale = Vector3.one;
            CurrentEquips.padding = new RectOffset() { bottom = 15, top = 15, left = 15, right = 15 };
            CurrentEquips.childAlignment = TextAnchor.MiddleCenter;
            CurrentEquips.spacing = new Vector2(10, 10);

            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            if (equipment.PrimaryWeapon)
            {
                ItemIconDisplay(CurrentEquips.transform, equipment.PrimaryWeapon.Icon);
            }
            else
            {
                ItemIconDisplay(CurrentEquips.transform, null);
            }
            return CurrentEquips.gameObject;
        }

        // Do we need to add a name tag?
        Image ItemIconDisplay(Transform Parent,Sprite image) {
            Image temp = new GameObject().AddComponent<Image>();
            temp.transform.SetParent(Parent, false);
            temp.sprite = image;
            if(!image)
                temp.color = new Color() { a = 0.0f };
            return temp;
        }
        ItemType DisplayItems = (ItemType)1;
        GameObject CreateItemPanel(InventoryBase inventory)
        {
            GameObject MainPanel = Manager.Panel(MenuPanelParent.transform, new Vector2(1400, 300), new Vector2(0, 150));
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            MainPanel.name = "Item Window";
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight =true; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = "Inventory";
            titleGO.fontSize = 24;
            titleGO.name = "Inventory Title TextBox";
            HorizontalLayoutGroup InventoryPanel = Manager.Panel(MainPanel.transform, new Vector2(400, 900), new Vector2(0, 150)).AddComponent<HorizontalLayoutGroup>();
            InventoryPanel.name = " Control Display Buttons";
            InventoryPanel.childControlHeight = false;
            InventoryPanel.childForceExpandHeight = false;
            MainPanel.name = "Items Window";

            for (int i = 1; i < 6; i++)
            {
                int test = i;
                Button Temp =Manager.UIButton(InventoryPanel.transform, ((ItemType)i).ToString());
                Temp.name = ((ItemType)i).ToString();
                Temp.onClick.AddListener(() => {
                    DisplayItems = (ItemType)test;
            itemsDisplayerPanel = ItemsDisplayPanel(MainPanel.transform, inventory, DisplayItems);
                });
            }
            itemsDisplayerPanel = ItemsDisplayPanel(MainPanel.transform, inventory, DisplayItems);

            return MainPanel;
        }

        GameObject itemsDisplayerPanel { get; set; }
        GameObject ItemsDisplayPanel(Transform Parent, InventoryBase inventory, ItemType Type) {
            if (itemsDisplayerPanel)
                Object.Destroy(itemsDisplayerPanel);

            GridLayoutGroup Main = Manager.Panel(Parent, new Vector2(1400, 300), new Vector2(0, 150)).AddComponent<GridLayoutGroup>();
            Main.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            Main.spacing = new Vector2(20, 20);
            foreach (ItemSlot Item in inventory.ItemsInInventory)
            {
                if (Item.Item.Type == Type)
                {
                    Button temp =ItemButton(Main.transform, Item);
                    temp.onClick.AddListener(() =>
                    {
                        GameObject pop = PopUpItemPanel(temp.GetComponent<RectTransform>().anchoredPosition
                             + new Vector2(625, -225));
                        pop.AddComponent<PopUpMouseControl>();
                    });
                      
                
                }
            }
            
            return Main.gameObject;
        }
        Button ItemButton(Transform Parent, ItemSlot Slot) {
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

        GameObject PopUpItemPanel(Vector2 Pos)
        { 
            GameObject PopUp= Manager.Panel(Manager.UICanvas().transform, new Vector2(400, 400), Pos);

            return PopUp;
        }
    }

 
}