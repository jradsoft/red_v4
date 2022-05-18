<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfdi='http://www.sat.gob.mx/cfd/3'
    xmlns:nomina12='http://www.sat.gob.mx/nomina12'
                >
 
<xsl:output method = "html" /> 
<xsl:param name="id" select="."/>
 
<xsl:template match="//cfdi:Comprobante">
   <html>
   <head>
   <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
   <title>Factura Electronica <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/></title>
   </head>

    
   <body>
     <br></br>
     <fieldset>
       <legend>
         <font color="black" face="Century Gothic" size="6">
           DATOS FISCALES
         </font>
       </legend>

       <table width="100%" border="1">







         <tr>

           <td>
             <b>
               <h1>
                 <b>PERCEPCIONES</b>
               </h1>
             </b>
             <table border="1">
               <thead>
                 <th>
                   <h1>Tipo Perpepcion</h1>
                 </th>
                 <th>
                   <h1>Clave</h1>
                 </th>
                 <th>
                   <h1>Concepto</h1>
                 </th>
                 <th>
                   <h1>Importe Exento</h1>
                 </th>
                 <th>
                   <h1>Importe Gravado</h1>
                 </th>
               </thead>
               <font color="black" face="Courier New" size="10">
                 <xsl:apply-templates select="//nomina12:Percepcion"/>
                 <xsl:for-each select="Percepcion">

                 </xsl:for-each>
               </font>
             </table>

             <table width="100%" border="1">







               <tr>

                 <td>
                   <b>
                     <h1>
                       <b>HORAS EXTRAS</b>
                     </h1>
                   </b>
                   <table border="0">
                     <thead>
                       <th>
                         <h1>Dias</h1>
                       </th>
                       <th>
                         <h1>Horas Extra</h1>
                       </th>
                       <th>
                         <h1>Tipo de Hora</h1>
                       </th>
                       <th>
                         <h1></h1>
                       </th>
                       <th>
                         <h1>Importe Pagado</h1>
                       </th>
                     </thead>
                     
               <font color="black" face="Courier New" size="10">
                 
                 <xsl:apply-templates select="//nomina12:HorasExtra"/>
                 <xsl:for-each select="HorasExtra">

                 </xsl:for-each>
               </font>
            
                   </table>
                 </td>
               </tr>
               
             </table>

             <table border="0">
               <tr>
                 <td>
                   <h1>
                     <b>SUMA DE PERCEPCIONES </b>
                   </h1>
                 </td>
                 <td>
                   <font color="black" face="Courier New" size="12">
                     <b>
                     <xsl:value-of select='format-number(@subTotal,"$###,###,###.00")'/>
                     </b>
                   </font>
                 </td>
                 <td></td>
               </tr>
             </table>
             
           </td>

           <td>
             <b>
               <h1>
                 <b>DEDUCCIONES</b>
               </h1>
             </b>

             <table border="1">
               <thead>
                 <th>
                   <h1>Tipo Deduccion</h1>
                 </th>
                 <th>
                   <h1>Clave</h1>
                 </th>
                 <th>
                   <h1>Concepto</h1>
                 </th>
                 <th>
                   <h1>Importe Exento</h1>
                 </th>
                 <th>
                   <h1>Importe Gravado</h1>
                 </th>
               </thead>

               <font color="black" face="Courier New" size="10">
                 <xsl:apply-templates select="//nomina12:Deduccion"/>
                 <xsl:for-each select="Deduccion">

                 </xsl:for-each>

               </font>
               
               <!--
               <xsl:apply-templates select="//nomina12:Percepcion"/>
                 <xsl:for-each select="Percepcion">

                 </xsl:for-each>
                
               -->
               
             <!--
              <xsl:apply-templates select="//nomina12:Percepciones"/>
                 <xsl:for-each select="Percepcion">

                 </xsl:for-each>
             </table>


             <b>
               <h1>
                 <b>INCAPACIDADES</b>
               </h1>
             </b>

             <table border="1">
               <thead>
                 <th>
                   <h1>Tipo de incapacidad</h1>
                 </th>
                 <th>
                   <h1>Dias de incapacidad</h1>
                 </th>
                 
                 <th>
                   <h1>Descuento</h1>
                 </th>
               </thead>
               
               <font color="black" face="Courier New" size="10">
                 <xsl:apply-templates select="//nomina12:Incapacidad"/>
                 <xsl:for-each select="Incapacidad">

                 </xsl:for-each>
               </font>
               -->
              
             </table>

             <table border="0">
               <tr>
                 <td>
                   <h1>
                     <b>SUMA DE DEDUCCIONES </b>
                   </h1>
                 </td>
                 <td>
                   <font color="black" face="Courier New" size="12">
                     <b>
                       <xsl:value-of select='format-number(@descuento,"$###,###,###.00")'/>
                     </b>
                   </font>
                 </td>
                 <td></td>
               </tr>
             </table>

           </td>

          
         </tr>
         
       </table>




       <table>

         <!--
         <tr>

           <td align="right">
             <font color="black" face="Courier New" size="9">
               <p style="word-spacing: 2em;">
                 SUBTOTAL:
               </p>
             </font>
           </td>
           <td align="right" >
             <font color="black" face="Courier New" size="9">
               <xsl:value-of select='format-number(@subTotal,"$###,###,###.00")'/>
             </font>
           </td>
         </tr>


         <font color="black" face="Courier New" size="9">
           <xsl:apply-templates select="//cfdi:Traslado"/>
         </font>

         <font color="black" face="Courier New" size="9">
           <xsl:for-each select="Traslado">
           </xsl:for-each>
         </font>

         <font color="black" face="Courier New" size="9">
           <xsl:apply-templates select="//cfdi:Retencion"/>
           <xsl:for-each select="Retencion">
           </xsl:for-each>
         </font>

         <tr>
           <td align="right">
             <font color="black" face="Courier New" size="9">
               <b>
                 TOTAL:
               </b>
             </font>
           </td>
           <td align="right">
             <font color="black" face="Courier New" size="9">
               <b>
                 <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>
               </b>
             </font>
           </td>
         </tr>


-->
         <tr>

           <td> </td>

           <td align="left">
             <font color="black" face="Courier New" size="9">
               <b>
                 <PRE>
                   <xsl:value-of select="document('cad.xml')/Complemento/ImpuestosAdicionales"/>
                 </PRE>
               </b>
             </font>
           </td>

           <td> </td>
           <td align="right"></td>

         </tr>

         <tr>
           
           <th>
             <font color="black" face="Courier New" size="20">
               Cantidad con letra
             </font>
           </th>



           <td>
             <font color="black" face="Courier New" size="20">
               <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
             </font>
           </td>

           <tr align="center">

             <th>
               <font color="black" face="Courier New" size="20">
                 <b>ISR Retenido:</b>
                 <font color="black" face="Courier New" size="9">
                   <xsl:apply-templates select="//cfdi:Retencion"/>
                   <xsl:for-each select="Retencion">
                   </xsl:for-each>
                 </font>

               </font>
             </th>

           </tr>
           
           <tr aling="center">
             <th>
               <font color="black" face="Courier New" size="20">
                 <b>NETO A PAGAR:</b>
                 <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>
               </font>
             </th>
           </tr>
           
           
           
           
          
         </tr>

         <tr>
           <td>
             
           </td>
           <td>

           </td>
           <td colspan="2">

           
             <font color="black" face="Courier New" size="12">
               <b>
                 <br></br>
                 Firma del Empleado <br></br><br></br>
                 _______________________________________
               </b>
             </font>
           </td>
         </tr>


       </table>

       <br></br>
       <br></br>
       <br></br>
       <br></br>
       
       <table width="100%" border="0">


         <tr>
           <td colspan="4">
             <center>
               <font color="black" face="Courier New" size="16">
                 <b>
                   <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                 </b>
               </font>
             </center>
           </td>
         </tr>

         <tr>
           <th>
             <font color="black" face="Courier New" size="16">
               Folio Fiscal:
             </font>
           </th>
           <td>
             <font color="black" face="Courier New" size="16">


               <b>

                 <xsl:value-of select="document('cad.xml')/Complemento/UUID"/>
               </b>
             </font>
           </td>
         </tr>


         <tr>

           

             <th>
               <font color="black" face="Courier New" size="16">
                 Certificado Emisor:
               </font>
             </th>
             <td>
               <font color="black" face="Courier New" size="16">
                 <b>
                   <xsl:value-of select="@noCertificado"/>
                 </b>
               </font>
             </td>
           
           <th>
             <font color="black" face="Courier New" size="16">
             Certificado SAT:
             </font>  
               </th>
           <td>
             <font color="black" face="Courier New" size="16">
               
              
               <b>
                 <xsl:value-of select="document('cad.xml')/Complemento/NoCertificadoSAT"/>
               </b>
             </font>
           </td>
         
         </tr>

         <tr>

           <th>
             <font color="black" face="Courier New" size="16">
               Fecha de Certificación del CFDi:

               
             </font>
           </th>
           
           <td>
             <font color="black" face="Courier New" size="16">
              <b>

                 <xsl:value-of select="document('cad.xml')/Complemento/FechaTimbrado"/>
               </b>
             </font>
           </td>

           <th>
             <font color="black" face="Courier New" size="16">
               Fecha de Emision :
             </font>
          </th>
               <td>
             <font color="black" face="Courier New" size="16">
                         
               <b>

                 <xsl:value-of select="@fecha"/>
               </b>
             </font>
           </td>
         </tr>

         
         <tr>
           <th>
             <font color="black" face="Courier New" size="16">
               Lugar de expedicion:
             </font>

           </th>
           
           <td>
           
           <font color="black" face="Courier New" size="16">
             <b>
               <xsl:value-of select="@LugarExpedicion"/>
             </b>
           </font>
         </td>


           <th>
             <font color="black" face="Courier New" size="16">
               Tipo de Comprobante:
             </font>
           </th>
           <td>
             <font color="black" face="Courier New" size="16">
               <b>
                 <xsl:value-of select="@tipoDeComprobante"/>
               </b>
             </font>
           </td>

          </tr>
        
         <tr>
           <th>
             <font color="black" face="Courier New" size="16">
               Método de Pago:
             </font>
          </th>
          
           <td>
             <font color="black" face="Courier New" size="16">
               <b>
                 <xsl:value-of select="@metodoDePago"/>
               </b>
             </font>
           </td>

           <th>
             <font color="black" face="Courier New" size="16">
               Numero de cuenta:
             </font>
           </th>
           
           <td>
             <font color="black" face="Courier New" size="16">
               <b>
                 <xsl:value-of select="@NumCtaPago"/>
               </b>
             </font>
           </td>
         </tr>
         
         <tr width="100%">


           <th>
             <font color="black" face="Courier New" size="16">
               Forma de Pago:
             </font>
           </th>
           <td>
             <font color="black" face="Courier New" size="16">
               <b>
                 <xsl:value-of select="@formaDePago"/>
               </b>
             </font>
           </td>

           <th>
             <font color="black" face="Courier New" size="16">
               Regimen fiscal:
             </font>
           </th>
           <td>
             <font color="black" face="Courier New" size="12">
               <b>
                 <xsl:value-of select="cfdi:Emisor/cfdi:RegimenFiscal/@Regimen"/>
               </b>
             </font>
           </td>
         </tr>
         




         <tr>
         <tr>
           <td colspan="4"></td>
         </tr>
           <tr>
             <td colspan="4"></td>
           </tr>
           <tr>
             <td colspan="4"></td>
           </tr>
           <tr>
             <td colspan="4"></td>
           </tr>
         <th colspan="4">
             <font color="black" face="Courier New" size="6">
              
               Cadena Original del Complemento de certificación digital del SAT
             </font>
           </th>
         </tr>
         <tr>
           <td colspan="4">
             <font color="black" face="Courier New" size="6">
               <pre>
                 <b>
                   
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 0, 130)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 130, 130)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 260, 130)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/CadenaTFD, 390)"/>
                   
                 </b>
               </pre>
             </font>

           </td>
         </tr>

         <tr>
           <th colspan="4">
             <font color="black" face="Courier New" size="6">
               Sello Digital Emisor
             </font>
           </th>
         </tr>

         <tr>
           <td colspan="4">
             <font color="black" face="Courier New" size="6">
               <pre>
                 <b>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 0, 150)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 150, 150)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 300, 150)"/>
                   <br></br>
                   <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloCFD, 450)"/>
                   
                 </b>
               </pre>
             </font>
           </td>
         </tr>





         <tr width="100%">

           <th colspan="4">
             <font color="black" face="Courier New" size="6">
               Sello Digital SAT
             </font>
           </th>
         </tr>
         <tr>
           <td colspan="4">
             <font color="black" face="Courier New" size="6">
               <pre>
                 <b>
                   <center>
                     <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 0, 150)"/>
                     <br></br>
                     <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 150, 150)"/>
                     <br></br>
                     <xsl:value-of select="substring(document('cad.xml')/Complemento/SelloSAT, 300)"/>
                     
                   </center>
                 </b>
               </pre>

             </font>
           </td>

         </tr>




         <tr>

           <td>
             <br></br>
             <font color="black" face="Courier New" size="8">
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
 
     
 
<xsl:template match="//cfdi:Concepto">

    <tr>
      <td align="center" width="10%">
        
        
        <font color="black" face="Courier New" size="9">
          <xsl:value-of select='format-number(@cantidad,"###,###,###")'/> 
        </font>
        
      </td>
      <td width="50%">
        <font color="black" face="Courier New" size="10">
          <pre>
          <xsl:value-of select="@descripcion"/>
          </pre>
        </font>
      </td>
      
      <td align="center" width="20%">       
        <font color="black" face="Courier New" size="9">
          <xsl:value-of select='format-number(@valorUnitario,"$###,###,###.00")'/>

          
        </font>
        
      </td>
      <td align="right" width="20%">
        <font color="black" face="Courier New" size="11">

          <b>
            <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
          </b>
        </font>
      </td>
    </tr>
  <tr width='100%'>
    <td width='100%' colspan='4'>
      <hr></hr>
    </td>
  </tr>
  
</xsl:template>


  <xsl:template match="//nomina12:Deduccion">

    <tr>

      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b><xsl:value-of select="@TipoDeduccion"/>
          </b>
        </font>

      </td>
      
      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b><xsl:value-of select="@Clave"/>
          </b>
        </font>

      </td>
      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
            <xsl:value-of select="@Concepto"/>
            </b>
          </pre>
        </font>
      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
            <xsl:value-of select="@Importe"/>
            </b>
          </pre>
        </font>
      </td>

      <!--
      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
            <xsl:value-of select="@ImporteGravado"/>
            </b>
          </pre>
        </font>
      </td>
-->
    </tr>
    
  </xsl:template>
  
  <xsl:template match="//nomina12:Percepcion">

    <tr>

      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b><xsl:value-of select="@TipoPercepcion"/>
          </b>
        </font>

      </td>

      
      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b>
            <xsl:value-of select="@Clave"/>
          </b>
        </font>

      </td>
      
      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
              <xsl:value-of select="@Concepto"/>
            </b>
          </pre>
        </font>
      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
            <xsl:value-of select="@ImporteExento"/>
            </b>
          </pre>
        </font>
      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
            <xsl:value-of select="@ImporteGravado"/>
            </b>
          </pre>
        </font>
      </td>
      
     
      
    </tr>
  
  </xsl:template>


  <xsl:template match="//nomina12:HorasExtra">

    <tr>

      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b> 
            <xsl:value-of select="@Dias"/>
          </b>
        </font>

      </td>


      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b> 
            <xsl:value-of select="@HorasExtra"/>
          </b>
        </font>

      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b> 
              <xsl:value-of select="@TipoHoras"/>
            </b>
          </pre>
        </font>
      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
             
            </b>
          </pre>
        </font>
      </td>

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
              <xsl:value-of select="@ImportePagado"/>
            </b>
          </pre>
        </font>
      </td>



    </tr>

  </xsl:template>


  <xsl:template match="//nomina12:Incapacidad">

    <tr>

      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b>
            <xsl:value-of select="@TipoIncapacidad"/>
          </b>
        </font>

      </td>


      <td align="center" width="10%">


        <font color="black" face="Courier New" size="12">
          <b>
            <xsl:value-of select="@DiasIncapacidad"/>
          </b>
        </font>

      </td>

     

     

      <td width="50%">
        <font color="black" face="Courier New" size="12">
          <pre>
            <b>
              <xsl:value-of select="@Descuento"/>
            </b>
          </pre>
        </font>
      </td>



    </tr>

  </xsl:template>

  <xsl:template match="//cfdi:Traslado">
 
  
    <tr>

      <td align="right">
        <font color="black" face="Courier New" size="8">
          <xsl:value-of select="@impuesto"/>

          <font color="White" face="Courier New" size="8">_</font>
          
          
        </font>
      </td>
      <td align="right">
        <font color="black" face="Courier New" size="8">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")' />
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="black" face="Courier New" size="8">

         

        </font>
      </td>
      <td align="right">
        <font color="black" face="Courier New" size="8">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
        </font>
      </td>

    </tr>
  </xsl:template>



</xsl:stylesheet>

 



