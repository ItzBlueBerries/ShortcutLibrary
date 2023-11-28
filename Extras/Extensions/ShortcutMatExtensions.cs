using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extensions
{
    public static class ShortcutMatExtensions
    {
        /// <summary>
        /// Gets the <see cref="Material"/> of a <see cref="SlimeAppearanceStructure"/> at the <see cref="int"/> index via the <see cref="SlimeAppearance"/>.
        /// </summary>
        /// <param name="slimeAppearance">The <see cref="SlimeAppearance"/> to grab the structures from.</param>
        /// <param name="structureIndex">The index <see cref="int"/> to find the specific structure to grab the <see cref="Material"/> from.</param>
        /// <returns><see cref="Material"/></returns>
        public static Material GetStructureMat(this SlimeAppearance slimeAppearance, int structureIndex) => slimeAppearance?.Structures[structureIndex]?.DefaultMaterials[0];

        /// <summary>
        /// Sets the <see cref="Color"/>s of a <see cref="Material"/> based on the <see cref="Color"/>s that a slime <see cref="Material"/> has to be set.
        /// </summary>
        /// <param name="material">The <see cref="Material"/> to set the colors on.</param>
        /// <param name="top">The top <see cref="Color"/>.</param>
        /// <param name="middle">The middle <see cref="Color"/>.</param>
        /// <param name="bottom">The bottom <see cref="Color"/>.</param>
        public static void SetSlimeColors(this Material material, Color top, Color middle, Color bottom)
        {
            material.SetColor("_TopColor", top);
            material.SetColor("_MiddleColor", middle);
            material.SetColor("_BottomColor", bottom);
        }

        /// <summary>
        /// Sets the <see cref="Material"/> of a <see cref="GameObject"/> via it's <see cref="Renderer"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to set the <see cref="Material"/> for.</param>
        /// <param name="material">The <see cref="Material"/> to be set to the <see cref="GameObject"/>.</param>
        /// <param name="includeRendererComponent">The <see cref="bool"/> to include setting the <see cref="Renderer"/> component. Only if <see cref="MeshRenderer"/> & <see cref="SkinnedMeshRenderer"/> is not found.</param>
        public static void SetMaterial(this GameObject obj, Material material, bool includeRendererComponent = false)
        {
            var renderer = obj?.GetComponent<Renderer>();
            var meshRenderer = obj?.GetComponent<MeshRenderer>();
            var skinnedRenderer = obj?.GetComponent<SkinnedMeshRenderer>();

            if (meshRenderer)
                meshRenderer.material = material;
            else if (skinnedRenderer)
                skinnedRenderer.sharedMaterial = material;
            else if (renderer && includeRendererComponent)
                renderer.material = material;
        }

        /// <summary>
        /// Gets the <see cref="Material"/> of a <see cref="GameObject"/> via it's <see cref="Renderer"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to grab the <see cref="Material"/> from.</param>
        /// <param name="includeRendererComponent">The <see cref="bool"/> to include grabbing the <see cref="Renderer"/> component. Only if <see cref="MeshRenderer"/> & <see cref="SkinnedMeshRenderer"/> is not found.</param>
        /// <returns><see cref="Material"/></returns>
        public static Material GetMaterial(this GameObject obj, bool includeRendererComponent = false)
        {
            var renderer = obj?.GetComponent<Renderer>();
            var meshRenderer = obj?.GetComponent<MeshRenderer>();
            var skinnedRenderer = obj?.GetComponent<SkinnedMeshRenderer>();

            if (meshRenderer)
                return meshRenderer.material;
            else if (skinnedRenderer)
                return skinnedRenderer.sharedMaterial;
            else if (renderer && includeRendererComponent)
                return renderer.material;

            return null;
        }
    }
}
