﻿#pragma checksum "C:\Users\Brian\Documents\Visual Studio 2013\Projects\PDIS\PDIS\tumbavu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F250D7ED2F70771A575873C691A0E8D2"
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
    
    
    public partial class tumbavu : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox currentCount;
        
        internal System.Windows.Controls.TextBox phoneStatus;
        
        internal System.Windows.Controls.TextBox notifyStatus;
        
        internal System.Windows.Controls.TextBox currentStatus;
        
        internal System.Windows.Controls.Image overlayCamera;
        
        internal System.Windows.Controls.Canvas mainCamera;
        
        internal System.Windows.Media.VideoBrush mainCameraBrush;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/PDIS;component/tumbavu.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.currentCount = ((System.Windows.Controls.TextBox)(this.FindName("currentCount")));
            this.phoneStatus = ((System.Windows.Controls.TextBox)(this.FindName("phoneStatus")));
            this.notifyStatus = ((System.Windows.Controls.TextBox)(this.FindName("notifyStatus")));
            this.currentStatus = ((System.Windows.Controls.TextBox)(this.FindName("currentStatus")));
            this.overlayCamera = ((System.Windows.Controls.Image)(this.FindName("overlayCamera")));
            this.mainCamera = ((System.Windows.Controls.Canvas)(this.FindName("mainCamera")));
            this.mainCameraBrush = ((System.Windows.Media.VideoBrush)(this.FindName("mainCameraBrush")));
        }
    }
}
