
using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Stats;
using UnityEngine;
using UnityEngine.UI;
using DreamersInc.UI;
using DreamersInc.MagicSkill;
namespace Dreamers.InventorySystem.UISystem
{
    public  partial class DisplayMenu 
    {
        readonly UIManager Manager;
     
        public enum PanelToRefresh { Inventory, CAD, PlayerStat, Equipment}
     
        public bool Displayed { get; private set; }

        Canvas getCanvas;
        public DisplayMenu(BaseCharacter player, Canvas MenuCanvas) {
            Manager = UIManager.instance;
            this.Character = player;
            getCanvas = MenuCanvas;
            Displayed = false;
        }

        public void OpenCharacterMenu(InventoryBase inventory) {

         CreateMenu();
            Displayed = true;
        }

        public void CloseCharacterMenu() {
            Transform[] children = getCanvas.GetComponentsInChildren<Transform>();
            foreach (Transform child in getCanvas.transform)
            {
                Object.Destroy(child.gameObject);
            }

            Displayed = false;
        }

        private BaseCharacter Character;

        GameObject statPanel;
        void CreateMenu()
        {
            statPanel = Object.Instantiate(Manager.StatsWindow, getCanvas.transform);
            CharacterStatModal characterStat = statPanel.GetComponent<CharacterStatModal>();
            characterStat.ShowAsCharacterStats(Character, Character.GetComponent<CharacterInventory>().Equipment);
        }

        GameObject TabView;
        public Sprite[] TabIcons;
        GameObject CreateTabView( Transform parent) {
            if (TabView)
                Object.Destroy(TabView);

            GameObject contextPanel = Manager.GetPanel(parent, new Vector2(1400, 0), Vector2.zero, Global.LayoutGroup.Vertical, "Context Panel");
            VerticalLayoutGroup verticalLayoutGroup = contextPanel.GetComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childControlHeight = verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.childForceExpandHeight = false;
            GameObject tabGroup = Manager.GetPanel(contextPanel.transform, new Vector2(1400, 100), Vector2.zero, Global.LayoutGroup.Horizontal, "Tab Panel");
            TabGroup groupMaster = tabGroup.AddComponent<TabGroup>();
           TabButton inventory = Manager.TabButton (tabGroup.transform,"Inventory", "Inventory");
           
            inventory.OnTabSelected.
                AddListener(() => {
                 GetInventoryPanel.CreatePanel(contextPanel.transform);
            });
            inventory.OnTabDeslected.AddListener(() =>
               GetInventoryPanel.DestoryPanel());
            
            TabButton CAD = Manager.TabButton(tabGroup.transform,  "CAD", "CAD");
            CAD.OnTabSelected.AddListener(() => 
                GetCADPanel.CreatePanel(TabView.transform));
           
            CAD.OnTabDeslected.AddListener(() => GetCADPanel.DestoryPanel());
            TabButton save = Manager.TabButton(tabGroup.transform, "Save/Load", "Save/Load");

            groupMaster.TabIdle = Color.white;
            groupMaster.TabHovered = Color.red;
            groupMaster.TabSelected = Color.green;
            return contextPanel;
        }

    }

 
}