using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Presets
{
    public static class Sizes
    {
        public static Vector3 ONE
        {
            get
            {
                return new Vector3(1, 1, 1);
            }
        }

        public static Vector3 TWO
        {
            get
            {
                return new Vector3(2, 2, 2);
            }
        }

        public static Vector3 POGOFRUIT
        {
            get
            {
                return new Vector3(0.25f, 0.25f, 0.25f);
            }
        }

        public static Vector3 CARROT
        {
            get
            {
                return new Vector3(2, 2, 2);
            }
        }

        public static Vacuumable.Size VAC_N
        {
            get
            {
                return Vacuumable.Size.NORMAL;
            }
        }

        public static Vacuumable.Size VAC_L
        {
            get
            {
                return Vacuumable.Size.LARGE;
            }
        }

        public static Vacuumable.Size VAC_G
        {
            get
            {
                return Vacuumable.Size.GIANT;
            }
        }
    }
}
