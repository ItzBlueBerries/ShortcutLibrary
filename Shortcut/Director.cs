using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// <see cref="SceneContext"/> / <see cref="GameContext"/> director fields.
    /// </summary>
    public static class Director
    {
        public static MailDirector Mail
        {
            get
            {
                return SceneContext.Instance.MailDirector;
            }
        }

        public static AchievementsDirector Achieve
        {
            get
            {
                return SceneContext.Instance.AchievementsDirector;
            }
        }

        public static AmbianceDirector Ambiance
        {
            get
            {
                return SceneContext.Instance.AmbianceDirector;
            }
        }

        public static EconomyDirector Economy
        {
            get
            {
                return SceneContext.Instance.EconomyDirector;
            }
        }

        public static ExchangeDirector Exchange
        {
            get
            {
                return SceneContext.Instance.ExchangeDirector;
            }
        }

        public static GadgetDirector Gadget
        {
            get
            {
                return SceneContext.Instance.GadgetDirector;
            }
        }

        public static HolidayDirector Holiday
        {
            get
            {
                return SceneContext.Instance.HolidayDirector;
            }
        }

        public static InstrumentDirector Instrument
        {
            get
            {
                return SceneContext.Instance.InstrumentDirector;
            }
        }

        public static MetadataDirector Metadata
        {
            get
            {
                return SceneContext.Instance.MetadataDirector;
            }
        }

        public static ModDirector Mod
        {
            get
            {
                return SceneContext.Instance.ModDirector;
            }
        }

        public static PediaDirector Pedia
        {
            get
            {
                return SceneContext.Instance.PediaDirector;
            }
        }

        public static PopupDirector Popup
        {
            get
            {
                return SceneContext.Instance.PopupDirector;
            }
        }

        public static ProgressDirector Progress
        {
            get
            {
                return SceneContext.Instance.ProgressDirector;
            }
        }

        public static RanchDirector Ranch
        {
            get
            {
                return SceneContext.Instance.RanchDirector;
            }
        }

        public static SceneParticleDirector SceneParticle
        {
            get
            {
                return SceneContext.Instance.SceneParticleDirector;
            }
        }

        public static SECTRDirector Sector
        {
            get
            {
                return SceneContext.Instance.SECTRDirector;
            }
        }

        public static TimeDirector Time
        {
            get
            {
                return SceneContext.Instance.TimeDirector;
            }
        }

        public static SlimeAppearanceDirector Appearance
        {
            get
            {
                return SceneContext.Instance.SlimeAppearanceDirector;
            }
        }

        public static TutorialDirector Tutorial
        {
            get
            {
                return SceneContext.Instance.TutorialDirector;
            }
        }

        public static LookupDirector Lookup
        {
            get
            {
                return GameContext.Instance.LookupDirector;
            }
        }

        public static AutoSaveDirector AutoSave
        {
            get
            {
                return GameContext.Instance.AutoSaveDirector;
            }
        }

        public static DLCDirector DLC
        {
            get
            {
                return GameContext.Instance.DLCDirector;
            } }

        public static GalaxyDirector Galaxy
        {
            get
            {
                return GameContext.Instance.GalaxyDirector;
            }
        }

        public static InputDirector Input
        {
            get
            {
                return GameContext.Instance.InputDirector;
            }
        }

        public static MessageDirector Message
        {
            get
            {
                return GameContext.Instance.MessageDirector;
            }
        }

        public static MessageOfTheDayDirector MessageOfTheDay
        {
            get
            {
                return GameContext.Instance.MessageOfTheDayDirector;
            }
        }

        public static OptionsDirector Options
        {
            get
            {
                return GameContext.Instance.OptionsDirector;
            }
        }

        public static RailDirector Rail
        {
            get
            {
                return GameContext.Instance.RailDirector;
            }
        }

        public static RichPresence.Director Presence
        {
            get
            {
                return GameContext.Instance.RichPresenceDirector;
            }
        }

        public static ToyDirector Toy
        {
            get
            {
                return GameContext.Instance.ToyDirector;
            }
        }
    }
}
