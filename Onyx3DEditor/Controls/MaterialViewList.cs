using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

using Onyx3D;

namespace Onyx3DEditor
{

	public partial class MaterialViewList : AssetViewList
	{
		
		protected override void AddBuiltIn(int selectedGuid)
		{
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.Default), 0, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.Unlit), 1, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.UnlitVertexColor), 2, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.ReflectionProbe), 3, selectedGuid);

		}

		// --------------------------------------------------------------------

		protected override Bitmap GeneratePreview(int guid)
		{
			mPreview.SetMaterial(guid);
			mPreview.Render();
			Bitmap preview = mPreview.AsBitmap();
			
			return preview;
		}

		
		// --------------------------------------------------------------------

		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Materials;
		}
	}
}
