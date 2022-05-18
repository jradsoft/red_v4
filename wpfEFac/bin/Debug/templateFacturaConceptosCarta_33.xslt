<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/3'
    xmlns:cartaporte='http://www.sat.gob.mx/CartaPorte'>
 
<xsl:output method = "html" /> 
<xsl:param name="id" select="."/>
 
<xsl:template match="//cfdi:Comprobante">
   <html>
   <head>
   <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
   <title>Factura Electronica <xsl:value-of select="@Serie"/><xsl:value-of select="@Folio"/></title>
   </head>

    
   <body background="fondo.jpg">
     <fieldset>
    <legend>
      <font color="#000000" face="Century Gothic" size="6">
        CONCEPTOS
      </font>
    </legend>
       <table width="100%" border="0">


         


          <tr>
	         <th>
               <font color="#000000" face="Century Gothic" size="6">
                 Codigo
               </font>
             </th>

             <th>
               <font color="#000000" face="Century Gothic" size="6">
                 Cantidad 
               </font>
             </th>

             <th>
               <font color="#000000" face="Century Gothic" size="6">
                Clave Unidad
               </font>
             </th>
             
             <th>
               <font color="#000000" face="Century Gothic" size="6">
                 Unidad
               </font>
                 </th>
               
                 <th>
                   <font color="#000000" face="Century Gothic" size="6">
                     Descripcion
                   </font>
                     
                     </th>
                 <th>
                   <font color="#000000" face="Century Gothic" size="6">
                    Precio Unitario
                   </font>           
                 </th>

             <th>
               <font color="#000000" face="Century Gothic" size="6">
                 Descuento
               </font>
             </th>
                 <th>
                   <font color="#000000" face="Century Gothic" size="6">
                     Importe
                   </font>
                     </th>
             </tr>

         <font color="#000000" face="Courier New" size="6">

           <PRE>
             <b>
               <xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
             </b>
           </PRE>

         </font>

         
         <font color="#000000" face="Courier New" size="8">
           <b>
           <xsl:apply-templates select="//cfdi:Concepto"/>
           <xsl:for-each select="Concepto">             
           </xsl:for-each>
           </b>

         </font>

        	<table>

         <tr>

          

           <td align="left">
             <font color="#000000" face="Courier New" size="8">
               <PRE>
                 <b>
                 <!--<xsl:value-of select="document('cad.xml')/Complemento/Observaciones"/>-->
                 </b>
               </PRE>
             </font>

           </td>

           
           

         </tr>

	</table>

         <table width="100%" border="0" style="background:url('fondoo.jpg') no-repeat bottom">
   
           <tr>

             <td align="right">
               <font color="#000000" face="Courier New" size="8">
                 <p style="word-spacing: 2em;">
                   <b>
                     SUBTOTAL:
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   <xsl:value-of select='format-number(@SubTotal,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>

           </tr>

           <tr>
             <xsl:if test="@Descuento > 0 ">
               <td align="right">

                 <font color="#000000" face="Courier New" size="8">
                   <p style="word-spacing: 2em;">
                     <b>
                       Descuento:
                     </b>
                   </p>
                 </font>
               </td>
               <td align="right" >
                 <font color="#000000" face="Courier New" size="8">
                   <b>
                     <xsl:value-of select='format-number(@Descuento,"$###,###,###.00")'/>
                   </b>
                 </font>
               </td>
             </xsl:if>
           </tr>
            <!--Traslados-->
     


             <font color="#000000" face="Courier New" size="8">
               <b>

                 <xsl:for-each select="//cfdi:Impuestos/@TotalImpuestosTrasladados">
                   

                   <xsl:if test="@Impuesto = '002' and (@Importe > 0) ">
                     <tr>
                       <td align="right">
                         <font color="#000000" face="Courier New" size="8">
                           <b>
                             <xsl:value-of select="@Impuesto"/>-IVA
                           </b>
                         </font>
                       </td>

                       <td align="right">
                         <font color="#000000" face="Courier New" size="8">
                           <b>
                             <xsl:value-of select="@Importe"/>
                           </b>
                         </font>
                       </td>
                     </tr>
                   </xsl:if>
                 </xsl:for-each>

               </b>

             </font>


           <!--<tr>

             <td align="right">
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   <xsl:value-of select="//cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@Impuesto"/>-IVA:
                 </b>

              

               </font>
             </td>
             <td align="right">
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   <xsl:value-of select='format-number(//cfdi:Impuestos/@TotalImpuestosTrasladados,"$###,###,###.00")' />
                 </b>
               </font>
             </td>
           </tr>-->

           <!--Retenciones-->
           
           
          <font color="#000000" face="Courier New" size="8">
           <b>

             <xsl:for-each select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
               <xsl:if test="(@Impuesto = '001') and (@Importe > 0)  ">
                 <tr>
                   <td align="right">
                     <font color="#000000" face="Courier New" size="8">
                       <b>
                        <xsl:value-of select="@Impuesto"/>-RET ISR
                       </b>
                     </font>
                   </td>

                   <td align="right">
                     <font color="#000000" face="Courier New" size="8">
                       <b>
                         <xsl:value-of select="@Importe"/>
                       </b>
                     </font>
                   </td>
                 </tr>
               </xsl:if>

               <xsl:if test="@Impuesto = '002' and (@Importe > 0) ">
                 <tr>
                   <td align="right">
                     <font color="#000000" face="Courier New" size="8">
                       <b>
                         <xsl:value-of select="@Impuesto"/>-RET IVA
                       </b>
                     </font>
                   </td>

                   <td align="right">
                     <font color="#000000" face="Courier New" size="8">
                       <b>
                         <xsl:value-of select="@Importe"/>
                       </b>
                     </font>
                   </td>
                 </tr>
               </xsl:if>
             </xsl:for-each>           
           
           </b>

         </font>
           
           
           
           

           <!--<xsl:for-each select="//cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
             <xsl:if test="@Impuesto = '001' or (@Importe > 0) ">
               <tr>

                 <td align="right">
                   <font color="#000000" face="Courier New" size="8">
                     <b>
                       <xsl:value-of select='format-number(@Importe,"$###,###,###.00")' />
                       --><!--<xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>--><!--
                     </b>
                   </font>
                 </td>
               </tr>
             </xsl:if>
           </xsl:for-each>-->












           <!--<xsl:for-each select="//cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
             <xsl:if test="@Impuesto= 001 and @Importe > 0">
               <tr>

                 <td align="right">
                   <font color="#000000" face="Courier New" size="8">
                     <b>
                       <xsl:value-of select="//cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto"/>Ret.IVA:
                     </b>
                   </font>
                 </td>
                 <td align="right">
                   <font color="#000000" face="Courier New" size="8">
                     <b>

                       <xsl:value-of select='format-number(//cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Importe,"$###,###,###.00")' />
                     </b>
                   </font>
                 </td>
               </tr>
             </xsl:if>
           </xsl:for-each>-->
           





           <tr>
             <td align="right">
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   TOTAL: 
                 </b>
               </font>
                 </td>
             <td align="right">
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   <xsl:value-of select='format-number(@Total,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>
           </tr>


         </table>

       </table>



       <xsl:if test="//cfdi:Complemento/cartaporte:CartaPorte">

         <fieldset>


           <legend>
             <font color="#000000" face="Century Gothic" size="6">
               Complemento Carta porte
             </font>
           </legend>

           <table width="70%" border="0" >
             <tr width="70%" align="center">

               <td>
                 <font color="#000000" face="Courier New" size="6">
                   Version:

                 </font>
               </td>
               <td>
                 <font color="#000000" face="Courier New" size="6">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@Version"/>
                   </b>
                 </font>
               </td>

               <td>
                 <font color="#000000" face="Courier New" size="6">
                   Transporte Internacional:

                 </font>
               </td>
               <td>
                 <font color="#000000" face="Courier New" size="6">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@TranspInternac"/>
                   </b>
                 </font>
               </td>


               <td>
                 <font color="#000000" face="Courier New" size="6">
                   Total Distancia Recorrida:
                 </font>
               </td>
               <td>
                 <font color="#000000" face="Courier New" size="6">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@TotalDistRec"/>
                   </b>


                 </font>
               </td>
             </tr>



             <!--Ubicaciones-->
             <table width="100%">

               <tr>
                 <td>
                   <font color="#00000"  size="6">

                     <b>	Ubicaciones </b>
                   </font>
                 </td>

               </tr>


               <font color="#00000"  size="6">
                 <b>
                   <xsl:apply-templates select="//cartaporte:Ubicacion"/>

                 </b>
               </font>


             </table>

             <br></br>
             <br></br>

             <!--Mercancias-->
             <table width="100%">


               <legend>
                 <font color="#000000" face="Century Gothic" size="6">
                   <b>Mercancia</b>
                 </font>
               </legend>


               <table width="20%">
                 <td>
                   <font color="#000000" face="Courier New" size="6">
                     Total de mercancias:

                   </font>
                 </td>
                 <td>
                   <font color="#000000" face="Courier New" size="6">
                     <b>
                       <xsl:value-of select="//cartaporte:Mercancias/@NumTotalMercancias"/>
                     </b>
                   </font>
                 </td>
               </table>


               <table width="100%">
                 <tr>
                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="6">
                       Peso
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="6">
                       Clave del producto
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000" face="Century Gothic" size="6">
                       Descripción
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="6">
                       Cantidad
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"  face="Century Gothic" size="6">
                       Unidad
                     </font>

                   </th>
                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"  face="Century Gothic" size="6">
                       Moneda
                     </font>
                   </th>


                 </tr>

                 <font color="#00000"  size="6">
                   <b>
                     <xsl:apply-templates select="//cartaporte:Mercancia"/>

                   </b>
                 </font>
               </table>

             </table>

             <br></br>
             <br></br>
             <!--AutoTrasnporte-->
             <table width="100%">
               <font color="#00000"  size="6">
                 <b>
                   <xsl:apply-templates select="//cartaporte:AutotransporteFederal"/>

                 </b>
               </font>
             </table>

             <br></br>
             <br></br>
             <!--Operadores-->
             <table width="100%">
               <legend>
                 <font color="#000000" face="Century Gothic" size="6">
                   <b>Operadores</b>
                 </font>
               </legend>

               <table width="30%">
                 <td>
                   <font color="#000000" face="Courier New" size="6">
                     Figura transporte clave:

                   </font>
                 </td>
                 <td>
                   <font color="#000000" face="Courier New" size="6">
                     <b>
                       <xsl:value-of select="//cartaporte:FiguraTransporte/@CveTransporte"/>
                     </b>
                   </font>
                 </td>
               </table>
               <table width="100%">

                 <font color="#00000"  size="6">
                   <b>


                     <xsl:apply-templates select="//cartaporte:Operador"/>
                     <xsl:for-each select="Operador">
                     </xsl:for-each>


                   </b>
                 </font>

               </table>
             </table>


           </table>



         </fieldset>
       </xsl:if>
	
       
       
   



       <table width="100%" border="0">


         <tr>
           <td>
             <center>
               <font color="#000000" face="Courier New" size="8">
                 <b>
                   <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                 </b>
               </font>
             </center>
           </td>
         </tr>



   



         <tr>
           <th>
             <font color="#000000" face="Courier New" size="8">
               Cantidad con letra
             </font>
               </th>
         </tr>

         <tr>
           <td>
             <font color="#000000" face="Courier New" size="8">
               <b>
               <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
               </b>
             </font>
           </td>
         </tr>


      
         
             <tr>
               <th>
                 <font color="#000000" face="Courier New" size="8">
               Cadena Original del Complemento de certificación digital del SAT
               </font>
               </th>
             </tr>
             <tr>
               <td>
                 <font color="#000000" face="Courier New" size="6">
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
                 <font color="#000000" face="Courier New" size="8">
                 Sello Digital Emisor
                   </font>
                   </th>
             </tr>

             <tr>
               <td>
                 <font color="#000000" face="Courier New" size="6">
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
             <font color="#000000" face="Courier New" size="8">
             Sello Digital SAT
               </font>
               </th>
         </tr>
         <tr>
           <td width="50%">
             <font color="#000000" face="Courier New" size="6">
               <pre>
                 <b>
                  
                 <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 0, 160)"/>
                 <br></br>
                 <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 160, 160)"/>
                 <br></br>
                 <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 320, 160)"/>
                 <br></br>
                 <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 480, 160)"/> 
                   
               </b>
               </pre>

             </font>
           </td>
           
         </tr>


        

         <tr>

           <td>
             <br></br>
             <font color="#000000" face="Courier New" size="8">
               <b>  </b>
             </font>
           </td>
           

         </tr>



       </table>
        
     
        <br></br>





     </fieldset>
     
   </body>

 </html>

</xsl:template>


  <!-- Concepto-->
  <xsl:template match="//cfdi:Concepto">

    <tr>

	      <td align="center" width="10%">
        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@ClaveProdServ"/>
          </b>
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="8">
          <b>
          <xsl:value-of select='format-number(@Cantidad,"###,###,###")'/>
          </b>
          
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@ClaveUnidad"/>
          </b>
        </font>
      </td>
      
      <td align="center" width="10%">
        
        
        <font color="#000000" face="Courier New" size="8">
          <b>
          <xsl:value-of select="@Unidad"/>
          </b>
        </font>
      </td>
      
      <td width="50%">
        <font color="#000000" face="Courier New" size="8">
          <pre>
            <b>
          <xsl:value-of select="@Descripcion"/>
            </b>
          </pre>
        </font>
      </td>
      
      <td align="center" width="20%">       
        <font color="#000000" face="Courier New" size="8">
          <b>
          <xsl:value-of select='format-number(@ValorUnitario,"$###,###,###.00")'/>
          </b>
        </font>
        
      </td>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:if test="@Descuento > 0 ">
              <xsl:value-of select='format-number(@Descuento,"$###,###,###.00")'/>
            </xsl:if>
            <xsl:if test="not(@Descuento)">
              0.00
            </xsl:if>
            
          </b>
        </font>
      </td>
      
      <td align="right" width="20%">
        <font color="#000000" face="Courier New" size="8">

          <b>
            <xsl:if test="@Descuento > 0 ">
              <xsl:value-of select='format-number(@Importe - @Descuento,"$###,###,###.00")'/>
            </xsl:if>
            <xsl:if test="not(@Descuento)">
              <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
            </xsl:if>
          </b>
        </font>
      </td>
    </tr>
  <tr width='100%'>
    <td width='100%' colspan='7'>
      <hr></hr>
    </td>
  </tr>

  
  
</xsl:template>


  <!-- Origen -->
  <xsl:template match="//cartaporte:Origen">



    <tr width="100%" >

      <td>
        <font color="#000000" face="Courier New" size="6">
          Nombre del Remitente:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NombreRemitente"/>
          </b>
        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          RFC:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@RFCRemitente"/>
          </b>
        </font>
      </td>


      <td>
        <font color="#000000" face="Courier New" size="6">
          Hora de Salida:
        </font>
      </td>
      <td width="20%">
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@FechaHoraSalida"/>
          </b>


        </font>
      </td>

      <!--<xsl:call-template  name="Domicilio"/>-->
    </tr>
    
  </xsl:template>

  <!-- Domicilio -->
  <xsl:template match="//cartaporte:Domicilio">

    <tr width="100%" >

      <td>
        <font color="#000000" face="Courier New" size="6">
          Calle:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Calle"/>
          </b>
        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          Número Exterior:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NumeroExterior"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Colonia:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Colonia"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Localidad:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Localidad"/>
          </b>
        </font>
      </td>
    </tr>
    <br></br>
    <tr>
    
      <td>
        <font color="#000000" face="Courier New" size="6">
          Municipio:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Municipio"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Estado:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Estado"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Codigo Postal:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@CodigoPostal"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Pais:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@Pais"/>
          </b>
        </font>
      </td>
      
      
    </tr>

  </xsl:template>

  <!-- Destino -->
  <xsl:template match="//cartaporte:Destino">
    
    <tr width="100%" >
    
      <td>
        <font color="#000000" face="Courier New" size="6">
          Id ResidenciaFiscal:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NumRegIdTrib"/>
          </b>
        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          Residencia Fiscal:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@ResidenciaFiscal"/>
          </b>
        </font>
      </td>


      <td>
        <font color="#000000" face="Courier New" size="6">
          Fecha de Llegada:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@FechaHoraProgLlegada"/>
          </b>


        </font>
      </td>
    </tr>

  </xsl:template>



  <!-- Mercancias -->
  <xsl:template match="//cartaporte:Mercancia">

    <tr>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@PesoEnKg"/> KG
          </b>
        </font>
      </td>

      <td align="center" width="20%">


        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@BienesTransp"/>
          </b>

        </font>
      </td>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="8">
          <pre>
            <b>
              <xsl:value-of select="@Descripcion"/>
            </b>
          </pre>
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select='format-number(@Cantidad,"###,###,###")'/>
          </b>

        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@ClaveUnidad"/>
          </b>
        </font>
      </td>



      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="8">
          <b>
            <xsl:value-of select="@Moneda"/>
          </b>
        </font>
      </td>






    </tr>




  </xsl:template>

  <!-- Autotransporte -->
  <xsl:template match="//cartaporte:AutotransporteFederal">

    <tr width="100%" >

      <td>
        <font color="#000000" face="Courier New" size="6">
          No.Permiso SCT:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NumPermisoSCT"/>
          </b>
        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          Aseguradora:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NombreAseg"/>
          </b>
        </font>
      </td>


      <td>
        <font color="#000000" face="Courier New" size="6">
          No Poliza:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NumPolizaSeguro"/>
          </b>


        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          Tipo permiso SCT:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@PermSCT"/>
          </b>


        </font>
      </td>
    </tr>

  </xsl:template>

  <!-- Operador -->
  <xsl:template match="//cartaporte:Operador">

    <tr width="100%" >

      <td>
        <font color="#000000" face="Courier New" size="6">
          Nombre:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NombreOperador"/>
          </b>
        </font>
      </td>

      <td>
        <font color="#000000" face="Courier New" size="6">
          RFC:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@RFCOperador"/>
          </b>
        </font>
      </td>


      <td>
        <font color="#000000" face="Courier New" size="6">
          No. de licencia:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@NumLicencia"/>
          </b>


        </font>
      </td>


      <td>
        <font color="#000000" face="Courier New" size="6">
          Residencia fiscal:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="@ResidenciaFiscalOperador"/>
          </b>


        </font>
      </td>

     
    </tr>

    <tr width="100%" >


      <td>
        <font color="#000000" face="Courier New" size="6">
          Calle:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Calle"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Número Exterior:

        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@NumeroExterior"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Colonia:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Colonia"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Localidad:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Localidad"/>
          </b>
        </font>
      </td>
    </tr>
    <tr>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Municipio:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Municipio"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Estado:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Estado"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Codigo Postal:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@CodigoPostal"/>
          </b>
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          Pais:
        </font>
      </td>
      <td>
        <font color="#000000" face="Courier New" size="6">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Pais"/>
          </b>
        </font>
      </td>

    </tr>

  </xsl:template>

 




</xsl:stylesheet>

 



