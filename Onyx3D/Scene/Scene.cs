﻿
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
	public class Scene : GameAsset, IXmlSerializable, IDisposable
	{
		private Onyx3DInstance mContext;
		private List<EntityProxy> mEntityProxies = new List<EntityProxy>();

		public Onyx3DInstance Context
		{
			get
			{
				return mContext != null ? mContext : Onyx3DEngine.Instance;
			}
		}

		public SceneObject Root;
		public Camera ActiveCamera;
		public Lighting Lighting = new Lighting();
		public Sky Sky = new Sky();

		public bool IsDirty { get; private set; }
		public List<EntityProxy> EntityProxies { get { return mEntityProxies; } }

		// --------------------------------------------------------------------

		public Scene(Onyx3DInstance context = null)
		{
			mContext = context;
			Root = new SceneObject("", this);
			SetDirty(true);
		}

		// --------------------------------------------------------------------

		public void SetDirty(bool isDirty = true)
		{
			IsDirty = isDirty;

			mEntityProxies.Clear();
			Root.GetEntityProxiesInChildren(mEntityProxies);
		}

		// --------------------------------------------------------------------

		public void Dispose()
		{
			Root.ForEachChild((c) =>
			{
				c.Destroy();
			});

			Sky.Dispose();
			Lighting.Dispose();
		}

		

		// ------ Serialization ------

		public XmlSchema GetSchema()
		{
			throw new System.NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			
			reader.ReadStartElement("Scene");

			Lighting.ReadXml(reader);

			reader.ReadToNextSibling("Root");
			this.Root = new SceneObject("", this, 0);
			this.Root.ReadXml(reader);
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartDocument();

			writer.WriteStartElement("Scene");

			Lighting.WriteXml(writer);

			writer.WriteStartElement("Root");

			for (int i = 0; i < Root.ChildCount; ++i)
				Root.GetChild(i).WriteXml(writer);

			writer.WriteEndElement();

			writer.WriteEndElement();

			writer.WriteEndDocument();
		}
	}
}
