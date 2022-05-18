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

          <legend>COMPROBANTE SIN VALOR FISCAL</legend>
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
                <img src="encabezado.jpg"/>
              </td>
            </tr>
            <tr>
              <td width="65%">


                
              </td>




              <td width="35%">
                


              </td>
            </tr>
          </table>

          <table width="100%">

            <tr>
              <td width="50%">


                <fieldset border="0">

                  <table width="100%" border="0">

                    <legend>
                      EMISOR
                    </legend>

                    <tr width="100%">

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          RFC :
                          <b>
                            <xsl:value-of select="cfdi:Emisor/@rfc"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">
                      <td>
                        <font color="#0B0B6" face="Courier New" size="3">
                          Nombre :
                          <b>

                            <xsl:value-of select="cfdi:Emisor/@nombre"/>
                          </b>
                        </font>
                      </td>
                    </tr>
                    <font color="#0B0B6" face="Courier New" size="3">
                      <b>
                        <xsl:apply-templates select="//cfdi:DomicilioFiscal"/>
                      </b>
                    </font>

                  </table>
                </fieldset>
              </td>




              <td width="50%">
                <fieldset>
                  <table width="100%" border="0">
                    <legend>
                      RECEPTOR
                    </legend>

                    <tr width ="100%">

                      <td width="75%">
                        <font color="#0B0B6" face="Courier New" size="3">
                          RFC: <b>
                            <xsl:value-of select="cfdi:Receptor/@rfc"/>
                          </b>
                        </font>
                      </td>
                    </tr>


                    <tr>

                      <td>

                        <font color="#0B0B6" face="Courier New" size="3">
                          Nombre: <b>
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
            <xsl:value-of select="@pais"/><font color="white" face="Courier New" size="3">,</font>
            C.P. <xsl:value-of select="@codigoPostal"/>
          </b>
        </font>

      </td>


    </tr>






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
            <xsl:value-of select="@pais"/>
            <font color="white" face="Courier New" size="3">,</font>
            <xsl:value-of select="@codigoPostal"/>
          </b>
        </font>
      </td>
    </tr>




  </xsl:template>





</xsl:stylesheet>






