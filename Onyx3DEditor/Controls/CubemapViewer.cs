using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;

namespace Onyx3DEditor
{
    public partial class CubemapViewer : UserControl
    {
        private Cubemap mCubemap;

        public CubemapViewer()
        {
            InitializeComponent();
        }

        public void Init(Cubemap cubemap)
        {
            mCubemap = cubemap;
            UpdateImages();
        }

        public void UpdateImages()
        {
            pictureBoxTest.Image = mCubemap.TextureFront.AsBitmap();
            pictureBoxTest2.Image = mCubemap.TextureLeft.AsBitmap();
            pictureBoxTest3.Image = mCubemap.TextureBack.AsBitmap();
            pictureBoxTest4.Image = mCubemap.TextureRight.AsBitmap();
        }
    }
}
