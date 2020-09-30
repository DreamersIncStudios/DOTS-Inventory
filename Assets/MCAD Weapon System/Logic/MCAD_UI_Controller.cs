using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamers.Global;
using MCADSystem;
using Dreamers.InventorySystem;
using Dreamers.InventorySystem.Base;


namespace EquipmentStats
{
    public partial class MCAD_UI_Controller  : MonoBehaviour
    {
         UIManager Manager;
        public CharacterInventory Inventory;
        public MCADModes MCADMode { get { return EquipmentRef.MCADMode; } }
        public Vector2 Size = new Vector2(200, 250);
        public Vector2 Pos = new Vector2(1600, -750);
        // Start is called before the first frame update

        void Start()
        {
            Manager = UIManager.instance;
            Inventory = GetComponentInParent<CharacterInventory>();
        }


        GameObject MenuPanelParent { get; set; }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                MenuPanelParent = CreateMenu();
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton5))
            {
                Destroy(MenuPanelParent);
            }
            if (!Input.GetKey(KeyCode.JoystickButton5))
            {    Destroy(MenuPanelParent);
            }
        }
        GameObject CreateMenu() {
            if (MenuPanelParent)
                Object.Destroy(MenuPanelParent);
            GameObject Parent = Manager.UICanvas();
            GameObject MainPanel = Manager.Panel(Parent.transform, Size, Pos);
            MainPanel.transform.localScale = Vector3.one;
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.MiddleCenter;
            VLG.childControlHeight = false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;
            switch (MCADMode) {
                case MCADModes.Off:
                    for (int i = 0; i < System.Enum.GetNames(typeof(Menus)).Length; i++)
                    {
                        int index = i;

                        if (i == 1 || i == 3 || i == 4)
                        {
                            //do nothing

                        }
                        else
                        {
                            Button Temp = Manager.UIButton(MainPanel.transform, ((Menus)i).ToString());
                            if (i == 0)
                            {
                                Temp.Select();
                            }
                            Temp.navigation = Navigation.defaultNavigation;
                            Temp.onClick.AddListener(() => {
                                MenuPanelParent = CreateSub(index);
                            });
                        }
                    }
                    break;
                case MCADModes.Disabled:
                case MCADModes.Jammed:
                    for (int i = 0; i < System.Enum.GetNames(typeof(Menus)).Length; i++)
                    {
                        int index = i;
                        Button Temp = Manager.UIButton(MainPanel.transform, ((Menus)i).ToString());
                        if (i == 0)
                        {
                            Temp.Select();
                        }
                        Temp.navigation = Navigation.defaultNavigation;
                        Temp.onClick.AddListener(() => {
                            MenuPanelParent = CreateSub(index);
                        });
                        if (i == 1 || i == 3 || i == 4) {
                            Temp.interactable = false;
                        }
                    }
                    break;
                case MCADModes.Normal:
                case MCADModes.Overload:
                case MCADModes.Training:
                    for (int i = 0; i < System.Enum.GetNames(typeof(Menus)).Length; i++)
                    {
                        int index = i;
                        Button Temp = Manager.UIButton(MainPanel.transform, ((Menus)i).ToString());
                        if (i == 0)
                        {
                            Temp.Select();
                        }
                        Temp.navigation = Navigation.defaultNavigation;
                        Temp.onClick.AddListener(() => {
                            MenuPanelParent = CreateSub(index);
                        });
                    }
                    break;
            
            }

            


            return MainPanel;

        }

        GameObject CreateSub(int index) {
            if (MenuPanelParent)
                Object.Destroy(MenuPanelParent);

            GameObject Parent = Manager.UICanvas();
            GameObject MainPanel = Manager.Panel(Parent.transform, Size, Pos);
            MainPanel.transform.localScale = Vector3.one;
            VerticalLayoutGroup VLG = MainPanel.AddComponent<VerticalLayoutGroup>();
            VLG.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            VLG.childAlignment = TextAnchor.MiddleCenter;
            VLG.childControlHeight = false; VLG.childControlWidth = true;
            VLG.childForceExpandHeight = false; VLG.childForceExpandWidth = true;

            Button cancel;

            switch ((Menus)index) {
                case Menus.Skill:
                    cancel = Manager.UIButton(MainPanel.transform, "Cannel");
                    cancel.navigation = Navigation.defaultNavigation;
                    cancel.onClick.AddListener(() => {
                         MenuPanelParent = CreateMenu();
                    });
                    break;
                case Menus.Magic:
                    cancel = Manager.UIButton(MainPanel.transform, "Cannel");
                    cancel.navigation = Navigation.defaultNavigation;

                    cancel.onClick.AddListener(() => {
                        MenuPanelParent = CreateMenu();
                    });
                    break;
                case Menus.Item:
                    DisplayUsableItemsInInventory(MainPanel.transform);

                    break;
                case Menus.Summons:
                    cancel = Manager.UIButton(MainPanel.transform, "Cancel");
                    cancel.navigation = Navigation.defaultNavigation;

                    cancel.onClick.AddListener(() => {
                        MenuPanelParent = CreateMenu();
                    }); 
                    break;
                case Menus.OverDrive:    
                    cancel = Manager.UIButton(MainPanel.transform, "Cancel");
                    cancel.navigation = Navigation.defaultNavigation;

                    cancel.onClick.AddListener(() => {
                        MenuPanelParent = CreateMenu();
                    }); 
                    break;
                case Menus.Shortcuts:
                    //TBD
                   cancel = Manager.UIButton(MainPanel.transform, "Cancel");
                    cancel.navigation = Navigation.defaultNavigation;
                    cancel.Select();
                    cancel.onClick.AddListener(() => {
                        MenuPanelParent = CreateMenu();
                    }); 
                    break;


            }
            return MainPanel;
        }


        void DisplayUsableItemsInInventory(Transform ParentTransform) {
            InventoryBase inventory = Inventory.Inventory;
            List<Button> stupid = new List<Button>();
            foreach (ItemSlot Slot in inventory.ItemsInInventory) 
            {
               switch(Slot.Item.Type)
                {
                    case ItemType.General:
                    string ButtonText = Slot.Item.ItemName + " " + Slot.Count;
                    Button temp = Manager.UIButton(ParentTransform, ButtonText);
                    temp.navigation = Navigation.defaultNavigation;
                        stupid.Add(temp);
                break;
                }
            }
           Button cancel = Manager.UIButton(ParentTransform, "Cancel");
            cancel.navigation = Navigation.defaultNavigation;

            cancel.onClick.AddListener(() => {
                MenuPanelParent = CreateMenu();
            });
            if (stupid[0])
            {
                stupid[0].Select();
            }
            else {
                cancel.Select();
            }
        }
    }
}