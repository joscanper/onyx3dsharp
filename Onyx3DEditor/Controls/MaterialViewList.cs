using Onyx3D;
using System.Collections.Generic;
using System.Drawing;

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
			SingleMeshPreviewRenderer meshPreview = (SingleMeshPreviewRenderer)mPreview;
			meshPreview.SetMaterial(guid);
			meshPreview.Render();
			Bitmap preview = meshPreview.AsBitmap();

			return preview;
		}

		// --------------------------------------------------------------------

		protected override PreviewRenderer InstantiatePreviewRenderer()
		{
			return new SingleMeshPreviewRenderer();
		}

		// --------------------------------------------------------------------

		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Materials;
		}
	}
}