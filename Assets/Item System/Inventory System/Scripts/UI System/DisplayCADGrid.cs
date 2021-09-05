
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
            public CADPanel( Vector2 Size, Vector2 Position)
            {
                Setup(Size, Position);
                gridSquares = Resources.Load<Sprite>("Sprites/Grid_Square");
            }
        

            public override GameObject CreatePanel(Transform Parent)
            {
                if (Top)
                    UnityEngine.Object.Destroy(Top);
                Top = Manager.GetPanel(Parent, Size, Position);
                Top.name = "CAD Menu";
                GridLayoutGroup gridLayoutGroup = Top.AddComponent<GridLayoutGroup>();
           
                gridSquares = Resources.Load<Sprite>("Sprites/Grid_Square");
                Manager.GetImage(Top.transform, gridSquares, "gridSquare");
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

        public CADPanel GetCADPanel = new CADPanel(new Vector2(1400,300), new Vector2(0,150));

        
    }

}