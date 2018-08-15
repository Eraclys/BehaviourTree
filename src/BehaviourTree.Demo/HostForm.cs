using BehaviourTree.Demo.GameEngine;
using System.Drawing;
using System.Windows.Forms;

namespace BehaviourTree.Demo
{
    public sealed partial class HostForm : Form, IRenderSystem
    {
        private readonly Image _backBuffer;

        public HostForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            ClientSize = new Size(1280, 720);
            _backBuffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics = Graphics.FromImage(_backBuffer);
        }

        public Graphics Graphics { get; }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            BackgroundImage = _backBuffer;
            Invalidate();
        }
    }
}
