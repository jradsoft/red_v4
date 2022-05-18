<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfd='http://www.sat.gob.mx/cfd/2'>
 
<xsl:output method = "html" /> 
<xsl:param name="id" select="."/>
 
<xsl:template match="//cfd:Comprobante">
   <html>
   <head>
   <link rel="STYLESHEET" media="screen" type="text/css" href="factura.css"/>
   <title>Factura Electronica <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/></title>
   </head>
   <body>

<table width="100%" border="0">
      <tr>
        <td>
            <h1> <xsl:value-of select="cfd:Emisor/@nombre"/> </h1>
            <br></br>
                <xsl:value-of select="document('cad.xml')/Complemento/TipoComprobante"/>



        </td>

       
	    <td colspan="2" align="right">
          <table border="2" bordercolor="black">
               <tr><th class="h1">Serie</th><td class="h1"><xsl:value-of select="@serie"/></td></tr>
               <tr><th class="h1">Folio</th><td class="h1"><xsl:value-of select="@folio"/></td></tr>
               <tr><th class="h1">Fecha</th><td class="h1"><xsl:value-of select="@fecha"/></td></tr>
               <tr><th class="h1">Aprobacion</th><td class="h1"><xsl:value-of select="@noAprobacion"/></td>
			   </tr>

          </table>
           </td>
           </tr>
      
 
  <tr>
     <td width="50%">
				 <table width="100%" border="0"><tr><th colspan="2" class="h1">Emisor</th></tr>
               <tr><th>RFC</th><td><xsl:value-of select="cfd:Emisor/@rfc"/></td></tr>
               <tr><th>Nombre</th><td><xsl:value-of select="cfd:Emisor/@nombre"/></td></tr>
                <xsl:apply-templates select="//cfd:DomicilioFiscal"/>
 
             </table> 
          </td>
          <td width="50%">
          <table width="100%" border="0"><tr><th colspan="2" class="h1">Receptor</th></tr>
             <tr><th>RFC</th><td><xsl:value-of select="cfd:Receptor/@rfc"/></td></tr>
             <tr><th>Nombre</th><td><xsl:value-of select="cfd:Receptor/@nombre"/></td></tr>
             <xsl:apply-templates select="//cfd:Domicilio"/>
 
 
           </table>
           </td>
         </tr>
		 <tr><table width="100%" border="0">
             <tr><th>Cantidad</th>
                 <th>Descripcion</th>
                 <th>Precio</th>
                 <th>Importe</th>
             </tr>

 
            <xsl:apply-templates select="//cfd:Concepto"/>
             <xsl:for-each select="Concepto">
             </xsl:for-each>
 


</table>
         
	    </tr>
<tr>
  
        <table width="100%">

          <tr>
            <th width="90%"></th>
            <th width="10%"></th>
            
            
          </tr>

          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <br></br>
          <tr>

                      <td align="right"  >Cantidad</td>
                      <td align="right" >
                        <xsl:value-of select="@subTotal"/>
                      </td>
                    </tr>

                    <xsl:apply-templates select="//cfd:Traslado"/>
                    <xsl:for-each select="Traslado">
                    </xsl:for-each>


                    <xsl:apply-templates select="//cfd:Retencion"/>
                    <xsl:for-each select="Retencion">
                    </xsl:for-each>

                    <tr>
                      <td align="right">Total</td>
                      <td align="right">
                        <xsl:value-of select="@total"/>
                      </td>
                    </tr>

   

        
     </table>
    
</tr>
     
     
</table>
        
       <table width="100%" border="0">
         <tr>
           <th>Observaciones</th>
         </tr>
         <tr>
           <td>
             <xsl:value-of select="document('cad.xml')/Complemento/Comentarios"/>
           </td>
         </tr>


         <tr><th>Cantidad Letra</th></tr>
            <tr><td>		<xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/></td></tr>
 

            <tr><th>Numero de serie del Certificado</th></tr>
            <tr><td><font color="#0B0B61" face="courier"><xsl:value-of select="@noCertificado"/></font></td></tr>
            <tr><th>Cadena Original</th></tr>
            <tr><td> 
		<font color="#0B0B61" face="courier"><xsl:value-of select="document('cad.xml')/Complemento/Cadena"/></font>

		</td></tr>
            <tr><th>Sello Digital</th></tr>

            <tr><td><small><small><font color="#0B0B61" face="courier"><center><xsl:value-of select="@sello"/></center></font></small></small></td></tr>
        </table>
  <br></br>
  
        <center><font color="#0B0B61" face="courier">
        <b>Este documento es una impresion de un comprobante fiscal digital</b>
        </font> </center>
		
    </body>
	    </html>
</xsl:template>
 
 
<xsl:template match="//cfd:DomicilioFiscal">
    <tr><th colspan="2" class="h2">Domicilio</th></tr>
    <tr><td colspan="2"><xsl:value-of select="@calle"/> - <xsl:value-of select="@noExterior"/>  <xsl:value-of select="@noInterior"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@colonia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@localidad"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@referencia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@municipio"/>
    <xsl:if test="@codigoPostal"> C.P. <xsl:value-of select="@codigoPostal"/></xsl:if>
         </td></tr>
     <tr><td colspan="2"><xsl:value-of select="@estado"/></td></tr>
     <tr><td colspan="2"><xsl:value-of select="@pais"/></td></tr>
</xsl:template>
 
<xsl:template match="//cfd:Domicilio">
    <tr><th colspan="2" class="h2">Domicilio</th></tr>
    <tr><td colspan="2"><xsl:value-of select="@calle"/> - <xsl:value-of select="@noExterior"/>  <xsl:value-of select="@noInterior"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@colonia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@localidad"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@referencia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@municipio"/>
        <xsl:if test="@codigoPostal"> C.P. <xsl:value-of select="@codigoPostal"/></xsl:if>
        </td></tr>
    <tr><td colspan="2"><xsl:value-of select="@estado"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@pais"/></td></tr>
</xsl:template>
 
<xsl:template match="//cfd:Concepto">
    <tr><td align="center"><xsl:value-of select="@cantidad"/></td>
        <td><xsl:value-of select="@descripcion"/></td>
        <td align="right"><xsl:value-of select="@valorUnitario"/></td>
        <td align="right"><xsl:value-of select="@importe"/></td>
    </tr>
</xsl:template>
 
<xsl:template match="//cfd:Traslado">
    <tr>
        
	<td align="right"><xsl:value-of select="@impuesto"/></td>
	<td align="right"><xsl:value-of select="@importe"/></td>
    </tr>
</xsl:template>
 

<xsl:template match="//cfd:Retencion">
    <tr>
        
        <td align="right">RET. <xsl:value-of select="@impuesto"/></td>
        <td align="right"><xsl:value-of select="@importe"/></td>

    </tr>
</xsl:template>


</xsl:stylesheet>

