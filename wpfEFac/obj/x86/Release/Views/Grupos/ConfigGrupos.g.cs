﻿#pragma checksum "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C34EFE2BEC2FC0CC37BAD05DD167D2B426EF8914D084FC31A7F6BF98DD180884"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace wpfEFac {
    
    
    /// <summary>
    /// ConfigGrupos
    /// </summary>
    public partial class ConfigGrupos : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 15 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox grbGruposTrabajo;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttAgregarGrupo;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgGruposTrabajo;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn strID;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn strNombre;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn dtmFechaCreacion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/wpfEFac;component/views/grupos/configgrupos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
            ((wpfEFac.ConfigGrupos)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grbGruposTrabajo = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.bttAgregarGrupo = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
            this.bttAgregarGrupo.Click += new System.Windows.RoutedEventHandler(this.bttAgregarGrupo_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dtgGruposTrabajo = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.strID = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.strNombre = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.dtmFechaCreacion = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 33 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bttEditar_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 34 "..\..\..\..\..\Views\Grupos\ConfigGrupos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bttBorrar_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

