
using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Dreamers.InventorySystem.UISystem
{
    public  partial class DisplayMenu 
    {
        readonly UIManager Manager;
        public bool Displayed { get { return (bool)MenuPanelParent; } }

        public DisplayMenu(BaseCharacter player,CharacterInventory characterInventory) {
            Manager = UIManager.instance;
        }

        public void OpenCharacterMenu(InventoryBase inventory) {
            MenuPanelParent = CreateMenu(
        new Vector2(0, 0),
        new Vector2(0, 0));
            playerStats = CreatePlayerPanel();
            itemPanel = CreateItemPanel();

        }

        public void CloseCharacterMenu() {
             Object.Destroy(MenuPanelParent);
        }
        private InventoryBase Inventory => CharacterInventory.Inventory;
        private EquipmentBase Equipment=> CharacterInventory.Equipment;
        private BaseCharacter Character => GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacter>();
        private CharacterInventory CharacterInventory => Character.GetComponent<CharacterInventory>();
       // PlayerCharacter PC;

        GameObject MenuPanelParent { get; set; }
        GameObject CreateMenu(Vector2 Size, Vector2 Position) {
            if (MenuPanelParent) 
                Object.Destroy(MenuPanelParent);

            GameObject Parent = Manager.UICanvas();
            GameObject MainPanel = Manager.GetPanel(Parent.transform, Size, Position);
            MainPanel.transform.localScale = Vector3.one;
            RectTransform PanelRect = MainPanel.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(1, 1);
            PanelRect.anchorMin = new Vector2(.0f, .0f);

            HorizontalLayoutGroup HLG = MainPanel.AddComponent<HorizontalLayoutGroup>();
            DisplayItems = (ItemType)0; // change to zero when all tab is added


            HLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            HLG.spacing = 10;
            HLG.childAlignment = TextAnchor.UpperLeft;
            HLG.childControlHeight = true; HLG.childControlWidth = false;
            HLG.childForceExpandHeight = true; HLG.childForceExpandWidth = true;

            return MainPanel;
        }
        GameObject playerStats { get;  set; }
        GameObject CreatePlayerPanel() {
            if (playerStats)
                Object.Destroy(playerStats);

            GameObject MainPanel = Manager.GetPanel(MenuPanelParent.transform, new Vector2(400, 300), new Vector2(0, 150));
            MainPanel.name = "Player Window";
            MainPanel.transform.SetSiblingIndex(0);
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = " Player";
            titleGO.fontSize = 24;
            VerticalLayoutGroup PlayerStatsWindow = Manager.GetPanel(MainPanel.transform, new Vector2(400, 450), new Vector2(0, 150)).AddComponent<VerticalLayoutGroup>();
            PlayerStatsWindow.name = "Player Stats Window";
            PlayerStatsWindow.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            PlayerStatsWindow.childAlignment = TextAnchor.UpperCenter;
            PlayerStatsWindow.childControlHeight = true; PlayerStatsWindow.childControlWidth = true;
            PlayerStatsWindow.childForceExpandHeight = true; PlayerStatsWindow.childForceExpandWidth = true;
            
            Text statsText= Manager.TextBox(PlayerStatsWindow.transform, new Vector2(400, 50)).GetComponent<Text>();
            statsText.alignment = TextAnchor.UpperLeft;
            statsText.text = " Player";
            statsText.fontSize = 24;

            statsText.text = Character.Name + " Lvl: " + Character.Level;
            statsText.text += "\nHealth:\t\t" + Character.CurHealth+"/" + Character.MaxHealth;
            statsText.text += "\nMana:\t\t\t" + Character.CurMana + "/" + Character.MaxMana+"\n";

            for (int i = 0; i < System.Enum.GetValues(typeof(AttributeName)).Length; i++)
            {
                statsText.text += "\n"+((AttributeName)i).ToString() + ":\t\t\t" + Character.GetPrimaryAttribute(i).BaseValue;
                statsText.text += " + "+ Character.GetPrimaryAttribute(i).BuffValue;
                statsText.text += " + " + Character.GetPrimaryAttribute(i).AdjustBaseValue;


            }
            currentEquipWindow = CurrentEquipWindow(MainPanel.transform);

            return MainPanel;


        }
        GameObject currentEquipWindow { get; set; }
        GameObject CurrentEquipWindow(Transform Parent) {
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
        Button ItemIconDisplay(Transform Parent, ItemBaseSO so) {
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
                    playerStats = CreatePlayerPanel();
                    itemPanel = CreateItemPanel();

                });
                }
            }
            return temp;
        }
        ItemType DisplayItems;
        GameObject itemPanel { get; set; }
        
        GameObject itemsDisplayerPanel { get; set; }


    }

 
}