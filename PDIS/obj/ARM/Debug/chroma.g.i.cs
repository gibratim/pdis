﻿#pragma checksum "C:\Users\Brian\Documents\Visual Studio 2013\Projects\PDIS\PDIS\chroma.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "513E35412A42159EE1F18FF4038C5639"
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
    
    
    public partial class chroma : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.MediaElement BackgroundMediaElement;
        
        internal System.Windows.Controls.Image FilteredImage;
        
        internal System.Windows.Media.CompositeTransform FilteredImageTransform;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/PDIS;component/chroma.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.BackgroundMediaElement = ((System.Windows.Controls.MediaElement)(this.FindName("BackgroundMediaElement")));
            this.FilteredImage = ((System.Windows.Controls.Image)(this.FindName("FilteredImage")));
            this.FilteredImageTransform = ((System.Windows.Media.CompositeTransform)(this.FindName("FilteredImageTransform")));
        }
    }
}
