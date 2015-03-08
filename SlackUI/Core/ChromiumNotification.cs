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
                        DisplayWrapperForm();
                    }
                };
            });
        }

        #region Private Methods
        private static void DisplayWrapperForm()
        {
            // Focus the wrapper form or display it to the user
            if (Program.WrapperForm.Visible)
            {
                // Restore normal window state if form is minimized
                if (Program.WrapperForm.WindowState == FormWindowState.Minimized)
                {
                    Program.WrapperForm.WindowState = FormWindowState.Normal;
                }

                // Activates the wrapper form and gives it focus
                Program.WrapperForm.Activate();
            }
            else
            {
                // Prompt for initial team domain to load or shows the wrapper form
                if (Program.Settings.Data.InitialTeamToLoad.Equals(String.Empty))
                {
                    using (TeamPickerForm teamPickerForm = new TeamPickerForm())
                    {
                        if (teamPickerForm.ShowDialog() == DialogResult.OK)
                        {
                            Program.Settings.Data.InitialTeamToLoad = teamPickerForm.SlackTeamDomain;
                            Program.WrapperForm.Show();
                        }
                    }
                }
                else
                {
                    // Displays the wrapper form to the user and gives it focus
                    Program.WrapperForm.Show();
                    Program.WrapperForm.Activate();
                }
            }
        }
    }
    #endregion
}
