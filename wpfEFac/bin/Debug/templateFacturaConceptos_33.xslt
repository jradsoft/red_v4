<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/4'
    xmlns:cartaporte='http://www.sat.gob.mx/CartaPorte20'
    xmlns:servicioparcial='http://www.sat.gob.mx/servicioparcialconstruccion'
                >
 
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
      <font color="#000000" face="Century Gothic" size="3">
        CONCEPTOS
      </font>
    </legend>
       <table width="100%" border="0">


         


           <tr>
	    <th>
               <font color="#000000" face="Century Gothic" size="3">
                 Codigo
               </font>
             </th>

             <th>
               <font color="#000000" face="Century Gothic" size="3">
                 Cantidad 
               </font>
             </th>

             <th>
               <font color="#000000" face="Century Gothic" size="3">
                Clave Unidad
               </font>
             </th>
             
             <th>
               <font color="#000000" face="Century Gothic" size="3">
                 Unidad
               </font>
                 </th>
               
                 <th>
                   <font color="#000000" face="Century Gothic" size="3">
                     Descripcion
                   </font>
                     
                     </th>
                 <th>
                   <font color="#000000" face="Century Gothic" size="3">
                    Precio Unitario
                   </font>           
                 </th>

             <th>
               <font color="#000000" face="Century Gothic" size="3">
                 Descuento
               </font>
             </th>
                 <th>
                   <font color="#000000" face="Century Gothic" size="3">
                     Importe
                   </font>
                     </th>
             </tr>

         <font color="#000000" face="Courier New" size="3">

           <PRE>
             <b>
               <xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
             </b>
           </PRE>

         </font>



         <tr>

           <td  align="center" > </td>

           <td align="left">
             

           </td>

           <td  align="center" > </td>
           <td align="right"></td>

         </tr>


         
         <font color="#000000" face="Courier New" size="3">
           <b>
           <xsl:apply-templates select="//cfdi:Concepto"/>
           <xsl:for-each select="Concepto">    
             
             
           </xsl:for-each>
             <tr width='100%'>


               


             </tr>
           </b>

         </font>

	<table>

         <tr>

          

           <td align="left">
             <font color="#000000" face="Courier New" size="3">
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
             <th width="90%"></th>
             <th width="10%"></th>


           </tr>


           
           
           <tr>

             <td align="right">
               <font color="#000000" face="Courier New" size="3">
                 <p style="word-spacing: 2em;">
                   <b>
                     SUBTOTAL:
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#000000" face="Courier New" size="3">
                 <b>
                   <xsl:value-of select='format-number(@SubTotal,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>

           </tr>

           <tr>
             <xsl:if test="@Descuento > 0 ">
               <td align="right">

                 <font color="#000000" face="Courier New" size="3">
                   <p style="word-spacing: 2em;">
                     <b>
                       Descuento:
                     </b>
                   </p>
                 </font>
               </td>
               <td align="right" >
                 <font color="#000000" face="Courier New" size="3">
                   <b>
                     <xsl:value-of select='format-number(@Descuento,"$###,###,###.00")'/>
                   </b>
                 </font>
               </td>
             </xsl:if>
           </tr>
            <!--Traslados-->


           <font color="#000000" face="Courier New" size="3">
             <b>

               <xsl:for-each select="./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado">
                 


                 <xsl:if test="@Impuesto = '002' and (@Importe > 0) ">
                   <tr>
                     <td align="right">
                       <font color="#000000" face="Courier New" size="3">
                         <b>
                           <xsl:value-of select="@Impuesto"/>-IVA
                         </b>
                       </font>
                     </td>

                     <td align="right">
                       <font color="#000000" face="Courier New" size="3">
                         <b>
                           <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
                         </b>
                       </font>
                     </td>
                   </tr>
                 </xsl:if>
               </xsl:for-each>

             </b>

           </font>
	 
	




           <!--Retenciones-->
           
           
          <font color="#000000" face="Courier New" size="3">
           <b>

             <xsl:for-each select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
               <xsl:if test="(@Impuesto = '001') and (@Importe > 0)  ">
                 <tr>
                   <td align="right">
                     <font color="#000000" face="Courier New" size="3">
                       <b>
                        <xsl:value-of select="@Impuesto"/>-RET ISR
                       </b>
                     </font>
                   </td>

                   <td align="right">
                     <font color="#000000" face="Courier New" size="3">
                       <b>
                         <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
                       </b>
                     </font>
                   </td>
                 </tr>
               </xsl:if>

               <xsl:if test="@Impuesto = '002' and (@Importe > 0) ">
                 <tr>
                   <td align="right">
                     <font color="#000000" face="Courier New" size="3">
                       <b>
                         <xsl:value-of select="@Impuesto"/>-RET IVA
                       </b>
                     </font>
                   </td>

               
                   
                   

                   <td align="right">
                     <font color="#000000" face="Courier New" size="3">
                       <b>
                         <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
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
                   <font color="#000000" face="Courier New" size="3">
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
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto"/>Ret.IVA:
                     </b>
                   </font>
                 </td>
                 <td align="right">
                   <font color="#000000" face="Courier New" size="3">
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
               <font color="#000000" face="Courier New" size="3">
                 <b>
                   TOTAL: 
                 </b>
               </font>
                 </td>
             <td align="right">
               <font color="#000000" face="Courier New" size="3">
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
             <font color="#000000" face="Century Gothic" size="3">
               Complemento Carta porte
             </font>
           </legend>

           <table width="70%" border="0" >
             <tr width="70%" align="center">

               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   Version:

                 </font>
               </td>
               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@Version"/>
                   </b>
                 </font>
               </td>

               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   Transporte Internacional:

                 </font>
               </td>
               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@TranspInternac"/>
                   </b>
                 </font>
               </td>


               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   Total Distancia Recorrida:
                 </font>
               </td>
               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
                   <b>
                     <xsl:value-of select="//cfdi:Complemento/cartaporte:CartaPorte/@TotalDistRec"/>
                   </b>


                 </font>
               </td>
             </tr>



             <!--Ubicaciones-->
             <table width="100%">

               <tr>
                 <td  align="center" >
                   <font color="#00000"  size="3">

                     <b>	Ubicaciones </b>
                   </font>
                 </td>

               </tr>


               <font color="#00000"  size="3">
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
                 <font color="#000000" face="Century Gothic" size="3">
                   <b>Mercancia</b>
                 </font>
               </legend>


               <table  width="80%">
                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Total de mercancias:

                   </font>
                 </td>
                 <td align="left">
                   <font align="left" color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:Mercancias/@NumTotalMercancias"/>
                     </b>
                   </font>
                 </td>

                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Unidad Peso:

                   </font>
                 </td>
                 <td align="left">
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:Mercancias/@UnidadPeso"/>
                     </b>
                   </font>
                 </td>


                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Peso Bruto total:

                   </font>
                 </td>
                 <td align="left">
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:Mercancias/@PesoBrutoTotal"/>
                     </b>
                   </font>
                 </td>
               </table>


               <table width="100%">
                 <tr>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="3">
                       Cantidad
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"  face="Century Gothic" size="3">
                       Unidad
                     </font>

                   </th>
                  

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="3">
                       Clave del producto
                     </font>
                   </th>

                   <th  bgcolor="#CDCCCC">
                     <font color="#000000" face="Century Gothic" size="3">
                       Descripción
                     </font>
                   </th>


                   <th  bgcolor="#CDCCCC">
                     <font color="#000000"   face="Century Gothic" size="3">
                       Peso
                     </font>
                   </th>


                 </tr>

                 <font color="#00000"  size="3">
                   <b>
                     <xsl:apply-templates select="//cartaporte:Mercancia"/>

                   </b>
                 </font>
               </table>

             </table>

             
             <!--AutoTrasnporte-->
             <table width="100%">

               <legend>
                 <font color="#000000" face="Century Gothic" size="3">
                   <b>Autotransporte</b>
                 </font>
               </legend>
               <font color="#00000"  size="3">
                 <b>
                   <xsl:apply-templates select="//cartaporte:Autotransporte"/>
                 
                 </b>
               </font>
             </table>

             <!--Remolques-->
             <table width="50%">

               <legend>
                 <font color="#000000" face="Century Gothic" size="3">
                   <b>Remolques</b>
                 </font>
               </legend>
               <font color="#00000"  size="3">
                 <b>
                   <xsl:apply-templates select="//cartaporte:Remolque"/>

                 </b>
               </font>
             </table>


            
             <!--Operadores-->
             <table width="100%">
               <legend>
                 <font color="#000000" face="Century Gothic" size="3">
                   <b>Operadores</b>
                 </font>
               </legend>

               <table width="100%" valign="left">

                 <tr>
                   <th> Numero de Licencia</th>
                   <th>  RFC Figura</th>
                   <th>  Nombre Figura</th>
                   <th>  Tipo Figura</th>
                   
                   
                   
                 </tr>

                 <tr>
                 
                 <!--<td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Numero de Licencia:

                   </font>
                 </td>-->
                 <td >
                   <font color="#000000" face="Courier New" size="3" >
                     <b>
                       <xsl:value-of select="//cartaporte:FiguraTransporte/cartaporte:TiposFigura/@NumLicencia"/>
                     </b>
                   </font>
                 </td>


                 <!--<td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     RFC Figura:

                   </font>
                 </td>-->
                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:FiguraTransporte/cartaporte:TiposFigura/@RFCFigura"/>
                     </b>
                   </font>
                 </td>

                 <!--<td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Nombre Figura:

                   </font>
                 </td>-->
                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:FiguraTransporte/cartaporte:TiposFigura/@NombreFigura"/>
                     </b>
                   </font>
                 </td>

                 <!--<td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     Tipo Figura:

                   </font>
                 </td>-->
                 <td  align="center" >
                   <font color="#000000" face="Courier New" size="3">
                     <b>
                       <xsl:value-of select="//cartaporte:FiguraTransporte/cartaporte:TiposFigura/@TipoFigura"/>
                     </b>
                   </font>
                 </td>

                 </tr>
                 
               </table>
              
             </table>


           </table>



         </fieldset>
       </xsl:if>

       <br></br>
       <br></br>
       <xsl:if test="//cfdi:Complemento/servicioparcial:parcialesconstruccion">
         <fieldset>


           <legend>
             <font color="#000000" face="Century Gothic" size="3">
               Complemento para incorporar información de servicios parciales de construcción de inmuebles destinados a casa habitación.
             </font>
           </legend>

           <table width="100%" border="1">

             <font color="#00000"  size="3">
               <b>
                 <xsl:apply-templates select="//servicioparcial:parcialesconstruccion"/>
               </b>
             </font>


           </table>





         </fieldset>
         
       </xsl:if>





       <table width="100%" border="0">


         <tr>
           <td  align="center" >
             <center>
               <font color="#000000" face="Courier New" size="3">
                 <b>
                   <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                 </b>
               </font>
             </center>
           </td>
         </tr>



   



         <tr>
           <th>
             <font color="#000000" face="Courier New" size="3">
               Cantidad con letra
             </font>
               </th>
         </tr>

         <tr>
           <td  align="center" >
             <font color="#000000" face="Courier New" size="3">
               <b>
               <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
               </b>
             </font>
           </td>
         </tr>


      
         
             <tr>
               <th>
                 <font color="#000000" face="Courier New" size="3">
               Cadena Original del Complemento de certificación digital del SAT
               </font>
               </th>
             </tr>
             <tr>
               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
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
                 <font color="#000000" face="Courier New" size="3">
                 Sello Digital Emisor
                   </font>
                   </th>
             </tr>

             <tr>
               <td  align="center" >
                 <font color="#000000" face="Courier New" size="3">
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
             <font color="#000000" face="Courier New" size="3">
             Sello Digital SAT
               </font>
               </th>
         </tr>
         <tr>
           <td width="50%">
             <font color="#000000" face="Courier New" size="3">
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


   



       </table>
        
     
        <br></br>





     </fieldset>
     
   </body>

 </html>

</xsl:template>
 
     
 
<xsl:template match="//cfdi:Concepto">

    <tr>

	    <td align="center" width="10%">
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@ClaveProdServ"/>
          </b>
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="3">
          <b>
          <xsl:value-of select='format-number(@Cantidad,"###,###,###0.000")'/>
          </b>
          
        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@ClaveUnidad"/>
          </b>
        </font>
      </td>
      
      <td align="center" width="10%">
        
        
        <font color="#000000" face="Courier New" size="3">
          <b>
          <xsl:value-of select="@Unidad"/>
          </b>
        </font>
      </td>
      
      <td width="50%" align="left">
        <font color="#000000" face="Courier New" size="3">
         
            <b>
          <xsl:value-of select="@Descripcion"/>
              <br></br>

              <font color="#000000" face="Courier New" size="1">
                <xsl:if test="./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@Impuesto = '002' and (./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@Importe > 0) ">

                  <b>
                    Traslados   Base <xsl:value-of select="./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@Impuesto"/>- IVA   Tasa <xsl:value-of select="./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@TasaOCuota"/>   Importe <xsl:value-of select='format-number(./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado/@Importe,"$###,###,###.00")'/>
                  </b>

                </xsl:if>


                <xsl:if test="(./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto = '001') and (./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Importe > 0)  ">
                  <b>
                    Retenidos  Base <xsl:value-of select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto"/>-RET ISR   Tasa <xsl:value-of select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@TasaOCuota"/>   Importe <xsl:value-of select='format-number(./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Importe,"$###,###,###.00")'/>
                  </b>
                </xsl:if>

                <xsl:if test="(./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto = '002') and (./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Importe > 0)  ">
                  <b>
                    Retenidos  Base <xsl:value-of select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Impuesto"/>-RET IVA   Tasa <xsl:value-of select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@TasaOCuota"/>   Importe <xsl:value-of select='format-number(./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion/@Importe,"$###,###,###.00")'/>
                  </b>
                </xsl:if>
                
                
                
                
              </font>
              
              
              
            </b>
          
        </font>
      </td>
      
      
      
      <td align="center" width="20%">       
        <font color="#000000" face="Courier New" size="3">
          <b>
          <xsl:value-of select='format-number(@ValorUnitario,"$###,###,###.00")'/>
          </b>
        </font>
        
      </td>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="3">
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
        <font color="#000000" face="Courier New" size="3">

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

  
  
 
</xsl:template>

  <!-- Ubicaciones -->
  <xsl:template match="//cartaporte:Ubicacion">

    <table width="100%" >
      <tr>
      <th> Id Ubicacion </th>
      <th> Hora de Salida-Llegada </th>
      <th>Tipo Ubicacion </th>
        <xsl:if test="@DistanciaRecorrida > 0 ">
          <th>Distancia Recorrida</th>
        </xsl:if>
      
      
      
    </tr>

      <tr width="100%">

        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="@IDUbicacion"/>
            </b>
          </font>
        </td>
        <td align="center">
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="@FechaHoraSalidaLlegada"/>
            </b>
          </font>
        </td>
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="@TipoUbicacion"/>
            </b>
          </font>
        </td>

        <xsl:if test="@DistanciaRecorrida > 0 ">
          <td  align="center" >
            <font color="#000000" face="Courier New" size="3">
              <b>
                <xsl:value-of select="@DistanciaRecorrida"/>
              </b>
            </font>
          </td>
        </xsl:if>
        
        
        
      </tr>

    </table>

    <table width="100%" >

      <tr>
        <th> RFC </th>
        <th> Nombre Remitente o Destinatario </th>
       


      </tr>

      <tr width="100%" >

       
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="@RFCRemitenteDestinatario"/>
            </b>
          </font>
        </td>

        
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="@NombreRemitenteDestinatario"/>
            </b>
          </font>
        </td>

      </tr>
      
      
    </table>


    <table width="100%" >

      <tr>
        <th> Calle </th>
        <th>Número Exterior  </th>
        <th> Colonia </th>
        <th> Localidad </th>
        



      </tr>


      <tr width="100%" >
        <tr width="100%" >


        
          <td  align="center" >
            <font color="#000000" face="Courier New" size="3">
              <b>
                <xsl:value-of select="cartaporte:Domicilio/@Calle"/>
              </b>
            </font>
          </td>
          <td  align="center" >
            <font color="#000000" face="Courier New" size="3">
              <b>
                <xsl:value-of select="cartaporte:Domicilio/@NumeroExterior"/>
              </b>
            </font>
          </td>
          <td  align="center" >
            <font color="#000000" face="Courier New" size="3">
              <b>
                <xsl:value-of select="cartaporte:Domicilio/@Colonia"/>
              </b>
            </font>
          </td>
          <td  align="center" >
            <font color="#000000" face="Courier New" size="3">
              <b>
                <xsl:value-of select="cartaporte:Domicilio/@Localidad"/>
              </b>
            </font>
          </td>
        </tr>
      </tr>
    </table>

    <table width="100%" >

      <tr>
        <th> Municipio </th>
        <th> Estado </th>
        <th> Codigo Postal </th>
        <th> Pais </th>
        
      </tr>


      <tr width="100%" >

        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="cartaporte:Domicilio/@Municipio"/>
            </b>
          </font>
        </td>       
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="cartaporte:Domicilio/@Estado"/>
            </b>
          </font>
        </td>     
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="cartaporte:Domicilio/@CodigoPostal"/>
            </b>
          </font>
        </td> 
        <td  align="center" >
          <font color="#000000" face="Courier New" size="3">
            <b>
              <xsl:value-of select="cartaporte:Domicilio/@Pais"/>
            </b>
          </font>
        </td>



      </tr>
    </table>


   

  </xsl:template>

  

  


  <!-- Mercancias -->
  <xsl:template match="//cartaporte:Mercancia">

    <tr>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select='format-number(@Cantidad,"###,###,###")'/>
          </b>

        </font>
      </td>

      <td align="center" width="10%">


        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@ClaveUnidad"/>
          </b>
        </font>
      </td>

    

      <td align="center" width="20%">


        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@BienesTransp"/>
          </b>

        </font>
      </td>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="3">
          <pre>
            <b>
              <xsl:value-of select="@Descripcion"/>
            </b>
          </pre>
        </font>
      </td>

      <td align="center" width="20%">
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@PesoEnKg"/> KG
          </b>
        </font>
      </td>

      



     






    </tr>




  </xsl:template>

  <!-- Autotransporte -->
  <xsl:template match="//cartaporte:Autotransporte">

    <tr>
      <th>No.Permiso SCT</th>
      <th>Tipo permiso SCT</th>
      <th>Año/Modelo</th>
      <th>Placa</th>
      <th>Configuracion Vehicular</th>
    </tr>
    
    <tr width="100%" >
      
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@NumPermisoSCT"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@PermSCT"/>
          </b>


        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:IdentificacionVehicular/@AnioModeloVM"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:IdentificacionVehicular/@PlacaVM"/>
          </b>


        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:IdentificacionVehicular/@ConfigVehicular"/>
          </b>


        </font>
      </td>
    </tr>

    <table width="100%">


      <tr>
        <th>Asegura carga</th>
        <th>Poliza Responsabilidad Civil</th>
        <th> Aseguradora Responsabilidad Civil</th>
      </tr>
      <tr width="100%">
    
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:Seguros/@AseguraCarga"/>

           
          </b>


        </font>
      </td>   
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:Seguros/@PolizaRespCivil"/>
          </b>


        </font>
      </td>  
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="//cartaporte:Seguros/@AseguraRespCivil"/>
          </b>


        </font>
      </td>

     
    </tr>
    </table>

  </xsl:template>

  <!-- Remolques -->
  <xsl:template match="//cartaporte:Remolque">

    <tr>
      <th>Placa</th>
      <th>Tipo remolque</th>
    </tr>

    <tr width="100%" >

      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@Placa"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@SubTipoRem"/>
          </b>


        </font>
      </td>
     
    </tr>

   

  </xsl:template>



 

  

  <!-- Operador -->
  <xsl:template match="//cartaporte:Operador">

    <tr width="100%" >

      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Nombre:

        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@NombreOperador"/>
          </b>
        </font>
      </td>

      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          RFC:

        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@RFCOperador"/>
          </b>
        </font>
      </td>


      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          No. de licencia:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@NumLicencia"/>
          </b>


        </font>
      </td>


      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Residencia fiscal:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@ResidenciaFiscalOperador"/>
          </b>


        </font>
      </td>


    </tr>

    <tr width="100%" >


      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Calle:

        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Calle"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Número Exterior:

        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@NumeroExterior"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Colonia:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Colonia"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Localidad:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Localidad"/>
          </b>
        </font>
      </td>
    </tr>
    <tr>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Municipio:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Municipio"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Estado:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Estado"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Codigo Postal:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@CodigoPostal"/>
          </b>
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          Pais:
        </font>
      </td>
      <td  align="center" >
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="cartaporte:Domicilio/@Pais"/>
          </b>
        </font>
      </td>

    </tr>

  </xsl:template>



  <!-- Servicios parciales para la construccion -->

  <xsl:template match="//servicioparcial:parcialesconstruccion">

    <td align="center" width="10%">


      <font color="#00000"  size="3">
        <b>
          Version : <xsl:value-of select='//servicioparcial:parcialesconstruccion/@Version'/>
        </b>

      </font>
    </td>

    <td align="center" width="10%">


      <font color="#00000"  size="3">
        <b>
          Número de permiso : <xsl:value-of select='//servicioparcial:parcialesconstruccion/@NumPerLicoAut'/>
        </b>

      </font>
    </td>
    
    <xsl:for-each select="servicioparcial:Inmueble">
      <tr>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Calle
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            No NoExterior
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            No Interior
          </font>

        </th>


        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Colonia
          </font>

        </th>

      </tr>
      <tr>
        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@Calle'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@NoExterior'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@NoInterior'/>
            </b>

          </font>
        </td>


        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@Colonia'/>
            </b>

          </font>
        </td>


      </tr>

      <tr>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Localidad
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Municipio
          </font>

        </th>

        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Estado
          </font>

        </th>


        <th width="10%">
          <font color="#00000" face="Century Gothic" size="2">
            Codigo Postal
          </font>

        </th>
        
      </tr>

      <tr>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@Localidad'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@Municipio'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@Estado'/>
            </b>

          </font>
        </td>

        <td align="center" width="10%">


          <font color="#00000"  size="3">
            <b>
              <xsl:value-of select='@CodigoPostal'/>
            </b>

          </font>
        </td>


      </tr>

    </xsl:for-each>
  </xsl:template>
  
  
  <!--<xsl:template match="//cfdi:Traslado">
 
  
    <tr>

      <td align="right">
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select="@Impuesto"/>-IVA
          </b>

          <font color="White" face="Courier New" size="3">_</font>
          
          
        </font>
      </td>
      <td align="right">
        <font color="#000000" face="Courier New" size="3">
          <b>
          <xsl:value-of select='format-number(@Importe,"$###,###,###.00")' />
          </b>
        </font>
      </td>
    </tr>
  </xsl:template>
-->


  <!--<xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select='@Impuesto'/>
          </b>


        </font>
      </td>
      <td align="right">
        <font color="#000000" face="Courier New" size="3">
          <b>
            <xsl:value-of select='format-number(@Importe,"$###,###,###.00")'/>
          </b>
        </font>
      </td>

    </tr>
  </xsl:template>-->



</xsl:stylesheet>

 



