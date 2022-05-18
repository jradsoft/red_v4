<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/3'>
 
<xsl:output method = "html" /> 
<xsl:param name="id" select="."/>
 
<xsl:template match="//cfdi:Comprobante">
   <html>
   <head>
   <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
   <title>Factura Electronica <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/></title>
   </head>

    
   <body background="fondo.jpg">
     
     <fieldset>
    
       <legend>CONCEPTOS</legend>





       <fieldset>
         <legend>

         </legend>





         <xsl:for-each select="//cfdi:Concepto">
           <xsl:if test="(@unidad='TON') or (@unidad='VIAJE')">
             <table width="100%">
               <tr width="100%">
                 <td width="50%">
                   <font color="#0B0B6" face="Courier New" size="5">

                     VALOR UNITARIO, CUOTA CONVENIDA POR TONELADA O CARGA FRACCIONARIA


                   </font>
                 </td>

                 <td width="25%">
                   <font color="#0B0B6" face="Courier New" size="5">
<!--
                     <b>
                       <xsl:value-of select='format-number(@valorUnitario,"$###,###,###.00")'/>
                     </b>
-->
                   </font>
                 </td>

                 <td width="25%">
                   <font color="#0B0B6" face="Courier New" size="5">
                     
                     VALOR DECLARADO

                     <b>
                       
                     </b>

                   </font>
                 </td>


               </tr>
             </table>
           </xsl:if>
         </xsl:for-each>
       </fieldset>










       <table width="100%" border="1">

         <tr border ="2">

           <td width="80%">
             <table width="100%">           
               
               
               

              
               <tr>
               <th>
                 <font color="#0B0B6" face="Century Gothic" size="6">
                   BULTOS
                 </font>
               </th>

               <th>
                 <font color="#0B0B6" face="Century Gothic" size="6">
                   QUE EL REMITENTE DICEN CONTIENEN
                 </font>

               </th>
               <th>
                 <font color="#0B0B6" face="Century Gothic" size="6">
                   PESO
                 </font>

               </th>
               <th>
                 <font color="#0B0B6" face="Century Gothic" size="6">
                   VOLUMEN
                 </font>
               </th>
             </tr>


             <tr>

               <td> </td>

               <td align="left">
                 <font color="#0B0B6" face="Courier New" size="6">

                   <PRE>
                     
                       
                     
                   </PRE>

                 </font>

               </td>

               <td> </td>
               <td align="right"></td>

             </tr>



             <font color="#0B0B6" face="Courier New" size="6">
               

               <xsl:for-each select="//cfdi:Concepto">
                 
                 <xsl:if test="(@unidad = 'TON') or (@unidad = 'VIAJE')">
                   



                   <tr>
                     
                     <td width="60%" colspan="2">
                       <font color="#0B0B6" face="Courier New" size="6.8">
                         <pre>
                           <b>
                             <xsl:value-of select="@descripcion"/>
                           </b>
                         </pre>
                       </font>
                     </td>
                     <td align="right" width="20%">
                       <font color="#0B0B6" face="Courier New" size="6.8">
                         <b>
                           <xsl:if test="(@unidad = 'TON')">
                              <xsl:value-of select='@cantidad'/>
                           </xsl:if>
                         </b>

                       </font>
                     </td>
                     <td align="right" width="20%">
                       <font color="#0B0B6" face="Courier New" size="6.5">

                         <b>
                           <center>
                           <xsl:if test="(@unidad = 'TON')">
                              <xsl:value-of select='@unidad'/>
                           </xsl:if>
                           </center>
                         </b>
                       </font>
                     </td>
                   </tr>

                 </xsl:if> 
               </xsl:for-each>


             </font>



             <tr>

               <td> </td>

               <td align="left">
                 <font color="#0B0B6" face="Courier New" size="6">
                   <PRE>

                     REMISIONES: MAZZ<xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
                     
                     
                   </PRE>
                   <pre>
                     CONDUCTOR: <xsl:value-of select="document('cad.xml')/Complemento/Observaciones"/>
                   </pre>
                 </font>

               </td>
               

               
               <td align="center"></td>
               <font color="#0B0B6" face="Courier New" size="5" ></font>
               

             </tr>

               </table>
           </td>
           <td width="20%">

         <table width="100%" border="1" style="background:url('fondoo.jpg') no-repeat bottom">
           

           


           <tr>
             <th width="60%"></th>
             <th width="40%"></th>


           </tr>


           
           
           <tr>

             <td align="right" heigth="40">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                     FLETE
                     </b>
                 </p>
               </font>
                 </td>
             <td align="right" heigth="40">
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'TON' or (@unidad='VIAJE')">
                      <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
                   </xsl:if>
                 </xsl:for-each>
               </font>
             </td>
           </tr>


           <tr>

             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                  <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'SEGURO'">  
                     <xsl:value-of select='@descripcion'/>
                       </xsl:if>
              </xsl:for-each>  
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'SEGURO' and (@importe > 0)">
                     <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
                   </xsl:if>
                 </xsl:for-each>
               </font>
             </td>
           </tr>


           <tr>

             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                    <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'MANIOBRA'">  
                     <xsl:value-of select='@descripcion'/>
                       </xsl:if>
                 </xsl:for-each>  
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="(@unidad = 'MANIOBRA') and (@importe > 0)">
                     <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
                   </xsl:if>
                 </xsl:for-each>
               </font>
             </td>
           </tr>


           <tr>

             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                  <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'AUTOP'">  
                     <xsl:value-of select='@descripcion'/>
                       </xsl:if>
                 </xsl:for-each>  
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'AUTOP' and (@importe > 0)">
                     <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
                   </xsl:if>
                 </xsl:for-each>
               </font>
             </td>
           </tr>


           <tr>

             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                   
 	          <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'OTROS'">  
                     <xsl:value-of select='@descripcion'/>
                       </xsl:if>
                 </xsl:for-each>  
                     
                   
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:for-each select="//cfdi:Concepto">
                   <xsl:if test="@unidad = 'OTROS' and (@importe > 0)">
                     <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
                   </xsl:if>
                 </xsl:for-each>
               </font>
             </td>
           </tr>


           <tr>

             <td align="right" heigth="40">
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   <b>
                     SUB-TOTAL:
                   </b>
                 </p>
               </font>
             </td>
             <td align="right" heigth="40">
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:value-of select='format-number(@subTotal,"$###,###,###.00")'/>
               </font>
             </td>
           </tr>

           <font color="#0B0B6" face="Courier New" size="6">
             <xsl:apply-templates select="//cfdi:Traslado"/>
                <xsl:for-each select="Traslado">
             </xsl:for-each>
           </font>

           

           <tr>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <b>
                   TOTAL: 
                 </b>
               </font>
                 </td>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <b>
                   <xsl:value-of select='format-number(@subTotal + //cfdi:Traslado/@importe,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>
           </tr>


           <font color="#0B0B6" face="Courier New" size="6">
             <xsl:apply-templates select="//cfdi:Retencion"/>
             <xsl:for-each select="Retencion">
             </xsl:for-each>
           </font>


           <tr>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 
                 <b>
                   GRAN TOTAL:
                 </b>
               </font>
             </td>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="6">
                 <b>
                   <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>                   
                 </b>
               </font>
             </td>
           </tr>

           <tr>

             <td> </td>

             <td align="left">
               <font color="#0B0B6" face="Courier New" size="6">
                 <b>
                   <PRE>
               
                   </PRE>
                 </b>
               </font>
             </td>

             <td> </td>
             <td align="right"></td>
             

           </tr>


         </table>

           </td>
         </tr>

       </table>
         
	
       
        <hr/>


       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       <table width="100%" border="0">


         
         <tr>
           <td>
             <center>
               <font color="#0B0B6" face="Courier New" size="6">
                 <b>
                   <pre>CONTRIBUYENTE DEL REGIMEN SIMPLIFICADO</pre>
                   <pre>IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO</pre>

                   <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                 </b>
               </font>
             </center>
           </td>
         </tr>




         
         


         <tr>
           <th>
             <font color="#0B0B6" face="Courier New" size="6">
               Cantidad con letra
             </font>
               </th>
         </tr>

         <tr>
           <td>
             <font color="#0B0B6" face="Courier New" size="6">
               <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
             </font>
           </td>
         </tr>


      
         
             <tr>
               <th>
                 <font color="#0B0B6" face="Courier New" size="6">
               Cadena Original del Complemento de certificación digital del SAT
               </font>
               </th>
             </tr>
             <tr>
               <td>
                 <font color="#0B0B6" face="Courier New" size="6">
                   
                     <b>
                       <pre>
                         <center>
                       <xsl:value-of select="document('cad.xml')/Complemento/CadenaTFD"/>
                         </center>
                       </pre>
                     </b>
                     
                 </font>
                 
               </td>
             </tr>
             
              <tr>
               <th>
                 <font color="#0B0B6" face="Courier New" size="6">
                 Sello Digital Emisor
                   </font>
                   </th>
             </tr>

             <tr>
               <td>
                 <font color="#0B0B6" face="Courier New" size="6">
                   <pre>
                   <b>
                     <center>
                     <xsl:value-of select="document('cad.xml')/Complemento/SelloCFD"/>
                     </center>
                   </b>
                     </pre>
                 </font>
               </td>
             </tr>


         


         <tr width="100%">

           <th >
             <font color="#0B0B6" face="Courier New" size="6">
             Sello Digital SAT
               </font>
               </th>
         </tr>
         <tr>
           <td width="50%">
             <font color="#0B0B6" face="Courier New" size="6">
               <pre>               <b>
               <xsl:value-of select="document('cad.xml')/Complemento/SelloSAT"/>
               </b>
               </pre>

             </font>
           </td>
           <tr>

             <td>
               <br></br>
               <font color="#0B0B6" face="Courier New" size="4">
                 DEBE(MOS) INCONDICIONALMENTE A LA ORDEN DE <b> <xsl:value-of select="cfdi:Emisor/@nombre"/></b> EN LA CIUDAD DE ESTE PAGARE MERCANTIL EL DIA<b> <xsl:value-of select="@fecha"/></b>
                 LA CANTIDAD DE
                <b> <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>
                 <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
                </b>
                 VALOR DE LOS SERVICIOS RECIBIDOS A MI ENTERA SATISFACCION.
                 LA FIRMA O SELLO ESTAMPADO EN CUALQUIER LUGAR INDICA LA ACEPTACION DE ESTE DOCUMENTO, LLEGANDO A SU VENCIMIENTO DE PAGO Y DE NO EFECTUARSE ESTE
                 DOCUMENTO CAUSARA INTERESES MORATORIOS DEL_____% MENSUAL EN PERJUICIO DEL SUSCRIPTOR Y EN BENEFICIO DEL TENEDOR MISMO. ESTE PAGARE ES
                 MERCANTIL Y ESTA PROTEGIDO POR LA LEY GENERAL DE TITULOS Y OPERACIONES DE CREDITO EN SU ART. 173 Y ARTICULOS CORRELATIVOS POR NO SER PAGARE DOMICILIADO.
               </font>
             </td>


           </tr>


         </tr>


        


       </table>
        
    


     </fieldset>
     
   </body>

 </html>

</xsl:template>
 
     
 
<xsl:template match="//cfdi:Concepto">

  

    <tr>
      <td align="center" width="10%">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select="@noIdentificacion"/>
        </font>
      </td>
      <td width="50%">
        <font color="#0B0B6" face="Courier New" size="8">
          <xsl:value-of select="@descripcion"/>
          <pre></pre>
        </font>
      </td>
      <td align="right" width="20%">
        <font color="#0B0B6" face="Courier New" size="8">
          <xsl:value-of select='@cantidad'/>

          
        </font>
      </td>
      <td align="right" width="20%">
        <font color="#0B0B6" face="Courier New" size="8">

          <b>
            <xsl:value-of select='@unidad'/>
          </b>
        </font>
      </td>
    </tr>
  
</xsl:template>





  
  <xsl:template match="//cfdi:Traslado">
 
  
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select="@impuesto"/>
          <xsl:value-of select='format-number(@tasa," ###,###,###")'/> %
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")' />
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="4">
          (-)I.V.A RETENIDO POR FLETE 4%
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
        </font>
      </td>

    </tr>
  </xsl:template>



</xsl:stylesheet>

 



