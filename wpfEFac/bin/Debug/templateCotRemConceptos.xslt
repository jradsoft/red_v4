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
     <table>
     <tr>

       <td> </td>

       <td align="left">
         <font color="#0B0B6" face="Courier New" size="7">

           <PRE>
             <b>
               <xsl:value-of select="document('cad.xml')/Complemento/Encabezado"/>
             </b>
           </PRE>

         </font>

       </td>

       <td> </td>
       <td align="right"></td>

     </tr>
     </table>

     <fieldset>
    <legend>
      
      <h3>
        CONCEPTOS
      </h3>
      
    </legend>
       <table width="100%" border="0">
          
         
         
         <tr><th>
               <font color="#0B0B6" face="Century Gothic" size="7">
                 Cantidad
               </font>
                 </th>
               
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="7">
                     Descripcion
                   </font>
                     
                     </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="7">
                     Precio
                   </font>
                                  
                 </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="7">
                     Importe
                   </font>
                     </th>
             </tr>

         <font color="#0B0B6" face="Courier New" size="4">
           <xsl:apply-templates select="//cfdi:Concepto"/>
           <xsl:for-each select="Concepto">
           </xsl:for-each>

         </font>



         <tr>

           <td> </td>

           <td align="left">
             <font color="#0B0B6" face="Courier New" size="7">
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
               <font color="#0B0B6" face="Courier New" size="6">
                 <p style="word-spacing: 2em;">
                   SUBTOTAL:     
                 </p>
               </font>
                 </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="6">
                 <xsl:value-of select='format-number(@subTotal,"$###,###,###.000000")'/>
               </font>
             </td>
           </tr>


           <font color="#0B0B6" face="Courier New" size="6">
             <xsl:apply-templates select="//cfdi:Traslado"/>
           </font>

           <font color="#0B0B6" face="Courier New" size="6">
             <xsl:for-each select="Traslado">
             </xsl:for-each>
           </font>

           <font color="#0B0B6" face="Courier New" size="6">
             <xsl:apply-templates select="//cfdi:Retencion"/>
             <xsl:for-each select="Retencion">
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
                   <xsl:value-of select='format-number(@total,"$###,###,###.000000")'/>
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
           <td>
             <font color="white" face="Courier New" size="6">
               .........................................................................................................................................
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
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select="@cantidad"/>
        </font>
      </td>
      <td width="50%">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select="@descripcion"/>
          <pre></pre>
        </font>
      </td>
      <td align="right" width="20%">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select='format-number(@valorUnitario,"$###,###,###.000000")'/>

          
        </font>
      </td>
      <td align="right" width="20%">
        <font color="#0B0B6" face="Courier New" size="6">

          <b>
            <xsl:value-of select='format-number(@importe,"$###,###,###.000000")'/>
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
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select='format-number(@importe,"$###,###,###.000000")' />
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfdi:Retencion">
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          RET. <xsl:value-of select="@impuesto"/>
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="6">
          <xsl:value-of select='format-number(@importe,"$###,###,###.000000")'/>
        </font>
      </td>

    </tr>
  </xsl:template>



</xsl:stylesheet>

 



