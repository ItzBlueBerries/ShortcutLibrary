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

namespace ShortcutLib
{
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
            /*SRCallbacks.PreSaveGameLoad += delegate (SceneContext context)
            {
                GameObject inkFountain01 = Other.CreateFountain(something.TEST_LIQUID,
                    "zoneREEF/cellReef_Intro/Sector/Resources/waterFountain01",
                    "zoneRANCH/cellRanch_Entrance/Sector/Resources",
                    "inkFountain01",
                    new Vector3(101.6396f, 12.31307f, -131.6149f),
                    "RanchInk"
                );

                Other.ColorFountain(inkFountain01,
                    Color.white,
                    Color.yellow,
                    Color.white,
                    Color.yellow,
                    Color.white,
                    Color.yellow,
                    Color.white,
                    Color.yellow,
                    Color.white
                );
            };*/
        }

        public override void Load()
        {

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