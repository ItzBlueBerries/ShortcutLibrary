using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extras
{
    public static class ShortcutExtensions
    {
        public static Material GetMaterial(this GameObject obj) => obj?.GetComponent<MeshRenderer>()?.material;

        public static Material GetSkinnedMaterial(this GameObject obj) => obj?.GetComponent<SkinnedMeshRenderer>()?.material;

        public static Material GetSlimeMaterial(this SlimeDefinition slimeDefinition, int structureIndex) => slimeDefinition?.AppearancesDefault[0]?.Structures[structureIndex]?.DefaultMaterials[0];

        public static void SetSlimeColors(this Material material, Color top, Color middle, Color bottom)
        {
            material.SetColor("_TopColor", top);
            material.SetColor("_MiddleColor", middle);
            material.SetColor("_BottomColor", bottom);
        }
    }
}
