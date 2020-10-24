using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
    public interface ICommandLineAbility
    {
        bool IsCommandLineAbility { get; }
        bool IsOnCommandLine { get; set; }
    }
}