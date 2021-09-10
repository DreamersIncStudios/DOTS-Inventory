
using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Stats;
using UnityEngine;
using UnityEngine.UI;
using DreamersInc.MagicSkill;
using System;

namespace Dreamers.InventorySystem.UISystem
{
    public partial class DisplayMenu
    {

       public  class CADPanel : Panel
        {
            Sprite gridSquares;
            CastingDevice CAD;
            public CADPanel( Vector2 Size, Vector2 Position, CastingDevice CAD)
            {
                Setup(Size, Position);
                gridSquares = Resources.Load<Sprite>("Sprites/Grid_Square");
                this.CAD = CAD;
            }

            public override GameObject CreatePanel(Transform Parent)
            {
                if (Top)
                    UnityEngine.Object.Destroy(Top);
                Top = Manager.GetPanel(Parent, Size, Position);
                Top.name = "CAD Menu";
                VerticalLayoutGroup verticalLayoutGroup = Top.AddComponent<VerticalLayoutGroup>();
              
                GameObject CADGRID = Manager.GetPanel(Top.transform, new Vector2(0, 300), new Vector2(0, 150));
                    GridLayoutGroup gridLayoutGroup = CADGRID.AddComponent<GridLayoutGroup>();
                gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayoutGroup.startCorner = GridLayoutGroup.Corner.LowerLeft;
                gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
                gridLayoutGroup.constraintCount = CAD.GetDimensions.x;
                gridLayoutGroup.cellSize = new Vector2(50, 50);
                gridSquares = Resources.Load<Sprite>("Sprites/Grid_Square");

                for (int y = 0; y < CAD.GetDimensions.y; y++)
                {
                    for (int x = 0; x < CAD.GetDimensions.x; x++)
                    {
                        int i = x; int j = y;
                        //Todo Check to see if slot is taken 

                        Button gridUI= Manager.UIButton(CADGRID.transform,"");
                        CAD.grid.GetGridObject(i, j).SetButton(gridUI);
                        gridUI.onClick.AddListener(() => {

                            CAD.grid.GetGridObject(i, j).AddMapToGrid(CAD.test.Grid);
                        });
                    }
                }
                return Top;
            }

            public override void DestoryPanel()
            {
                UnityEngine.Object.Destroy(Top);

            }
            public override void Refresh()
            {
                throw new System.NotImplementedException();
            }
        }

        public CADPanel GetCADPanel;

        
    }

}