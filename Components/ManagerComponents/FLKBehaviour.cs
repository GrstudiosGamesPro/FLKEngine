using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    [System.Serializable]
    public abstract class FLKBehaviour
    {
        public GameObject owner = null;

        public virtual void Start() { }
        public virtual void Update() { }
    }
}
