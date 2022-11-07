using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Librarys.LuaImplementation.Assemblys
{
    public class InputData
    {
        List<Keys> keysExist = new List<Keys> ();

        public InputData()
        {
            keysExist = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToList();

        }

        public bool OnKeyDown (string Key)
        {
            var input = EngineWindows.instance.KeyboardState;

            foreach (Keys d in keysExist)
            {
                Keys key = d;

                if (d.ToString() == Key)
                { 
                    return input.IsKeyPressed(key);
                }
            }

            return false;
        }

        public bool OnKeyPressed (string Key)
        {
            var input = EngineWindows.instance.KeyboardState;

            foreach (Keys d in keysExist)
            {
                Keys key = d;

                if (d.ToString() == Key)
                {
                    return input.IsKeyDown(key);
                }
            }

            return false;
        }

        public bool OnKeyUp(string Key)
        {
            var input = EngineWindows.instance.KeyboardState;

            foreach (Keys d in keysExist)
            {
                Keys key = d;

                if (d.ToString() == Key)
                {
                    return input.IsKeyReleased(key);
                }
            }

            return false;
        }
    }
}
