using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using InputSimulator.Native;

namespace InputSimulator
{
    public class KeyboardSimulator : IKeyboardSimulator
    {
        public IKeyboardSimulator KeyPress(params Key[] keys)
        {
            var inputs = new List<KeyboardInput>();

            foreach (var key in keys)
            {
                inputs.Add(ToKeyDown(key));
                inputs.Add(ToKeyUp(key));
            }

            DispatchKeyboardInput(inputs);

            return this;
        }

        public IKeyboardSimulator KeyUp(params Key[] keys)
        {
            DispatchKeyboardInput(keys.Select(ToKeyUp));
            return this;
        }

        public IKeyboardSimulator KeyDown(params Key[] keys)
        {
            DispatchKeyboardInput(keys.Select(ToKeyDown));
            return this;
        }

        public IKeyboardSimulator SendKeys(string value)
        {
            var inputs = new List<KeyboardInput>();

            foreach (var character in value)
            {
                inputs.Add(ToCharacterDown(character));
                inputs.Add(ToCharacterUp(character));
            }

            DispatchKeyboardInput(inputs);
            return this;
        }

        private static bool IsExtendedKey(Key keyCode)
        {
            return keyCode == Key.Menu ||
                   keyCode == Key.Ctrl ||
                   keyCode == Key.RightCtrl ||
                   keyCode == Key.Insert ||
                   keyCode == Key.Delete ||
                   keyCode == Key.Home ||
                   keyCode == Key.End ||
                   keyCode == Key.Right ||
                   keyCode == Key.Up ||
                   keyCode == Key.Left ||
                   keyCode == Key.Down ||
                   keyCode == Key.NumLock ||
                   keyCode == Key.PrintScreen ||
                   keyCode == Key.Divide;
        }

        private static KeyboardInput ToKeyDown(Key keyCode)
        {
            return new KeyboardInput
            {
                KeyCode = (ushort)keyCode,
                Scan = 0,
                Flags = IsExtendedKey(keyCode) ? (uint)KeyboardFlags.ExtendedKey : 0,
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
        }

        private static KeyboardInput ToKeyUp(Key keyCode)
        {
            return new KeyboardInput
            {
                KeyCode = (ushort)keyCode,
                Scan = 0,
                Flags = (uint)(IsExtendedKey(keyCode)
                    ? KeyboardFlags.KeyUp | KeyboardFlags.ExtendedKey
                    : KeyboardFlags.KeyUp),
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
        }

        private static KeyboardInput ToCharacterDown(char character)
        {
            ushort scanCode = character;

            var input = new KeyboardInput
            {
                KeyCode = 0,
                Scan = scanCode,
                Flags = (uint)KeyboardFlags.Unicode,
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };

            if ((scanCode & 0xFF00) == 0xE000)
            {
                input.Flags |= (int) KeyboardFlags.ExtendedKey;
            }

            return input;
        }

        private static KeyboardInput ToCharacterUp(char character)
        {
            ushort scanCode = character;

            var input = new KeyboardInput
            {
                KeyCode = 0,
                Scan = scanCode,
                Flags = (uint)(KeyboardFlags.KeyUp | KeyboardFlags.Unicode),
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };

            if ((scanCode & 0xFF00) == 0xE000)
            {
                input.Flags |= (int) KeyboardFlags.ExtendedKey;
            }

            return input;
        }


        private static void DispatchKeyboardInput(IEnumerable<KeyboardInput> keyboardInputs)
        {
            var inputs = keyboardInputs.Select(ki => new Native.Input
            {
                Type = 1,
                Data =
                {
                    Keyboard = ki
                }
            }).ToArray();

            var successful = SafeNativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<Native.Input>());

            if (successful != inputs.Length)
            {
                throw new Exception("Not all inputs were sent");
            }
        }
    }
}