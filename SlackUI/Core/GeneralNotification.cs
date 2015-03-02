using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackUI.Core {
    class GeneralNotification
    {
        public string permission;
                
        public void onclick()
        {
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/onclick]");
        }

        public void onshow()
        {
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/onshow]");
        }

        public void onerror()
        {
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/onerror]");
        }

        public void onclose()
        {
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/onclose]");
        }

        public void requestPermission(String callback)
        {
            this.permission = "granted";
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/requestPermission]");
        }
        public void close()
        {
            System.Diagnostics.Debug.WriteLine("[GeneralNotification/close]");
        }
    }
}
