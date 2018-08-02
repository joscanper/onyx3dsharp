using System;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    public class SingletonForm<T> : Form where T : Form, new()
    {
        private static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null || mInstance.IsDisposed)
                    mInstance = new T();
                return mInstance;
            }
        }

    }
}
