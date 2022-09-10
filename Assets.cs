using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib
{
    public class Assets
    {
        /*public static Texture2D LoadImage(string filename) // thanks aidan or whoever created this at first- lol
        {
            var a = Assembly.GetExecutingAssembly();
            var spriteData = a.GetManifestResourceStream(a.GetName().Name + "." + filename);
            var rawData = new byte[spriteData.Length];
            spriteData.Read(rawData, 0, rawData.Length);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(rawData);
            tex.filterMode = FilterMode.Bilinear;
            return tex;
        }
        public static Sprite CreateSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);*/
        
        public static Texture2D LoadAsset(string imagePath)
        {
            byte[] byteArray = File.ReadAllBytes($"{Environment.CurrentDirectory}\\" + imagePath);
            Texture2D sampleTexture = new Texture2D(2, 2);
            sampleTexture.LoadImage(byteArray);
            return sampleTexture;
        }

        public static AssetBundle LoadBundle(string bundlePath)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile($"{Environment.CurrentDirectory}\\" + bundlePath);
            return bundle;
        }

        private static Dictionary<Type, UnityEngine.Object[]> cache = new Dictionary<Type, UnityEngine.Object[]>();
        public static T LoadResource<T>(string name) where T : UnityEngine.Object
        {
            if (!cache.ContainsKey(typeof(T)))
                cache[typeof(T)] = Resources.FindObjectsOfTypeAll<T>();
            return (T)cache[typeof(T)].FirstOrDefault(x => x.name == name);
        }
    }
}
