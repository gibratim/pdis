﻿#pragma checksum "C:\Users\Brian\Documents\Visual Studio 2013\Projects\PDIS\PDIS\proces.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "233988D4A76BAFB87389F71078B179C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PDIS {
    
    
    public partial class proces : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button seki;
        
        internal System.Windows.Controls.Image photoImage;
        
        internal System.Windows.Controls.TextBox txtCountdown;
        
        internal System.Windows.Controls.TextBox textbox;
        
        internal System.Windows.Controls.TextBlock width;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PDIS;component/proces.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.seki = ((System.Windows.Controls.Button)(this.FindName("seki")));
            this.photoImage = ((System.Windows.Controls.Image)(this.FindName("photoImage")));
            this.txtCountdown = ((System.Windows.Controls.TextBox)(this.FindName("txtCountdown")));
            this.textbox = ((System.Windows.Controls.TextBox)(this.FindName("textbox")));
            this.width = ((System.Windows.Controls.TextBlock)(this.FindName("width")));
        }
    }
}

