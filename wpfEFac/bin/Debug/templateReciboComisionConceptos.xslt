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
    <legend>
      <font color="#0B0B6" face="Century Gothic" size="6">
        POR CONCEPTO DE:
      </font>
    </legend>
       <table width="100%" border="0">


         


           <tr><th>
               <font color="#0B0B6" face="Century Gothic" size="6">
                 
               </font>
                 </th>
               
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="8">
                     Descripcion
                   </font>
                     
                     </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="6">
                    
                   </font>
                                  
                 </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="8">
                     Importe
                   </font>
                     </th>
             </tr>


         <tr>

           <td> </td>

           <td align="left">
             <font color="#0B0B6" face="Courier New" size="7">
               
               <PRE>
                 <b>  <xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
                   </b>
               </PRE>
               
             </font>

           </td>

           <td> </td>
           <td align="right"></td>

         </tr>


         
         <font color="#0B0B6" face="Courier New" size="9">
           
           <xsl:apply-templates select="//cfdi:Concepto"/>
           <xsl:for-each select="Concepto">
             
           </xsl:for-each>

         </font>



         <tr>

           <td> </td>

           <td align="left">
             <font color="#0B0B6" face="Courier New" size="8">
               <PRE>
                 <b>
                 <xsl:value-of select="document('cad.xml')/Complemento/Observaciones"/>
                 </b>
               </PRE>
             </font>

           </td>

           <td> </td>
           <td align="right"></td>

         </tr>



         <table width="100%" border="0" style="background:url('fondoo.jpg') no-repeat bottom">

           


           <tr>
             <th width="90%"></th>
             <th width="10%"></th>


           </tr>


           
           
           <tr>

             <td align="right">
               <font color="#0B0B6" face="Courier New" size="9">
                 <p style="word-spacing: 2em;">
                   SUBTOTAL:     
                 </p>
               </font>
                 </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="9">
                 <xsl:value-of select='format-number(@subTotal,"$###,###,###.00")'/>
               </font>
             </td>
           </tr>


           <font color="#0B0B6" face="Courier New" size="9">
             <xsl:apply-templates select="//cfdi:Traslado"/>
           </font>

           <font color="#0B0B6" face="Courier New" size="9">
             <xsl:for-each select="Traslado">
             </xsl:for-each>
           </font>

           <font color="#0B0B6" face="Courier New" size="9">
             <xsl:apply-templates select="//cfdi:Retencion"/>
             <xsl:for-each select="Retencion">
             </xsl:for-each>
           </font>

           <tr>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="9">
                 <b>
                   TOTAL: 
                 </b>
               </font>
                 </td>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="9">
                 <b>
                   <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>
           </tr>



           <tr>

             <td> </td>

             <td align="left">
               <font color="#0B0B6" face="Courier New" size="9">
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


         </table>




       </table>
         
	
       
        <hr/>


       <table width="100%" border="0">


         <tr>
           <td>
             <center>
               <font color="#0B0B6" face="Courier New" size="8">
                 <b>
                   <pre> E F E C T O S   F I S C A L E S   A L   P A G O </pre>
                 </b>
               </font>
             </center>
           </td>
         </tr>



   



         <tr>
           <th>
             <font color="#0B0B6" face="Courier New" size="8">
               Cantidad con letra
             </font>
               </th>
         </tr>

         <tr>
           <td>
             <font color="#0B0B6" face="Courier New" size="9">
               <xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/>
             </font>
           </td>
         </tr>


      
         
             <tr>
               <th>
                 <font color="#0B0B6" face="Courier New" size="9">
               Cadena Original del Complemento de certificación digital del SAT
               </font>
               </th>
             </tr>
             <tr>
               <td>
                 <font color="#0B0B6" face="Courier New" size="9">
                   <pre>
                     <b>
                       <xsl:value-of select="document('cad.xml')/Complemento/CadenaTFD"/>
                     </b>
                     </pre>
                 </font>
                 
               </td>
             </tr>
             
              <tr>
               <th>
                 <font color="#0B0B6" face="Courier New" size="9">
                 Sello Digital Emisor
                   </font>
                   </th>
             </tr>

             <tr>
               <td>
                 <font color="#0B0B6" face="Courier New" size="9">
                   <pre>
                   <b>
                     <xsl:value-of select="document('cad.xml')/Complemento/SelloCFD"/>
                   </b>
                     </pre>
                 </font>
               </td>
             </tr>


         


         <tr width="100%">

           <th >
             <font color="#0B0B6" face="Courier New" size="8">
             Sello Digital SAT
               </font>
               </th>
         </tr>
         <tr>
           <td width="50%">
             <font color="#0B0B6" face="Courier New" size="9">
               <pre>
                 <b>
                   <center>
               <xsl:value-of select="document('cad.xml')/Complemento/SelloSAT"/>
                   </center>
               </b>
               </pre>

             </font>
           </td>
           
         </tr>


        

         <tr>

           <td>
             <br></br>
             <font color="#0B0B6" face="Courier New" size="8">
               <b> </b>
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
        
        <!--
        <font color="#0B0B6" face="Courier New" size="9">
            <xsl:value-of select="@cantidad"/> 
        </font>
        -->
      </td>
      <td width="50%">
        <font color="#0B0B6" face="Courier New" size="11">
          <pre>
          <xsl:value-of select="@descripcion"/>
          </pre>
        </font>
      </td>
      <td align="right" width="20%">

        <!--
        <font color="#0B0B6" face="Courier New" size="9">
          <xsl:value-of select='format-number(@valorUnitario,"$###,###,###.00")'/>

          
        </font>
        -->
      </td>
      <td align="right" width="20%">
        <font color="#0B0B6" face="Courier New" size="11">

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





  
  <xsl:template match="//cfdi:Traslado">
 
  
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="8">
          <xsl:value-of select="@impuesto"/>

          <font color="White" face="Courier New" size="8">_</font>
          
          <xsl:value-of select='format-number(@tasa,"###,###,###")'/> %
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="8">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")' />
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="8">

          RET. <xsl:value-of select="@impuesto"/>

          4 %

        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="8">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
        </font>
      </td>

    </tr>
  </xsl:template>



</xsl:stylesheet>

 



