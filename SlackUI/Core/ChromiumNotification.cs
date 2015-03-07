using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackUI.Core {
    class ChromiumNotification {
        private const string GroupMessageTitle = "New message in #";
        private const string MemberMessageTitle = "New message from ";

        public void ShowNotification(String title, String body, String icon, String tag) {
            Program.WrapperForm.InvokeOnUiThreadIfRequired(() => {
                var notification = Toaster.Toast.ShowNotification(title, body, icon, tag);
                notification.FormClosed += delegate {
                    if (!notification.Dismissed)
                    {
                        var target = "";
                        if (notification.Title.Contains(GroupMessageTitle))
                        {
                            target = Program.ActiveTeamAddress + "/messages/" + notification.Title.Replace(GroupMessageTitle, "");
                        }
                        if (notification.Title.Contains(MemberMessageTitle))
                        {
                            target = Program.ActiveTeamAddress + "/messages/@" + notification.Title.Replace(MemberMessageTitle, "");
                        }

                        if (target != "" && !Program.WrapperForm.address().Contains(target)) { Program.WrapperForm.redirect(target); }
                        NotificationContext.DisplayWrapperForm();
                    }
                };
            });
        }
    }
}
