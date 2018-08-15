using System;
using System.Drawing;
using System.IO;

namespace BehaviourTree.Demo
{
    public static class SpriteManager
    {
        public static Image GetSprite(string fileName)
        {
            var image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "Sprites", fileName));

            return image;
        }
    }
}