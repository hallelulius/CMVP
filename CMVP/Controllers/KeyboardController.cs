using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{
    class KeyboardController : Controller
    {
        public KeyboardController () : base()
        {

        }
        public override void updateController()
        {
            if (NativeKeyboard.IsKeyDown(KeyCode.Up))
            {
                outThrottle += 1/150;
            }
            if (NativeKeyboard.IsKeyDown(KeyCode.Down))
            {
                outThrottle -= 1 / 150;
            }
            if (NativeKeyboard.IsKeyDown(KeyCode.Left))
            {
                outSteer -= 1 / 150;
            }
            if (NativeKeyboard.IsKeyDown(KeyCode.Right))
            {
                outSteer += 1 / 150;
            }
            if (NativeKeyboard.IsKeyDown(KeyCode.Space)) // Emergency Stop 
            {
                outThrottle = 0; 
            }
        }

        /// <summary>
        /// Codes representing keyboard keys.
        /// </summary>
        internal enum KeyCode : int
        {
            /// <summary>
            /// The left arrow key.
            /// </summary>
            Left = 0x25,

            /// <summary>
            /// The up arrow key.
            /// </summary>
            Up,

            /// <summary>
            /// The right arrow key.
            /// </summary>
            Right,

            /// <summary>
            /// The down arrow key.
            /// </summary>
            Down,

            /// <summary>
            /// The down arrow key.
            /// </summary>
            Space
        }

        /// <summary>
        /// Provides keyboard access.
        /// </summary>
        internal static class NativeKeyboard
        {
            /// <summary>
            /// A positional bit flag indicating the part of a key state denoting
            /// key pressed.
            /// </summary>
            private const int KeyPressed = 0x8000;

            /// <summary>
            /// Returns a value indicating if a given key is pressed.
            /// </summary>
            /// <param name="key">The key to check.</param>
            /// <returns>
            /// <c>true</c> if the key is pressed, otherwise <c>false</c>.
            /// </returns>
            public static bool IsKeyDown(KeyCode key)
            {
                return (GetKeyState((int)key) & KeyPressed) != 0;
            }

            /// <summary>
            /// Gets the key state of a key.
            /// </summary>
            /// <param name="key">Virtuak-key code for key.</param>
            /// <returns>The state of the key.</returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            private static extern short GetKeyState(int key);
        }

    }
}


