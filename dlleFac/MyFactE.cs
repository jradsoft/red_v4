using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Ionic.Zip;
using System.Web.Services.Protocols;


namespace dlleFac
{
    

    public class MyFactE
    {

        string strAddenda;
        bool isAddenda;

        public Factura createXML30(int intTipoComprobante, string strFolio, string RFC, string fileCer, string fileKey, string passwd, string path, string path2,
            dlleFac.Comprobante cfd, 
            string strEncabezado, string strObservaciones, string strImpuestosAdicionales,  
            string strTipoComprobante, string templateReportHeader, string templateReport, bool CFDAprobado,
            
         string origen,
         string recogerEn, 
        
         string destino,
         string destinatario,
         string rfcDestinatario,
         string domicilioDestinatario,
         string entregarEn,
         bool isAddenda,
         string strUs,
         string strValue,
         string idEquipo

            )
        {

            this.isAddenda = isAddenda;

            string fechaActual = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') +
                "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

            
            string strFileCER = fileCer;
            string strFileKEY = fileKey;
            string strCadenaOriginal = "";
            string strFileCERPEM = fileCer + ".PEM";
            string strFileKEYPEM = fileKey + ".PEM";
            
            string strPasswd = passwd;
            string myUUID="";
            int value = 0;
            Complemento myComp = new Complemento();
            

            string fileName = "adesoft.xml";




            string fileNameXMLtemp = "";

            string fileNameOK; //= path + "\\" + "XML_" + cfd.folio + "_" + RFC + "_" + fechaActual + "_OK.xml";
            string fileNamepdfOK; //= path + "\\" + "PDF_" + cfd.folio + "_" + RFC + "_" + fechaActual + "_OK.pdf";
            //string fileNameHTMLOK = "";

            if (CFDAprobado)
            {
                //fileNameHTMLOK = path + "\\" + "HTML_" + cfd.folio + "_" + RFC + "_" + fechaActual + "_OK.html";
                fileNameXMLtemp = "adesoft.xml";
                //fileNameOK = path + "\\" + "XML_" + cfd.folio + "_" + RFC + "_" + fechaActual + "_OK.xml";
                fileNameOK = "adsoftOK.xml";
                fileNamepdfOK = path + "\\" + strTipoComprobante.Replace(' ', '_') +  "_" + cfd.Folio + "_" + RFC + "_" + fechaActual + "_OK.pdf";

            }
            else
            {

                fileNameXMLtemp = "adesoft.xml";
                fileNameOK = "adsoftOK.xml";

                fileNamepdfOK = path2 + "\\" + strTipoComprobante.Replace(' ', '_') + "_" + cfd.Folio + "_" + RFC + "_" + fechaActual + "_OK.pdf";
            }

            string strNoCertificado;
            strNoCertificado = getCertificado(strFileCER, strPasswd);

            ExecuteCommandSync("openssl enc -base64 -in " + strFileCER + " -out certificado.txt");

            string strCertificado;
            strCertificado = System.IO.File.ReadAllText("certificado.txt", UTF8Encoding.UTF8);
            strCertificado = strCertificado.Replace("\n", "");
            cfd.Certificado = strCertificado;
            cfd.NoCertificado = strNoCertificado;


            generateXMLCFD20(fileNameXMLtemp, cfd, intTipoComprobante);


            ExecuteCommandSync("openssl pkcs8 -inform DER -in " + strFileKEY + "  -out " + strFileKEYPEM + " -passin pass:" + strPasswd);


            ExecuteCommandSync("xsltproc deleteNull.xslt " + fileNameXMLtemp + " > " + fileNameOK);
            //ExecuteCommandSync("xsltproc cadenaoriginal_3_0.xslt " + fileNameOK + " > cadena.txt");
            //ExecuteCommandSync("xsltproc cadenaoriginal_3_0.xslt " + fileNameOK + " | openssl dgst -sha1 -sign " + strFileKEYPEM + " | openssl enc -base64 -A > sello.txt");
            ExecuteCommandSync("xsltproc cadenaoriginal40.xslt " + fileNameOK + " > cadena.txt");
            ExecuteCommandSync("xsltproc cadenaoriginal40.xslt " + fileNameOK + " | openssl dgst -sha256 -sign " + strFileKEYPEM + " | openssl enc -base64 -A > sello.txt");

            strCadenaOriginal = System.IO.File.ReadAllText("cadena.txt", UTF8Encoding.UTF8);


            string strSello; 
            strSello = System.IO.File.ReadAllText("sello.txt", UTF8Encoding.UTF8);
            cfd.Sello = strSello;

            generateXMLCFD20(fileNameXMLtemp, cfd, intTipoComprobante);

            //if (CFDAprobado) 
                
            //else
             //   cfd.sello = "MuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValorMuestraFiscalSinValor";
            //cfd.sello = "ijj/6sdCwrCbsxBOoL07muE32MgO+FlMpGMs0fc9piT0YkiV7I8U0MI9sSjmoYyoi+BCwC+EEiTDaUGv3ygujsGl3AKnuz+uCfWicgaTZuBn8CuZhkc58ktdHYKCCtAzFJ3HYlST5sMrqdd8jvlCFD47Buas/aSmACUesJzul5Y=";

            if (this.isAddenda)
            {
                strAddenda = System.IO.File.ReadAllText(fileNameXMLtemp, UTF8Encoding.UTF8);
                int a = strAddenda.IndexOf("<cfdi:Addenda>");
                int b = strAddenda.IndexOf("</cfdi:Comprobante>");
                strAddenda = strAddenda.Substring(a, b - a);

                if (cfd.Receptor.Rfc == "SVE770906NS5")
                {
                    strAddenda = strAddenda.Replace("cfdi:", "");
                    strAddenda = strAddenda.Replace("Addenda", "cfdi:Addenda");
                    
                }
            }

            ExecuteCommandSync("xsltproc deleteNull.xslt " + fileNameXMLtemp + " > " + fileNameOK);
            updateEncoding(fileNameOK);
            AppendAttribute(fileNameOK, intTipoComprobante);

            Boolean sucess;
            
            sucess = true;
            
            /*******************************************************/

            if ((intTipoComprobante == 1) || (intTipoComprobante == 2) || (intTipoComprobante == 3) || (intTipoComprobante == 4) || (intTipoComprobante == 5) || (intTipoComprobante == 6) || (intTipoComprobante == 7))
            {


                if (CFDAprobado)
                {

                    ExecuteCommandSync("zip adesoftCfdi.zip -o -j " + fileNameOK);
                    // ExecuteCommandSync("c:\\myfacturae\\zip -o c:\\myfacturae\\adesoftCfdi.zip adsoftOK.xml"); 
                    byte[] rawData = File.ReadAllBytes("adesoftCfdi.zip");
                  // SIFEIService myService = new SIFEIService();
                   ServicioTimbradoWS myService = new ServicioTimbradoWS();
                   
                    //CFDiService myService = new CFDiService(); //Edicomn
                    //byte[] MyCfdiTimbrado = null; //sifei
                   //string MyCfdiTimbrado = "";
                    byte[] MyTFD = null;

                    RespuestaTimbrado MyCfdiTimbrado = null; //sifei

                    // Load the xml file into XmlDocument object.
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fileNameOK);

                    // Now create StringWriter object to get data from xml document.
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    xmlDoc.WriteTo(xw);
                    String XmlString = sw.ToString();

                    
                    try
                    {
                        ExecuteCommandSync("delete timbreCFDi.xml");
                        

                       
                        //MyCfdiTimbrado = myService.timbrar(idEquipo, XmlString);   /* Produccion*/
                        MyCfdiTimbrado = myService.timbrar("0955d485e26c486392909ee79f5ad5c3", XmlString);  //test


                        // valor para deserializar complemento
                        if (intTipoComprobante == 6)
                        {
                            value = 1;
                        }

                        if (MyCfdiTimbrado.data.Contains("<cartaporte20:CartaPorte")) {
                            value = 1;
                        }

                        if (MyCfdiTimbrado.data.Contains("<servicioparcial:parcialesconstruccion"))
                        {
                            value = 1;
                        }

                        

                       // MyCfdiTimbrado = myService.getCFDI(strUs, strValue, rawData, "", idEquipo);




                        //Stream stream = new MemoryStream(MyCfdiTimbrado);
                        //using (ZipFile zip = ZipFile.Read(/*"adesoftCfdiTimbrado.zip")*/ stream))
                        //{
                        //    MemoryStream mystream = new MemoryStream();
                        //    foreach (ZipEntry e in zip)
                        //    {
                        //        e.Extract(mystream);
                        //        System.IO.File.WriteAllBytes("SIGN_adsoftOK.xml", mystream.ToArray());
                        //    }
                        //}

                       



                      
                        try
                        {
                            if (MyCfdiTimbrado.code == "200")
                            {

                                System.IO.File.WriteAllText("SIGN_adsoftOK.xml", MyCfdiTimbrado.data);

                                fileNameOK = "SIGN_adsoftOK.xml";




                                Comprobante myComprobante = DeserializeCFD32(fileNameOK);

                                ComprobanteComplemento myComplemento = myComprobante.Complemento;// revisar

                                XmlAttributeCollection myTFD = myComplemento.Any[value].Attributes;




                                myUUID = myTFD.GetNamedItem("UUID").Value; //myTFD[2].Value;
                                string FechaTimbrado = myTFD.GetNamedItem("FechaTimbrado").Value;//myTFD[1].Value; //myTokenIzer[4];
                                string SelloCFD = myTFD.GetNamedItem("SelloCFD").Value; //myTFD[4].Value;//myTokenIzer[7];
                                string NoCertificadoSAT = myTFD.GetNamedItem("NoCertificadoSAT").Value;//myTFD[3].Value;// myTokenIzer[10];
                                string SelloSAT = myTFD.GetNamedItem("SelloSAT").Value;//myTFD[5].Value;//myTokenIzer[13];

                                string strCadenaOriginalTFD = getCadenaTFD(myTFD);


                                myComp.UUID = myUUID.Trim();
                                myComp.FechaTimbrado = FechaTimbrado;
                                myComp.NoCertificadoSAT = NoCertificadoSAT;
                                myComp.SelloCFD = SelloCFD.Substring(0, SelloCFD.Length / 2) + "\n" + SelloCFD.Substring(SelloCFD.Length / 2, SelloCFD.Length / 2);
                                myComp.SelloSAT = SelloSAT.Substring(0, SelloSAT.Length / 2) + "\n" + SelloSAT.Substring(SelloSAT.Length / 2, SelloSAT.Length / 2);


                                myComp.CadenaTFD = strCadenaOriginalTFD.Substring(0, strCadenaOriginalTFD.Length / 2) + "\n" +
                                strCadenaOriginalTFD.Substring(strCadenaOriginalTFD.Length / 2, strCadenaOriginalTFD.Length / 2);

                            }
                            else
                            {
                                throw new Exception(MyCfdiTimbrado.message);

                            }
                        }
                        catch (Exception e)
                        {


                            sucess = false;
                            throw new Exception(e.Message);
                        }


                        //    }


                    }
                    catch (SoapException ex)
                    {
                        sucess = false;
                        throw new Exception(ex.Detail.InnerText);
                    }

                }
                else
                {

                    myComp.UUID = "00000000-0000-0000-0000-000000000000";
                    myComp.FechaTimbrado = "1900-01-01T00:00:00";
                    myComp.NoCertificadoSAT = "000000000000000";
                    myComp.SelloCFD = "muestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalor";
                    myComp.SelloSAT = "muestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalor";
                    myComp.CadenaTFD = "muestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalor" + "\n" +
                    "muestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalormuestrafiscalsinvalor";


                    //MyCfdiTimbrado = myService.getCfdiTest("ADE1004192V6", "smkoecfpw", rawData);
                    //MyTFD = myService.getTimbreCfdiTest("ADE1004192V6", "smkoecfpw", rawData);


                }
               

            }


            /*******************************************************/

            Factura myf;

            if (sucess)
            {

                myComp.TipoComprobante = strTipoComprobante;
                myComp.Cadena = strCadenaOriginal;
                if (cfd.Moneda.Equals("MXN"))
                {
                    myComp.CantidadLetra = ConvertidorNumeroLetra.NumeroALetras(cfd.Total.ToString(), "PESOS");
                }
                if (cfd.Moneda.Equals("USD"))
                {
                    myComp.CantidadLetra = ConvertidorNumeroLetra.NumeroALetras(cfd.Total.ToString(), "DOLARES");
                }
                myComp.Encabezado = strEncabezado;
                myComp.Observaciones = strObservaciones;
                myComp.ImpuestosAdicionales = strImpuestosAdicionales;

                myComp.Origen = origen;
                myComp.RecogerEn = recogerEn;

                myComp.Destino = destino;
                myComp.Destinatario = destinatario;
                myComp.RfcDestinatario = rfcDestinatario;
                myComp.DomicilioDestinatario = domicilioDestinatario;
                myComp.EntregarEn = entregarEn;

                generateXMLCFD20Complemento(myComp);



                myf = new Factura()
                {
                    sello = strSello,
                    certificado = strCertificado,
                    noCertificado = strNoCertificado,
                    xml = generateXMLCFD20(fileNameXMLtemp, cfd, intTipoComprobante),
                    filePath = fileNameOK,
                    cadenaOriginal = strCadenaOriginal,
                    fileXMLpath = fileNameOK.Replace("&",""),
                    filePDFpath = fileNamepdfOK.Replace("&", ""),
                    serie = cfd.Serie,
                    folio = cfd.Folio,
                    fechaAprobacion = cfd.Fecha,
                    UUID = myUUID.Trim()
                };

            }
            else
                myf = null;
            
            return (myf);
            
            
        }
        public string getCadenaTFD(XmlAttributeCollection myTFD)
        {
            string myCadenaTFD;

            //   myUUID + "|" +
            //                        FechaTimbrado + "|" +
            //                        SelloCFD + "|" +
            //                        NoCertificadoSAT +
            //             


            myCadenaTFD = "||1.0|" +
                                               myTFD.GetNamedItem("UUID").Value + "|" +
                                               myTFD.GetNamedItem("FechaTimbrado").Value + "|" +
                                               myTFD.GetNamedItem("SelloCFD").Value + "|" +
                                               myTFD.GetNamedItem("NoCertificadoSAT").Value +
                                               "||";
            return myCadenaTFD;
        }
        public Comprobante DeserializeCFD32(string xmlFile)
        {


            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(Comprobante));

            // A FileStream is needed to read the XML document.

            FileStream fs = new FileStream(xmlFile, FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);

            // Declare an object variable of the type to be deserialized.
            Comprobante myComprobante = null;

            // Use the Deserialize method to restore the object's state.
            try
            {

                myComprobante = (Comprobante)serializer.Deserialize(reader);

            }
            catch (Exception e)
            {

            }
            fs.Close();
            reader.Close();

            return myComprobante;

        }


        private void updateEncoding(string xmlFile)
        {


            if (File.Exists(xmlFile))
            {
                try
                {
                    string strXML;

                    Char comilla = '"';
                    strXML = System.IO.File.ReadAllText(xmlFile, UTF8Encoding.UTF8);

                    strXML = strXML.Replace("?>", " encoding=" + comilla + "utf-8" + comilla + "?>");


                    System.IO.File.WriteAllText(xmlFile, strXML, UTF8Encoding.UTF8);

                }



                catch (Exception e)
                {
                }
            }
        }
        private void AppendAttribute(string xmlFile, int intTipoComprobante)
        {

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(xmlFile))
            {

                xmlDoc.Load(xmlFile);

                //XmlNodeList list = xmlDoc.GetElementsByTagName("cfdi:Comprobante");

                int i = 0;
                try
                {
                    XmlAttribute newAttr = xmlDoc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
                    if ((intTipoComprobante == 1) || (intTipoComprobante == 2) || (intTipoComprobante == 3) || (intTipoComprobante == 4) || (intTipoComprobante == 5) || (intTipoComprobante == 7))
                    {

                        newAttr.Value = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd http://www.sat.gob.mx/servicioparcialconstruccion http://www.sat.gob.mx/sitio_internet/cfd/servicioparcialconstruccion/servicioparcialconstruccion.xsd";
                    }

                    if (intTipoComprobante == 6)
                    {

                        newAttr.Value = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd ";
                    }

                    //if (intTipoComprobante == 7)
                    //{

                    //    newAttr.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd";
                    //}
             

                    XmlAttributeCollection attrColl = xmlDoc.DocumentElement.Attributes;
                    attrColl.InsertBefore(newAttr, attrColl[0]);
                    //attrColl.InsertBefore(newAttr_nom12, attrColl[0]);

                    xmlDoc.Save(xmlFile);
                }
                catch (Exception e)
                {
                }
            }

        }


        public string createPDF30(int intTipoComprobante,string strFolio, string strTipoComprobante, string path, string path2, string emisor, string receptor, string templateEncabezado, string templateCuerpo, bool CFDAprobado)
        {
            //string fileNameXMLtemp="";
            string fechaActual = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') +
             "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

            
            string fileNameOK = "";
            string fileNamepdfOK = "";
            string fileNameXmlOK = "";

            if (CFDAprobado)
            {
                
              //  fileNameXMLtemp = "adesoft.xml";
                
                fileNameOK = "SIGN_adsoftOK.xml";
                fileNameXmlOK = path + "\\CFDi_" + strFolio + "_" + strTipoComprobante.Replace(' ', '_') + "_" + emisor + "_" + receptor + "_" + fechaActual + ".xml";
                fileNameXmlOK = fileNameXmlOK.Replace("&", "");
                ExecuteCommandSync("copy SIGN_adsoftOK.xml " + fileNameXmlOK);
                
                ExecuteCommandSync("copy timbreCFDi.xml " + path + "\\TIMBRE_" + strTipoComprobante.Replace(' ', '_') + "_" + emisor + "_" + receptor + "_" + fechaActual + ".xml");

                fileNamepdfOK = path + "\\CFDi_" + strFolio + "_" + strTipoComprobante.Replace(' ', '_') + "_" + emisor + "_" + receptor + "_" + fechaActual + ".pdf";

            }
            else
            {

               // fileNameXMLtemp = "adesoft.xml";
                fileNameOK = "adsoftOK.xml";

                fileNamepdfOK = path2 + "\\" + strTipoComprobante.Replace(' ', '_') + "TEST_"  + emisor + "_" + receptor + "_" + "_" + fechaActual + ".pdf";
                
            }

            fileNamepdfOK = fileNamepdfOK.Replace("&", "");

            ExecuteCommandSync("xsltproc " + templateEncabezado + " " + fileNameOK + " > header.html");
            ExecuteCommandSync("xsltproc " + templateCuerpo + " " + fileNameOK + " > conceptos.html");
           // if ( (intTipoComprobante == 7))
           //     ExecuteCommandSync("wkhtmltopdf.exe --footer-html footerCR.html --footer-spacing 2 --margin-bottom 50mm --header-html  header.html --header-spacing 2 --margin-top 100mm conceptos.html " + fileNamepdfOK);
           // else
                //ExecuteCommandSync("wkhtmltopdf.exe --footer-html footer.html --footer-spacing 2 --margin-bottom 50mm --header-html  header.html --header-spacing 2 --margin-top 95mm conceptos.html " + fileNamepdfOK);
            ExecuteCommandSync("wkhtmltopdf.exe --footer-html footer.html --footer-spacing 2 --margin-bottom 50mm --margin-left 0mm --margin-right 0mm --header-html  header.html --header-spacing 2 --margin-top 95mm conceptos.html " + fileNamepdfOK);

            return fileNameXmlOK.Replace("&", "") + "|" + fileNamepdfOK.Replace("&", "");
        }
       
        /*
        public string createPDF30(string filenameOK, string filenamepdfOK, string templateReportHeader, string templateReport, bool CFDAprobado)
        {
            

            ExecuteCommandSync("xsltproc " + templateReportHeader + " " + filenameOK + " > header.html");
            ExecuteCommandSync("xsltproc " + templateReport + " " + filenameOK + " > conceptos.html");
            
            ExecuteCommandSync("wkhtmltopdf.exe --footer-html footer.html --footer-spacing 2 --margin-bottom 30mm --header-html  header.html --header-spacing 2 --margin-top 100mm conceptos.html " + filenamepdfOK);
            
            return "";
        }
        */


        public XmlTextWriter generateXMLCFD20Complemento(Complemento myComp)
        {

            //  XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();



            XmlTextWriter xmlTextWriter = new XmlTextWriter("cad.xml", Encoding.UTF8);

            xmlTextWriter.Formatting = Formatting.Indented;
            XmlSerializer xs = new XmlSerializer(typeof(Complemento));

            xs.Serialize(xmlTextWriter, myComp, null);
            xmlTextWriter.Close();
            return xmlTextWriter;

        }
        public string getCertificado(string strFileCER, string strPasswd)
        {
            /*
             * Obtencion del CERTIFICADO
             */

            X509Certificate2 objCert = new X509Certificate2(strFileCER, strPasswd);
            StringBuilder objSB = new StringBuilder("Detalle del certificado: \n\n");
            /*
            objSB.AppendLine("Persona = " + objCert.Subject);
            objSB.AppendLine("Emisor = " + objCert.Issuer);
            objSB.AppendLine("Válido desde = " + objCert.NotBefore.ToString());
            objSB.AppendLine("Válido hasta = " + objCert.NotAfter.ToString());
            objSB.AppendLine("Tamaño de la clave = " + objCert.PublicKey.Key.KeySize.ToString());*/
            objSB.AppendLine("Número de serie = " + objCert.SerialNumber);
            //objSB.AppendLine("Hash = " + objCert.Thumbprint);

            string DatoHex = objCert.SerialNumber;
            string Data1 = "";
            string Resultado = "";

            while (DatoHex.Length > 0)
            {
                Data1 = System.Convert.ToChar(System.Convert.ToUInt32(DatoHex.Substring(0, 2), 16)).ToString();
                Resultado = (Resultado + Data1);
                DatoHex = DatoHex.Substring(2, DatoHex.Length - 2);
            }

           
            return (Resultado);
        }




        public string GetSello() 
        {
            return null;
        }

        /*
        public XmlTextWriter GetXML(string certificado, string sello, string noCertificado,string fileName, ComprobanteFiscalDigital cfd) 
        {
            //return generateXML(sello, certificado, noCertificado, fileName, cfd);
            //return generateXML( fileName, cfd);
        }
        */



        public XmlTextWriter generateXMLCFD20(string fileName, dlleFac.Comprobante cfd, int idComprobante) 
        {
            XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8);

            try
            { 
            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            
            
            
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            xmlNameSpace.Add("cartaporte20", "http://www.sat.gob.mx/CartaPorte20");
            xmlNameSpace.Add("servicioparcial", "http://www.sat.gob.mx/servicioparcialconstruccion");


            if (idComprobante == 6)
            {
                xmlNameSpace.Add("pago20", "http://www.sat.gob.mx/Pagos20");
            }

            

            
            xmlTextWriter.Formatting = Formatting.Indented;
            XmlSerializer xs = new XmlSerializer(typeof(dlleFac.Comprobante));
            
            xs.Serialize(xmlTextWriter, cfd, xmlNameSpace);
            xmlTextWriter.Close();
            }
            catch(Exception e)
            {

            }
            return xmlTextWriter;
            
        }



        private string ExecuteCommandSync(object command)
        {
            string result = "";

            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                //MessageBox.Show(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }

            return result;
        }



        //private XmlTextWriter generateXML(string fileName, ComprobanteFiscalDigital cfd)
        //{
        //    if (cfd != null)
        //    {
        //        //strCadenaOriginal = strCadenaOriginal.Substring(2, strCadenaOriginal.Length - 4);
        //        //Array arrCO = strCadenaOriginal.Split('|');
        //        //int n = arrCO.Length;
        //        XmlTextWriter w = new XmlTextWriter(fileName, Encoding.UTF8);

        //        w.WriteStartDocument();

        //        w.WriteStartElement("Comprobante");

        //        foreach (var item in cfd.nameSpaces)
        //        {
        //            w.WriteStartAttribute("xmlns");
        //            w.WriteValue(item);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteStartAttribute("xmlns:xsi");
        //        w.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
        //        w.WriteEndAttribute();

        //        string location = string.Empty;

        //        foreach (var item in cfd.schemaLocation)
        //        {
        //            location += item;
        //        }

        //        w.WriteStartAttribute("xsi:noNamespaceSchemaLocation");
        //        w.WriteValue("cfdv2.xsd");
        //        w.WriteEndAttribute();    

        //        w.WriteStartAttribute("version");
        //        w.WriteValue(cfd.Version);
        //        w.WriteEndAttribute();
        //        if (!string.IsNullOrWhiteSpace(cfd.Serie))
        //        {
        //            w.WriteStartAttribute("serie");
        //            w.WriteValue(cfd.Serie);
        //            w.WriteEndAttribute();
        //        }
        //        w.WriteStartAttribute("folio");
        //        w.WriteValue(cfd.Folio);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("fecha");
        //        w.WriteValue(cfd.Fecha);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("noAprobacion");
        //        w.WriteValue(cfd.NoAprobacion);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("anoAprobacion");
        //        w.WriteValue(cfd.AñoAprobacion);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("tipoDeComprobante");
        //        w.WriteValue(cfd.TipoComprobante);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("formaDePago");
        //        w.WriteValue(cfd.FormaPago);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("subTotal");
        //        w.WriteValue(decimal.Parse(cfd.SubTotal));
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("descuento");
        //        //w.WriteValue(string.IsNullOrEmpty(cfd.Descuento)?"0":cfd.Descuento);
        //        w.WriteValue(decimal.Parse(cfd.Descuento));
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("total");
        //        w.WriteValue(decimal.Parse(cfd.Total));
        //        w.WriteEndAttribute();
        //        /*
        //        w.WriteStartAttribute("certificado");
        //        w.WriteValue(certificado);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("noCertificado");
        //        w.WriteValue(noCertificado);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("sello");
        //        w.WriteValue(sello);
        //        w.WriteEndAttribute();
        //        */
        //        /*
        //         * Emisor
        //         */

        //        w.WriteStartElement("Emisor");

        //        w.WriteStartAttribute("rfc");
        //        w.WriteValue(cfd.Emisor.RFCEmisor);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("nombre");
        //        w.WriteValue(cfd.Emisor.NombreEmisor);
        //        w.WriteEndAttribute();

        //        w.WriteStartElement("DomicilioFiscal");

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Calle))
        //        {
        //            w.WriteStartAttribute("calle");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Calle);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.NumeroExterior))
        //        {
        //            w.WriteStartAttribute("noExterior");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.NumeroExterior);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.NumeroInterior))
        //        {
        //            w.WriteStartAttribute("noInterior");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.NumeroInterior);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Colonia))
        //        {
        //            w.WriteStartAttribute("colonia");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Colonia);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Localidad))
        //        {
        //            w.WriteStartAttribute("localidad");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Localidad);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Municipio))
        //        {
        //            w.WriteStartAttribute("municipio");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Municipio);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Estado))
        //        {
        //            w.WriteStartAttribute("estado");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Estado);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteStartAttribute("pais");
        //        w.WriteValue(cfd.DomicilioFiscalEmisor.Pais);
        //        w.WriteEndAttribute();

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.CodigoPostal))
        //        {
        //            w.WriteStartAttribute("codigoPostal");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.CodigoPostal);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteEndElement();

        //        w.WriteStartElement("ExpedidoEn");

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Calle))
        //        {
        //            w.WriteStartAttribute("calle");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Calle);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.NumeroExterior))
        //        {
        //            w.WriteStartAttribute("noExterior");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.NumeroExterior);
        //            w.WriteEndAttribute();
        //        }

        //        //if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.NumeroInterior))
        //        //{
        //            w.WriteStartAttribute("noInterior");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.NumeroInterior);
        //            w.WriteEndAttribute();
        //        //}

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Colonia))
        //        {
        //            w.WriteStartAttribute("colonia");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Colonia);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Localidad))
        //        {
        //            w.WriteStartAttribute("localidad");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Localidad);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Municipio))
        //        {
        //            w.WriteStartAttribute("municipio");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Municipio);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.Estado))
        //        {
        //            w.WriteStartAttribute("estado");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.Estado);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteStartAttribute("pais");
        //        w.WriteValue(cfd.DomicilioFiscalEmisor.Pais);
        //        w.WriteEndAttribute();

        //        if (!string.IsNullOrWhiteSpace(cfd.DomicilioFiscalEmisor.CodigoPostal))
        //        {
        //            w.WriteStartAttribute("codigoPostal");
        //            w.WriteValue(cfd.DomicilioFiscalEmisor.CodigoPostal);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteEndElement();


        //        w.WriteEndElement();


        //        w.WriteStartElement("Receptor");

        //        w.WriteStartAttribute("rfc");
        //        w.WriteValue(cfd.Receptor.RFCReceptor);
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("nombre");
        //        w.WriteValue(cfd.Receptor.NombreReceptor);
        //        w.WriteEndAttribute();

        //        w.WriteStartElement("Domicilio");

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.Calle))
        //        {
        //            w.WriteStartAttribute("calle");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.Calle);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.NumeroExterior))
        //        {
        //            w.WriteStartAttribute("noExterior");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.NumeroExterior);
        //            w.WriteEndAttribute();
        //        }

        //        //if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.NumeroInterior))
        //        //{
        //            w.WriteStartAttribute("noInterior");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.NumeroInterior);
        //            w.WriteEndAttribute();
        //        //}

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.Colonia))
        //        {
        //            w.WriteStartAttribute("colonia");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.Colonia);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.Localidad))
        //        {
        //            w.WriteStartAttribute("localidad");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.Localidad);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.Municipio))
        //        {
        //            w.WriteStartAttribute("municipio");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.Municipio);
        //            w.WriteEndAttribute();
        //        }

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.Estado))
        //        {
        //            w.WriteStartAttribute("estado");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.Estado);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteStartAttribute("pais");
        //        w.WriteValue(cfd.DomicilioFiscalReceptor.Pais);
        //        w.WriteEndAttribute();

        //        if (!string.IsNullOrEmpty(cfd.DomicilioFiscalReceptor.CodigoPostal))
        //        {
        //            w.WriteStartAttribute("codigoPostal");
        //            w.WriteValue(cfd.DomicilioFiscalReceptor.CodigoPostal);
        //            w.WriteEndAttribute();
        //        }

        //        w.WriteEndElement();

        //        w.WriteEndElement();

        //        /*
        //         * Conceptos de la factura
        //         */
        //        w.WriteStartElement("Conceptos");
        //        //int x = 42;
        //        //string idConcepto;

        //        //idConcepto = arrCO.GetValue(x).ToString();

        //            foreach (var item in cfd.Conceptos)
        //            {
        //                w.WriteStartElement("Concepto");
        //                    w.WriteStartAttribute("cantidad");
        //                    w.WriteValue(decimal.Parse(item.Cantidad));
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("unidad");
        //                    w.WriteValue(item.UnidadMedida);
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("descripcion");
        //                    w.WriteValue(item.Descripcion);
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("valorUnitario");
        //                    w.WriteValue(decimal.Parse(item.ValorUnitario));
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("importe");
        //                    w.WriteValue(decimal.Parse(item.Importe));
        //                    w.WriteEndAttribute();
        //                w.WriteEndElement();
        //            }
        //        w.WriteEndElement();
        //        //while (idConcepto != "IVA")
        //        //{
        //        //    w.WriteStartElement("Concepto");

        //        //    w.WriteStartAttribute("cantidad");
        //        //    w.WriteValue(arrCO.GetValue(x));
        //        //    w.WriteEndAttribute();

        //        //    w.WriteStartAttribute("unidad");
        //        //    w.WriteValue(arrCO.GetValue(x + 1));
        //        //    w.WriteEndAttribute();

        //        //    w.WriteStartAttribute("descripcion");
        //        //    w.WriteValue(arrCO.GetValue(x + 2));
        //        //    w.WriteEndAttribute();

        //        //    w.WriteStartAttribute("valorUnitario");
        //        //    w.WriteValue(arrCO.GetValue(x + 3));
        //        //    w.WriteEndAttribute();

        //        //    w.WriteStartAttribute("importe");
        //        //    w.WriteValue(arrCO.GetValue(x + 4));
        //        //    w.WriteEndAttribute();
        //        //    x += 5;
        //        //    idConcepto = arrCO.GetValue(x).ToString();
        //        //    w.WriteEndElement();
        //        //}
        //        //w.WriteEndElement();

        //        w.WriteStartElement("Impuestos");

        //        w.WriteStartAttribute("totalImpuestosTrasladados");
        //        w.WriteValue(decimal.Parse(cfd.Impuestos.TotalTraslados));
        //        w.WriteEndAttribute();

        //        w.WriteStartAttribute("totalImpuestosRetenidos");
        //        w.WriteValue(decimal.Parse(cfd.Impuestos.TotalTraslados));
        //        w.WriteEndAttribute();

        //        if (cfd.Impuestos.Traslados != null)
        //        {
        //            //w.WriteStartElement("totalImpuestosTrasladados");
        //            //w.WriteValue(decimal.Parse(cfd.Impuestos.TotalTraslados));
        //            //w.WriteEndElement();

        //            w.WriteStartElement("Traslados");
                    
        //            foreach (var item in cfd.Impuestos.Traslados)
        //            {
        //                 w.WriteStartElement("Traslado");
                        
        //                    w.WriteStartAttribute("impuesto");
        //                    w.WriteValue(item.TipoImpuesto);
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("tasa");
        //                    w.WriteValue(decimal.Parse(item.Tasa));
        //                    w.WriteEndAttribute();

        //                    w.WriteStartAttribute("importe");
        //                    w.WriteValue(decimal.Parse(item.Importe));
        //                    w.WriteEndAttribute();
                        
        //                w.WriteEndElement();
        //            }
                         
        //            w.WriteEndElement();
        //        }
               
        //        if (cfd.Impuestos.Retenciones != null)
        //        {
        //            //w.WriteStartElement("totalImpuestosRetenidos");
        //            //w.WriteValue(decimal.Parse(cfd.Impuestos.TotalTraslados));
        //            //w.WriteEndElement();

        //            w.WriteStartElement("Retenciones");
                    
        //            foreach (var item in cfd.Impuestos.Retenciones)
        //            {
        //                w.WriteStartElement("Retencion");
                    
        //                w.WriteStartAttribute("impuesto"); // ISR IVA
        //                w.WriteValue(item.TipoImpuesto);
        //                w.WriteEndAttribute();
                        
                       
        //                w.WriteStartAttribute("importe");
        //                w.WriteValue(decimal.Parse(item.Importe));
        //                w.WriteEndAttribute();
                        
        //                w.WriteEndElement();
        //            }

        //            w.WriteEndElement();

        //        }
                
        //        w.WriteEndElement();

        //        w.WriteEndElement();

        //        w.WriteEndDocument();


        //        w.Flush();
        //        w.Close();

        //        return (w);
        //    }

        //    return null;

        //}

        


    }
}
