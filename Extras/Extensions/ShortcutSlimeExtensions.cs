using HarmonyLib;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extensions
{
    public static class ShortcutSlimeExtensions
    {
        /// <summary>
        /// Sets the default jiggle <see cref="SlimeAppearance.SlimeBone"/>s to an <see cref="Array"/> of <see cref="SlimeAppearance.SlimeBone"/>.
        /// </summary>
        /// <param name="array">The <see cref="Array"/> of <see cref="SlimeAppearance.SlimeBone"/>s to set the jiggle bones to.</param>
        /// <returns><see cref="SlimeAppearance.SlimeBone[]"/></returns>
        public static SlimeAppearance.SlimeBone[] SetJiggleBones(this SlimeAppearance.SlimeBone[] array)
        {
            array = new SlimeAppearance.SlimeBone[]
            {
                SlimeAppearance.SlimeBone.JiggleBack,
                SlimeAppearance.SlimeBone.JiggleBottom,
                SlimeAppearance.SlimeBone.JiggleFront,
                SlimeAppearance.SlimeBone.JiggleLeft,
                SlimeAppearance.SlimeBone.JiggleRight,
                SlimeAppearance.SlimeBone.JiggleTop
            };
            return array;
        }

        /// <summary>
        /// Changes the <see cref="SlimeFace.SlimeExpression"/> for the face eyes to a different <see cref="SlimeFace.SlimeExpression"/> for the given <see cref="SlimeAppearance"/>.
        /// </summary>
        /// <param name="slimeAppearance">The <see cref="SlimeAppearance"/> that is having it's face being modified.</param>
        /// <param name="slimeExpression">The <see cref="SlimeFace.SlimeExpression"/> to change to.</param>
        /// <param name="ignoredExpressions">The ignored <see cref="SlimeFace.SlimeExpression"/>s that won't be changed during the process.</param>
        public static void ModifyEyes(this SlimeAppearance slimeAppearance, SlimeFace.SlimeExpression slimeExpression, [Optional] SlimeFace.SlimeExpression[] ignoredExpressions)
        {
            if (ignoredExpressions.Length < 1)
                ignoredExpressions = new SlimeFace.SlimeExpression[0];

            Material material = UnityEngine.Object.Instantiate(slimeAppearance.Face.ExpressionFaces.First(x => x.SlimeExpression == slimeExpression).Eyes);
            for (int i = 0; i < slimeAppearance.Face.ExpressionFaces.Length; i++)
            {
                if (ignoredExpressions.Contains(slimeAppearance.Face.ExpressionFaces[i].SlimeExpression)) 
                    continue;
                slimeAppearance.Face.ExpressionFaces[i].Eyes = material;
            }
        }

        /// <summary>
        /// Changes the <see cref="SlimeFace.SlimeExpression"/> for the <see cref="SlimeFace"/> mouths to a different <see cref="SlimeFace.SlimeExpression"/> for the given <see cref="SlimeAppearance"/>.
        /// </summary>
        /// <param name="slimeAppearance">The <see cref="SlimeAppearance"/> that is having it's face being modified.</param>
        /// <param name="slimeExpression">The <see cref="SlimeFace.SlimeExpression"/> to change to.</param>
        /// <param name="ignoredExpressions">The ignored <see cref="SlimeFace.SlimeExpression"/>s that won't be changed during the process.</param>
        public static void ModifyMouths(this SlimeAppearance slimeAppearance, SlimeFace.SlimeExpression slimeExpression, [Optional] SlimeFace.SlimeExpression[] ignoredExpressions)
        {
            if (ignoredExpressions.Length < 1)
                ignoredExpressions = new SlimeFace.SlimeExpression[0];

            Material material = UnityEngine.Object.Instantiate(slimeAppearance.Face.ExpressionFaces.First(x => x.SlimeExpression == slimeExpression).Mouth);
            for (int i = 0; i < slimeAppearance.Face.ExpressionFaces.Length; i++)
            {
                if (ignoredExpressions.Contains(slimeAppearance.Face.ExpressionFaces[i].SlimeExpression))
                    continue;
                slimeAppearance.Face.ExpressionFaces[i].Mouth = material;
            }
        }

        /// <summary>
        /// Recolors the <see cref="SlimeFace"/> eyes & mouth to a different color.
        /// </summary>
        /// <param name="slimeAppearance">The <see cref="SlimeAppearance"/> that is having it's face recolored.</param>
        /// <param name="eyeRed">The RGB red <see cref="Color"/> for the eyes. (Those not mean it has to be red)</param>
        /// <param name="eyeGreen">The RGB green <see cref="Color"/> for the eyes. (Those not mean it has to be green)</param>
        /// <param name="eyeBlue">The RGB blue <see cref="Color"/> for the eyes. (Those not mean it has to be blue)</param>
        /// <param name="mouthTop">The top <see cref="Color"/> for the mouths.</param>
        /// <param name="mouthMid">The middle <see cref="Color"/> for the mouths.</param>
        /// <param name="mouthBot">The bottom <see cref="Color"/> for the mouths.</param>
        public static void RecolorFace(this SlimeAppearance slimeAppearance, Color eyeRed, Color eyeGreen, Color eyeBlue, Color mouthTop, Color mouthMid, Color mouthBot)
        {
            SlimeExpressionFace[] expressionFaces = new SlimeExpressionFace[0];
            foreach (var slimeExpressionFace in slimeAppearance.Face.ExpressionFaces)
            {
                Material slimeEyes = null;
                Material slimeMouth = null;

                if (slimeExpressionFace.Eyes)
                    slimeEyes = UnityEngine.Object.Instantiate(slimeExpressionFace.Eyes);
                if (slimeExpressionFace.Mouth)
                    slimeMouth = UnityEngine.Object.Instantiate(slimeExpressionFace.Mouth);

                if (slimeEyes)
                {
                    slimeEyes.SetColor("_EyeRed", eyeRed);
                    slimeEyes.SetColor("_EyeGreen", eyeGreen);
                    slimeEyes.SetColor("_EyeBlue", eyeBlue);
                }
                if (slimeMouth)
                {
                    slimeMouth.SetColor("_MouthBot", mouthBot);
                    slimeMouth.SetColor("_MouthMid", mouthMid);
                    slimeMouth.SetColor("_MouthTop", mouthTop);
                }

                expressionFaces = expressionFaces.AddToArray(new SlimeExpressionFace()
                {
                    Eyes = slimeEyes,
                    Mouth = slimeMouth,
                    SlimeExpression = slimeExpressionFace.SlimeExpression
                });
            }
            slimeAppearance.Face.ExpressionFaces = expressionFaces;
            slimeAppearance.Face.OnEnable();
        }
    }
}
