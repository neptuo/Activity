﻿using Neptuo;
using Neptuo.Productivity.ActivityLog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neptuo.Productivity.ActivityLog
{
    public class ApplicationTrayIcon : DisposableBase
    {
        private readonly INavigator navigator;
        private NotifyIcon icon;

        public ApplicationTrayIcon(INavigator navigator)
        {
            Ensure.NotNull(navigator, "navigator");
            this.navigator = navigator;

            icon = new NotifyIcon();
            icon.Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
            icon.Text = "ActivityLog";
            icon.MouseClick += OnIconClick;
            icon.Visible = true;

            icon.ContextMenu = new ContextMenu();
            icon.ContextMenu.MenuItems.Add("Today Overview", (sender, e) => { OnIconClick(sender, null); });
            icon.ContextMenu.MenuItems.Add("Configuration", (sender, e) => { navigator.Message("Not yet implemented"); });
            icon.ContextMenu.MenuItems.Add("Exit", OnExitClick);
        }

        private void OnIconClick(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == MouseButtons.Left)
                navigator.Overview();
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            navigator.Exist();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            icon.Dispose();
            icon = null;
        }
    }
}