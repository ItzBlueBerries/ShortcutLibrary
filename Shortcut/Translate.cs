using SRML.SR.Translation;
using SRML.SR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Components
{
    /// <summary>
    /// A bunch of translation methods, including creating pedia translations.
    /// </summary>
    public static class Translate
    {
        /// <summary>
        /// Translates a key in the actor bundle.
        /// </summary>
        /// <param name="actorKey">The key <see cref="string"/> to translate.</param>
        /// <param name="actorTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Actor(string actorKey, string actorTranslated) => TranslationPatcher.AddActorTranslation(actorKey, actorTranslated);

        /// <summary>
        /// Translates a key in the pedia bundle.
        /// </summary>
        /// <param name="pediaKey">The key <see cref="string"/> to translate.</param>
        /// <param name="pediaTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Pedia(string pediaKey, string pediaTranslated) => TranslationPatcher.AddPediaTranslation(pediaKey, pediaTranslated);

        /// <summary>
        /// Translates a key in the ui bundle.
        /// </summary>
        /// <param name="uiKey">The key <see cref="string"/> to translate.</param>
        /// <param name="uiTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void UI(string uiKey, string uiTranslated) => TranslationPatcher.AddUITranslation(uiKey, uiTranslated);

        /// <summary>
        /// Translates a key in the bundle provided.
        /// </summary>
        /// <param name="bundle">The existing bundle <see cref="string"/> to translate the key in.</param>
        /// <param name="key">The key <see cref="string"/> to be translated that is inside the bundle.</param>
        /// <param name="keyTranslated">The translation <see cref="string"/> of the key <see cref="string"/> in the bundle.</param>
        public static void AddKey(string bundle, string key, string keyTranslated) => TranslationPatcher.AddTranslationKey(bundle, key, keyTranslated);

        /// <summary>
        /// Translates a key in the achieve bundle.
        /// </summary>
        /// <param name="achieveKey">The key <see cref="string"/> to translate.</param>
        /// <param name="achieveTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Achieve(string achieveKey, string achieveTranslated) => TranslationPatcher.AddAchievementTranslation(achieveKey, achieveTranslated);

        /// <summary>
        /// Translates a key in the exchange bundle.
        /// </summary>
        /// <param name="exchangeKey">The key <see cref="string"/> to translate.</param>
        /// <param name="exchangeTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Exchange(string exchangeKey, string exchangeTranslated) => TranslationPatcher.AddExchangeTranslation(exchangeKey, exchangeTranslated);

        /// <summary>
        /// Translates a key in the global bundle.
        /// </summary>
        /// <param name="globalKey">The key <see cref="string"/> to translate.</param>
        /// <param name="globalTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Global(string globalKey, string globalTranslated) => TranslationPatcher.AddGlobalTranslation(globalKey, globalTranslated);

        /// <summary>
        /// Translates a key in the mail bundle.
        /// </summary>
        /// <param name="mailKey">The key <see cref="string"/> to translate.</param>
        /// <param name="mailTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Mail(string mailKey, string mailTranslated) => TranslationPatcher.AddMailTranslation(mailKey, mailTranslated);

        /// <summary>
        /// Adds a translation for an SRML Error.
        /// </summary>
        /// <param name="language">The language <see cref="MessageDirector.Lang"/> that the translation is in.</param>
        /// <param name="errorTranslated">The translated <see cref="string"/> of the error message.</param>
        /// <param name="abortTranslated">The translated <see cref="string"/> of the abort message.</param>
        public static void SRMLError(MessageDirector.Lang language, string errorTranslated, string abortTranslated) => TranslationPatcher.AddSRMLErrorUITranslation(language, errorTranslated, abortTranslated);

        /// <summary>
        /// Translates a key in the tutorial bundle.
        /// </summary>
        /// <param name="tutorialKey">The key <see cref="string"/> to translate.</param>
        /// <param name="tutorialTranslated">The translated <see cref="string"/> of the key <see cref="string"/>.</param>
        public static void Tutorial(string tutorialKey, string tutorialTranslated) => TranslationPatcher.AddTutorialTranslation(tutorialKey, tutorialTranslated);

        /// <summary>
        /// Creates a key <see cref="string"/> for translations.
        /// </summary>
        /// <param name="letter">The letter <see cref="string"/> that starts before the key.</param>
        /// <param name="prefix">The prefix <see cref="string"/> of the key.</param>
        /// <param name="localizationSuffix">The suffix <see cref="string"/> of the key.</param>
        /// <returns><see cref="string"/></returns>
        public static string CreateKey(string letter, string prefix, string suffix) => letter + "." + prefix + "." + suffix;

        /// <summary>
        /// Creates a key <see cref="string"/> for translations.
        /// </summary>
        /// <param name="letter">The letter <see cref="string"/> that starts before the key.</param>
        /// <param name="prefix">The prefix <see cref="string"/> of the key.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> that will be using this translation.</param>
        /// <returns><see cref="string"/></returns>
        public static string CreateIdentifiableKey(string letter, string prefix, Identifiable.Id identifiable) => letter + "." + prefix + "." + identifiable.ToString().ToLower();

        /// <summary>
        /// Creates a new <see cref="PediaEntryTranslation"/>.
        /// </summary>
        /// <param name="pediaIdentifiable">The <see cref="PediaDirector.Id"/> that will be attached to this <see cref="PediaEntryTranslation"/>.</param>
        /// <returns><see cref="PediaEntryTranslation"/></returns>
        public static PediaEntryTranslation CreatePediaTranslation(PediaDirector.Id pediaIdentifiable) => new PediaEntryTranslation(pediaIdentifiable);

        /// <summary>
        /// Creates a new <see cref="SlimePediaEntryTranslation"/>.
        /// </summary>
        /// <param name="pediaIdentifiable">The <see cref="PediaDirector.Id"/> that will be attached to this <see cref="SlimePediaEntryTranslation"/>.</param>
        /// <returns><see cref="SlimePediaEntryTranslation"/></returns>
        public static SlimePediaEntryTranslation CreateSlimepediaTranslation(PediaDirector.Id pediaIdentifiable) => new SlimePediaEntryTranslation(pediaIdentifiable);

        /// <summary>
        /// Adds a new pedia entry to the <see cref="PediaRegistry.PediaCategory.RESOURCES"/> category.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> attached to this pedia entry.</param>
        /// <param name="pediaIdentifiable">The <see cref="PediaDirector.Id"/> attached to this pedia entry.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for this pedia entry.</param>
        /// <param name="pediaTitle">The title <see cref="string"/> of this pedia entry.</param>
        /// <param name="pediaIntro">The intro <see cref="string"/> of this pedia entry.</param>
        /// <param name="pediaResourceType">The resource type <see cref="string"/> of this resource for this pedia entry.</param>
        /// <param name="pediaFavoredBy">The favored by <see cref="string"/> of this resource for this pedia entry.</param>
        /// <param name="pediaDescription">The description <see cref="string"/> of this pedia entry.</param>
        public static void AddResourcePedia(Identifiable.Id identifiable, PediaDirector.Id pediaIdentifiable, Sprite icon, string pediaTitle, string pediaIntro, string pediaResourceType, string pediaFavoredBy, string pediaDescription)
        {
            PediaRegistry.RegisterIdEntry(pediaIdentifiable, icon);
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);
            PediaRegistry.RegisterIdentifiableMapping(pediaIdentifiable, identifiable);

            PediaRegistry.SetPediaCategory(pediaIdentifiable, PediaRegistry.PediaCategory.RESOURCES);
            CreatePediaTranslation(pediaIdentifiable)
                .SetTitleTranslation(pediaTitle)
                .SetIntroTranslation(pediaIntro)
                .SetDescriptionTranslation(pediaDescription);

            Pedia(CreateIdentifiableKey("m", "resource_type", identifiable), pediaResourceType);
            Pedia(CreateIdentifiableKey("m", "favored_by", identifiable), pediaFavoredBy);
        }

        /// <summary>
        /// Adds a new pedia entry to the <see cref="PediaRegistry.PediaCategory.SLIMES"/> category.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> attached to this pedia entry.</param>
        /// <param name="pediaIdentifiable">The <see cref="PediaDirector.Id"/> attached to this pedia entry.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for this pedia entry.</param>
        /// <param name="pediaTitle">The title <see cref="string"/> of this pedia entry.</param>
        /// <param name="pediaIntro">The intro <see cref="string"/> of this pedia entry.</param>
        /// <param name="pediaDiet">The diet <see cref="string"/> of this slime for this pedia entry.</param>
        /// <param name="pediaFavorite">The favorite <see cref="string"/> of this slime for this pedia entry.</param>
        /// <param name="pediaSlimeology">The slimeology <see cref="string"/> of this slime for this pedia entry.</param>
        /// <param name="pediaRisks">The risks <see cref="string"/> of this slime for this pedia entry.</param>
        /// <param name="pediaPlortonomics">The plortonomics <see cref="string"/> of this slime for this pedia entry.</param>
        public static void AddSlimepedia(Identifiable.Id identifiable, PediaDirector.Id pediaIdentifiable, Sprite icon, string pediaTitle, string pediaIntro, string pediaDiet, string pediaFavorite, string pediaSlimeology, string pediaRisks, string pediaPlortonomics)
        {
            PediaRegistry.RegisterIdEntry(pediaIdentifiable, icon);
            PediaRegistry.RegisterIdentifiableMapping(pediaIdentifiable, identifiable);
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.SLIMES, identifiable);

            PediaRegistry.SetPediaCategory(pediaIdentifiable, PediaRegistry.PediaCategory.SLIMES);
            CreateSlimepediaTranslation(pediaIdentifiable)
                .SetTitleTranslation(pediaTitle)
                .SetIntroTranslation(pediaIntro)
                .SetDietTranslation(pediaDiet)
                .SetFavoriteTranslation(pediaFavorite)
                .SetSlimeologyTranslation(pediaSlimeology)
                .SetRisksTranslation(pediaRisks)
                .SetPlortonomicsTranslation(pediaPlortonomics);
        }
    }
}
