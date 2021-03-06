﻿
using System.Collections.Generic;

namespace EquipmentStats
{
    [System.Serializable]
    public class CalculatedStat:BaseStat
    {
        private List<ModifyingAttribute> _mods;
    private int _modValue;

        public CalculatedStat()
        {
            _mods = new List<ModifyingAttribute>();
            _modValue = 0;
            BaseValue = 5;
        }

        public void AddModifier(ModifyingAttribute mod)
        {
            _mods.Add(mod);
        }

        public void CalculateModValue()
        {
            _modValue = 0;
            if (_mods.Count > 0)
            {
                foreach (ModifyingAttribute mod in _mods)
                {
                    _modValue += (int)(mod.attribute.AdjustBaseValue * mod.ratio);
                }
            }
        }

        public new int AdjustBaseValue
        {
            get { return BaseValue + BuffValue + _modValue; }
        }

        public void Update()
        {
            CalculateModValue();
        }
    }

    public struct ModifyingAttribute {
        public Attributes attribute;
        public float ratio;
        public ModifyingAttribute(Attributes att, float rat)
        {
            attribute = att;
            ratio = rat;
        }
    }
}