using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extras
{
    public static class Asset
    {
        /// <summary>
        /// Searches and gets a <see cref="UnityEngine.Object"/> from the game resources.
        /// </summary>
        /// <typeparam name="T">The <see cref="UnityEngine.Object"/> to be searched for.</typeparam>
        /// <param name="name">The name <see cref="string"/> of the resource.</param>
        /// <returns><see cref="{T}"/></returns>
        public static T GetResource<T>(string name) where T : UnityEngine.Object => Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(found => found.name.Equals(name));

        /// <summary>
        /// Loads an <see cref="AssetBundle"/> from outside the assembly.
        /// </summary>
        /// <param name="path">The path <see cref="string"/> to the <see cref="AssetBundle"/> outside the assembly.</param>
        /// <returns><see cref="AssetBundle"/></returns>
        public static AssetBundle LoadBundle(string path) => AssetBundle.LoadFromFile(path);

        /// <summary>
        /// Converts a <see cref="Texture2D"/> to a <see cref="Sprite"/>.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to be converted.</param>
        /// <returns><see cref="Sprite"/></returns>
        public static Sprite ConvertToSprite(this Texture2D texture) => 
            Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);

        /// <summary>
        /// Loads a <see cref="Texture2D"/> from outside the assembly.
        /// </summary>
        /// <param name="path">The path <see cref="string"/> to the <see cref="Texture2D"/> outside the assembly.</param>
        /// <returns><see cref="Texture2D"/></returns>
        public static Texture2D LoadTexture(string path)
        {
            byte[] byteArray = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(byteArray);
            return texture;
        }
    }
}
