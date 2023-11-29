using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Presets
{
    public static class Paths
    {
        /// <summary>
        /// A preset path for a slime spawner.
        /// </summary>
        public static string SLIME_SPAWNER_PATH
        {
            get
            {
                return "zoneQUARRY/cellQuarry_Entrance/Sector/Slimes/nodeSlime";
            }
        }

        /// <summary>
        /// A preset path for a water fountain.
        /// </summary>
        public static string WATER_FOUNTAIN_PATH
        {
            get
            {
                return "zoneREEF/cellReef_Intro/Sector/Resources/waterFountain01";
            }
        }

        /// <summary>
        /// A preset path for a cosmetic pod. (Secret Styles)
        /// </summary>
        public static string COSMETIC_POD_PATH
        {
            get
            {
                return "zoneREEF/cellReef_GordoIsland/Sector/Loot/treasurePodCosmetic";
            }
        }

        /// <summary>
        /// A preset path for a treasure pod. (Rank 3)
        /// </summary>
        public static string TREASURE_POD_RANK3_PATH
        {
            get
            {
                return "zoneDESERT/cellDesert_TempleReceiver/Sector/Loot/treasurePod Rank3";
            }
        }
    }
}
