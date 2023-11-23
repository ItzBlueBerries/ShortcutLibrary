// default
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// my imports
using UnityEngine;
using SRML.Utils;
using SRML.SR;
using MonomiPark.SlimeRancher.Regions;
using SRML.SR.Translation;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using SRML.SR.Utils;
using SRML;
using System.Reflection;
using SRML.SR.SaveSystem;
using HarmonyLib;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

namespace ShortcutLib
{
    /// <summary>
    /// Shortcut Class [SHLIB]
    /// </summary>
    public class Shortcut
    {
        /// <summary>
        /// Ornament Methods [SHLIB]
        /// </summary>
        public static class Ornament
        {
            public static GameObject CreateOrnament(string ornamentName, Identifiable.Id ornamentPrefab, Identifiable.Id ornamentIdent, Texture2D ornamentImage, Sprite ornamentIcon, Color32 topColor, Color32 middleColor, Color32 bottomColor, [Optional] Color32 vacColor, Vacuumable.Size vacSize = Vacuumable.Size.NORMAL)
            {
                GameObject OrnamentPrefab = Prefab.ObjectPrefab(Prefab.GetPrefab(ornamentPrefab));
                GameObject OrnamentModel = OrnamentPrefab.transform.Find("model").gameObject;
                OrnamentPrefab.name = ornamentName;
                OrnamentPrefab.GetComponent<Identifiable>().id = ornamentIdent;

                Material OrnamentMat = (Material)Prefab.Instantiate(OrnamentModel.GetComponent<MeshRenderer>().material);
                OrnamentModel.GetComponent<MeshRenderer>().material.mainTexture = ornamentImage;
                OrnamentModel.GetComponent<MeshRenderer>().material.color = bottomColor;
                OrnamentModel.GetComponent<MeshRenderer>().material = OrnamentMat;

                Registry.RegisterIdentPrefab(OrnamentPrefab);
                Registry.RegisterAmmo(OrnamentPrefab);
                Registry.RegisterSilo(ornamentIdent);
                Registry.RegisterVac(vacColor, ornamentIdent, ornamentIcon, ornamentName.ToLower().Replace(" ", "") + "Definition");

                return OrnamentPrefab;
            }
        }

        /// <summary>
        /// Director Methods [SHLIB]
        /// </summary>
        public static class Director
        {
            public static MailDirector Mail() => SceneContext.Instance.MailDirector;

            public static AchievementsDirector Achieve() => SceneContext.Instance.AchievementsDirector;

            public static AmbianceDirector Ambiance() => SceneContext.Instance.AmbianceDirector;

            public static EconomyDirector Economy() => SceneContext.Instance.EconomyDirector;

            public static ExchangeDirector Exchange() => SceneContext.Instance.ExchangeDirector;

            public static GadgetDirector Gadget() => SceneContext.Instance.GadgetDirector;

            public static HolidayDirector Holiday() => SceneContext.Instance.HolidayDirector;

            public static InstrumentDirector Instrument() => SceneContext.Instance.InstrumentDirector;

            public static MetadataDirector Metadata() => SceneContext.Instance.MetadataDirector;

            public static ModDirector Mod() => SceneContext.Instance.ModDirector;

            public static PediaDirector Pedia() => SceneContext.Instance.PediaDirector;

            public static PopupDirector Popup() => SceneContext.Instance.PopupDirector;

            public static ProgressDirector Progress() => SceneContext.Instance.ProgressDirector;

            public static RanchDirector Ranch() => SceneContext.Instance.RanchDirector;

            public static SceneParticleDirector Particle() => SceneContext.Instance.SceneParticleDirector;

            public static SECTRDirector Sector() => SceneContext.Instance.SECTRDirector;

            public static TimeDirector Time() => SceneContext.Instance.TimeDirector;

            public static SlimeAppearanceDirector Appearance() => SceneContext.Instance.SlimeAppearanceDirector;

            public static TutorialDirector Tutorial() => SceneContext.Instance.TutorialDirector;

            public static LookupDirector Lookup() => GameContext.Instance.LookupDirector;

            public static AutoSaveDirector Autosave() => GameContext.Instance.AutoSaveDirector;

            public static DLCDirector DLC() => GameContext.Instance.DLCDirector;

            public static GalaxyDirector Galaxy() => GameContext.Instance.GalaxyDirector;

            public static InputDirector Input() => GameContext.Instance.InputDirector;

            public static MessageDirector Message() => GameContext.Instance.MessageDirector;

            public static MessageOfTheDayDirector MessageDay() => GameContext.Instance.MessageOfTheDayDirector;

            public static OptionsDirector Options() => GameContext.Instance.OptionsDirector;

            public static RailDirector Rail() => GameContext.Instance.RailDirector;

            public static RichPresence.Director Presence() => GameContext.Instance.RichPresenceDirector;

            public static ToyDirector Toy() => GameContext.Instance.ToyDirector;
        }

        /// <summary>
        /// Mail Methods [SHLIB]
        /// </summary>
        public static class Mail
        {
            public static void CreateStarmail(string mailKey, string mailFrom, string mailSubject, string mailBody, [Optional] Action<MailDirector, MailDirector.Mail> readCallback)
            {
                if (readCallback == null)
                    readCallback = delegate { };
                MailRegistry.RegisterMailEntry(new MailRegistry.MailEntry(mailKey))
                    .SetFromTranslation(mailFrom)
                    .SetSubjectTranslation(mailSubject)
                    .SetBodyTranslation(mailBody)
                    .SetReadCallback(readCallback);
            }

            public static void SendMail(string mailKey, MailDirector.Type mailType) => SceneContext.Instance.MailDirector.SendMailIfExists(mailType, mailKey);
        }

        /// <summary>
        /// Achieve Methods [SHLIB]
        /// </summary>
        public static class Achieve
        {
            public static void CreateAchievement(string achievementName, string achievementDesc, AchievementsDirector.Achievement achievementId, AchievementRegistry.Tier achievementTier, AchievementsDirector.Tracker achievementTracker)
            { 
                AchievementRegistry.RegisterModdedAchievement(achievementId, achievementTracker, achievementTier);
                Translate.TranslateAchieve("t." + achievementId.ToString().ToLower(), achievementName);
                Translate.TranslateAchieve("m.reqmt." + achievementId.ToString().ToLower(), achievementDesc);
            }

            public static void AwardAchieve(AchievementsDirector.Achievement achievementId)
            { SceneContext.Instance.AchievementsDirector.AwardAchievement(achievementId); }

            public static void AddStat(AchievementsDirector.IntStat achievementStat, int statAmount)
            { SceneContext.Instance.AchievementsDirector.AddToStat(achievementStat, statAmount); }

            public static void AddStat(AchievementsDirector.EnumStat achievementStat, Enum statValue)
            { SceneContext.Instance.AchievementsDirector.AddToStat(achievementStat, statValue); }

            public static void AddStat(AchievementsDirector.GameIntStat achievementStat, int statAmount)
            { SceneContext.Instance.AchievementsDirector.AddToStat(achievementStat, statAmount); }

            public static void AddStat(AchievementsDirector.GameIdDictStat achievementStat, Identifiable.Id statId, int statAmount)
            { SceneContext.Instance.AchievementsDirector.AddToStat(achievementStat, statId, statAmount); }

            public static void SetBoolStat(AchievementsDirector.BoolStat achievementStat)
            { SceneContext.Instance.AchievementsDirector.SetStat(achievementStat); }
        }

        /// <summary>
        /// Gameplay Methods [SHLIB]
        /// </summary>
        public static class Gameplay
        {
            public static GameObject GetPlot(LandPlot.Id plotId)
            { return GameContext.Instance.LookupDirector.GetPlotPrefab(plotId); }

            public static LandPlotUpgradeRegistry.UpgradeShopEntry CreatePlotUpgrade(string upgradeName, string upgradeDescription, LandPlot.Upgrade upgradeId, int upgradeCost, Sprite upgradeIcon, LandPlot.Upgrade unlockedUpgradeId = LandPlot.Upgrade.NONE, PediaDirector.Id landPlotPediaId = PediaDirector.Id.CORRAL, string landPlotName = "corral", string upgradeWarning = "This is a default warning!!", bool isAvailableEnabled = true, bool isUnlockedEnabled = false, bool holdToPurchase = false, bool shouldWarn = false)
            {
                LandPlotUpgradeRegistry.UpgradeShopEntry UpgradeEntry = new LandPlotUpgradeRegistry.UpgradeShopEntry();
                UpgradeEntry.cost = upgradeCost;
                UpgradeEntry.icon = upgradeIcon;
                UpgradeEntry.LandPlotName = landPlotName;
                UpgradeEntry.landplotPediaId = landPlotPediaId;
                if (isUnlockedEnabled)
                    UpgradeEntry.isUnlocked = ((LandPlot x) => !x.HasUpgrade(unlockedUpgradeId));
                if (isAvailableEnabled)
                    UpgradeEntry.isAvailable = ((LandPlot x) => !x.HasUpgrade(upgradeId));
                UpgradeEntry.mainImg = upgradeIcon;
                UpgradeEntry.upgrade = upgradeId;
                UpgradeEntry.holdtopurchase = holdToPurchase;
                if (shouldWarn)
                    UpgradeEntry.warning = upgradeWarning;
                Translate.TranslatePedia("m.upgrade.name." + landPlotPediaId.ToString().ToLower() + "." + upgradeId.ToString().ToLower(), upgradeName);
                Translate.TranslatePedia("m.upgrade.desc." + landPlotPediaId.ToString().ToLower() + "." + upgradeId.ToString().ToLower(), upgradeDescription);

                return UpgradeEntry;
            }
        }

        /// <summary>
        /// Style Methods [SHLIB]
        /// </summary>
        public static class Style
        {
            public static void CreateCosmetic(string podIDName, string podObject, string podParent, Vector3 podPosition, SlimeAppearance unlockableAppearance, SlimeDefinition unlockableDefinition, [Optional] Quaternion podRotation, bool keepOriginalName = true)
            {
                GameContext.Instance.DLCDirector.onPackageInstalled += delegate (DLCPackage.Id id)
                {
                    if (!Levels.isSpecial() && id == DLCPackage.Id.SECRET_STYLE)
                    {
                        if (podRotation == null)
                        { podRotation = Quaternion.identity; }
                        Transform PodParent = GameObject.Find(podParent).transform;
                        GameObject PodObject = GameObjectUtils.InstantiateInactive(GameObject.Find(podObject), podPosition, podRotation, PodParent, keepOriginalName);
                        string PodID = ModdedStringRegistry.ClaimID("pod", podIDName);
                        PodObject.GetComponent<TreasurePod>().director = PodObject.GetComponentInParent<IdDirector>();
                        PodObject.GetComponent<TreasurePod>().director.persistenceDict.Add(PodObject.GetComponent<TreasurePod>(), PodID);
                        PodObject.GetComponent<TreasurePod>().unlockedSlimeAppearance = unlockableAppearance;
                        PodObject.GetComponent<TreasurePod>().unlockedSlimeAppearanceDefinition = unlockableDefinition;
                        DLCDirector.SECRET_STYLE_TREASURE_PODS.Add(PodID);
                        PodObject.SetActive(true);
                    }
                };
            }

            public static SlimeAppearance CreateStyleAppearance(Identifiable.Id slimePrefab, SlimeDefinition slimeDefinition, string styleName)
            {
                SlimeAppearance StyledAppearance = Slime.GetSlimeApp(Slime.GetSlimeDef(slimePrefab));

                StyledAppearance.NameXlateKey = "l.secret_style_" + styleName.ToLower().Replace(" ", "_");
                Translate.TranslateActor(StyledAppearance.NameXlateKey, styleName);
                StyledAppearance.SaveSet = SlimeAppearance.AppearanceSaveSet.SECRET_STYLE;
                Registry.RegisterApp(slimeDefinition, Slime.GetSlimeDef(slimePrefab).AppearancesDefault[0], Slime.GetSlimeDef(slimePrefab).name);
                Registry.RegisterStyle(slimeDefinition, StyledAppearance);

                return StyledAppearance;
            }
        }

        /// <summary>
        /// Other Methods [SHLIB]
        /// </summary>
        public static class Other
        {
            public static UnityEngine.Object LoadAsset(Type assetType, AssetBundle assetBundle, string assetName)
            { return assetBundle.LoadAsset(assetName, assetType); }

            public static Color LoadHex(string hexCode)
            {
                ColorUtility.TryParseHtmlString(hexCode, out var returnedColor);
                return returnedColor;
            }

            public static GameObject CreateMeshObject(string objectName, Mesh objectMesh, Type colliderType, [Optional] Material meshMaterial, [Optional] Vector3 meshSize, bool usesSkinnedRenderer = false, bool hasDelaunchTrigger = true)
            {
                if (meshSize == new Vector3(0f, 0f, 0f))
                    meshSize = new Vector3(1f, 1f, 1f);

                GameObject MeshObject = new GameObject(objectName);
                MeshObject.Prefabitize();

                if (usesSkinnedRenderer)
                {
                    MeshObject.AddComponent<SkinnedMeshRenderer>().sharedMesh = objectMesh;
                    MeshObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = meshMaterial;
                }
                else
                {
                    MeshObject.AddComponent<MeshFilter>().sharedMesh = objectMesh;
                    MeshObject.AddComponent<MeshRenderer>().sharedMaterial = meshMaterial;
                }

                MeshObject.AddComponent(colliderType);
                MeshObject.layer = LayerMask.NameToLayer("Actor");

                if (hasDelaunchTrigger)
                    Prefab.ObjectPrefab(Prefab.GetPrefab(Identifiable.Id.PINK_SLIME).FindChild("DelaunchTrigger(Clone)")).transform.parent = MeshObject.transform;

                MeshObject.transform.localScale = meshSize;

                return MeshObject;
            }

            public static (Color[], Color[]) CreateChroma(Sprite chromaIcon, RanchDirector.Palette newPaletteID, string newPaletteName, Color[] darkColors, Color[] lightColors, ProgressDirector.ProgressType progressType = ProgressDirector.ProgressType.NONE, int partnerLevel = 0, int progressCount = 0, int order = 0)
            {
                if (darkColors.Length < 8)
                    throw new NullReferenceException("Please have at least 8 colors in your Dark Colors Array. (This includes Light Colors)");
                if (darkColors.Length > 8)
                    throw new NullReferenceException("Please don't have more than 8 colors in your Dark Colors Array. (This includes Light Colors)");
                if (lightColors.Length < 8)
                    throw new NullReferenceException("Please have at least 8 colors in your Light Colors Array. (This includes Dark Colors)");
                if (lightColors.Length > 8)
                    throw new NullReferenceException("Please don't have more than 8 colors in your Light Colors Array. (This includes Dark Colors)");

                ChromaRegistry.RegisterPaletteEntry(
                    new RanchDirector.PaletteEntry()
                    {
                        icon = chromaIcon,
                        palette = newPaletteID,
                        requiresPartnerLevel = partnerLevel,
                        requiresProgressCount = progressCount,
                        requiresProgressType = progressType,
                        order = order,
                        blackDark = darkColors[0],
                        blueDark = darkColors[1],
                        cyanDark = darkColors[2],
                        greenDark = darkColors[3],
                        magentaDark = darkColors[4],
                        redDark = darkColors[5],
                        whiteDark = darkColors[6],
                        yellowDark = darkColors[7],
                        blackLight = lightColors[0],
                        blueLight = lightColors[1],
                        cyanLight = lightColors[2],
                        greenLight = lightColors[3],
                        magentaLight = lightColors[4],
                        redLight = lightColors[5],
                        whiteLight = lightColors[6],
                        yellowLight = lightColors[7]
                    }
                );
                Translate.TranslatePedia("m.palette.name." + newPaletteID.ToString().ToLower(), newPaletteName);

                return (darkColors, lightColors);
            }

            public static (LiquidDefinition, GameObject, Material) CreateLiquid(Identifiable.Id liquidPrefab, Identifiable.Id newLiquidID, string liquidName, Texture2D colorRamp, Color liquidColor, Color vacColor, Sprite liquidIcon)
            {
                // PREFAB
                GameObject LiquidPrefab = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(liquidPrefab));
                LiquidDefinition liquidDefinition = ScriptableObject.CreateInstance<LiquidDefinition>();
                GameObject liquidIncomingFX = Prefab.ObjectPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetLiquidIncomingFX(liquidPrefab));
                GameObject liquidVacFailFX = Prefab.ObjectPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetLiquidVacFailFX(liquidPrefab));

                LiquidPrefab.name = liquidName;
                LiquidPrefab.GetComponent<Identifiable>().id = newLiquidID;
                liquidDefinition.name = liquidName;

                // MATERIAL
                GameObject SpherePrefab = LiquidPrefab.FindChild("Sphere");
                MeshRenderer LiquidRenderer = SpherePrefab.GetComponent<MeshRenderer>();
                Material liquidMaterial = UnityEngine.Object.Instantiate(LiquidRenderer.sharedMaterial);
                liquidMaterial.name = liquidName;
                liquidMaterial.SetTexture("_ColorRamp", colorRamp);
                LiquidRenderer.sharedMaterial = liquidMaterial;

                // PARTICLE
                GameObject fxWaterSplat = Prefab.ObjectPrefab(Assets.LoadResource<GameObject>("FX waterSplat"));
                liquidIncomingFX.transform.Find("Water Glops").GetComponent<ParticleSystemRenderer>().sharedMaterial = LiquidPrefab.transform.Find("Sphere").GetComponent<MeshRenderer>().sharedMaterial;
                var SprinklerSystemMain = LiquidPrefab.transform.Find("Sphere").Find("FX Sprinkler 1").GetComponent<ParticleSystem>().main;
                var SprinklerSystemOvertime = LiquidPrefab.transform.Find("Sphere").Find("FX Sprinkler 1").GetComponent<ParticleSystem>().colorOverLifetime;
                SprinklerSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor);
                SprinklerSystemOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor);

                LiquidPrefab.transform.Find("Sphere").Find("FX Water Glops").GetComponent<ParticleSystemRenderer>().sharedMaterial = LiquidPrefab.transform.Find("Sphere").GetComponent<MeshRenderer>().sharedMaterial;
                fxWaterSplat.transform.Find("Water Glops").GetComponent<ParticleSystemRenderer>().sharedMaterial = LiquidPrefab.transform.Find("Sphere").GetComponent<MeshRenderer>().sharedMaterial;

                var MainSystemMain = fxWaterSplat.GetComponent<ParticleSystem>().main;
                // var MainSystemMainOvertime = fxWaterSplat.GetComponent<ParticleSystem>().colorOverLifetime;

                var HitSystemMain = fxWaterSplat.transform.Find("Hit").GetComponent<ParticleSystem>().main;
                // var HitSystemOvertime = fxWaterSplat.transform.Find("Hit").GetComponent<ParticleSystem>().colorOverLifetime;

                var BubblesSystemMain = fxWaterSplat.transform.Find("Bubbles").GetComponent<ParticleSystem>().main;
                var BubblesSystemOvertime = fxWaterSplat.transform.Find("Bubbles").GetComponent<ParticleSystem>().colorOverLifetime;

                var SparklesSystemMain = fxWaterSplat.transform.Find("Sparkles").GetComponent<ParticleSystem>().main;
                // var SparklesSystemOvertime = fxWaterSplat.transform.Find("Sparkles").GetComponent<ParticleSystem>().colorOverLifetime;

                var WaveSystemMain = fxWaterSplat.transform.Find("Wave").GetComponent<ParticleSystem>().main;
                // var WaveSystemOvertime = fxWaterSplat.transform.Find("Wave").GetComponent<ParticleSystem>().colorOverLifetime;

                MainSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));
                // MainSystemMainOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));

                HitSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));
                // HitSystemOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));

                BubblesSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));
                BubblesSystemOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));

                SparklesSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));
                // SparklesSystemOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));

                WaveSystemMain.startColor = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));
                // WaveSystemOvertime.color = new ParticleSystem.MinMaxGradient(liquidColor, new Color(liquidColor.r, liquidColor.g, liquidColor.b, 0));

                LiquidPrefab.GetComponent<DestroyOnTouching>().destroyFX = fxWaterSplat;

                // DEFINITION
                typeof(LiquidDefinition).GetField("id", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(liquidDefinition, newLiquidID);
                typeof(LiquidDefinition).GetField("inFX", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(liquidDefinition, liquidIncomingFX);
                typeof(LiquidDefinition).GetField("vacFailFX", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(liquidDefinition, liquidVacFailFX);

                // REGISTER
                Identifiable.LIQUID_CLASS.Add(newLiquidID);
                Translate.TranslatePedia("t." + newLiquidID.ToString().ToLower(), liquidName);
                LookupRegistry.RegisterLiquid(liquidDefinition);
                LookupRegistry.RegisterIdentifiablePrefab(LiquidPrefab);
                LookupRegistry.RegisterVacEntry(newLiquidID, vacColor, liquidIcon);
                AmmoRegistry.RegisterPlayerAmmo(PlayerState.AmmoMode.DEFAULT, newLiquidID);

                return (liquidDefinition, LiquidPrefab, liquidMaterial);
            }

            public static GameObject CreateFountain(Identifiable.Id liquidObject, string fountainObject, string parent, string fountainName, Vector3 position, string dictionaryName)
            {
                // OBJECT
                GameObject ParentObject = GameObject.Find(parent);
                GameObject FountainObject = GameObjectUtils.InstantiateInactive(GameObject.Find(fountainObject));
                FountainObject.transform.parent = ParentObject.transform;
                FountainObject.name = fountainName;
                FountainObject.transform.position = position;

                // LIQUID
                LiquidSource componentInChildren = FountainObject.GetComponentInChildren<LiquidSource>();
                componentInChildren.director = IdHandlerUtils.GlobalIdDirector;
                componentInChildren.director.persistenceDict.Add(componentInChildren, dictionaryName);
                componentInChildren.liquidId = liquidObject;
                FountainObject.SetActive(true);

                return FountainObject;
            }

            public static void ColorFountain(GameObject fountainObject, Color mainColor, Color mistColor, Color hitColor, Color dripsColor, Color waveColor, Color rippleColor, Color tinyDripsColor, Color glowColor, Color sparklesColor)
            {
                PooledSceneParticle ParticlePrefab = fountainObject.GetComponentInChildren<PooledSceneParticle>();
                GameObject FountainParticle = Prefab.ObjectPrefab(ParticlePrefab.particlePrefab);

                var MainSystemMain = FountainParticle.GetComponent<ParticleSystem>().main;
                // var MainSystemMainOvertime = FountainParticle.GetComponent<ParticleSystem>().colorOverLifetime;

                var MistSystemMain = FountainParticle.transform.Find("Mist").GetComponent<ParticleSystem>().main;
                var MistSystemOvertime = FountainParticle.transform.Find("Mist").GetComponent<ParticleSystem>().colorOverLifetime;

                var HitSystemMain = FountainParticle.transform.Find("Hit").GetComponent<ParticleSystem>().main;
                // var HitSystemOvertime = FountainParticle.transform.Find("Hit").GetComponent<ParticleSystem>().colorOverLifetime;

                var DripsSystemMain = FountainParticle.transform.Find("Drips").GetComponent<ParticleSystem>().main;
                // var DripsSystemOvertime = FountainParticle.transform.Find("Drips").GetComponent<ParticleSystem>().colorOverLifetime;

                var WaveSystemMain = FountainParticle.transform.Find("Wave").GetComponent<ParticleSystem>().main;
                // var WaveSystemOvertime = FountainParticle.transform.Find("Wave").GetComponent<ParticleSystem>().colorOverLifetime;

                var RippleSystemMain = FountainParticle.transform.Find("Ripple").GetComponent<ParticleSystem>().main;
                // var RippleSystemOvertime = FountainParticle.transform.Find("Ripple").GetComponent<ParticleSystem>().colorOverLifetime;

                var TinyDripsSystemMain = FountainParticle.transform.Find("Tiny Drips").GetComponent<ParticleSystem>().main;
                var TinyDripsSystemOvertime = FountainParticle.transform.Find("Tiny Drips").GetComponent<ParticleSystem>().colorOverLifetime;

                var GlowSystemMain = FountainParticle.transform.Find("Glow").GetComponent<ParticleSystem>().main;
                // var GlowSystemOvertime = FountainParticle.transform.Find("Glow").GetComponent<ParticleSystem>().colorOverLifetime;

                var SparklesSystemMain = FountainParticle.transform.Find("Sparkles").GetComponent<ParticleSystem>().main;
                var SparklesSystemOvertime = FountainParticle.transform.Find("Sparkles").GetComponent<ParticleSystem>().colorOverLifetime;

                MainSystemMain.startColor = new ParticleSystem.MinMaxGradient(mainColor, new Color(mainColor.r, mainColor.g, mainColor.b, 0));
                // MainSystemMainOvertime.color = new ParticleSystem.MinMaxGradient(mainColor, new Color(mainColor.r, mainColor.g, mainColor.b, 0));

                MistSystemMain.startColor = new ParticleSystem.MinMaxGradient(mistColor, new Color(mistColor.r, mistColor.g, mistColor.b, 0));
                MistSystemOvertime.color = new ParticleSystem.MinMaxGradient(mistColor, new Color(mistColor.r, mistColor.g, mistColor.b, 0));

                HitSystemMain.startColor = new ParticleSystem.MinMaxGradient(hitColor, new Color(hitColor.r, hitColor.g, hitColor.b, 0));
                // HitSystemOvertime.color = new ParticleSystem.MinMaxGradient(hitColor, new Color(hitColor.r, hitColor.g, hitColor.b, 0));

                DripsSystemMain.startColor = new ParticleSystem.MinMaxGradient(dripsColor, new Color(dripsColor.r, dripsColor.g, dripsColor.b, 0));
                // DripsSystemOvertime.color = new ParticleSystem.MinMaxGradient(dripsColor, new Color(dripsColor.r, dripsColor.g, dripsColor.b, 0));

                WaveSystemMain.startColor = new ParticleSystem.MinMaxGradient(waveColor, new Color(waveColor.r, waveColor.g, waveColor.b, 0));
                // WaveSystemOvertime.color = new ParticleSystem.MinMaxGradient(waveColor, new Color(waveColor.r, waveColor.g, waveColor.b, 0));

                RippleSystemMain.startColor = new ParticleSystem.MinMaxGradient(rippleColor, new Color(rippleColor.r, rippleColor.g, rippleColor.b, 0));
                // RippleSystemOvertime.color = new ParticleSystem.MinMaxGradient(rippleColor, new Color(rippleColor.r, rippleColor.g, rippleColor.b, 0));

                TinyDripsSystemMain.startColor = new ParticleSystem.MinMaxGradient(tinyDripsColor, new Color(tinyDripsColor.r, tinyDripsColor.g, tinyDripsColor.b, 0));
                TinyDripsSystemOvertime.color = new ParticleSystem.MinMaxGradient(tinyDripsColor, new Color(tinyDripsColor.r, tinyDripsColor.g, tinyDripsColor.b, 0));

                GlowSystemMain.startColor = new ParticleSystem.MinMaxGradient(glowColor, new Color(glowColor.r, glowColor.g, glowColor.b, 0));
                // GlowSystemOvertime.color = new ParticleSystem.MinMaxGradient(glowColor, new Color(glowColor.r, glowColor.g, glowColor.b, 0));

                SparklesSystemMain.startColor = new ParticleSystem.MinMaxGradient(sparklesColor, new Color(sparklesColor.r, sparklesColor.g, sparklesColor.b, 0));
                SparklesSystemOvertime.color = new ParticleSystem.MinMaxGradient(sparklesColor, new Color(sparklesColor.r, sparklesColor.g, sparklesColor.b, 0));

                ParticlePrefab.particlePrefab = FountainParticle;
            }
        }

        /// <summary>
        /// EatMap Methods [SHLIB]
        /// </summary>
        public static class EatMap
        {
            public static void CreateBecomeMap(Identifiable.Id slimePrefab, Identifiable.Id toBecome, Identifiable.Id whatItEats, float minDrive = 1f, float extraDrive = 0f, SlimeEmotions.Emotion slimeDriver = SlimeEmotions.Emotion.AGITATION)
            {
                SlimeDefinition slimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab);

                slimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = toBecome,
                    driver = slimeDriver,
                    minDrive = minDrive,
                    eats = whatItEats,
                    extraDrive = extraDrive
                });
            }

            public static void CreateProduceMap(Identifiable.Id slimePrefab, Identifiable.Id toProduce, Identifiable.Id whatItEats, float minDrive = 1f, float extraDrive = 0f, bool isFavorite = false, int favProduceCount = 2, SlimeEmotions.Emotion slimeDriver = SlimeEmotions.Emotion.AGITATION)
            {
                SlimeDefinition slimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab);

                slimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    producesId = toProduce,
                    driver = slimeDriver,
                    minDrive = minDrive,
                    eats = whatItEats,
                    isFavorite = isFavorite,
                    favoriteProductionCount = favProduceCount,
                    extraDrive = extraDrive
                });
            }
        }

        /// <summary>
        /// Toy Methods [SHLIB]
        /// </summary>
        public static class Toy
        {
            public static ToyDefinition GetToyDef(Identifiable.Id toyId)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetToyDefinition(toyId); }

            public static void TranslateToy(Identifiable.Id toyID, string definitionNameKey, string toyName, string toyDescription)
            {
                Translate.TranslatePedia("m.toy.name." + definitionNameKey, toyName);
                Translate.TranslatePedia("m.toy.desc." + definitionNameKey, toyDescription);
            }

            /*public static GameObject CreateToy(Identifiable.Id toyPrefab, Identifiable.Id newToyID, string newToyName, Sprite newToyIcon, Material toyMaterial, int toyCost = 500, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE, bool isUpgradableToy = true)
            {
                GameObject ToyPrefab = Prefab.QuickPrefab(toyPrefab);

                ToyPrefab.name = newToyName;
                ToyPrefab.GetComponent<Identifiable>().id = newToyID;
                ToyPrefab.GetComponent<Vacuumable>().size = vacSetting;

                ToyPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial = (Material)Prefab.Instantiate(toyMaterial);

                Translate.TranslatePedia("t." + newToyID.ToString().ToLower(), newToyName);
                LookupRegistry.RegisterIdentifiablePrefab(ToyPrefab);
                LookupRegistry.RegisterToy(newToyID, newToyIcon, toyCost, newToyName);
                ToyRegistry.RegisterBasePurchasableToy(newToyID);
                if (isUpgradableToy)
                {
                    ToyRegistry.RegisterUpgradedPurchasableToy(newToyID);
                }

                return ToyPrefab;
            }*/
        }

        /// <summary>
        /// Structure Methods [SHLIB]
        /// </summary>
        public static class Structure
        {
            public static SlimeAppearanceStructure AddStructure(SlimeDefinition additionalSlimeDefinition, int structureNum)
            { return new SlimeAppearanceStructure(additionalSlimeDefinition.GetAppearanceForSet(SlimeAppearance.AppearanceSaveSet.CLASSIC).Structures[structureNum]); }

            public static Material ColorRadStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color middleColor, Color edgeColor, [Optional] Vector3 auraSize, int materialStructureNum, int structureNum)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (auraSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 1f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = auraSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_MiddleColor", middleColor);
                material.SetColor("_EdgeColor", edgeColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                slimeAppearance.Structures[structureNum].Element.Prefabs[0] = slimeAppearanceObject;

                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static Material ColorGlitchStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color bottomColor, int materialStructureNum, int structureNum, [Optional] Vector3 trailSize)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (trailSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 0.5f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = trailSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_BottomColor", bottomColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static Material ColorStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color topColor, Color middleColor, Color bottomColor, [Optional] Color specColor, int materialStructureNum, int structureNum, [Optional] Vector3 structureSize)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (structureSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 1f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = structureSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_TopColor", topColor);
                material.SetColor("_MiddleColor", middleColor);
                material.SetColor("_BottomColor", bottomColor);
                if (specColor == new Color32(0, 0, 0, 0))
                    material.SetColor("_SpecColor", middleColor);
                material.SetColor("_SpecColor", specColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static (GameObject, SlimeAppearanceObject, SlimeAppearance.SlimeBone[]) CreateBasicStructure(AssetBundle assetBundle, string bundledAsset, string objectName, SlimeAppearance.SlimeBone rootBone, SlimeAppearance.SlimeBone parentBone, SlimeAppearance.SlimeBone[] attachedBones, RubberBoneEffect.RubberType rubberType, bool rubberBoneEffect = true, bool usesMeshRenderer = false)
            {
                GameObject assetObject = assetBundle.LoadAsset<GameObject>(bundledAsset);
                GameObject slimeAppearanceObject = new GameObject(objectName);
                slimeAppearanceObject.Prefabitize();
                if (usesMeshRenderer)
                { slimeAppearanceObject.AddComponent<MeshFilter>(); slimeAppearanceObject.AddComponent<MeshRenderer>(); slimeAppearanceObject.GetComponent<MeshFilter>().sharedMesh = assetObject.GetComponentInChildren<MeshFilter>().sharedMesh; }
                else
                {
                    slimeAppearanceObject.AddComponent<SkinnedMeshRenderer>();
                    slimeAppearanceObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = assetObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                }
                slimeAppearanceObject.AddComponent<SlimeAppearanceObject>();
                slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().AttachRubberBoneEffect = rubberBoneEffect;
                slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().RootBone = rootBone;
                slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().ParentBone = parentBone;
                slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().RubberType = rubberType;
                slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().AttachedBones = attachedBones;

                return (slimeAppearanceObject, slimeAppearanceObject.GetComponent<SlimeAppearanceObject>(), attachedBones);
            }

            public static SlimeAppearanceElement SetStructureElement(string elementName, SlimeAppearance slimeAppearance, SlimeAppearanceObject[] objectPrefabs, int structureNum, bool addToArray = true, bool supportFaces = false)
            {
                if (addToArray)
                {
                    slimeAppearance.Structures = slimeAppearance.Structures.AddToArray(slimeAppearance.Structures[0].Clone());
                }
                SlimeAppearanceElement ObjectElement = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
                slimeAppearance.Structures[structureNum].Element = ObjectElement;
                slimeAppearance.Structures[structureNum].Element.Name = elementName;
                slimeAppearance.Structures[structureNum].Element.Prefabs = objectPrefabs;
                slimeAppearance.Structures[structureNum].SupportsFaces = supportFaces;

                return ObjectElement;
            }

            public static SlimeAppearanceObject SetStructurePrefab(SlimeAppearance slimeAppearance, SlimeAppearanceObject objectPrefab, int structureNum, int prefabsNum, bool addToArray = false)
            {
                if (addToArray)
                { slimeAppearance.Structures[structureNum].Element.Prefabs = slimeAppearance.Structures[structureNum].Element.Prefabs.AddToArray(objectPrefab); }
                return slimeAppearance.Structures[structureNum].Element.Prefabs[prefabsNum] = objectPrefab;
            }
        }

        /// <summary>
        /// Resource Methods [SHLIB]
        /// </summary>
        public static class Resource
        {
            public static GadgetDefinition GetGadgetDef(Gadget.Id gadgetId)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(gadgetId); }

            public static GameObject GetSpawnRes(SpawnResource.Id spawnResourceId)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetResourcePrefab(spawnResourceId); }

            public static GameObject CreateMeat(Identifiable.Id meatPrefab, Identifiable.Id meatID, string meatName, Color vacColor, Sprite icon, string meatAnimator, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, float transformChance = 5f, bool isChick = false, bool isElder = false)
            {
                GameObject MeatPrefab = Prefab.QuickPrefab(meatPrefab);
                MeatPrefab.name = meatName;

                SkinnedMeshRenderer mRenderMeat = MeatPrefab.transform.Find(meatAnimator).gameObject.GetComponent<SkinnedMeshRenderer>();
                Material MeatMat = UnityEngine.Object.Instantiate<Material>(mRenderMeat.sharedMaterial);
                MeatMat.SetTexture("_RampGreen", rampGreen);
                MeatMat.SetTexture("_RampRed", rampRed);
                MeatMat.SetTexture("_RampBlue",rampBlue);
                MeatMat.SetTexture("_RampBlack", rampBlack);
                mRenderMeat.sharedMaterial = MeatMat;

                MeatPrefab.GetComponent<Identifiable>().id = meatID;
                if (!isChick && !isElder)
                { MeatPrefab.GetComponent<TransformChanceOnReproduce>().transformChance = transformChance; }
                else
                { UnityEngine.Object.Destroy(MeatPrefab.GetComponent<TransformChanceOnReproduce>()); UnityEngine.Object.Destroy(MeatPrefab.GetComponent<Reproduce>()); };
                MeatPrefab.transform.Find(meatAnimator).gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = MeatMat;

                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, MeatPrefab);
                LookupRegistry.RegisterVacEntry(meatID, vacColor, icon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(meatID, vacColor, icon));

                if (isChick)
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.CHICK_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                }
                else if (isElder)
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.MEAT_CLASS.Add(meatID);
                    Identifiable.ELDER_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                }
                else
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.MEAT_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                };

                Translate.TranslatePedia("t." + meatID.ToString().ToLower(), meatName);
                LookupRegistry.RegisterIdentifiablePrefab(MeatPrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, meatID);

                return MeatPrefab;
            }

            public static (GameObject, Material) CreateFood(Identifiable.Id cropPrefab, Identifiable.Id newCropID, string newCropName, Sprite icon, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, [Optional] Vector3 foodSize, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL, bool isVeggie = false, bool isFruit = false, bool isPogoFruit = false, bool isCarrot = false)
            {
                Vector3 pogofruitDefault = new Vector3(0.25f, 0.25f, 0.25f);
                Vector3 carrotDefault = new Vector3(2.0f, 2.0f, 2.0f);
                GameObject CropPrefab = Prefab.QuickPrefab(cropPrefab);
                CropPrefab.name = newCropName;

                CropPrefab.GetComponent<Identifiable>().id = newCropID;
                CropPrefab.GetComponent<Vacuumable>().size = vacSetting;
                GameObject Model = GameObjectExtensions.FindChildWithPartialName(CropPrefab, "model_", false);

                MeshRenderer CropRender = Model.GetComponent<MeshRenderer>();
                Material CropMat = (Material)Prefab.Instantiate(CropRender.sharedMaterial);
                CropMat.SetTexture("_RampGreen", rampGreen);
                CropMat.SetTexture("_RampRed", rampRed);
                CropMat.SetTexture("_RampBlue", rampBlue);
                CropMat.SetTexture("_RampBlack", rampBlack);
                CropRender.sharedMaterial = CropMat;

                if (isPogoFruit)
                    Model.transform.localScale = pogofruitDefault;
                else if (isCarrot)
                    Model.transform.localScale = carrotDefault;
                else
                    Model.transform.localScale = foodSize;

                if (foodSize == new Vector3(0, 0, 0) && !isPogoFruit && !isCarrot)
                    Model.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, CropPrefab);
                LookupRegistry.RegisterVacEntry(newCropID, vacColor, icon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newCropID, vacColor, icon));

                if (isVeggie)
                { Identifiable.FOOD_CLASS.Add(newCropID); Identifiable.VEGGIE_CLASS.Add(newCropID); }
                else if (isFruit)
                { Identifiable.FOOD_CLASS.Add(newCropID); Identifiable.FRUIT_CLASS.Add(newCropID); }
                else { Identifiable.FOOD_CLASS.Add(newCropID); }

                Translate.TranslatePedia("t." + newCropID.ToString().ToLower(), newCropName);
                LookupRegistry.RegisterIdentifiablePrefab(CropPrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newCropID);

                return (CropPrefab, CropMat);
            }

            public static GameObject CreateGarden(SpawnResource.Id spawnResourcePrefab, SpawnResource.Id newSpawnResourceID, string newSpawnResourceName, GameObject[] spawnOptions, [Optional] GameObject[] additionalSpawnOptions, Identifiable.Id newFoodID, bool additionalFoods = false, float minSpawn = 10f, float maxSpawn = 20f, float minSpawnTime = 5f, float maxSpawnTime = 10f, float bonusFoodChance = 1f, int minBonusSpawn = 3)
            {
                GameObject SpawnPrefab = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetResourcePrefab(spawnResourcePrefab));
                SpawnPrefab.name = newSpawnResourceName;
                SpawnResource SpawnResource = SpawnPrefab.GetComponent<SpawnResource>();
                SpawnResource.id = newSpawnResourceID;
                SpawnResource.ObjectsToSpawn = spawnOptions;
                if (additionalFoods)
                {
                    SpawnResource.BonusObjectsToSpawn = additionalSpawnOptions;
                    SpawnResource.BonusChance = bonusFoodChance;
                    SpawnResource.minBonusSelections = minBonusSpawn;
                }
                SpawnResource.MinObjectsSpawned = minSpawn;
                SpawnResource.MaxObjectsSpawned = maxSpawn;
                SpawnResource.MinNutrientObjectsSpawned = SpawnResource.MaxObjectsSpawned;
                SpawnResource.MinSpawnIntervalGameHours = minSpawnTime;
                SpawnResource.MaxSpawnIntervalGameHours = maxSpawnTime;

                foreach (GameObject sprout in SpawnPrefab.FindChildren("Sprout"))
                {
                    GameObject SproutGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID);
                    sprout.GetComponent<MeshFilter>().sharedMesh = SproutGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                    sprout.GetComponent<MeshRenderer>().sharedMaterial = SproutGameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
                }
                foreach (Joint joint in SpawnResource.SpawnJoints)
                {
                    GameObject FoodGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID);
                    joint.gameObject.GetComponent<MeshFilter>().sharedMesh = FoodGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                    joint.gameObject.GetComponent<MeshRenderer>().sharedMaterial = FoodGameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
                }

                return SpawnPrefab;
            }

            public static GameObject CreateCrate(Identifiable.Id cratePrefab, Identifiable.Id newCrateID, string newCrateName, [Optional] Color crateColor, Identifiable.Id crateMaterial = Identifiable.Id.CRATE_REEF_01, Texture2D crateTexture = null, bool hasCustomMaterial = false, bool textureCrate = false, int minSpawn = 3, int maxSpawn = 6, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE)
            {
                GameObject CratePrefab = Prefab.QuickPrefab(cratePrefab);
                CratePrefab.name = newCrateName;

                CratePrefab.GetComponent<Identifiable>().id = newCrateID;
                CratePrefab.GetComponent<Vacuumable>().size = vacSetting;
                CratePrefab.GetComponent<BreakOnImpact>().minSpawns = minSpawn;
                CratePrefab.GetComponent<BreakOnImpact>().maxSpawns = maxSpawn;

                if (hasCustomMaterial)
                {
                    GameObject crateMatPrefab = Prefab.QuickPrefab(crateMaterial);
                    CratePrefab.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate(crateMatPrefab.GetComponent<MeshRenderer>().material);
                    CratePrefab.GetComponent<MeshRenderer>().material.SetColor("_Color", crateColor);
                }

                if (textureCrate)
                { CratePrefab.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", crateTexture); }

                Identifiable.STANDARD_CRATE_CLASS.Add(newCrateID);
                Translate.TranslateActor("l." + newCrateID.ToString().ToLower(), newCrateName);
                LookupRegistry.RegisterIdentifiablePrefab(CratePrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newCrateID);

                return CratePrefab;
            }

            public static GameObject CreateBottledResource(Identifiable.Id resourcePrefab, Identifiable.Id newResourceID, string newResourceName, Sprite resourceIcon, Material outsideMaterial, Material insideMaterial, Color vacColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject resourceObject = Prefab.QuickPrefab(resourcePrefab);
                resourceObject.GetComponent<Identifiable>().id = newResourceID;
                resourceObject.name = newResourceName;

                resourceObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = insideMaterial;
                resourceObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = outsideMaterial;

                resourceObject.GetComponent<Vacuumable>().size = vacSetting;

                Identifiable.CRAFT_CLASS.Add(newResourceID);
                Identifiable.NON_SLIMES_CLASS.Add(newResourceID);
                Translate.TranslatePedia("t." + newResourceID.ToString().ToLower(), newResourceName);
                LookupRegistry.RegisterIdentifiablePrefab(resourceObject);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newResourceID);
                AmmoRegistry.RegisterSiloAmmo((SiloStorage.StorageType x) => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.CRAFTING, newResourceID);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newResourceID));
                AmmoRegistry.RegisterRefineryResource(newResourceID);
                LookupRegistry.RegisterVacEntry(newResourceID, vacColor, resourceIcon);

                return resourceObject;
            }

            public static Extractor AddResourceToEx(Gadget.Id extractorId, Identifiable.Id resourceId, float weight, bool restrictZone = false, ZoneDirector.Zone zone = ZoneDirector.Zone.NONE)
            {
                Extractor component = SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(extractorId).prefab.GetComponent<Extractor>();
                Extractor.ProduceEntry item = new Extractor.ProduceEntry();

                if (!restrictZone)
                {
                    item = new Extractor.ProduceEntry
                    {
                        id = resourceId,
                        weight = weight,
                        spawnFX = component.produces[0].spawnFX
                    };
                } else if (restrictZone)
                {
                    item = new Extractor.ProduceEntry
                    {
                        id = resourceId,
                        weight = weight,
                        restrictZone = restrictZone,
                        zone = zone,
                        spawnFX = component.produces[0].spawnFX
                    };
                }

                List<Extractor.ProduceEntry> list = new List<Extractor.ProduceEntry>(); //Create a new list
                foreach (Extractor.ProduceEntry item2 in component.produces) //For each thing it produces
                {
                    list.Add(item2); //Add it to the list
                }
                list.Add(item); //Add your custom 'Extractor.ProduceEntry' to the list
                component.produces = list.ToArray(); //Set the new list

                return component;
            }

            public static GameObject CreateExtractor(Gadget.Id gadgetPrefab, Gadget.Id newGadgetID, string newGadgetName, Sprite newIcon, string newGadgetDescription, int extractorCost, int extractorBuyLimit, GadgetDefinition.CraftCost[] craftCosts, Extractor.ProduceEntry[] extractorProduces, ProgressDirector.ProgressType zoneUnlock, float unlockTime = 3f, int cycles = 3, int minProduce = 3, int maxProduce = 6, float timePerCycle = 12, bool infiniteCycles = false)
            {

                GameObject GadgetPrefab = Prefab.ObjectPrefab(Resource.GetGadgetDef(gadgetPrefab).prefab);
                GadgetPrefab.name = newGadgetName;

                GameObject fx = GadgetPrefab.GetComponent<Extractor>().produces[0].spawnFX;

                GadgetPrefab.GetComponent<Gadget>().id = newGadgetID;
                GadgetPrefab.GetComponent<Extractor>().produceMin = minProduce;
                GadgetPrefab.GetComponent<Extractor>().produceMax = maxProduce;
                GadgetPrefab.GetComponent<Extractor>().hoursPerCycle = timePerCycle;
                GadgetPrefab.GetComponent<Extractor>().produces = extractorProduces;
                GadgetPrefab.GetComponent<Extractor>().produces[0].spawnFX = fx;

                if (!infiniteCycles)
                { GadgetPrefab.GetComponent<Extractor>().cycles = cycles; }
                else { GadgetPrefab.GetComponent<Extractor>().infiniteCycles = infiniteCycles; }

                Sprite GadgetIcon = newIcon;
                GadgetDefinition GadgetDef = ScriptableObject.CreateInstance<GadgetDefinition>();
                GadgetDef.prefab = GadgetPrefab;
                GadgetDef.id = newGadgetID;
                GadgetDef.pediaLink = PediaDirector.Id.EXTRACTORS;
                GadgetDef.blueprintCost = extractorCost;
                GadgetDef.buyCountLimit = extractorBuyLimit;
                GadgetDef.icon = GadgetIcon;
                GadgetDef.craftCosts = craftCosts;

                LookupRegistry.RegisterGadget(GadgetDef);

                Gadget.EXTRACTOR_CLASS.Add(newGadgetID);

                new GadgetTranslation(newGadgetID).SetNameTranslation(newGadgetName).SetDescriptionTranslation(newGadgetDescription);

                GadgetRegistry.RegisterBlueprintLock(newGadgetID, x => x.CreateBasicLock(newGadgetID, Gadget.Id.NONE, zoneUnlock, unlockTime));

                return GadgetPrefab;
            }

            public static void ColorExtractor(Gadget.Id newExtractorId, Color32[] mainColors, Color32[] extColors1, Color32[] extColors2, int extrIndex, bool isApiary = false, bool isPump = false, bool isDrill = false)
            {
                if (mainColors.Length < 8)
                    throw new NullReferenceException("Please have at least 8 colors in your Main Colors Array. (This includes Ext Colors)");
                if (mainColors.Length > 8)
                    throw new NullReferenceException("Please don't have more than 8 colors in your Main Colors Array. (This includes Ext Colors)");
                if (extColors1.Length < 8)
                    throw new NullReferenceException("Please have at least 8 colors in your Ext Colors (1) Array. (This includes Main/Ext Colors)");
                if (extColors1.Length > 8)
                    throw new NullReferenceException("Please don't have more than 8 colors in your Ext Colors (1) Array. (This includes Main/Ext Colors)");
                if (extColors2.Length < 8)
                    throw new NullReferenceException("Please have at least 8 colors in your Ext Colors (2) Array. (This includes Main/Ext Colors)");
                if (extColors2.Length > 8)
                    throw new NullReferenceException("Please don't have more than 8 colors in your Ext Colors (2) Array. (This includes Main/Ext Colors)");

                GameObject NewGadgetPrefab = GetGadgetDef(newExtractorId).prefab;

                string ext23 = "none";
                string extr;

                string[] index = new string[]
                {
                    "ext_r1",
                    "ext_r2",
                    "ext_r3"
                };

                extr = index[extrIndex];

                if (isApiary)  
                { ext23 = "ext_apiary23";}

                if (isPump)
                { ext23 = "ext_pump23"; }

                if (isDrill)
                { ext23 = "ext_drill23"; }

                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material);

                // recolor x4 shader thingy (core)
                // red
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", mainColors[0]);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", mainColors[1]);
                // green
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", mainColors[2]);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", mainColors[3]);
                // blue
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", mainColors[4]);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", mainColors[5]);
                // black
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", mainColors[6]);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", mainColors[7]);

                // recolor x4 shader thingy (ext)
                // red
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", extColors1[0]);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", extColors1[1]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", extColors2[0]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", extColors2[1]);
                // green
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", extColors1[2]);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", extColors1[3]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", extColors2[2]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", extColors2[3]);
                // blue
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", extColors1[4]);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", extColors1[5]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", extColors2[4]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", extColors2[5]);
                // black
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", extColors1[6]);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", extColors1[7]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", extColors2[6]);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", extColors2[7]);
            }
        }

        /// <summary>
        /// Spawner Methods [SHLIB]
        /// </summary>
        public static class Spawner
        {
            public static (GameObject, DirectedActorSpawner.SpawnConstraint[]) CreateSlimeSpawner(string nodeObject, string nodeParent, Vector3 nodePosition, DirectedActorSpawner.TimeMode timeMode, SlimeSet.Member[] members, [Optional] Quaternion nodeRotation, bool enableStacking = false, bool spawnFeral = false, bool spawnAgitated = false, float spawnerRadius = 5f, float defaultWeight = 1f, int targetSpawnCount = 10, float spawnDelay = 1f, float minSpawnTime = 0, float maxSpawnTime = 24)
            {
                DirectedActorSpawner.SpawnConstraint[] constraints = new DirectedActorSpawner.SpawnConstraint[]
                {
                    new DirectedActorSpawner.SpawnConstraint()
                    {
                        window = new DirectedActorSpawner.TimeWindow()
                        {
                            timeMode = timeMode,
                            endHour = maxSpawnTime,
                            startHour = minSpawnTime
                        },
                        slimeset = new SlimeSet()
                        {
                            members = members
                        },
                        feral = spawnFeral,
                        maxAgitation = spawnAgitated,
                        weight = defaultWeight
                    }
                };

                if (nodeRotation == null)
                { nodeRotation = Quaternion.identity; }

                if (GameObject.Find(nodeParent).GetComponent<CellDirector>() != null)
                    GameObject.Find(nodeParent).GetComponent<CellDirector>().targetSlimeCount = targetSpawnCount;
                GameObject slimeNode = GameObjectUtils.InstantiateInactive(GameObject.Find(nodeObject));
                slimeNode.transform.parent = GameObject.Find(nodeParent).transform;
                slimeNode.transform.position = nodePosition;
                slimeNode.transform.rotation = nodeRotation;
                slimeNode.GetComponent<DirectedSlimeSpawner>().constraints = constraints;
                slimeNode.GetComponent<DirectedSlimeSpawner>().allowDirectedSpawns = true;
                slimeNode.GetComponent<DirectedSlimeSpawner>().spawnDelayFactor = spawnDelay;
                slimeNode.GetComponent<DirectedSlimeSpawner>().enableToteming = enableStacking;
                slimeNode.GetComponent<DirectedSlimeSpawner>().radius = spawnerRadius;
                slimeNode.SetActive(true);

                return (slimeNode, constraints);
            }

            public static void SlimeSpawner(Identifiable.Id slimeBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(slimeBeingSpawned),
                                weight = weight
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static void ActorSpawner(Identifiable.Id actorBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedActorSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedActorSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(actorBeingSpawned),
                                weight = weight
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static void MeatSpawner(Identifiable.Id meatBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedAnimalSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedAnimalSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(meatBeingSpawned),
                                weight = weight
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static List<SpawnResource> ReplaceFoodSpawner(Identifiable.Id foodPrefab, GameObject foodObject)
            {
                GameObject FoodPrefab = Prefab.GetPrefab(foodPrefab);
                List<SpawnResource> spawnResources = new List<SpawnResource>();
                SRCallbacks.OnSaveGameLoaded += context =>
                {
                    foreach (var spawnResource in UnityEngine.Object.FindObjectsOfType<SpawnResource>())
                    {
                        if (spawnResource.ObjectsToSpawn.Contains(FoodPrefab))
                        {
                            spawnResource.ObjectsToSpawn = new GameObject[] { foodObject };
                            spawnResources.Add(spawnResource);
                        }
                    }
                };

                return spawnResources;
            }
        }

        /// <summary>
        /// Prefab Methods [SHLIB]
        /// </summary>
        public static class Prefab
        {
            public static GameObject GetPrefab(Identifiable.Id objectToGet)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(objectToGet); }

            public static GameObject QuickPrefab(Identifiable.Id objectToCopy)
            { return PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(objectToCopy)); }

            public static GameObject ObjectPrefab(GameObject objectToCopy)
            { return PrefabUtils.CopyPrefab(objectToCopy); }

            public static UnityEngine.Object DeepPrefab(UnityEngine.Object objectToCopy)
            { return PrefabUtils.DeepCopyObject(objectToCopy); }

            public static UnityEngine.Object Instantiate(UnityEngine.Object objectToCopy)
            { return UnityEngine.Object.Instantiate(objectToCopy); }

            public static GameObject InstantiateActor(GameObject objectToCopy, RegionRegistry.RegionSetId region, Vector3 position, Quaternion rotation, bool nonActorOk = false)
            { return SRBehaviour.InstantiateActor(objectToCopy, region, position, rotation, nonActorOk); }

            public static void Destroy(UnityEngine.Object objectToDestroy)
            { UnityEngine.Object.Destroy(objectToDestroy); }

            public static void PermanentDestroy(UnityEngine.Object objectToDestroy, string source)
            { Destroyer.Destroy(objectToDestroy, source); }
        }

        /// <summary>
        /// EnumParse Methods [SHLIB]
        /// </summary>
        public static class EnumP
        {
            public static Identifiable.Id ParseID(string enumId)
            { return (Identifiable.Id)Enum.Parse(typeof(Identifiable.Id), enumId); }

            public static Gadget.Id ParseGad(string enumId)
            { return (Gadget.Id)Enum.Parse(typeof(Gadget.Id), enumId); }

            public static PediaDirector.Id ParsePedia(string enumId)
            { return (PediaDirector.Id)Enum.Parse(typeof(PediaDirector.Id), enumId); }

            public static SpawnResource.Id ParseSpawnRes(string enumId)
            { return (SpawnResource.Id)Enum.Parse(typeof(SpawnResource.Id), enumId); }

            public static LandPlot.Id ParsePlot(string enumId)
            { return (LandPlot.Id)Enum.Parse(typeof(LandPlot.Id), enumId); }

            public static SlimeEat.FoodGroup ParseGroup(string enumId)
            { return (SlimeEat.FoodGroup)Enum.Parse(typeof(SlimeEat.FoodGroup), enumId); }

            public static RanchDirector.Palette ParsePalette(string enumId)
            { return (RanchDirector.Palette)Enum.Parse(typeof(RanchDirector.Palette), enumId); }
        }

        /// <summary>
        /// Slime Methods [SHLIB]
        /// </summary>
        public static class Slime
        {
            public static GameObject GetGordo(Identifiable.Id gordoPrefab)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetGordo(gordoPrefab); }

            public static SlimeDefinition GetSlimeDef(Identifiable.Id slimePrefab)
            { return SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab); }

            public static SlimeAppearance GetSlimeApp(SlimeDefinition slimeDefinition)
            { return (SlimeAppearance)Prefab.DeepPrefab(slimeDefinition.AppearancesDefault[0]); }

            public static (SlimeDefinition, GameObject, SlimeAppearance) CreateSlime(Identifiable.Id slimePrefab, Identifiable.Id slimeObjectPrefab, Identifiable.Id newSlimeID, string newSlimeName, Sprite newIcon, Color vacColor, Color SplatColor1, [Optional] Color SplatColor2, [Optional] Color SplatColor3, [Optional] Color AmmoColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject slimeObject = Prefab.QuickPrefab(slimeObjectPrefab);
                SlimeDefinition preSlimeDefinition = GetSlimeDef(slimePrefab);
                SlimeDefinition slimeDefinition = (SlimeDefinition)Prefab.DeepPrefab(preSlimeDefinition);
                SlimeAppearance slimeAppearance = GetSlimeApp(preSlimeDefinition);

                if (SplatColor2 == new Color32(0, 0, 0, 0))
                { SplatColor2 = SplatColor1; }
                else if (SplatColor3 == new Color32(0, 0, 0, 0))
                { SplatColor3 = SplatColor1; }
                else if (AmmoColor == new Color32(0, 0, 0, 0))
                { AmmoColor = vacColor; }

                slimeAppearance.name = newSlimeName;
                slimeAppearance.Face.OnEnable();
                slimeAppearance.Icon = newIcon;
                slimeAppearance.ColorPalette = new SlimeAppearance.Palette
                {
                    Top = SplatColor1,
                    Middle = SplatColor2,
                    Bottom = SplatColor3,
                    Ammo = AmmoColor
                };

                slimeDefinition.name = newSlimeName;
                slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
                slimeDefinition.IdentifiableId = newSlimeID;
                slimeDefinition.Diet.EatMap?.Clear();
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                slimeObject.name = newSlimeName;
                slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
                slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
                slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
                slimeObject.GetComponent<Identifiable>().id = newSlimeID;
                slimeObject.GetComponent<Vacuumable>().size = vacSetting;
                UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());

                TranslationPatcher.AddPediaTranslation("t." + newSlimeID.ToString().ToLower(), newSlimeName);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, slimeObject);
                LookupRegistry.RegisterVacEntry(newSlimeID, vacColor, newIcon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newSlimeID, vacColor, newIcon));

                SceneContext.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(slimeDefinition, slimeDefinition.AppearancesDefault[0]);
                SceneContext.Instance.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(slimeDefinition, slimeDefinition.AppearancesDefault[0]);

                Identifiable.SLIME_CLASS.Add(newSlimeID);
                slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;
                LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
                SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);

                return (slimeDefinition, slimeObject, slimeAppearance);
            }

            public static Material ColorSlime(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color Color1, Color Color2, Color Color3, Color Color4, float Shininess = 1f, float Gloss = 1f)
            {
                SlimeAppearance slimeAppearance = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab).AppearancesDefault[0];

                Material material = UnityEngine.Object.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
                material.SetColor("_TopColor", Color1);
                material.SetColor("_MiddleColor", Color2);
                material.SetColor("_BottomColor", Color3);
                material.SetColor("_SpecColor", Color4);
                material.SetFloat("_Shininess", Shininess);
                material.SetFloat("_Gloss", Gloss);
                slimeAppearance.Structures[0].DefaultMaterials[0] = material;

                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static GameObject CreatePlort(Identifiable.Id plortPrefab, Identifiable.Id newPlortID, string newPlortName, Sprite newIcon, Color32 vacColor, float plortPrice = 12, float plortSaturation = 5, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject plortObject = Prefab.QuickPrefab(plortPrefab);
                plortObject.name = newPlortName;

                plortObject.GetComponent<Identifiable>().id = newPlortID;
                plortObject.GetComponent<Vacuumable>().size = vacSetting;

                Translate.TranslateActor("l." + newPlortID.ToString().ToLower(), newPlortName);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, plortObject);
                AmmoRegistry.RegisterSiloAmmo(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT, newPlortID);
                LookupRegistry.RegisterVacEntry(newPlortID, vacColor, newIcon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newPlortID, vacColor, newIcon));

                Identifiable.PLORT_CLASS.Add(newPlortID);
                Identifiable.NON_SLIMES_CLASS.Add(newPlortID);
                LookupRegistry.RegisterIdentifiablePrefab(plortObject);
                PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, newPlortID);
                PlortRegistry.AddEconomyEntry(newPlortID, plortPrice, plortSaturation);
                PlortRegistry.AddPlortEntry(newPlortID);
                DroneRegistry.RegisterBasicTarget(newPlortID);

                return plortObject;
            }

            public static GameObject ColorPlort(Identifiable.Id plortPrefab, Color Color1, Color Color2, Color Color3, [Optional] Identifiable.Id plortMaterial, [Optional] Color RockColor1, [Optional] Color RockColor2, [Optional] Color RockColor3, bool hasRocks = false)
            {
                GameObject PlortPrefab = Prefab.GetPrefab(plortPrefab);

                if (plortMaterial == Identifiable.Id.NONE)
                { plortMaterial = Identifiable.Id.PINK_PLORT; }

                Material plortMatPrefab = Prefab.QuickPrefab(plortMaterial).GetComponent<MeshRenderer>().material;
                PlortPrefab.GetComponent<MeshRenderer>().material = plortMatPrefab;

                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_TopColor", Color1);
                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", Color2);
                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", Color3);

                if (hasRocks)
                {
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_TopColor", RockColor1);
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", RockColor2);
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_BottomColor", RockColor3);
                }

                return PlortPrefab;
            }

            public static (SlimeDefinition, GameObject) CreateGordo(Identifiable.Id gordoPrefab, Identifiable.Id basePrefab, Identifiable.Id newGordoID, Sprite newGordoIcon, string newGordoName, string mapMarkerName, ZoneDirector.Zone gordoZone, int feedCount, List<GameObject> gordoRewards, Vacuumable.Size vacSetting = Vacuumable.Size.GIANT)
            {
                GameObject GordoPrefab = Prefab.ObjectPrefab(Slime.GetGordo(gordoPrefab));
                GordoPrefab.name = newGordoName;
                GameObject BasePrefab = Prefab.QuickPrefab(basePrefab);
                BasePrefab.GetComponent<Vacuumable>().size = vacSetting;
                SlimeDefinition BasePrefabDef = GetSlimeDef(basePrefab);

                Material ModelMat = BasePrefabDef.AppearancesDefault[0].Structures[0].DefaultMaterials[0];

                SlimeEyeComponents baseSlimeEyes = BasePrefab.GetComponent<SlimeEyeComponents>();
                SlimeMouthComponents baseSlimeMouth = BasePrefab.GetComponent<SlimeMouthComponents>();

                GordoFaceComponents GordoFace = GordoPrefab.GetComponent<GordoFaceComponents>();
                GordoFace.strainEyes = baseSlimeEyes.scaredEyes;
                GordoFace.strainMouth = baseSlimeMouth.chompClosedMouth;
                GordoFace.blinkEyes = baseSlimeEyes.blinkEyes;
                GordoFace.chompOpenMouth = baseSlimeMouth.chompOpenMouth;
                GordoFace.happyMouth = baseSlimeMouth.happyMouth;

                GordoDisplayOnMap disp = GordoPrefab.GetComponent<GordoDisplayOnMap>();
                GameObject MarkerPrefab = Prefab.ObjectPrefab(disp.markerPrefab.gameObject);
                MarkerPrefab.name = mapMarkerName;
                MarkerPrefab.GetComponent<Image>().sprite = newGordoIcon;
                
                disp.markerPrefab = MarkerPrefab.GetComponent<MapMarker>();

                GordoIdentifiable iden = GordoPrefab.GetComponent<GordoIdentifiable>();
                iden.id = newGordoID;
                iden.nativeZones = new ZoneDirector.Zone[1] { gordoZone };

                GordoEat eat = GordoPrefab.GetComponent<GordoEat>();
                SlimeDefinition oldDefinition = (SlimeDefinition)Prefab.DeepPrefab(eat.slimeDefinition);

                oldDefinition.AppearancesDefault = BasePrefabDef.AppearancesDefault;
                oldDefinition.Diet = BasePrefabDef.Diet;
                oldDefinition.IdentifiableId = newGordoID;
                oldDefinition.name = newGordoName;
                eat.slimeDefinition = oldDefinition;
                eat.targetCount = feedCount;

                GordoRewards GordoRewards = GordoPrefab.GetComponent<GordoRewards>();
                GordoRewards.rewardPrefabs = gordoRewards.ToArray();
                GordoRewards.slimePrefab = GameContext.Instance.LookupDirector.GetPrefab(basePrefab);
                GordoRewards.rewardOverrides = new GordoRewards.RewardOverride[0];

                GameObject child = GordoPrefab.transform.Find("Vibrating/slime_gordo").gameObject;
                SkinnedMeshRenderer render = child.GetComponent<SkinnedMeshRenderer>();
                render.sharedMaterial = ModelMat;
                render.sharedMaterials[0] = ModelMat;
                render.material = ModelMat;
                render.materials[0] = ModelMat;

                Translate.TranslatePedia("t." + newGordoID.ToString().ToLower(), newGordoName);
                Identifiable.GORDO_CLASS.Add(newGordoID);
                LookupRegistry.RegisterGordo(GordoPrefab);

                return (oldDefinition, GordoPrefab);
            }
        }

        /// <summary>
        /// Largo Methods [SHLIB]
        /// </summary>
        public static class Largo
        {
            public static (SlimeDefinition, GameObject, SlimeAppearance) CreateLargo(string largoName, Identifiable.Id firstSlime, Identifiable.Id secondSlime, Identifiable.Id newLargoID, float minDrive = 0.5f, int largoSize = 1, SlimeRegistry.LargoProps largoProperties = SlimeRegistry.LargoProps.NONE, SlimeEmotions.Emotion largoDriver = SlimeEmotions.Emotion.AGITATION)
            {
                Translate.TranslateActor("l." + newLargoID.ToString().ToLower(), largoName);
                SlimeRegistry.CraftLargo(newLargoID, firstSlime, secondSlime, largoProperties, out SlimeDefinition largoDefinition, out SlimeAppearance largoAppearance, out GameObject largoObject);
                largoDefinition.name = largoName;

                if (largoSize == 0)
                    largoObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                else
                    largoObject.transform.localScale *= largoSize;

                SlimeDefinition firstSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(firstSlime);
                SlimeDefinition secondSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(secondSlime);

                firstSlimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = newLargoID,
                    driver = largoDriver,
                    minDrive = minDrive,
                    eats = secondSlimeDefinition.Diet.Produces[0]
                });

                secondSlimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = newLargoID,
                    driver = largoDriver,
                    minDrive = minDrive,
                    eats = firstSlimeDefinition.Diet.Produces[0]
                });

                return (largoDefinition, largoObject, largoAppearance);
            }
        }

        /// <summary>
        /// Translate Methods [SHLIB]
        /// </summary>
        public static class Translate
        {
            public static void TranslateActor(string actorKey, string actorTranslated)
            { TranslationPatcher.AddActorTranslation(actorKey, actorTranslated); }

            public static void TranslatePedia(string pediaKey, string pediaTranslated)
            { TranslationPatcher.AddPediaTranslation(pediaKey, pediaTranslated); }

            public static void TranslateUI(string uiKey, string uiTranslated)
            { TranslationPatcher.AddUITranslation(uiKey, uiTranslated); }

            public static void TranslateKey(string bundle, string key, string value)
            { TranslationPatcher.AddTranslationKey(bundle, key, value); }

            public static void TranslateAchieve(string achieveKey, string achieveTranslated)
            { TranslationPatcher.AddAchievementTranslation(achieveKey, achieveTranslated); }

            public static void TranslateExchange(string exchangeKey, string exchangeTranslated)
            { TranslationPatcher.AddExchangeTranslation(exchangeKey, exchangeTranslated); }

            public static void TranslateGlobal(string globalKey, string globalTranslated)
            { TranslationPatcher.AddGlobalTranslation(globalKey, globalTranslated); }

            public static void TranslateMail(string mailKey, string mailTranslated)
            { TranslationPatcher.AddMailTranslation(mailKey, mailTranslated); }

            public static void TranslateSRMLError(MessageDirector.Lang language, string errorKey, string errorTranslated)
            { TranslationPatcher.AddSRMLErrorUITranslation(language, errorKey, errorTranslated); }

            public static void TranslateTutorial(string tutorialKey, string tutorialTranslated)
            { TranslationPatcher.AddTutorialTranslation(tutorialKey, tutorialTranslated); }

            public static void CreateSlimepedia(Identifiable.Id id, PediaDirector.Id entry, Sprite icon, string pediaTitle, string pediaIntro, string pediaDiet, string pediaFavorite, string pediaSlimeology, string pediaRisks, string pediaPlortonomics)
            {
                PediaRegistry.RegisterIdEntry(entry, icon);
                PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)1, id);
                PediaRegistry.RegisterIdentifiableMapping(entry, id);
                PediaRegistry.SetPediaCategory(entry, (PediaRegistry.PediaCategory)1);
                new SlimePediaEntryTranslation(entry)
                    .SetTitleTranslation(pediaTitle)
                    .SetIntroTranslation(pediaIntro)
                    .SetDietTranslation(pediaDiet)
                    .SetFavoriteTranslation(pediaFavorite)
                    .SetSlimeologyTranslation(pediaSlimeology)
                    .SetRisksTranslation(pediaRisks)
                    .SetPlortonomicsTranslation(pediaPlortonomics);
            }

            public static void CreateResourcePedia(Identifiable.Id id, PediaDirector.Id entry, Sprite icon, string pediaTitle, string pediaIntro, string pediaResourceType, string pediaFavoredBy, string pediaDescription)
            {
                PediaRegistry.RegisterIdEntry(entry, icon);
                PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)2, id);
                PediaRegistry.RegisterIdentifiableMapping(entry, id);
                PediaRegistry.SetPediaCategory(entry, (PediaRegistry.PediaCategory)2);
                new SlimePediaEntryTranslation(entry).SetTitleTranslation(pediaTitle).SetIntroTranslation(pediaIntro);
                TranslationPatcher.AddPediaTranslation("m.resource_type." + entry.ToString().ToLower(), pediaResourceType);
                TranslationPatcher.AddPediaTranslation("m.favored_by." + entry.ToString().ToLower(), pediaFavoredBy);
                TranslationPatcher.AddPediaTranslation("m.desc." + entry.ToString().ToLower(), pediaDescription);
            }
        }

        /// <summary>
        /// Fashion Pod Methods [SHLIB]
        /// </summary>
        public static class FashionP
        {
            public static GameObject CreateFashion(Sprite icon, string fashionName, Identifiable.Id fashionPrefab, Identifiable.Id fashionId, GameObject clipOnPrefab, Color vacColor, Fashion.Slot slot = Fashion.Slot.FRONT)
            {
                GameObject FashionPrefab = Prefab.QuickPrefab(fashionPrefab);
                FashionPrefab.name = fashionName;

                FashionPrefab.GetComponent<Identifiable>().id = fashionId;
                FashionPrefab.GetComponent<Fashion>().slot = slot;

                FashionPrefab.GetComponent<Fashion>().attachPrefab = clipOnPrefab;
                foreach (Image image in FashionPrefab.GetComponentsInChildren<Image>())
                { image.sprite = icon; }

                Identifiable.FASHION_CLASS.Add(fashionId);
                LookupRegistry.RegisterIdentifiablePrefab(FashionPrefab);
                LookupRegistry.RegisterVacEntry(fashionId, vacColor, icon);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, FashionPrefab);
                Translate.TranslateActor("l." + fashionId.ToString().ToLower(), fashionName);

                return FashionPrefab;
            }

            public static GameObject CreatePod(Gadget.Id podPrefab, Gadget.Id newPodID, string newPodName, string newPodDesc, Sprite newIcon, Identifiable.Id fashionId, ProgressDirector.ProgressType zoneUnlock, GadgetDefinition.CraftCost[] craftCost, float unlockTime = 3f, int podCost = 1000, int podLimit = 20)
            {
                GameObject PodPrefab = Prefab.ObjectPrefab(Resource.GetGadgetDef(podPrefab).prefab);
                PodPrefab.name = newPodName;

                PodPrefab.GetComponent<Gadget>().id = newPodID;
                PodPrefab.GetComponent<FashionPod>().fashionId = fashionId;

                GadgetRegistry.RegisterBlueprintLock(newPodID, (GadgetDirector x) => x.CreateBasicLock(newPodID, Gadget.Id.NONE, zoneUnlock, unlockTime));
                GadgetDefinition GadgetDef = ScriptableObject.CreateInstance<GadgetDefinition>();
                GadgetDef.prefab = PodPrefab;
                GadgetDef.id = newPodID;
                GadgetDef.blueprintCost = podCost;
                GadgetDef.buyCountLimit = podLimit;
                GadgetDef.icon = newIcon;
                GadgetDef.pediaLink = PediaDirector.Id.CURIOS;
                GadgetDef.craftCosts = craftCost;

                LookupRegistry.RegisterGadget(GadgetDef);

                Gadget.FASHION_POD_CLASS.Add(newPodID);

                PodPrefab.transform.Find("model_fashionPod").GetComponent<MeshRenderer>().material.mainTexture = newIcon.texture;
                GadgetTranslationExtensions.GetTranslation(newPodID).SetNameTranslation(newPodName).SetDescriptionTranslation(newPodDesc);

                return PodPrefab;
            }
        }

        /// <summary>
        /// Definition Methods [SHLIB]
        /// </summary>
        public static class Definition
        {
            public static GadgetDefinition CreateGadDefinition(GameObject prefab, Gadget.Id id, Sprite icon, PediaDirector.Id pediaLink, GadgetDefinition.CraftCost[] craftCosts, Gadget.Id[] countOtherIds, int countLimit, int blueprintCost, int buyCountLimit, bool destroyOnRemoval, bool buyInPairs)
            {
                GadgetDefinition GadgetDefinition = ScriptableObject.CreateInstance<GadgetDefinition>();

                GadgetDefinition.prefab = prefab;
                GadgetDefinition.id = id;
                GadgetDefinition.icon = icon;
                GadgetDefinition.pediaLink = pediaLink;
                GadgetDefinition.craftCosts = craftCosts;
                GadgetDefinition.countLimit = countLimit;
                GadgetDefinition.countOtherIds = countOtherIds;
                GadgetDefinition.blueprintCost = blueprintCost;
                GadgetDefinition.buyCountLimit = buyCountLimit;
                GadgetDefinition.destroyOnRemoval = destroyOnRemoval;
                GadgetDefinition.buyInPairs = buyInPairs;

                return GadgetDefinition;
            }

            public static VacItemDefinition CreateVacDefinition(string name, Identifiable.Id id, Sprite icon, Color color)
            {
                VacItemDefinition VacItemDefinition = ScriptableObject.CreateInstance<VacItemDefinition>();

                VacItemDefinition.name = name;
                VacItemDefinition.id = id;
                VacItemDefinition.icon = icon;
                VacItemDefinition.color = color;

                return VacItemDefinition;
            }

            public static SlimeDefinition CreateSlimeDefinition(string Name, Identifiable.Id IdentifiableId, bool IsLargo, bool CanLargofy, SlimeAppearance[] AppearancesDefault, SlimeDiet Diet, Identifiable.Id[] FavoriteToys, GameObject BaseModule, SlimeDefinition[] BaseSlimes, GameObject[] SlimeModules, SlimeSounds Sounds, float PrefabScale, [Optional] List<SlimeAppearance> AppearancesDynamic, bool dynamicAppearance = false)
            {
                SlimeDefinition SlimeDefinition = ScriptableObject.CreateInstance<SlimeDefinition>();

                SlimeDefinition.Name = Name;
                SlimeDefinition.IdentifiableId = IdentifiableId;
                SlimeDefinition.IsLargo = IsLargo;
                SlimeDefinition.CanLargofy = CanLargofy;
                SlimeDefinition.AppearancesDefault = AppearancesDefault;
                SlimeDefinition.Diet = Diet;
                SlimeDefinition.FavoriteToys = FavoriteToys;
                SlimeDefinition.BaseModule = BaseModule;
                SlimeDefinition.BaseSlimes = BaseSlimes;
                SlimeDefinition.SlimeModules = SlimeModules;
                SlimeDefinition.Sounds = Sounds;
                SlimeDefinition.PrefabScale = PrefabScale;
                if (dynamicAppearance)
                {
                    SlimeDefinition.AppearancesDynamic = AppearancesDynamic;
                }

                return SlimeDefinition;
            }

            public static ToyDefinition CreateToyDefinition(Identifiable.Id toyId, string nameKey, Sprite icon, int cost)
            {
                ToyDefinition ToyDefinition = ScriptableObject.CreateInstance<ToyDefinition>();

                ToyDefinition.toyId = toyId;
                ToyDefinition.nameKey = nameKey;
                ToyDefinition.icon = icon;
                ToyDefinition.cost = cost;

                return ToyDefinition;
            }

            public static UpgradeDefinition CreateUpgradeDefinition(PlayerState.Upgrade upgrade, Sprite icon, int cost)
            {
                UpgradeDefinition UpgradeDefinition = ScriptableObject.CreateInstance<UpgradeDefinition>();

                UpgradeDefinition.upgrade = upgrade;
                UpgradeDefinition.icon = icon;
                UpgradeDefinition.cost = cost;

                return UpgradeDefinition;
            }

            public static LiquidDefinition CreateLiquidDefinition(Identifiable.Id id, GameObject inFx, GameObject vacFailFx)
            {
                LiquidDefinition LiquidDefinition = ScriptableObject.CreateInstance<LiquidDefinition>();

                LiquidDefinition.id = id;
                LiquidDefinition.inFX = inFx;
                LiquidDefinition.vacFailFX = vacFailFx;

                return LiquidDefinition;
            }
        }

        /// <summary>
        /// Registry Methods [SHLIB]
        /// </summary>
        public static class Registry
        {
            public static void RegisterGadget(string enumName)
            { GadgetRegistry.CreateGadgetId(EnumPatcher.GetFirstFreeValue(typeof(Gadget.Id)), enumName); }

            public static void RegisterIdent(string enumName)
            { IdentifiableRegistry.CreateIdentifiableId(EnumPatcher.GetFirstFreeValue(typeof(Identifiable.Id)), enumName); }

            public static void RegisterIdentPrefab(GameObject prefab)
            { LookupRegistry.RegisterIdentifiablePrefab(prefab); }

            public static void RegisterPedia(PediaDirector.Id pedia, Identifiable.Id id)
            { PediaRegistry.RegisterIdentifiableMapping(pedia, id); }

            public static void RegisterSpawn(GameObject toRegister)
            { LookupRegistry.RegisterSpawnResource(toRegister); }

            public static void RegisterFoodGroup(Identifiable.Id id, SlimeEat.FoodGroup foodGroup)
            { FoodGroupRegistry.RegisterToFoodGroup(id, foodGroup); }

            public static void UnregisterFoodGroup(Identifiable.Id id, SlimeEat.FoodGroup foodGroup)
            { FoodGroupRegistry.UnregisterFromFoodGroup(id, foodGroup); }

            public static void RegisterSnare(Identifiable.Id id)
            { SnareRegistry.RegisterAsSnareable(id); }

            public static void RegisterDrone(Identifiable.Id id)
            { DroneRegistry.RegisterBasicTarget(id); }

            public static void RegisterDef(SlimeDefinition definition, bool refreshEatMaps = true)
            { SlimeRegistry.RegisterSlimeDefinition(definition, refreshEatMaps); }

            public static void RegisterToy(ToyDefinition definition)
            { LookupRegistry.RegisterToy(definition); }

            public static void RegisterAmmo(GameObject toRegister, PlayerState.AmmoMode ammoMode = PlayerState.AmmoMode.DEFAULT)
            { AmmoRegistry.RegisterAmmoPrefab(ammoMode, toRegister); }

            public static void RegisterRefinery(Identifiable.Id id)
            { AmmoRegistry.RegisterRefineryResource(id); }

            public static void RegisterSilo(Identifiable.Id id, SiloStorage.StorageType storageType = SiloStorage.StorageType.NON_SLIMES)
            { AmmoRegistry.RegisterSiloAmmo(storageType, id); }

            public static void RegisterToyPurchasable(Identifiable.Id id, bool isUpgradedToy = false)
            {
                if (!isUpgradedToy)
                { ToyRegistry.RegisterBasePurchasableToy(id); }
                else if (isUpgradedToy)
                { ToyRegistry.RegisterUpgradedPurchasableToy(id); }
            }

            public static void RegisterApp(SlimeDefinition definition, SlimeAppearance appearance, [Optional] string appearanceName)
            {
                if (appearanceName == null)
                { appearance.NameXlateKey = "l.classic_" + appearance.name.ToLower().Replace(" ", "_"); Translate.TranslateActor(appearance.NameXlateKey, appearance.name); }
                else { appearance.NameXlateKey = "l.classic_" + appearanceName.ToLower().Replace(" ", "_"); Translate.TranslateActor(appearance.NameXlateKey, appearanceName); }
                SlimeRegistry.RegisterAppearance(definition, appearance);
            }

            public static void RegisterStyle(SlimeDefinition definition, SlimeAppearance appearance)
            { StyleRegistry.RegisterSecretStyle(definition, appearance); }

            public static void RegisterVac(Color vacColor, Identifiable.Id toBeRegistered, Sprite vacIcon, string definitionName = "")
            {
                VacItemDefinition vacDef = ScriptableObject.CreateInstance<VacItemDefinition>();
                vacDef.name = definitionName;
                vacDef.color = vacColor;
                vacDef.icon = vacIcon;
                vacDef.id = toBeRegistered;

                LookupRegistry.RegisterVacEntry(vacDef); 
            }

            public static void RegisterFarmSlot([Optional] GameObject toRegister, [Optional] GameObject toRegisterDeluxe, Identifiable.Id foodId)
            {
                if (!toRegister && !toRegisterDeluxe)
                    throw new ArgumentNullException("At least one of toRegister or toRegisterDeluxe needs to be registered for a new farm slot.");

                if (toRegister)
                {
                    PlantSlotRegistry.RegisterPlantSlot(new GardenCatcher.PlantSlot()
                    {
                        id = foodId,
                        plantedPrefab = toRegister
                    });
                } else if (toRegisterDeluxe)
                {
                    PlantSlotRegistry.RegisterPlantSlot(new GardenCatcher.PlantSlot()
                    {
                        id = foodId,
                        deluxePlantedPrefab = toRegisterDeluxe
                    });
                }
            }
        }
    }
}
