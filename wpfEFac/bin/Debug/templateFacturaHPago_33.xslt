<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/4'
    xmlns:tfd='http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd'
                >

  <xsl:output method = "html" />
  <xsl:param name="id" select="."/>

  <xsl:template match="//cfdi:Comprobante">
    <html>
      <head>
        <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
        <title>
          Factura Electronica <xsl:value-of select="@Serie"/><xsl:value-of select="@Folio"/>
        </title>
      </head>

      <fieldset>

        <body>

<legend>
  <font color="red" face="arial" size="2">
    <b>
      CFDi - Comprobante Fiscal Digital por Internet
    </b>
  </font>
  
</legend>
          <table border="0" bordercolor="blue">
            <tr>
              <td class="h2" align="center">
                <center>
                  <font color="#000000" face="arial" size="2">
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

                  <table width="100%"  border="0">
                    <legend>
<font color="#000000" face="Courier New" size="2">
<b>
                  	    DATOS DE FISCALES
</b>
			</font>
</legend>
                    <!--
                    <tr>
                      <th colspan="2" class="h1">DATOS FISCALES</th>
                    </tr>
-->
                    <tr width="100%">

                      <td>
                         <font color="#000000" face="Courier New" size="2">
                           Serie/Folio:     

                         </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                          <xsl:value-of select="@Serie"/>
                          <xsl:value-of select="@Folio"/>
                            </b>
                        </font>
                      </td>
                      
                      
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Tasa de Cambio: 
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <!--<xsl:value-of select="@TipoCambio"/>-->1.0
                          </b>


                        </font>
                      </td>
                    </tr>

                    <tr width="100%">


                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Regimen Fiscal:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                               <xsl:if test="(cfdi:Emisor/@RegimenFiscal='601')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-General de Ley Personas Morales
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='603')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Personas Morales con Fines no Lucrativos
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='605')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Sueldos y Salarios e Ingresos Asimilados a Salarios
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='606')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Arrendamiento
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='608')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Demás ingresos
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='609')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Consolidación
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='610')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Residentes en el Extranjero sin Establecimiento Permanente en México
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='611')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Ingresos por Dividendos (socios y accionistas)
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='612')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Personas Físicas con Actividades Empresariales y Profesionales
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='614')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Ingresos por intereses
                            </xsl:if>
	        <xsl:if test="(cfdi:Emisor/@RegimenFiscal='615')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Régimen de los ingresos por obtención de premios
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='616')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Sin obligaciones fiscales
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='620')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Sociedades Cooperativas de Producción que optan por diferir sus ingresos
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='621')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Incorporación Fiscal
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='622')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='623')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Opcional para Grupos de Sociedades
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='624')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Coordinados
                            </xsl:if>
                            <xsl:if test="(cfdi:Emisor/@RegimenFiscal='625')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas
                            </xsl:if>
	        <xsl:if test="(cfdi:Emisor/@RegimenFiscal='626')">
                              <xsl:value-of select="cfdi:Emisor/@RegimenFiscal"/>-Régimen Simplificado de Confianza
                            </xsl:if>
                          </b>
                        </font>
                      </td>

                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Uso CFDi:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='G01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Adquisición de mercancias
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='G02')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Devoluciones, descuentos o bonificaciones
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='G03')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Gastos en general
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Construcciones
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I02')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Mobilario y equipo de oficina por inversiones
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I03')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Equipo de transporte
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I04')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Equipo de computo y accesorios
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I05')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Dados, troqueles, moldes, matrices y herramental
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I06')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Comunicaciones telefónicas
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I07')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Comunicaciones satelitales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='I08')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Otra maquinaria y equipo
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Honorarios médicos, dentales y gastos hospitalarios.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D02')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Gastos médicos por incapacidad o discapacidad
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D03')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Gastos funerales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D04')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Donativos.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D05')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D06')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Aportaciones voluntarias al SAR.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D07')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Primas por seguros de gastos médicos.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D08')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Gastos de transportación escolar obligatoria.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D09')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='D10')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Pagos por servicios educativos (colegiaturas)
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='P01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Por definir
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='CP01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Pagos
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='S01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Sin efectos fiscales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@UsoCFDI='CN01')">
                              <xsl:value-of select="cfdi:Receptor/@UsoCFDI"/>-Nómina
                            </xsl:if>

                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">

                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Tipo de Comprobante:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                         <b>
                           <xsl:if test="(@TipoDeComprobante='I')">
                             <xsl:value-of select="@TipoDeComprobante"/>-Ingreso
                           </xsl:if>
                           <xsl:if test="(@TipoDeComprobante='E')">
                             <xsl:value-of select="@TipoDeComprobante"/>-Egreso
                           </xsl:if>
                           <xsl:if test="(@TipoDeComprobante='T')">
                             <xsl:value-of select="@TipoDeComprobante"/>-Traslado
                           </xsl:if>
                           <xsl:if test="(@TipoDeComprobante='P')">
                             <xsl:value-of select="@TipoDeComprobante"/>-Pago
                           </xsl:if>
                           </b>
                        </font>
                      </td>


                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Método de Pago:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:if test="(@MetodoPago='PPD')">
                              <xsl:value-of select="@MetodoPago"/>-Pago en parcialidades o diferido
                            </xsl:if>
                            <xsl:if test="(@MetodoPago='PUE')">
                              <xsl:value-of select="@MetodoPago"/>-Pago en una sola exhibicion
                            </xsl:if> 
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">

                    

                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Forma de Pago:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:if test="(@FormaPago='01')">
                              <xsl:value-of select="@FormaPago"/>-Efectivo
                            </xsl:if>
                            <xsl:if test="(@FormaPago='02')">
                              <xsl:value-of select="@FormaPago"/>-Cheque nominativo
                            </xsl:if>
                            <xsl:if test="(@FormaPago='03')">
                              <xsl:value-of select="@FormaPago"/>-Transferencia electrónica de fondos
                            </xsl:if>
                            <xsl:if test="(@FormaPago='04')">
                              <xsl:value-of select="@FormaPago"/>-Tarjeta de crédito
                            </xsl:if>
                            <xsl:if test="(@FormaPago='05')">
                              <xsl:value-of select="@FormaPago"/>-Monedero electrónico
                            </xsl:if>
                            <xsl:if test="(@FormaPago='06')">
                              <xsl:value-of select="@FormaPago"/>-Dinero electrónico
                            </xsl:if>
                            <xsl:if test="(@FormaPago='08')">
                              <xsl:value-of select="@FormaPago"/>-Vales de despensa
                            </xsl:if>
                            <xsl:if test="(@FormaPago='12')">
                              <xsl:value-of select="@FormaPago"/>-Dación en pago
                            </xsl:if>
                            <xsl:if test="(@FormaPago='13')">
                              <xsl:value-of select="@FormaPago"/>-Pago por subrogación
                            </xsl:if>
                            <xsl:if test="(@FormaPago='14')">
                              <xsl:value-of select="@FormaPago"/>-Pago por consignación
                            </xsl:if>
                            <xsl:if test="(@FormaPago='15')">
                              <xsl:value-of select="@FormaPago"/>-Condonación
                            </xsl:if>
                            <xsl:if test="(@FormaPago='17')">
                              <xsl:value-of select="@FormaPago"/>-Compensación
                            </xsl:if>
                            <xsl:if test="(@FormaPago='23')">
                              <xsl:value-of select="@FormaPago"/>-Novación
                            </xsl:if>
                            <xsl:if test="(@FormaPago='24')">
                              <xsl:value-of select="@FormaPago"/>-Confusión
                            </xsl:if>
                            <xsl:if test="(@FormaPago='25')">
                              <xsl:value-of select="@FormaPago"/>-Remisión de deuda
                            </xsl:if>
                            <xsl:if test="(@FormaPago='26')">
                              <xsl:value-of select="@FormaPago"/>-Prescripción o caducidad
                            </xsl:if>
                            <xsl:if test="(@FormaPago='27')">
                              <xsl:value-of select="@FormaPago"/>-A satisfacción del acreedor
                            </xsl:if>
                            <xsl:if test="(@FormaPago='28')">
                              <xsl:value-of select="@FormaPago"/>-Tarjeta de débito
                            </xsl:if>
                            <xsl:if test="(@FormaPago='29')">
                              <xsl:value-of select="@FormaPago"/>-Tarjeta de servicios
                            </xsl:if>
                            <xsl:if test="(@FormaPago='30')">
                              <xsl:value-of select="@FormaPago"/>-Aplicación de anticipos
                            </xsl:if>
                            <xsl:if test="(@FormaPago='99')">
                              <xsl:value-of select="@FormaPago"/>-Por definir
                            </xsl:if>
                            </b>
                        </font>
                      </td>


                      <xsl:if test="//cfdi:CuentaPredial/@Numero != ''">
                        
                      
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                         No de Cuenta Predial:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:value-of select="//cfdi:CuentaPredial/@Numero"/>
                          </b>
                        </font>
                      </td>

                      </xsl:if>

                    </tr>

                    

                    <tr width="100%">

                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Divisa:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b> <xsl:value-of select="@Moneda"/>
                            </b>
                        </font>
                      </td>


                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Condiciones de pago:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                          <xsl:value-of select="@CondicionesDePago"/>
                            </b>
                        </font>
                      </td>
                    </tr>

                    

                    <tr width="100%">

                      <td>
                        <font color="#000000" face="Courier New" size="2">
                         Lugar Expedicion:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:value-of select="@LugarExpedicion"/>
                          </b>
                        </font>
                      </td>

 <td>
                        <font color="#000000" face="Courier New" size="3">
                         Versión de CFDi:
                        </font>
                      </td>
                      <td>
                        <font color="#000000" face="Courier New" size="3">
                          <b>
                            <xsl:value-of select="@Version"/>
                          </b>
                        </font>
                      </td>


                    </tr>


                
                  </table>
                </fieldset>
              </td>



              
	              <td width="35%">
                <fieldset>

                  <table width="100%" border="0">

                    
                      <legend>
			
 			<font color="#000000" face="Courier New" size="2">
<b>
                  	    DATOS DE TIMBRADO
</b>
			</font>
		</legend>
                    
                    
                    <tr>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Certificado SAT:
                          <br></br>
                            <b>
                              <xsl:value-of select="document('cad.xml')/Complemento/NoCertificadoSAT"/>
                            </b>
                        </font>
                      </td>
                    </tr>

                    <tr>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Folio Fiscal:
                          <br></br>
                          <b>

                            <xsl:value-of select="document('cad.xml')/Complemento/UUID"/>
                            </b>
                        </font>  
                      </td> 
                    </tr>

                    <tr>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                    Fecha de Certificación del CFDi:
                    <br></br>
                        <b>

                          <xsl:value-of select="document('cad.xml')/Complemento/FechaTimbrado"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr>
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                         Certificado de Emisor
                          <br></br>
                          <b>

                            <xsl:value-of select="@NoCertificado"/>
                          </b>
                        </font>
                      </td>
                    </tr>

                    <tr>
                      <td >
                        <font color="#000000" face="Courier New" size="2">
                          Fecha Emisión de Comprobante:
                          <br></br>
                          <b>
                            <xsl:value-of select="@Fecha"/>
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
                <xsl:if test="(@TipoDeComprobante ='E')">
                <tr>
                  <td width="100%" Colspan ="4">


                    <fieldset border="0">
                      <legend>
                        Datos Nota Credito
                      </legend>

                     

                      
                        <font color="#000000" face="Courier New" size="2">
                          CFDi Relacionado:
                        </font>
                      
                     
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:value-of select="//cfdi:CfdiRelacionados/cfdi:CfdiRelacionado/@UUID"/>
                          </b>
                        </font>
                     

                      
                        <font color="#000000" face="Courier New" size="2">
                          Tipo Relacion:
                        </font>
                      
                      
                        <font color="#000000" face="Courier New" size="2">
                          <b>
                            <xsl:value-of select="//cfdi:CfdiRelacionados/@TipoRelacion"/>
                          </b>
                        </font>
                      


                   

                    </fieldset>
                  </td>

                </tr>
                </xsl:if>
                
                <tr>
                 <td width="50%">


                <fieldset border="0">

                  <table width="100%" border="0">

                    <legend>
                       <font color="#000000" face="Courier New" size="2">
<b>
                      EMISOR
</b>
			</font>
                    </legend>

                    <tr width="100%">

                      <td>

                        <font color="#000000" face="Courier New" size="2">
                          RFC :
                          <b>
                          <xsl:value-of select="cfdi:Emisor/@Rfc"/>
                            </b>
                        </font>
                      </td>
                    </tr>

                    <tr width="100%">
                      <td>
                        <font color="#000000" face="Courier New" size="2">
                          Nombre :
                          <b>

                          <xsl:value-of select="cfdi:Emisor/@Nombre"/>
                            </b>
                        </font>
                      </td>
                    </tr>
                    <font color="#000000" face="Courier New" size="2">
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

			 <font color="#000000" face="Courier New" size="2">
<b>
                      RECEPTOR
</b>
			</font>
                    </legend>

                    <tr width ="100%">
                      
                      <td width="75%">
                        <font color="#000000" face="Courier New" size="2">
                         RFC: <b>
                          <xsl:value-of select="cfdi:Receptor/@Rfc"/>
                        </b>
                        </font>
                      </td>
                    </tr>


                    <tr>
                      
                      <td>
                        
                        <font color="#000000" face="Courier New" size="2">
                          Nombre: <b>
                          <xsl:value-of select="cfdi:Receptor/@Nombre"/>
                        </b>
                        </font>
                      </td>
                    </tr>

                    <font color="#000000" face="Courier New" size="2">
                      <b> <xsl:apply-templates select="//cfdi:Domicilio"/>
                        </b>
                    </font>


<td width="75%">
                        <font color="#000000" face="Courier New" size="3">
                         Regimen Fiscal: <b>
                           <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='601')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-General de Ley Personas Morales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='603')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Personas Morales con Fines no Lucrativos
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='605')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Sueldos y Salarios e Ingresos Asimilados a Salarios
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='606')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Arrendamiento
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='608')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Demás ingresos
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='609')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Consolidación
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='610')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Residentes en el Extranjero sin Establecimiento Permanente en México
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='611')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Ingresos por Dividendos (socios y accionistas)
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='612')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Personas Físicas con Actividades Empresariales y Profesionales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='614')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Ingresos por intereses
                            </xsl:if>
	        <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='615')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Régimen de los ingresos por obtención de premios
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='616')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Sin obligaciones fiscales
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='620')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Sociedades Cooperativas de Producción que optan por diferir sus ingresos
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='621')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Incorporación Fiscal
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='622')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='623')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Opcional para Grupos de Sociedades
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='624')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Coordinados
                            </xsl:if>
                            <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='625')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas
                            </xsl:if>
	        <xsl:if test="(cfdi:Receptor/@RegimenFiscalReceptor='626')">
                              <xsl:value-of select="cfdi:Receptor/@RegimenFiscalReceptor"/>-Régimen Simplificado de Confianza
                            </xsl:if>

                        </b>
                        </font>
                      </td>








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
        <font color="#000000" face="Courier New" size="2">
          Domicilio:
          <b>
          <xsl:value-of select="@calle"/><font color="white" face="Courier New" size="2">,</font>
            
            
          <xsl:value-of select="@noExterior"/><font color="white" face="Courier New" size="2">,</font>
            
          <xsl:value-of select="@noInterior"/><font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@colonia"/><font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@municipio"/><font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@estado"/><font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@pais"/><font color="white" face="Courier New" size="2">,</font>
          C.P. <xsl:value-of select="@codigoPostal"/>
        </b>
        </font>

      </td>


    </tr>

    
   
    


  </xsl:template>





  <xsl:template match="//cfdi:Domicilio">

    
    <tr>
      <td colspan="2">
        <font color="#000000" face="Courier New" size="2">
    
          Domicilio: 
          <b>
          <xsl:value-of select="@calle"/>
            <font color="white" face="Courier New" size="2">,</font>

          <xsl:value-of select="@noExterior"/>
            <font color="white" face="Courier New" size="2">,</font>

          <xsl:value-of select="@noInterior"/>
            <font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@colonia"/>
            <font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@municipio"/>
            <font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@estado"/>
            <font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@pais"/>
            <font color="white" face="Courier New" size="2">,</font>
          <xsl:value-of select="@codigoPostal"/>
        </b>
        </font>
      </td>
    </tr>

 
 
 
  </xsl:template>

  <xsl:template match="//cfdi:ExpedidoEn">



    <xsl:value-of select="@calle"/><font color="white" face="Courier New" size="2">,</font>


    <xsl:value-of select="@noExterior"/><font color="white" face="Courier New" size="2">,</font>

    <xsl:value-of select="@noInterior"/><font color="white" face="Courier New" size="2">,</font>
    <xsl:value-of select="@colonia"/><font color="white" face="Courier New" size="2">,</font>
    <xsl:value-of select="@municipio"/><font color="white" face="Courier New" size="2">,</font>
    <xsl:value-of select="@estado"/><font color="white" face="Courier New" size="2">,</font>
    <xsl:value-of select="@pais"/><font color="white" face="Courier New" size="2">,</font>
    C.P. <xsl:value-of select="@codigoPostal"/>






  </xsl:template>




</xsl:stylesheet>






