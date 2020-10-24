using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.Global;
using MCADSystem;

namespace EquipmentStats
{
    public partial class MCAD_UI_Controller : MonoBehaviour
    {
        public int ActivationCost;
        public int ActivationMax;
        public int ActivationCur;

        bool Activiation { get {
                bool temp = false;
                if(MCADMode== MCADModes.Normal || MCADMode == MCADModes.Overload|| MCADMode == MCADModes.Training)
                    temp = ActivationCur >= ActivationCost;
                return temp;
            } }
        bool timeScaleChange { get { return Time.timeScale != 1.0f; } }
        public BaseEquipment EquipmentRef;

        public void setup(int activationCost, int activationMax, BaseEquipment equipmentRef)
        {
            ActivationCost = activationCost;
            ActivationMax = activationMax;
            EquipmentRef = equipmentRef;
        }

        public void IncreaseActivationPoints() { }
        public void DecreaseActivationPoints() { }

    }
}