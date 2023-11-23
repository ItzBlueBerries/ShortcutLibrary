using SRML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRML.Utils.Enum;
using UnityEngine;
using SRML.Utils;
using SRML.SR.Utils;
using SRML.SR;
using MonomiPark.SlimeRancher.DataModel;
using static ShortcutLib.Shortcut;
using HarmonyLib;
using MonomiPark.SlimeRancher.Regions;
using System.Reflection;

namespace ShortcutLib
{
    internal class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
        }

        public override void PostLoad()
        {
            if (Configuration.ABNORMAL_SIZES)
            {
                Configuration.LoadAbnormalSizes();
            }
        }
    }
}