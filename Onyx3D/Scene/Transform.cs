using OpenTK;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Xml.Schema;

namespace Onyx3D
{
	public class Transform : IXmlSerializable
	{
		public SceneObject SceneObject;
		
		private Vector4 mLocalScale = Vector4.One;
		private Vector4 mLocalPosition = Vector4.UnitW;
		private Quaternion mLocalRotation = Quaternion.Identity;

        private Matrix4 mOffset = Matrix4.Identity;

		private Matrix4 mBakedModelM;
        private Matrix4 mBakedNormalM;
        private Vector4 mBakedPosition;
        
        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }
        //private Vector4 mBakedRotation;

        public Vector3 LocalScale
		{
			get { return mLocalScale.Xyz; }
			set {
				mLocalScale = new Vector4(value, 1);
				SetDirty();
			}
		}

		public Vector3 LocalPosition
		{
			get { return mLocalPosition.Xyz; }
			set {
				mLocalPosition = new Vector4(value, 1);
				SetDirty();
			}
		}

		public Quaternion LocalRotation
		{
			get { return mLocalRotation; }
			set {
				mLocalRotation = value;
				SetDirty();
			}
		}

        public Matrix4 Offset
        {
            get { return mOffset; }
            set
            {
                mOffset = value;
                SetDirty();
            }
        }

		public Vector3 Position {
			get { return mBakedPosition.Xyz; }
			set
			{
				mLocalPosition = new Vector4(WorldToLocal(value), 1);
				SetDirty();
			}
		}

		public Matrix4 ModelMatrix { get { return mBakedModelM; } }

        public Matrix4 NormalMatrix { get { return mBakedNormalM; } }

        public Transform(SceneObject sceneObject)
		{
			SceneObject = sceneObject;
			SetDirty();
		}

        public void FromMatrix(Matrix4 m)
        {
            mLocalPosition = new Vector4(m.ExtractTranslation(), 1);
            mLocalRotation = m.ExtractRotation();
            mLocalScale = new Vector4(m.ExtractScale(), 1);
            SetDirty();
        }

		public Matrix4 CalculateModelMatrix()
		{
			Matrix4 t = Matrix4.CreateTranslation(mLocalPosition.X, mLocalPosition.Y, mLocalPosition.Z);
			Matrix4 r = Matrix4.CreateFromQuaternion(mLocalRotation);
			Matrix4 s = Matrix4.CreateScale(mLocalScale.X, mLocalScale.Y, mLocalScale.Z);

			Matrix4 model = s * r * t;
			if (SceneObject.Parent != null)
			{
				model *= SceneObject.Parent.Transform.ModelMatrix; //SceneObject.Parent.Transform.CalculateModelMatrix();
			}

            if (Offset != Matrix4.Identity)
                model *= Offset;

            return model;
		}

		Matrix4 GetScaleMatrix()
		{
			return Matrix4.CreateScale(mLocalScale.X, mLocalScale.Y, mLocalScale.Z);
		}

		public Matrix4 GetTranslationMatrix()
		{
			return Matrix4.CreateTranslation(mLocalPosition.X, mLocalPosition.Y, mLocalPosition.Z);
		}

        public Matrix4 GetRotationMatrix()
        {
			return Matrix4.CreateFromQuaternion(mLocalRotation);
		}

        public Matrix4 GetYawMatrix(float rotY)
        {
			return Matrix4.CreateRotationY(rotY);
        }

		public Matrix4 GetPitchMatrix(float rotX)
		{
			return Matrix4.CreateRotationX(rotX);
		}

		public Matrix4 GetRollMatrix(float rotZ)
		{
			return Matrix4.CreateRotationZ(rotZ);
		}

		public void Rotate(Vector3 euler)
		{
			Quaternion rotation = Quaternion.FromEulerAngles(euler);
			Rotate(rotation);
		}

		public void Rotate(Quaternion rot)
		{
			mLocalRotation = mLocalRotation * rot;
			SetDirty();
		}

		public void Translate(Vector3 translation)
		{
			mLocalPosition += new Vector4(translation,1);
			SetDirty();
		}

		public Vector3 LocalToWorld(Vector3 point)
		{
			return (new Vector4(point, 1)  * mBakedModelM).Xyz;
		}

		public Vector3 WorldToLocal(Vector3 point)
		{
			return (new Vector4(point, 1) * mBakedModelM.Inverted()).Xyz;
		}

		public void LookAt(Vector3 position, Vector3 up)
		{
			Matrix3 lookAt = new Matrix3(Matrix4.LookAt(Position, position, up));
			mLocalRotation = Quaternion.FromMatrix(lookAt);
		}

		public void SetDirty()
		{
			SetModelMatrix(CalculateModelMatrix());
		}

		public void SetModelMatrix(Matrix4 model)
		{
			mBakedModelM = model;
			mBakedNormalM = Matrix4.Transpose(Matrix4.Invert(mBakedModelM));
			mBakedPosition = Vector4.UnitW * mBakedModelM;
			//mBakedRotation = GetModelMatrix() * mLocalRotation;

			Right = new Vector3(Vector4.UnitX * mBakedModelM);
			Up = new Vector3(Vector4.UnitY * mBakedModelM);
			Forward = new Vector3(Vector4.UnitZ * mBakedModelM);

			for (int c = 0; c < SceneObject.Components.Count; c++)
			{
				SceneObject.Components[c].OnDirtyTransform();
			}

			for (int i = 0; i < SceneObject.ChildCount; ++i)
			{
				SceneObject.GetChild(i).Transform.SetDirty();
			}
		}

		public void Copy(Transform other)
		{
			mLocalPosition = other.mLocalPosition;
			mLocalScale = other.mLocalScale;
			mLocalRotation = other.mLocalRotation;
			SetDirty();
		}

        // --------------------- Serialization

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            LocalPosition = XmlUtils.StringToVector3(reader.GetAttribute("position"));
            Vector4 rotation = XmlUtils.StringToVector4(reader.GetAttribute("rotation"));
            LocalRotation = Quaternion.FromAxisAngle(rotation.Xyz, rotation.W);
            LocalScale = XmlUtils.StringToVector3(reader.GetAttribute("scale"));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Transform");
            writer.WriteAttributeString("position", XmlUtils.Vector3ToString(LocalPosition));
            writer.WriteAttributeString("rotation", XmlUtils.Vector4ToString(LocalRotation.ToAxisAngle()));
            writer.WriteAttributeString("scale", XmlUtils.Vector3ToString(LocalScale));
            writer.WriteEndElement();
        }
    }
}
