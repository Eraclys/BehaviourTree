using System.Drawing;
using System.Linq;

namespace BehaviourTree.Demo
{
    public static class Assets
    {
        private const bool SimpleMode = false;

        private static readonly Image ObjectsSprite = SpriteManager.GetSprite("objects.png");
        private static readonly Image CharacterSprite = SpriteManager.GetSprite("character.png");
        private static readonly Image WorldSprite = SpriteManager.GetSprite("Overworld.png");

        private static readonly Image SimpleGrass = Square(80, 80, Color.Black);
        private static readonly Image SimpleTree = Triangle(13, 30, Color.DarkOliveGreen);
        private static readonly Image SimpleStone = Ellipse(40, 40, Color.DarkGray);
        private static readonly Image SimpleBot = Ellipse(13, 22, Color.Chocolate);
        private static readonly Image SimpleLoot = Ellipse(13, 13, Color.Gold);
        private static readonly Image SimpleFood = Square(15, 23, Color.Red);
        private static readonly Image SimpleHouse = Square(74, 80, Color.Brown);
        private static readonly Image SimpleTombstone = Square(19, 24, Color.CadetBlue);

        public static readonly Image Grass = SimpleMode ?
            SimpleGrass :
            WorldSprite.Select(new Rectangle(272, 464, 32, 32));

        public static readonly Image Tree = SimpleMode ?
            SimpleTree :
            ObjectsSprite.Select(new Rectangle(256, 192, 32, 31)).Resize(1.5f);

        public static readonly Image Stone = SimpleMode ?
            SimpleStone :
            ObjectsSprite.Select(new Rectangle(384, 6, 32, 26)).Resize(2f);

        public static readonly Image Loot = SimpleMode ?
            SimpleLoot :
            ObjectsSprite.Select(new Rectangle(73, 106, 13, 13)).Resize(1.5f);

        public static readonly Image Food = SimpleMode ?
            SimpleFood :
            WorldSprite.Select(new Rectangle(449, 320, 15, 23)).Resize(1.5f);

        public static readonly Image House = SimpleMode ?
            SimpleHouse :
            WorldSprite.Select(new Rectangle(99, 0, 74, 80));

        public static readonly Image Tombstone = SimpleMode ?
            SimpleTombstone :
            WorldSprite.Select(new Rectangle(567, 83, 19, 24)).Resize(1.5f);


        public static readonly Image BotRight1 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(2, 38, 13, 22)).Resize(1.5f);

        public static readonly Image BotRight2 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(18, 38, 13, 22)).Resize(1.5f);

        public static readonly Image BotRight3 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(34, 38, 13, 22)).Resize(1.5f);

        public static readonly Image BotRight4 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(50, 38, 13, 22)).Resize(1.5f);


        public static readonly Image BotLeft1 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(1, 102, 13, 22)).Resize(1.5f);

        public static readonly Image BotLeft2 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(18, 102, 13, 22)).Resize(1.5f);

        public static readonly Image BotLeft3 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(34, 102, 13, 22)).Resize(1.5f);

        public static readonly Image BotLeft4 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(50, 102, 13, 22)).Resize(1.5f);


        public static readonly Image BotUp1 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(0, 69, 15, 23)).Resize(1.5f);

        public static readonly Image BotUp2 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(16, 69, 15, 23)).Resize(1.5f);

        public static readonly Image BotUp3 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(32, 69, 15, 23)).Resize(1.5f);

        public static readonly Image BotUp4 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(48, 69, 15, 23)).Resize(1.5f);


        public static readonly Image BotDown1 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(1, 6, 15, 22)).Resize(1.5f);

        public static readonly Image BotDown2 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(17, 6, 15, 22)).Resize(1.5f);

        public static readonly Image BotDown3 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(33, 6, 15, 22)).Resize(1.5f);

        public static readonly Image BotDown4 = SimpleMode ?
            SimpleBot :
            CharacterSprite.Select(new Rectangle(49, 6, 15, 22)).Resize(1.5f);

        public static readonly Image TreeInventoryIcon = Tree.Resize(15, 15);
        public static readonly Image StoneInventoryIcon = Stone.Resize(15, 15);
        public static readonly Image LootInventoryIcon = Loot.Resize(15, 15);
        public static readonly Image FoodInventoryIcon = Food.Resize(15, 15);

        public static readonly int MaxIconWidth = new[]
        {
            TreeInventoryIcon.Width,
            StoneInventoryIcon.Width,
            LootInventoryIcon.Width,
            FoodInventoryIcon.Width
        }.Max();

        public static readonly int MaxIconHeight = new[]
        {
            TreeInventoryIcon.Height,
            StoneInventoryIcon.Height,
            LootInventoryIcon.Height,
            FoodInventoryIcon.Height
        }.Max();

        private static Image Ellipse(int width, int height, Color color)
        {
            var returnBitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(returnBitmap))
            {
                g.FillEllipse(
                    new SolidBrush(color),
                    new Rectangle(new Point(0,0), new Size(width, height)));
            }

            return returnBitmap;
        }

        private static Image Triangle(int width, int height, Color color)
        {
            var returnBitmap = new Bitmap(width, height);

            using (var imageGraphics = Graphics.FromImage(returnBitmap))
            {
                var solidBrush = new SolidBrush(color);
                Point[] points = { new Point(0, height), new Point(width/2, 0), new Point(width, height) };
                imageGraphics.FillPolygon(solidBrush, points);
            }

            return returnBitmap;
        }

        private static Image Square(int width, int height, Color color)
        {
            var returnBitmap = new Bitmap(width, height);

            using (var imageGraphics = Graphics.FromImage(returnBitmap))
            {
                imageGraphics.FillRectangle(
                    new SolidBrush(color),
                    new Rectangle(new Point(0, 0), new Size(width, height)));
            }

            return returnBitmap;
        }
    }
}
