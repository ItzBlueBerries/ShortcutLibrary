using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    public static class Ornament
    {
        public static GameObject CreateOrnament(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Texture2D texture, Color32 color, Color32 vacColor)
        {
            GameObject prefab = Prefab.ObjectCopy(Prefab.GetPrefab(baseIdentifiable));
            prefab.name = "ornament" + name.Replace(" ", "");
            prefab.GetComponent<Identifiable>().id = identifiable;

            GameObject model = prefab.transform.Find("model").gameObject;
            Material material = (Material)Prefab.Instantiate(model.GetComponent<MeshRenderer>().material);
            material.mainTexture = texture;
            material.color = color;
            model.GetComponent<MeshRenderer>().material = material;

            /*Registry.RegisterIdentPrefab(OrnamentPrefab);
            Registry.RegisterAmmo(OrnamentPrefab);
            Registry.RegisterSilo(ornamentIdent);
            Registry.RegisterVac(vacColor, ornamentIdent, ornamentIcon, ornamentName.ToLower().Replace(" ", "") + "Definition");*/
            return prefab;
        }
    }
}
