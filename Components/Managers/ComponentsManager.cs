using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components.Managers
{
    [System.Serializable]
    public class ComponentsManager
    {
        public List<FLKBehaviour> behaviours = new List<FLKBehaviour>()
        {
          new Camera()
        };
    }
}