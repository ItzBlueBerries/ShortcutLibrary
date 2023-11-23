using MonomiPark.SlimeRancher.Regions;
using ShortcutLib;
using SRML.SR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Presets
{
    public static class Sizes
    {
        public static readonly Vector3 DEFAULT_SIZE = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 POGOFRUIT_SIZE = new Vector3(0.25f, 0.25f, 0.25f);
        public static readonly Vector3 CARROT_SIZE = new Vector3(2.0f, 2.0f, 2.0f);
        public static readonly Vacuumable.Size VAC_N = Vacuumable.Size.NORMAL;
        public static readonly Vacuumable.Size VAC_L = Vacuumable.Size.LARGE;
        public static readonly Vacuumable.Size VAC_G = Vacuumable.Size.GIANT;
    }

    public static class Cells
    {
        public static readonly string COSMETIC_CELL = "zoneREEF/cellReef_GordoIsland/Sector/Loot/treasurePodCosmetic";
        public static readonly string FOUNTAIN_CELL = "zoneREEF/cellReef_Intro/Sector/Resources/waterFountain01";
    }

    public class Rands
    {
        public static readonly Identifiable.Id RAND_SLIME = Identifiable.SLIME_CLASS.GetRandom();
        public static readonly Identifiable.Id RAND_LARGO = Identifiable.LARGO_CLASS.GetRandom();
        public static readonly double RAND_DOUBLE = new System.Random().NextDouble();

        public static int RAND_INT(int min = int.MinValue, int max = int.MaxValue)
        { return new System.Random().Next(min, max); }
    }
}

public static class ExtensionsMethods
{
    public static Material GetSlimeMat(this SlimeDefinition slimeDefinition, int structureIndex)
    { return slimeDefinition.AppearancesDefault[0].Structures[structureIndex].DefaultMaterials[0]; }

    public static void SetSlimeColor(this Material material, Color color1, Color color2, Color color3)
    { material.SetColor("_TopColor", color1); material.SetColor("_MiddleColor", color2); material.SetColor("_BottomColor", color3); }

    public static void LogComps(this GameObject gObject, bool logToFile = true)
    { Debugging.LogComponents(gObject, logToFile); }

    public static void LogContents(this HashSet<Identifiable.Id> hashset, bool logToFile = true)
    { Debugging.LogClassContents(hashset, logToFile); }

    public static void LogChilds(this GameObject gameObject, bool logToFile = true)
    { Debugging.LogChildren(gameObject, logToFile); }

    public static void RegisterUpgrade<T>(this LandPlotUpgradeRegistry.UpgradeShopEntry plotUpgradeRegistry) where T : LandPlotUI
    { LandPlotUpgradeRegistry.RegisterPurchasableUpgrade<T>(plotUpgradeRegistry); }

    public static void SetMaterial(this GameObject gameObject, Material mat, bool skinnedRenderer)
    {
        if (skinnedRenderer)
            gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = mat;
        else
            gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    public static void AddComponents(this GameObject gameObject, params Type[] types)
    {
        foreach (Type type in types)
        {
            gameObject.AddComponent(type);
        }
    }

    public static void ChangeEyes(this SlimeAppearance appearance, SlimeFace.SlimeExpression face, SlimeFace.SlimeExpression[] ignoredFaces)
    {
        Material eyes = appearance.Face.ExpressionFaces.First(x => x.SlimeExpression == face).Eyes;
        for (int i = 0; i < appearance.Face.ExpressionFaces.Length; i++)
        {
            if (ignoredFaces.Contains(appearance.Face.ExpressionFaces[i].SlimeExpression)) continue;
            appearance.Face.ExpressionFaces[i].Eyes = eyes;
        }
    }

    public static void ChangeMouth(this SlimeAppearance appearance, SlimeFace.SlimeExpression face, SlimeFace.SlimeExpression[] ignoredFaces)
    {
        Material mouth = appearance.Face.ExpressionFaces.First(x => x.SlimeExpression == face).Mouth;
        for (int i = 0; i < appearance.Face.ExpressionFaces.Length; i++)
        {
            if (ignoredFaces.Contains(appearance.Face.ExpressionFaces[i].SlimeExpression)) continue;
            appearance.Face.ExpressionFaces[i].Mouth = mouth;
        }
    }

    public static void GMeshProps(this GameObject gObject, bool hasIdentifiable, bool shouldParent, [Optional] Identifiable.Id meshId, [Optional] Transform meshParent, Vacuumable.Size vacSize = Vacuumable.Size.NORMAL)
    {
        if (hasIdentifiable)
        {
            gObject.AddComponent<RegionMember>();
            gObject.AddComponent<Rigidbody>();
            gObject.AddComponent<Identifiable>().id = meshId;
            gObject.AddComponent<Vacuumable>().size = vacSize;
        }

        if (shouldParent)
        {
            gObject.transform.parent = meshParent;
        }
    }

    public static SlimeAppearance.SlimeBone[] AddDefaultBones(this SlimeAppearance.SlimeBone[] slimeBones)
    {
        slimeBones = new SlimeAppearance.SlimeBone[]
        {
            SlimeAppearance.SlimeBone.JiggleBack,
            SlimeAppearance.SlimeBone.JiggleBottom,
            SlimeAppearance.SlimeBone.JiggleFront,
            SlimeAppearance.SlimeBone.JiggleLeft,
            SlimeAppearance.SlimeBone.JiggleRight,
            SlimeAppearance.SlimeBone.JiggleTop
        };

        return slimeBones;
    }

    public static void ColorFace(this SlimeAppearance appearance, Color eyeRed, Color eyeGreen, Color eyeBlue, Color mouthTop, Color mouthMid, Color mouthBot)
    {
        SlimeExpressionFace[] expressionFaces = appearance.Face.ExpressionFaces;
        for (int k = 0; k < expressionFaces.Length; k++)
        {
            SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
            if ((bool)slimeExpressionFace.Mouth)
            {
                slimeExpressionFace.Mouth.SetColor("_MouthBot", mouthBot);
                slimeExpressionFace.Mouth.SetColor("_MouthMid", mouthMid);
                slimeExpressionFace.Mouth.SetColor("_MouthTop", mouthTop);
            }
            if ((bool)slimeExpressionFace.Eyes)
            {
                slimeExpressionFace.Eyes.SetColor("_EyeRed", eyeRed);
                slimeExpressionFace.Eyes.SetColor("_EyeGreen", eyeGreen);
                slimeExpressionFace.Eyes.SetColor("_EyeBlue", eyeBlue);
            }
        }
        appearance.Face.OnEnable();
    }
}