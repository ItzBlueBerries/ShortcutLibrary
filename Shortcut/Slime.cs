using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    public static class Slime
    {
        public static SlimeAppearance CopySlimeAppearance(SlimeDefinition slimeDefinition) =>
            (SlimeAppearance)Prefab.DeepCopy(slimeDefinition.AppearancesDefault[0]);

        public static SlimeDefinition GetSlimeDefinition(Identifiable.Id identifiable) => 
            SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(identifiable);
    }
}
