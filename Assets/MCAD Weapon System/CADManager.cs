using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.CADSystem.Interfaces;
using Dreamers.Global;
using UnityEngine.UI;
using Dreamers.CADSystem;

namespace EquipmentStats
{

    public class CADManager : MonoBehaviour
    {
        [SerializeField]
        public CADGridSystem test;

        public List<GenericCADSlot> SlotsToAdd;
        private SkillsManager skillsManager;

        private void Awake()
        {
            test = new CADGridSystem(20);
            skillsManager = this.gameObject.GetComponent<SkillsManager>();


        }
        // Start is called before the first frame update
        void Start()
        {
          
             test.addSlotToCAD(SlotsToAdd[0], skillsManager, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public sealed class CADUISYSTEM {
        static GameObject HomePanel;
        public static void display(CADGridSystem CAD, Vector2 Size, Vector2 Position) {

            UIManager manager = UIManager.instance;
            HomePanel = manager.UICanvas();
            GameObject MainPanel = manager.GetPanel(HomePanel.transform, Size, Position);
            MainPanel.transform.localScale = Vector3.one;
            RectTransform PanelRect = MainPanel.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(1, 1);
            PanelRect.anchorMin = new Vector2(.0f, .0f);
            GameObject CADpanel = manager.GetPanel(HomePanel.transform, new Vector2(900, 900), Position);
            CADpanel.transform.localScale = Vector3.one;

            RectTransform cadPanelRect = CADpanel.GetComponent<RectTransform>();
            cadPanelRect.pivot = new Vector2(0.5f, .5f);
            cadPanelRect.anchorMax = new Vector2(.5f, .5f);
            cadPanelRect.anchorMin = new Vector2(.5f, .5f);
            cadPanelRect.anchoredPosition = Vector2.zero;

            GridLayoutGroup gridLayoutGroup = CADpanel.AddComponent<GridLayoutGroup>();
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = 10;
            gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            gridLayoutGroup.cellSize = new Vector2(75, 75);
            gridLayoutGroup.spacing = new Vector2(5, 5);
            gridLayoutGroup.padding = new RectOffset() { bottom = 20, top = 20, left = 20, right = 20 };
            for (int y = 0; y < 3; y++)
            { 
                for (int x = 0; x < 10; x++)
                {
                    manager.GetImage(CADpanel.transform, CAD.Grid[x, y].image);
                }
            }

        }

    }
}