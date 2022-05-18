<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/3'
    xmlns:tfd='http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd'
                >

  <xsl:output method = "html" />
  <xsl:param name="id" select="."/>

  <xsl:template match="//cfdi:Comprobante">
    <html>
      <head>
        <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
        <title>
          Factura Electronica <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/>
        </title>
      </head>

      <fieldset>

        <body>

<legend>
  <font color="red" face="arial" size="6">
    <b>
      CFDi - MUESTRA FISCAL SIN VALOR
    </b>
  </font>
</legend>
          <table border="0" bordercolor="blue">
            <tr>
              <td class="h2" align="center">
                <center>
                  <font color="#0B0B6" face="arial" size="6">
                    <xsl:value-of select="document('cad.xml')/Complemento/TipoComprobante"/>
                  </font>
                </center>
              </td>
            </tr>
          </table>

            <table width="100%">
            <tr>
              <td colspan ="2">
                <img src="encabezado.jpg" weigth="1200" heigth="200"/>
              </td>
            </tr>
            <tr>
              <td width="65%">


                <fieldset>

                  <table width="100%" border="0">
                    <legend>DATOS FISCALES</legend>
                    <!--
                    <tr>
                      <th colspan="2" class="h1">DATOS FISCALES</th>
                    </tr>
-->
                    <tr width="100%">

                      <td>
                         <font color="#0B0B6" face="Courier New" size="3">
                           Serie/Folio:     

                         </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                          <xsl:value-of select="@serie"/>
                          <xsl:value-of select="@folio"/>
                            </b>
                        </font>
                      </td>
                      
                      
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Tasa de Cambio: 
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                            <xsl:value-of select="@TipoCambio"/>
                          </b>


                        </font>
                      </td>
                    </tr>

                    <tr width="100%">

                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Fecha Emisión de Comprobante: 
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3" >
                          <b>
                            <xsl:value-of select="@fecha"/>
                          </b>
                        </font>
                      </td>


                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Motivo Descuento:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                          <xsl:value-of select="@motivoDescuento"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">

                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Tipo de Comprobante:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                         <b> <xsl:value-of select="@tipoDeComprobante"/>
                           </b>
                        </font>
                      </td>


                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Método de Pago:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                            <xsl:value-of select="@metodoDePago"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">

                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Certificado Emisor:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b><xsl:value-of select="@noCertificado"/>
                            </b>
                        </font>
                      </td>


                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Forma de Pago:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                          <xsl:value-of select="@formaDePago"/>
                            </b>
                        </font>
                      </td>
                    </tr>

                    

                    <tr width="100%">

                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Divisa:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b> <xsl:value-of select="@Moneda"/>
                            </b>
                        </font>
                      </td>


                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Condiciones de pago:
                        </font>
                      </td>
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          <b>
                          <xsl:value-of select="@condicionesDePago"/>
                            </b>
                        </font>
                      </td>
                    </tr>
                    
                
                  </table>
                </fieldset>
              </td>            


              
            </tr>
          </table>
          
          
          
          
              <table width="100%">

            <tr>
              <td width="100%" Colspan ="2">


                <fieldset border="0">
                  <legend>
                    LUGAR DE EXPEDICIÓN
                  </legend>

                  <b>
                    <xsl:apply-templates select="//cfdi:ExpedidoEn"/>
                  </b>
                  
                </fieldset>
              </td>
              
            </tr>                
            <tr>
              <td width="50%">


                <fieldset border="0">

                  <table width="100%" border="0">

                    <legend>
                      ORIGEN
                    </legend>

                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          ORIGEN: <b>

                            <xsl:value-of select="document('cad.xml')/Complemento/Origen"/>
                          </b>
                        </font>
                      </td>
                    </tr>


                    <tr width="100%">

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          RFC :
                          <b>
                          <xsl:value-of select="cfdi:Receptor/@rfc"/>
                            </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Nombre :
                          <b>

                          <xsl:value-of select="cfdi:Receptor/@nombre"/>
                            </b>
                        </font>
                      </td>
                    </tr>
                    <font color="#0B0B6" face="Courier New" size="3">
                      <b>
                      <xsl:apply-templates select="//cfdi:Domicilio"/>
                        </b>
                    </font>

                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          SE RECOGERA: <b>
                            <xsl:value-of select="document('cad.xml')/Complemento/RecogerEn"/>
                          </b>
                        </font>
                      </td>
                    </tr>


                  </table>
                </fieldset>
              </td>




              <td width="50%">
                <fieldset>
                  <table width="100%" border="0">
                    <legend>
                      DESTINO
                    </legend>

                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          DESTINO: <b>
                            <xsl:value-of select="document('cad.xml')/Complemento/Destino"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          Destinatario: <b>
                            <xsl:value-of select="document('cad.xml')/Complemento/Destinatario"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    
                    <tr width ="100%">
                      
                      <td width="75%">
                        <font color="#0B0B6" face="Courier New" size="3">
                         RFC: <b>
                           <xsl:value-of select="document('cad.xml')/Complemento/RfcDestinatario"/>
                        </b>
                        </font>
                      </td>
                    </tr>

                    <tr width ="100%">

                      <td width="75%">
                   
                    <font color="#0B0B6" face="Courier New" size="3">
                       Domicilio:
                      <b>
                        <xsl:value-of select="document('cad.xml')/Complemento/DomicilioDestinatario"/> </b>
                    </font>
                      </td>
                    </tr>
                    

                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          SE ENTREGARA EN: <b>
                            <xsl:value-of select="document('cad.xml')/Complemento/EntregarEn"/>
                          </b>
                        </font>
                      </td>
                    </tr>
                    
                  </table>
                </fieldset>
              </td>
            </tr>


          </table>

        </body>
      </fieldset>
    </html>

  </xsl:template>



  
  
  <xsl:template match="//cfdi:DomicilioFiscal">


    
    <tr>
      <td colspan="2">
        <font color="#0B0B6" face="Courier New" size="3">
          Domicilio:
          <b>
          <xsl:value-of select="@calle"/><font color="white" face="Courier New" size="3">,</font>
            
            
          <xsl:value-of select="@noExterior"/><font color="white" face="Courier New" size="3">,</font>
            
          <xsl:value-of select="@noInterior"/><font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@colonia"/><font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@municipio"/><font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@estado"/><font color="white" face="Courier New" size="3">,</font>          
          C.P. <xsl:value-of select="@codigoPostal"/>
          <xsl:value-of select="@pais"/><font color="white" face="Courier New" size="3">,</font> 
        </b>
        </font>

      </td>


    </tr>

    
   
    


  </xsl:template>



  <xsl:template match="//cfdi:ExpedidoEn">



            <xsl:value-of select="@calle"/><font color="white" face="Courier New" size="3">,</font>


            <xsl:value-of select="@noExterior"/><font color="white" face="Courier New" size="3">,</font>

            <xsl:value-of select="@noInterior"/><font color="white" face="Courier New" size="3">,</font>
            <xsl:value-of select="@colonia"/><font color="white" face="Courier New" size="3">,</font>
            <xsl:value-of select="@municipio"/><font color="white" face="Courier New" size="3">,</font>
            <xsl:value-of select="@estado"/><font color="white" face="Courier New" size="3">,</font>            
            C.P. <xsl:value-of select="@codigoPostal"/><font color="white" face="Courier New" size="3">,</font>
    <xsl:value-of select="@pais"/><font color="white" face="Courier New" size="3">,</font>





  </xsl:template>




  <xsl:template match="//cfdi:Domicilio">

    
    <tr>
      <td colspan="2">
        <font color="#0B0B6" face="Courier New" size="3">
    
          Domicilio: 
          <b>
          <xsl:value-of select="@calle"/>
            <font color="white" face="Courier New" size="3">,</font>

          <xsl:value-of select="@noExterior"/>
            <font color="white" face="Courier New" size="3">,</font>

          <xsl:value-of select="@noInterior"/>
            <font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@colonia"/>
            <font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@municipio"/>
            <font color="white" face="Courier New" size="3">,</font>
          <xsl:value-of select="@estado"/>            
            <font color="white" face="Courier New" size="3">,</font>
          C.P. <xsl:value-of select="@codigoPostal"/>
            <font color="white" face="Courier New" size="3">,</font>
            <xsl:value-of select="@pais"/>
        </b>
        </font>
      </td>
    </tr>

 
 
 
  </xsl:template>





</xsl:stylesheet>






