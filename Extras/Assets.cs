using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Extras
{
    public static class Assets
    {
        public static T GetResource<T>(string name) where T : UnityEngine.Object => Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(found => found.name.Equals(name));

        public static AssetBundle LoadBundle(string path) => AssetBundle.LoadFromFile(path);

        public static Texture2D LoadAsset(string path)
        {
            byte[] byteArray = File.ReadAllBytes(path);
            Texture2D sampleTexture = new Texture2D(2, 2);
            sampleTexture.LoadImage(byteArray);
            return sampleTexture;
        }
    }
}
