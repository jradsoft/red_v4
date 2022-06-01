<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/4'
    xmlns:pago10='http://www.sat.gob.mx/Pagos20'>

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


      <body background="fondo.jpg">
        <fieldset>
          <legend>
            <font color="#00000" face="Century Gothic" size="6">
              COMPLEMENTO DE PAGO
            </font>
          </legend>

          <table width="100%" border="1">

            <tr>

              <th>
                <font color="#00000" size="6">
                  Cantidad
                </font>
              </th>

              <th>
                <font color="#00000" size="6">
                  Unidad
                </font>
              </th>
              <th>
                <font color="#00000" size="6">
                  Codigo
                </font>
              </th>

              <th>
                <font color="#00000" size="6">
                  Descripcion
                </font>

              </th>
              <th>
                <font color="#00000" size="6">
                  Precio Unitario
                </font>

              </th>
              <th>
                <font color="#00000" size="6">
                  Importe
                </font>
              </th>
            </tr>



            <font color="#00000"  size="7">

              <PRE>
                <b>
                  <xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
                </b>
              </PRE>

            </font>

            <font color="#000000"  size="8">
              <b>
                <xsl:apply-templates select="//cfdi:Concepto"/>
                <xsl:for-each select="Concepto">
                </xsl:for-each>
              </b>

            </font>



            <table width="100%" border="1">

              <tr>


                <th>
                  <font color="#00000" size="6">
                    Fecha/Hora pago
                  </font>

                </th>
  

                <th>
                  <font color="#00000" size="6">
                    Total pago
                  </font>

                </th>


              </tr>


              <tr>


                <td>
                  <font color="#00000"  size="6">

                    <b>
                      <xsl:value-of select="//cfdi:Complemento/pago10:Pagos/pago10:Pago/@FechaPago"/>
                    </b>

                  </font>
                </td>
               

                <td>
                  <font color="#00000"  size="6">
                    <b>
                      <xsl:value-of select='format-number(//cfdi:Complemento/pago10:Pagos/pago10:Pago/@Monto,"$###,###,###.00")'/>
                     

                    </b>
                  </font>

                </td>


              </tr>
            </table>

            <table width="100%" border="1">

              <tr>


                <th>
                  <font color="#00000" size="6">
                    Cuenta Beneficiario
                  </font>

                </th>


                <th>
                  <font color="#00000" size="6">
                    Cuenta Ordenante
                  </font>

                </th>




              </tr>


              <tr>


                <td>
                  <font color="#00000"  size="6">

                    <b>
                      <xsl:value-of select="//cfdi:Complemento/pago10:Pagos/pago10:Pago/@CtaBeneficiario"/>
                    </b>

                  </font>
                </td>
                <td>
                  <font color="#00000"  size="6">
                    <b>
                      <xsl:value-of select="//cfdi:Complemento/pago10:Pagos/pago10:Pago/@CtaOrdenante"/>


                    </b>
                  </font>

                </td>




              </tr>
            </table>

            <table width="100%" border="1">

              <tr>


                <th>
                  <font color="#00000" size="6">
                    RFC Banco Beneficiario
                  </font>

                </th>


                <th>
                  <font color="#00000" size="6">
                    RFC Banco Ordenante
                  </font>

                </th>




              </tr>


              <tr>


                <td>
                  <font color="#00000"  size="6">

                    <b>
                      <xsl:value-of select="//cfdi:Complemento/pago10:Pagos/pago10:Pago/@RfcEmisorCtaBen"/>
                    </b>

                  </font>
                </td>
                <td>
                  <font color="#00000"  size="6">
                    <b>
                      <xsl:value-of select="//cfdi:Complemento/pago10:Pagos/pago10:Pago/@RfcEmisorCtaOrd"/>


                    </b>
                  </font>

                </td>




              </tr>
            </table>


            <table width="100%" border="0">
              <tr>
                <td>
                  <font color="#00000"  size="6">

                    <b>		CFDI's relacionados </b>
                  </font>
                </td>
                
              </tr>


            
             

              <table width="100%" border="1">

                <font color="#00000"  size="6">
                  <b>
                    <xsl:apply-templates select="//pago10:Pagos"/>

   
                  </b>
                </font>


              </table>

            



            </table>



            <hr/>


            <table width="100%" border="0">


              <tr>
                <td>
                  <center>
                    <font color="#00000"  size="6">
                      <b>
                        <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                      </b>
                    </font>
                  </center>
                </td>
              </tr>







              <tr>
                <th>
                  <font color="#00000"  size="6">
                    Cantidad con letra
                  </font>
                </th>
              </tr>

              <tr>
                <td>
                  <font color="#00000"  size="6">
                    <b>
                      <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
                    </b>
                  </font>
                </td>
              </tr>




              <tr>
                <th>
                  <font color="#00000"  size="6">
                    Cadena Original del Complemento de certificación digital del SAT
                  </font>
                </th>
              </tr>
              <tr>
                <td>
                  <font color="#00000"  size="6">
                    <pre>
                      <b>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 0, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 160, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 320, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 480, 60)"/>
                      </b>
                    </pre>
                  </font>

                </td>
              </tr>

              <tr>
                <th>
                  <font color="#00000"  size="6">
                    Sello Digital Emisor
                  </font>
                </th>
              </tr>

              <tr>
                <td>
                  <font color="#00000"  size="6">
                    <pre>
                      <b>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 0, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 160, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 320, 160)"/>
                        <br></br>
                        <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 480, 160)"/>
                      </b>
                    </pre>
                  </font>
                </td>
              </tr>





              <tr width="100%">

                <th >
                  <font color="#00000"  size="6">
                    Sello Digital SAT
                  </font>
                </th>
              </tr>
              <tr>
                <td width="50%">
                  <font color="#00000"  size="6">
                    <pre>
                      <b>
                        <center>
                          <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 0, 160)"/>
                          <br></br>
                          <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 160, 160)"/>
                          <br></br>
                          <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 320, 160)"/>
                          <br></br>
                          <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 480, 160)"/>
                        </center>
                      </b>
                    </pre>

                  </font>
                </td>

              </tr>




              <tr>

                <td>
                  <br></br>
                  <font color="#00000"  size="6">
                    <b>  </b>
                  </font>
                </td>


              </tr>



            </table>


            <br></br>




          </table>

        </fieldset>
      </body>

    </html>

  </xsl:template>


  <xsl:template match="//pago10:Pagos">
    <xsl:for-each select="//pago10:Pago">
      <tr>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Fecha Pago
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Numero Operacion
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Moneda
          </font>

        </th>


        <th width="30%">
          <font color="#00000" face="Century Gothic" size="2">
            Forma Pago
          </font>

        </th>
      </tr>
      <tr>
        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@FechaPago'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@NumOperacion'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@MonedaP'/>
            </b>

          </font>
        </td>

        <td>
          <font color="#00000"  size="3">
            <b>
              <xsl:if test="(@FormaDePagoP='01')">
                <xsl:value-of select="@FormaDePagoP"/>-Efectivo
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='02')">
                <xsl:value-of select="@FormaDePagoP"/>-Cheque nominativo
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='03')">
                <xsl:value-of select="@FormaDePagoP"/>-Transferencia electrónica de fondos
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='04')">
                <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de crédito
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='05')">
                <xsl:value-of select="@FormaDePagoP"/>-Monedero electrónico
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='06')">
                <xsl:value-of select="@FormaDePagoP"/>-Dinero electrónico
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='08')">
                <xsl:value-of select="@FormaDePagoP"/>-Vales de despensa
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='12')">
                <xsl:value-of select="@FormaDePagoP"/>-Dación en pago
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='13')">
                <xsl:value-of select="@FormaDePagoP"/>-Pago por subrogación
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='14')">
                <xsl:value-of select="@FormaDePagoP"/>-Pago por consignación
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='15')">
                <xsl:value-of select="@FormaDePagoP"/>-Condonación
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='17')">
                <xsl:value-of select="@FormaDePagoP"/>-Compensación
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='23')">
                <xsl:value-of select="@FormaDePagoP"/>-Novación
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='24')">
                <xsl:value-of select="@FormaDePagoP"/>-Confusión
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='25')">
                <xsl:value-of select="@FormaDePagoP"/>-Remisión de deuda
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='26')">
                <xsl:value-of select="@FormaDePagoP"/>-Prescripción o caducidad
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='27')">
                <xsl:value-of select="@FormaDePagoP"/>-A satisfacción del acreedor
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='28')">
                <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de débito
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='29')">
                <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de servicios
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='30')">
                <xsl:value-of select="@FormaDePagoP"/>-Aplicación de anticipos
              </xsl:if>
              <xsl:if test="(@FormaDePagoP='99')">
                <xsl:value-of select="@FormaDePagoP"/>-Por definir
              </xsl:if>



            </b>
          </font>

        </td>

       
      </tr>

      <xsl:for-each select="pago10:DoctoRelacionado">
        <tr>

          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              UUID
            </font>

          </th>

          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Serie
            </font>

          </th>

          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Folio
            </font>

          </th>



          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Parcialidad
            </font>

          </th>


          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Metodo Pago
            </font>

          </th>



          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Total
            </font>

          </th>

          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Saldo anterior
            </font>

          </th>
          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Saldo Pendiente
            </font>
          </th>
          <th width="10%">
            <font color="#00000" face="Century Gothic" size="3">
              Monto pagado
            </font>
          </th>

        </tr>
        <tr>

          <td align="center" width="30%">


            <font color="#00000"  size="3">
              <b>
                <xsl:value-of select='@IdDocumento'/>
              </b>

            </font>
          </td>

          <td align="center" width="10%">


            <font color="#00000"  size="3">
              <b>
                <xsl:value-of select='@Serie'/>
              </b>
            </font>
          </td>

          <td align="center" width="10%">


            <font color="#00000"  size="3">
              <b>
                <xsl:value-of select='@Folio'/>
              </b>
            </font>
          </td>

          <td align="center" width="10%">


            <font color="#00000"  size="3">
              <b>
                <xsl:value-of select='@NumParcialidad'/>
              </b>
            </font>
          </td>
          

          <td width="10%">
            <font color="#00000"  size="3">
              <pre>
                <b>
                  <xsl:value-of select='@MetodoDePagoDR'/>-Pago en parcialidades o diferido
                </b>
              </pre>
            </font>
          </td>

          <td align="center" width="10%">
            <font color="#00000"  size="3">
              <b>
                <xsl:value-of select='format-number(@ImpPagado,"$###,###,###.00")'/>

              </b>
            </font>

          </td>

          <td align="right" width="10%">
            <font color="#00000"  size="3">

              <b>
                <xsl:value-of select='format-number(@ImpSaldoAnt,"$###,###,###.00")'/>

              </b>
            </font>
          </td>


          <td align="right" width="10%">
            <font color="#00000"  size="3">

              <b>

                <xsl:value-of select='format-number(@ImpSaldoInsoluto,"$###,###,###.00")'/>
              </b>
            </font>
          </td>



          <td align="right" width="10%">
            <font color="#00000"  size="3">

              <b>
                <xsl:value-of select='format-number(@ImpPagado,"$###,###,###.00")'/>

              </b>


              <br></br>
              <br></br>
              
            </font>
          </td>


        </tr>

       
      </xsl:for-each>

      <br></br>
    </xsl:for-each>
  </xsl:template>

 
  <!--<xsl:template match="//pago10:Pagos/pago10:Pago">

    <tr>
    <td align="center" width="30%">


      <font color="#00000"  size="2">
        <b>
          <xsl:value-of select='@FechaPago'/>
        </b>

      </font>
    </td>

    <td align="center" width="30%">


      <font color="#00000"  size="2">
        <b>
          <xsl:value-of select='@NumOperacion'/>
        </b>

      </font>
    </td>

    <td align="center" width="30%">


      <font color="#00000"  size="2">
        <b>
          <xsl:value-of select='@Moneda'/>
        </b>

      </font>
    </td>

    <td>
        <font color="#00000"  size="5">
          <b>
            <xsl:if test="(@FormaDePagoP='01')">
              <xsl:value-of select="@FormaDePagoP"/>-Efectivo
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='02')">
              <xsl:value-of select="@FormaDePagoP"/>-Cheque nominativo
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='03')">
              <xsl:value-of select="@FormaDePagoP"/>-Transferencia electrónica de fondos
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='04')">
              <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de crédito
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='05')">
              <xsl:value-of select="@FormaDePagoP"/>-Monedero electrónico
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='06')">
              <xsl:value-of select="@FormaDePagoP"/>-Dinero electrónico
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='08')">
              <xsl:value-of select="@FormaDePagoP"/>-Vales de despensa
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='12')">
              <xsl:value-of select="@FormaDePagoP"/>-Dación en pago
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='13')">
              <xsl:value-of select="@FormaDePagoP"/>-Pago por subrogación
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='14')">
              <xsl:value-of select="@FormaDePagoP"/>-Pago por consignación
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='15')">
              <xsl:value-of select="@FormaDePagoP"/>-Condonación
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='17')">
              <xsl:value-of select="@FormaDePagoP"/>-Compensación
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='23')">
              <xsl:value-of select="@FormaDePagoP"/>-Novación
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='24')">
              <xsl:value-of select="@FormaDePagoP"/>-Confusión
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='25')">
              <xsl:value-of select="@FormaDePagoP"/>-Remisión de deuda
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='26')">
              <xsl:value-of select="@FormaDePagoP"/>-Prescripción o caducidad
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='27')">
              <xsl:value-of select="@FormaDePagoP"/>-A satisfacción del acreedor
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='28')">
              <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de débito
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='29')">
              <xsl:value-of select="@FormaDePagoP"/>-Tarjeta de servicios
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='30')">
              <xsl:value-of select="@FormaDePagoP"/>-Aplicación de anticipos
            </xsl:if>
            <xsl:if test="(@FormaDePagoP='99')">
              <xsl:value-of select="@FormaDePagoP"/>-Por definir
            </xsl:if>



          </b>
        </font>

      </td>

      <xsl:apply-templates select="//pago10:DoctoRelacionado"/>
    </tr>
    
  </xsl:template>

  <xsl:template match="//pago10:DoctoRelacionado">

    <tr>

      <td align="center" width="30%">


        <font color="#00000"  size="2">
          <b>
            <xsl:value-of select='@IdDocumento'/>
          </b>

        </font>
      </td>

      <td align="center" width="10%">


        <font color="#00000"  size="2">
          <b>
            <xsl:value-of select='@Serie'/>
          </b>
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#00000"  size="2">
          <b>
            <xsl:value-of select='@Folio'/>
          </b>
        </font>
      </td>

      <td width="10%">
        <font color="#00000"  size="2">
          <pre>
            <b>
              <xsl:value-of select='@MetodoDePagoDR'/>-Pago en parcialidades o diferido
            </b>
          </pre>
        </font>
      </td>

      <td align="center" width="10%">
        <font color="#00000"  size="2">
          <b>
            <xsl:value-of select='format-number(@ImpPagado,"$###,###,###.00")'/>

          </b>
        </font>

      </td>

      <td align="right" width="10%">
        <font color="#00000"  size="2">

          <b>
            <xsl:value-of select='format-number(@ImpSaldoAnt,"$###,###,###.00")'/>

          </b>
        </font>
      </td>


      <td align="right" width="10%">
        <font color="#00000"  size="2">

          <b>

            <xsl:value-of select='format-number(@ImpSaldoInsoluto,"$###,###,###.00")'/>
          </b>
        </font>
      </td>



      <td align="right" width="10%">
        <font color="#00000"  size="2">

          <b>
            <xsl:value-of select='format-number(@ImpPagado,"$###,###,###.00")'/>

          </b>
        </font>
      </td>


    </tr>
    --><!-- 
  <tr width='100%'>
    <td width='100%' colspan='5'>
      <hr></hr>
    </td>
  </tr>
  --><!--
  </xsl:template>-->


  <xsl:template match="//cfdi:Concepto">

    <tr>

      <td align="center" width="10%">


        <font color="#000000"  size="9">
          <b>
            <xsl:value-of select='format-number(@Cantidad,"###,###,###")'/>
          </b>

        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000"  size="9">
          <b>
            <xsl:value-of select="@ClaveUnidad"/>
          </b>
        </font>
      </td>
      <td align="center" width="10%">


        <font color="#000000"  size="9">
          <b>
            <xsl:value-of select="@ClaveProdServ"/>
          </b>
        </font>
      </td>

      <td width="50%">
        <font color="#000000"  size="10">
          <pre>
            <b>
              <xsl:value-of select="@Descripcion"/>
            </b>
          </pre>
        </font>
      </td>

      <td align="center" width="20%">
        <font color="#000000"  size="9">
          <b>
            <xsl:value-of select='format-number(@ValorUnitario,"$###,###,###.00")'/>
          </b>
        </font>

      </td>

      <td align="right" width="20%">
        <font color="#000000"  size="11">

          <b>
            <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
          </b>
        </font>
      </td>

    </tr>

    <tr width='100%'>
      <td width='100%' colspan='5'>
        <hr></hr>
      </td>
    </tr>

  </xsl:template>






  <xsl:template match="//cfdi:Traslado">


    <tr>

      <td align="right">
        <font color="#00000"  size="6">
          <b>
            <xsl:value-of select="@Impuesto"/>
          </b>

          <font color="White"  size="6">_</font>


        </font>
      </td>
      <td align="right">
        <font color="#00000"  size="6">
          <b>
            <xsl:value-of select='format-number(@Importe,"$###,###,###.00")' />
          </b>
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="#00000"  size="6">



        </font>
      </td>
      <td align="right">
        <font color="#00000"  size="6">
          <b>
            <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
          </b>
        </font>
      </td>

    </tr>
  </xsl:template>



</xsl:stylesheet>





