using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamers.InventorySystem.Base;
using Dreamers.Global;
namespace Dreamers.InventorySystem.UISystem
{
    public class DisplayInventory
    {
        UIManager Manager;

        public DisplayInventory(InventoryBase inventory, Vector2 Size, Vector2 Position)
        {
            Manager = UIManager.instance;
            Inventory = inventory;
            CreateUI(Size,Position);


        }
        public InventoryBase Inventory;

        Vector2 Size = new Vector2(400, 600);
        Vector2 Position = new Vector2(200, -600);

        void CreateUI( Vector2 Size, Vector2 Position)
        {
            GameObject Parent= Manager.UICanvas();
            GameObject MainPanel = Manager.Panel(Parent.transform,Size, Position);
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.UpperCenter;
            VLG.childControlHeight = false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;
            Text titleGO = Manager.TextBox(MainPanel.transform, new Vector2(400,50)).GetComponent<Text>();
            titleGO.alignment = TextAnchor.MiddleCenter;
            titleGO.text = "Inventory";
            titleGO.fontSize = 24;
            GridLayoutGroup InventoryGrid = Manager.Panel(MainPanel.transform, new Vector2(400,700), Position).AddComponent<GridLayoutGroup>();
            InventoryGrid.padding = new RectOffset() { bottom = 20, top = 20, left = 7, right = 7 };
            InventoryGrid.childAlignment = TextAnchor.UpperCenter;
            InventoryGrid.spacing = new Vector2(5,5)



            for (int i = 0; i < 20; i++)
            {
                Manager.UIButton(InventoryGrid.transform).GetComponentInChildren<Text>().text= "Button: "+i;

            }

        }

    }
    public class InventoryUICanvas {
    
    
    }
    public struct UIWindowSettings {
        public Vector2 Size;
        public Vector2 Postion;
        public Sprite Background;

    
    }

}