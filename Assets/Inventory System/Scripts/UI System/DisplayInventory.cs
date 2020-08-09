using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamers.InventorySystem.Base;
using Dreamers.Global;
using Stats;
namespace Dreamers.InventorySystem.UISystem
{
    public class DisplayInventory
    {
        readonly UIManager Manager;

        public DisplayInventory(InventoryBase inventory, EquipmentBase equipment, BaseCharacter character, Vector2 Size, Vector2 Position)
        {
            Manager = UIManager.instance;
            Equipment = equipment;
            BC = character;
            Inventory = inventory;
            InventoryPanel = CreateUI(Size, Position);


        }

        InventoryBase Inventory { get; set; }
        EquipmentBase Equipment { get; set; }
        BaseCharacter BC { get; set; }
        
      //  Vector2 Position = new Vector2(200, -600);
        GameObject InventoryPanel { get; set; }

       GameObject CreateUI(Vector2 Size, Vector2 Position)
        {
            if (InventoryPanel) { Object.Destroy(InventoryPanel); }

            GameObject Parent = Manager.UICanvas();
            GameObject MainPanel = Manager.Panel(Parent.transform, Size, Position);
            MainPanel.transform.localScale = Vector3.one;

            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = true; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;
            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = "Inventory";
            titleGO.fontSize = 24;

            InventoryGrid(MainPanel);
            return MainPanel;
        }
        
        private GameObject inventoryGrid { get; set; }
        GameObject InventoryGrid(GameObject Parent) {
            if (inventoryGrid) {
                Object.Destroy(inventoryGrid);
            }
           
            GridLayoutGroup InventoryGrid = Manager.Panel(Parent.transform, new Vector2(300, 400), new Vector2(150,200)).AddComponent<GridLayoutGroup>();
            InventoryGrid.transform.localScale = Vector3.one;
            InventoryGrid.padding = new RectOffset() { bottom = 20, top = 20, left = 7, right = 7 };
            InventoryGrid.childAlignment = TextAnchor.UpperCenter;
            InventoryGrid.spacing = new Vector2(5, 5);



            foreach (ItemSlot itemSlot in Inventory.ItemsInInventory)
            {
                Button temp = Manager.UIButton(InventoryGrid.transform, itemSlot.Item.ItemName).GetComponent<Button>();
                temp.transform.localScale = Vector3.one;
                temp.onClick.AddListener(() => CreateItemUsageWindow(itemSlot.Item, Inventory.ItemsInInventory.IndexOf(itemSlot)));
                temp.name = itemSlot.Item.ItemName;
                temp.GetComponentInChildren<Text>().text = itemSlot.Item.ItemName;
                temp.GetComponent<Image>().sprite = itemSlot.Item.Icon;

            }
            inventoryGrid = InventoryGrid.gameObject;
            return inventoryGrid;
        }
        GameObject activeWindow;
        void CreateItemUsageWindow(ItemBaseSO SO, int IndexOf)
        {
            if (activeWindow)
            {
                Object.Destroy(activeWindow);
            }
            switch (SO.Type)
            {
                case ItemType.General:
                    activeWindow = ModalWindowItem((RecoveryItemSO)SO , IndexOf);
                    break;
                case ItemType.Weapon:
                    activeWindow = ModalWindowWeapon((WeaponSO)SO, IndexOf);
                    break;
                case ItemType.Armor:
                    activeWindow = ModalWindowArmor((ArmorSO)SO, IndexOf);
                    break;
                    
            }
            Debug.Log("Pressed");
        }

        GameObject ModalWindowBase()
        {

            GameObject Parent = Manager.UICanvas();
            Parent.transform.localScale = Vector3.one;
            GameObject MainPanel = Manager.Panel(Parent.transform, new Vector2(600, 300), new Vector2(0, 150));
            MainPanel.name = "Modal Item Window";
            RectTransform PanelRect = MainPanel.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(.5f, .5f);
            PanelRect.anchorMin = new Vector2(.5f, .5f);
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.spacing = 10;
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = true; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;
            return MainPanel;
        }

        GameObject ModalWindowItem(RecoveryItemSO Item, int IndexOf)
        {
            GameObject VLG = ModalWindowBase();
            Text titleGO = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.text = Item.ItemName;
            titleGO.fontSize = 24;
            titleGO.alignment = TextAnchor.MiddleCenter;

            Text TaskDescription = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            TaskDescription.text = "What would you like to do with " + Item.ItemName + "?";
            TaskDescription.fontSize = 20;
            TaskDescription.alignment = TextAnchor.MiddleCenter;


            HorizontalLayoutGroup ButtonGroup = Manager.Panel(VLG.transform, new Vector2(600, 300), new Vector2(0, 150)).AddComponent<HorizontalLayoutGroup>();
            ButtonGroup.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            ButtonGroup.spacing = 8;
            
            Button use = Manager.UIButton(ButtonGroup.transform, "Use").GetComponent<Button>();
            use.onClick.AddListener(() => {
                Item.Use(Inventory, IndexOf, BC);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });

            Button Equip = Manager.UIButton(ButtonGroup.transform ,"Equip Item").GetComponent<Button>();
            Equip.onClick.AddListener(() => {
                Item.EquipItem(Inventory, Equipment, IndexOf, BC);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button Drop = Manager.UIButton(ButtonGroup.transform, "Drop Item").GetComponent<Button>();
            Drop.onClick.AddListener(() => {
                //Create a function to put Item on ground in Front of player
                // Tie into DOTS Spawner System;
                Item.RemoveFromInventory(Inventory, IndexOf);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button cancel = Manager.UIButton(ButtonGroup.transform,"Cancel").GetComponent<Button>();
            cancel.onClick.AddListener(() => {
                Object.Destroy(VLG);
            });

            return VLG.gameObject;
        }

        GameObject ModalWindowWeapon(WeaponSO Item, int IndexOf)
        {
            GameObject VLG = ModalWindowBase();
            Text titleGO = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.text = Item.ItemName;
            titleGO.fontSize = 24;
            titleGO.alignment = TextAnchor.MiddleCenter;

            Text TaskDescription = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            TaskDescription.text = "What would you like to do with " + Item.ItemName + "?";
            TaskDescription.fontSize = 20;
            TaskDescription.alignment = TextAnchor.MiddleCenter;


            HorizontalLayoutGroup ButtonGroup = Manager.Panel(VLG.transform, new Vector2(600, 300), new Vector2(0, 150)).AddComponent<HorizontalLayoutGroup>();
            ButtonGroup.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            ButtonGroup.spacing = 8;

     
            Button Equip = Manager.UIButton(ButtonGroup.transform, "Equip Item").GetComponent<Button>();
            Equip.onClick.AddListener(() => {
                Item.EquipItem(Inventory, Equipment, IndexOf, BC);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button Drop = Manager.UIButton(ButtonGroup.transform, "Drop Item").GetComponent<Button>();
            Drop.onClick.AddListener(() =>
            {
                Item.RemoveFromInventory(Inventory, IndexOf);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button dismantle = Manager.UIButton(ButtonGroup.transform, "Dismante").GetComponent<Button>();
            dismantle.onClick.AddListener(() =>
            {
                Debug.LogWarning("Dismantle function need to be implemented ");
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button cancel = Manager.UIButton(ButtonGroup.transform, "Cancel").GetComponent<Button>();
            cancel.onClick.AddListener(() => {
                Object.Destroy(VLG);
            });


            return VLG.gameObject;
        }


        GameObject ModalWindowArmor(ArmorSO Item, int IndexOf)
        {
            GameObject VLG = ModalWindowBase();
            Text titleGO = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.text = Item.ItemName;
            titleGO.fontSize = 24;
            titleGO.alignment = TextAnchor.MiddleCenter;

            Text TaskDescription = Manager.TextBox(VLG.transform, new Vector2(400, 50)).GetComponent<Text>();
            TaskDescription.text = "What would you like to do with " + Item.ItemName + "?";
            TaskDescription.fontSize = 20;
            TaskDescription.alignment = TextAnchor.MiddleCenter;


            HorizontalLayoutGroup ButtonGroup = Manager.Panel(VLG.transform, new Vector2(600, 300), new Vector2(0, 150)).AddComponent<HorizontalLayoutGroup>();
            ButtonGroup.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            ButtonGroup.spacing = 8;

            Button Equip = Manager.UIButton(ButtonGroup.transform, "Equip Item").GetComponent<Button>();
            Equip.onClick.AddListener(() => {
                Item.EquipItem(Inventory, Equipment, IndexOf, BC);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button Drop = Manager.UIButton(ButtonGroup.transform, "Drop Item").GetComponent<Button>();
            Drop.onClick.AddListener(() =>
            {
                Item.RemoveFromInventory(Inventory, IndexOf);
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });

            Button dismantle = Manager.UIButton(ButtonGroup.transform, "Dismante").GetComponent<Button>();
            dismantle.onClick.AddListener(() =>
            {
                Debug.LogWarning("Dismantle function need to be implemented ");
                InventoryGrid(InventoryPanel);
                Object.Destroy(VLG);
            });
            Button cancel = Manager.UIButton(ButtonGroup.transform, "Cancel").GetComponent<Button>();

            cancel.onClick.AddListener(() => {
                Object.Destroy(VLG);
            });

            return VLG.gameObject;
        }
    }
        public class InventoryUICanvas
        {


        }
        public struct UIWindowSettings
        {
            public Vector2 Size;
            public Vector2 Postion;
            public Sprite Background;


        }

    
}
