
using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Stats;
using UnityEngine;
using UnityEngine.UI;
using DreamersInc.UI;

namespace Dreamers.InventorySystem.UISystem
{
    public partial class DisplayMenu
    {

       public  class CADPanel : Panel
        {
            public CADPanel( Vector2 Size, Vector2 Position)
            {
                Setup(Size, Position);
            }

            public override GameObject CreatePanel(Transform Parent)
            {
                if (Top)
                    Object.Destroy(Top);
                Top = Manager.GetPanel(Parent, Size, Position);
                Top.name = "CAD Menu";

                return Top;
            }

            public override void DestoryPanel()
            {
                Object.Destroy(Top);

            }
            public override void Refresh()
            {
                throw new System.NotImplementedException();
            }
        }

        public CADPanel GetCADPanel = new CADPanel(new Vector2(1400,300), new Vector2(0,150));

        
    }

}