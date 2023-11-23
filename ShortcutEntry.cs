using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortcutLib.Shortcut;
using SRML;
using SRML.Utils.Enum;

namespace ShortcutLib
{
    [EnumHolder]
    internal class Enums
    {
        // public static readonly Identifiable.Id TEST_SLIME;
    }

    internal class ShortcutEntry : ModEntryPoint
    {
        public override void PreLoad() => HarmonyInstance.PatchAll();

        public override void Load()
        {

        }

        public override void PostLoad()
        {
            
        }
    }
}
