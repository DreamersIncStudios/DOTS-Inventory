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
        public DisplayMenu(PlayerCharacter player, EquipmentBase equipment) {
            Manager = UIManager.instance;
            MenuPanelParent = createMenu(
                new Vector2(0,0),
                new Vector2(0,0));
            playerstats = CreatePlayerPanel(player,equipment);
            CreateItemPanel();
        }
       // PlayerCharacter PC;

        GameObject MenuPanelParent { get; set; }
        GameObject createMenu(Vector2 Size, Vector2 Position) {
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
        GameObject playerstats { get;  set; }
        GameObject CreatePlayerPanel(PlayerCharacter Player , EquipmentBase equipment) {
            if (playerstats)
                Object.Destroy(playerstats);

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
            VerticalLayoutGroup PlayerStatsWindow = Manager.Panel(MainPanel.transform, new Vector2(400, 425), new Vector2(0, 150)).AddComponent<VerticalLayoutGroup>();
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
          GridLayoutGroup CurrentEquips = Manager.Panel(MainPanel.transform, new Vector2(400, 425), new Vector2(0, 150)).AddComponent<GridLayoutGroup>();

            statsText.text += "\nPrimary Weapon: ";
            statsText.text += equipment.PrimaryWeapon ? equipment.PrimaryWeapon.ItemName : "";
            statsText.text += "\nSecondary Weapon: ";
            statsText.text += equipment.SecondaryWeapon ? equipment.SecondaryWeapon.ItemName : "";

            statsText.text += "\nProjectile: ";
            statsText.text += equipment.ProjectileWeopon ? equipment.ProjectileWeopon.ItemName : "";


            statsText.text += "\n";
            statsText.text += "\n";
            return MainPanel;


        }
        GameObject CreateItemPanel()
        {
            GameObject MainPanel = Manager.Panel(MenuPanelParent.transform, new Vector2(1400, 300), new Vector2(0, 150));
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            MainPanel.name = "Item Window";
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight =false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400, 50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = "Inventory";
            titleGO.fontSize = 24;
            GameObject Inventory = Manager.Panel(MainPanel.transform, new Vector2(400, 900), new Vector2(0, 150));
            MainPanel.name = "Player Stats Window";

            return MainPanel;
        }

    }
}