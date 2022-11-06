using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    /// <summary>Class responsible for handling ComponentManager i.e. add, delete, get</summary>
    public class ComponentManager
    {
        public Dictionary<Type, FLKBehaviour> components;

        public ComponentManager()
        {
            components = new Dictionary<Type, FLKBehaviour>();
        }

        public void addComponent(FLKBehaviour comp)
        {
            components[comp.GetType()] = comp;
        }

        public T getComponent<T>() where T : FLKBehaviour
        {
            return components.TryGetValue(typeof(T), out var value) ? (T)value : null;
        }

        public void removeComponent<T>() where T : FLKBehaviour
        {
            components.Remove(typeof(T));
        }

        public GameObject FindObjectPerName (string ObjectName)
        {
            foreach (GameObject d in EngineWindows.instance.CurrentOpenScene.ObjectsInScene)
            {
                if (d.Name == ObjectName)
                {
                    return d;
                }
            }

            return null;
        }
    }
}