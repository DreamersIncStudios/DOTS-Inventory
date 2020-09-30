using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCADSystem
{
    public interface iMCAD
    {


    }

    public enum MCADModes { 
        None,
        Off,
        Normal,
        Disabled,
        Training,
        Jammed,
        Overload, 
    }
}