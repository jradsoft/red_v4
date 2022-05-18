﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.4984
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="SIFEIPortBinding", Namespace="http://MApeados/")]
public partial class SIFEIService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback getTimbreCFDIOperationCompleted;
    
    private System.Threading.SendOrPostCallback cancelaCFDISignatureOperationCompleted;
    
    private System.Threading.SendOrPostCallback CambiaPasswordOperationCompleted;
    
    private System.Threading.SendOrPostCallback getCFDIOperationCompleted;
    
    private System.Threading.SendOrPostCallback cancelaCFDIOperationCompleted;
    
    private System.Threading.SendOrPostCallback getXMLOperationCompleted;
    
    /// <remarks/>
    public SIFEIService() {
        this.Url = "http://devcfdi.sifei.com.mx:8080/SIFEI/SIFEI";
    }
    
    /// <remarks/>
    public event getTimbreCFDICompletedEventHandler getTimbreCFDICompleted;
    
    /// <remarks/>
    public event cancelaCFDISignatureCompletedEventHandler cancelaCFDISignatureCompleted;
    
    /// <remarks/>
    public event CambiaPasswordCompletedEventHandler CambiaPasswordCompleted;
    
    /// <remarks/>
    public event getCFDICompletedEventHandler getCFDICompleted;
    
    /// <remarks/>
    public event cancelaCFDICompletedEventHandler cancelaCFDICompleted;
    
    /// <remarks/>
    public event getXMLCompletedEventHandler getXMLCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
    public byte[] getTimbreCFDI([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Usuario, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Password, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)] byte[] archivoXMLZip, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Serie, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IdEquipo) {
        object[] results = this.Invoke("getTimbreCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo});
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetTimbreCFDI(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getTimbreCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte[] EndgetTimbreCFDI(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public void getTimbreCFDIAsync(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo) {
        this.getTimbreCFDIAsync(Usuario, Password, archivoXMLZip, Serie, IdEquipo, null);
    }
    
    /// <remarks/>
    public void getTimbreCFDIAsync(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo, object userState) {
        if ((this.getTimbreCFDIOperationCompleted == null)) {
            this.getTimbreCFDIOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetTimbreCFDIOperationCompleted);
        }
        this.InvokeAsync("getTimbreCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo}, this.getTimbreCFDIOperationCompleted, userState);
    }
    
    private void OngetTimbreCFDIOperationCompleted(object arg) {
        if ((this.getTimbreCFDICompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getTimbreCFDICompleted(this, new getTimbreCFDICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string cancelaCFDISignature([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string usuarioSIFEI, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string passUser, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)] byte[] archivoZIP) {
        object[] results = this.Invoke("cancelaCFDISignature", new object[] {
                    usuarioSIFEI,
                    passUser,
                    archivoZIP});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegincancelaCFDISignature(string usuarioSIFEI, string passUser, byte[] archivoZIP, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("cancelaCFDISignature", new object[] {
                    usuarioSIFEI,
                    passUser,
                    archivoZIP}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndcancelaCFDISignature(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void cancelaCFDISignatureAsync(string usuarioSIFEI, string passUser, byte[] archivoZIP) {
        this.cancelaCFDISignatureAsync(usuarioSIFEI, passUser, archivoZIP, null);
    }
    
    /// <remarks/>
    public void cancelaCFDISignatureAsync(string usuarioSIFEI, string passUser, byte[] archivoZIP, object userState) {
        if ((this.cancelaCFDISignatureOperationCompleted == null)) {
            this.cancelaCFDISignatureOperationCompleted = new System.Threading.SendOrPostCallback(this.OncancelaCFDISignatureOperationCompleted);
        }
        this.InvokeAsync("cancelaCFDISignature", new object[] {
                    usuarioSIFEI,
                    passUser,
                    archivoZIP}, this.cancelaCFDISignatureOperationCompleted, userState);
    }
    
    private void OncancelaCFDISignatureOperationCompleted(object arg) {
        if ((this.cancelaCFDISignatureCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.cancelaCFDISignatureCompleted(this, new cancelaCFDISignatureCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public bool CambiaPassword([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Usuario, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Password, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string NewPassword) {
        object[] results = this.Invoke("CambiaPassword", new object[] {
                    Usuario,
                    Password,
                    NewPassword});
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginCambiaPassword(string Usuario, string Password, string NewPassword, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("CambiaPassword", new object[] {
                    Usuario,
                    Password,
                    NewPassword}, callback, asyncState);
    }
    
    /// <remarks/>
    public bool EndCambiaPassword(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public void CambiaPasswordAsync(string Usuario, string Password, string NewPassword) {
        this.CambiaPasswordAsync(Usuario, Password, NewPassword, null);
    }
    
    /// <remarks/>
    public void CambiaPasswordAsync(string Usuario, string Password, string NewPassword, object userState) {
        if ((this.CambiaPasswordOperationCompleted == null)) {
            this.CambiaPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCambiaPasswordOperationCompleted);
        }
        this.InvokeAsync("CambiaPassword", new object[] {
                    Usuario,
                    Password,
                    NewPassword}, this.CambiaPasswordOperationCompleted, userState);
    }
    
    private void OnCambiaPasswordOperationCompleted(object arg) {
        if ((this.CambiaPasswordCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.CambiaPasswordCompleted(this, new CambiaPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
    public byte[] getCFDI([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Usuario, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Password, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)] byte[] archivoXMLZip, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Serie, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IdEquipo) {
        object[] results = this.Invoke("getCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo});
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetCFDI(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte[] EndgetCFDI(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public void getCFDIAsync(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo) {
        this.getCFDIAsync(Usuario, Password, archivoXMLZip, Serie, IdEquipo, null);
    }
    
    /// <remarks/>
    public void getCFDIAsync(string Usuario, string Password, byte[] archivoXMLZip, string Serie, string IdEquipo, object userState) {
        if ((this.getCFDIOperationCompleted == null)) {
            this.getCFDIOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetCFDIOperationCompleted);
        }
        this.InvokeAsync("getCFDI", new object[] {
                    Usuario,
                    Password,
                    archivoXMLZip,
                    Serie,
                    IdEquipo}, this.getCFDIOperationCompleted, userState);
    }
    
    private void OngetCFDIOperationCompleted(object arg) {
        if ((this.getCFDICompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getCFDICompleted(this, new getCFDICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string cancelaCFDI([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string usuarioSIFEI, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string passUser, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string rfc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)] byte[] pfx, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string passPFX, [System.Xml.Serialization.XmlElementAttribute("UUIDS", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)] string[] UUIDS) {
        object[] results = this.Invoke("cancelaCFDI", new object[] {
                    usuarioSIFEI,
                    passUser,
                    rfc,
                    pfx,
                    passPFX,
                    UUIDS});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegincancelaCFDI(string usuarioSIFEI, string passUser, string rfc, byte[] pfx, string passPFX, string[] UUIDS, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("cancelaCFDI", new object[] {
                    usuarioSIFEI,
                    passUser,
                    rfc,
                    pfx,
                    passPFX,
                    UUIDS}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndcancelaCFDI(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void cancelaCFDIAsync(string usuarioSIFEI, string passUser, string rfc, byte[] pfx, string passPFX, string[] UUIDS) {
        this.cancelaCFDIAsync(usuarioSIFEI, passUser, rfc, pfx, passPFX, UUIDS, null);
    }
    
    /// <remarks/>
    public void cancelaCFDIAsync(string usuarioSIFEI, string passUser, string rfc, byte[] pfx, string passPFX, string[] UUIDS, object userState) {
        if ((this.cancelaCFDIOperationCompleted == null)) {
            this.cancelaCFDIOperationCompleted = new System.Threading.SendOrPostCallback(this.OncancelaCFDIOperationCompleted);
        }
        this.InvokeAsync("cancelaCFDI", new object[] {
                    usuarioSIFEI,
                    passUser,
                    rfc,
                    pfx,
                    passPFX,
                    UUIDS}, this.cancelaCFDIOperationCompleted, userState);
    }
    
    private void OncancelaCFDIOperationCompleted(object arg) {
        if ((this.cancelaCFDICompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.cancelaCFDICompleted(this, new cancelaCFDICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://MApeados/", ResponseNamespace="http://MApeados/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string getXML([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string rfc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string pass, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string hash) {
        object[] results = this.Invoke("getXML", new object[] {
                    rfc,
                    pass,
                    hash});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetXML(string rfc, string pass, string hash, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getXML", new object[] {
                    rfc,
                    pass,
                    hash}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndgetXML(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void getXMLAsync(string rfc, string pass, string hash) {
        this.getXMLAsync(rfc, pass, hash, null);
    }
    
    /// <remarks/>
    public void getXMLAsync(string rfc, string pass, string hash, object userState) {
        if ((this.getXMLOperationCompleted == null)) {
            this.getXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetXMLOperationCompleted);
        }
        this.InvokeAsync("getXML", new object[] {
                    rfc,
                    pass,
                    hash}, this.getXMLOperationCompleted, userState);
    }
    
    private void OngetXMLOperationCompleted(object arg) {
        if ((this.getXMLCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getXMLCompleted(this, new getXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void getTimbreCFDICompletedEventHandler(object sender, getTimbreCFDICompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getTimbreCFDICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getTimbreCFDICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public byte[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((byte[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void cancelaCFDISignatureCompletedEventHandler(object sender, cancelaCFDISignatureCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class cancelaCFDISignatureCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal cancelaCFDISignatureCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void CambiaPasswordCompletedEventHandler(object sender, CambiaPasswordCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CambiaPasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal CambiaPasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public bool Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void getCFDICompletedEventHandler(object sender, getCFDICompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getCFDICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getCFDICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public byte[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((byte[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void cancelaCFDICompletedEventHandler(object sender, cancelaCFDICompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class cancelaCFDICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal cancelaCFDICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void getXMLCompletedEventHandler(object sender, getXMLCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
