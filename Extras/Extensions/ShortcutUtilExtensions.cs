using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extensions
{
    public static class ShortcutUtilExtensions
    {
        /// <summary>
        /// Adds multiple <see cref="Component"/>s to a <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to add the <see cref="Component"/>s to.</param>
        /// <param name="types">The <see cref="Array"/> of <see cref="Type"/>s to be added to the <see cref="GameObject"/>. Must be valid <see cref="Component"/>s.</param>
        public static void AddComponents(this GameObject obj, params Type[] types)
        {
            foreach (Type type in types)
                obj.AddComponent(type);
        }

        /// <summary>
        /// Adds a set of <see cref="Component"/>s that custom <see cref="Identifiable.Id"/>s naturally have.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to add the <see cref="Component"/>s to.</param>
        public static void AddIdentifiableComponents(this GameObject obj)
        {
            obj.AddComponent<Rigidbody>();
            obj.AddComponent<RegionMember>();
            obj.AddComponent<Identifiable>();
            obj.AddComponent<Vacuumable>();
        }
    }
}
