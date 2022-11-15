using FLKEngine.Librarys.LuaImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    [Serializable]
    public class Prefab
    {
        public List<GameObject> ObjectsInPrefab = new List<GameObject>();
        public List<LuaCompiller> Scripts = new List<LuaCompiller>();
    }
}