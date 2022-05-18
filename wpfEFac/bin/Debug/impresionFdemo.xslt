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
   <body background="factinvalida.jpg">
   <table width="100%" border="0">
      <tr>
        <td>    

<img src="logo.jpg" height="150" align="150" alt="PUTO LOGO SI SALE ES UNA LInda poposita"/></td>

       
	    <td colspan="2" align="right">
          <table border="2" bordercolor="blue">
            <tr>
              <td class="h2" align="center">
                <center>
			FACTURA SIN VALOR FISCAL
				</center>
              </td>
            </tr>
			
               <tr><th class="h1">Serie</th><td class="h1"><xsl:value-of select="@serie"/></td></tr>
               <tr><th class="h1">Folio</th><td class="h1"><xsl:value-of select="@folio"/></td></tr>
               <tr><th class="h1">Fecha</th><td class="h1"><xsl:value-of select="@fecha"/></td></tr>
               <tr><th class="h1">Aprobacion</th><td class="h1"><xsl:value-of select="@noAprobacion"/></td>
			   </tr>
           </table>
           </td>
           </tr>
      <tr><td width="100%">
				 <table width="100%" border="0"><tr><th colspan="2" class="h1">Emisor</th></tr>
               <tr><th>RFC</th><td><xsl:value-of select="cfd:Emisor/@rfc"/></td></tr>
               <tr><th>Nombre</th><td><xsl:value-of select="cfd:Emisor/@nombre"/></td></tr>
                <xsl:apply-templates select="//cfd:DomicilioFiscal"/>
 
             </table> 
          </td>
          <td>
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
 

    <tr><td colspan="2" align="right"></td>
        <td align="right">Subtotal</td>
        <td><xsl:value-of select="@subTotal"/></td>
    </tr>

             <xsl:apply-templates select="//cfd:Traslado"/>
             <xsl:for-each select="Traslado">
             </xsl:for-each>
 

             <xsl:apply-templates select="//cfd:Retencion"/>
             <xsl:for-each select="Retencion">
             </xsl:for-each>
           
    <tr><td colspan="2" align="right"></td>
        <td align="right">Total</td>
        <td><xsl:value-of select="@total"/></td>
    </tr>


            </table>
         
	</tr>
        </table>
        <hr/>
        
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
		
		<center>
		<table border="0">
		 <tr>
		 <td>
		 <img src="tamsa.gif"/><td></td><td><img src="ColocarImagenAki.jpg" height="150" align="150"/></td><td><img src="tamsa.gif"/></td>
		 </td>
		 </tr>
		 </table>
         </center>

    </body>
	    </html>
</xsl:template>
 
 
<xsl:template match="//cfd:DomicilioFiscal">
    <tr><th colspan="2" class="h2">Domicilio</th></tr>
    <tr><td colspan="2"><xsl:value-of select="@calle"/> # <xsl:value-of select="@noExterior"/> - <xsl:value-of select="@noInterior"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@colonia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@localidad"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@referencia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@municipio"/>
    <xsl:if test="@codigoPostal"> CODIGO POSTAL <xsl:value-of select="@codigoPostal"/></xsl:if>
         </td></tr>
     <tr><td colspan="2"><xsl:value-of select="@estado"/></td></tr>
     <tr><td colspan="2"><xsl:value-of select="@pais"/></td></tr>
</xsl:template>
 
<xsl:template match="//cfd:Domicilio">
    <tr><th colspan="2" class="h2">Domicilio</th></tr>
    <tr><td colspan="2"><xsl:value-of select="@calle"/> # <xsl:value-of select="@noExterior"/> - <xsl:value-of select="@noInterior"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@colonia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@localidad"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@referencia"/></td></tr>
    <tr><td colspan="2"><xsl:value-of select="@municipio"/>
        <xsl:if test="@codigoPostal"> CODIGO POSTAL <xsl:value-of select="@codigoPostal"/></xsl:if>
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
        <td align="right"></td>
	<td colspan="2" align="right"><xsl:value-of select="@impuesto"/></td>
	<td align="right"><xsl:value-of select="@importe"/></td>
    </tr>
</xsl:template>
 

<xsl:template match="//cfd:Retencion">
    <tr>
        <td align="right"></td>
        <td colspan="2" align="right">RET. <xsl:value-of select="@impuesto"/></td>
        <td align="right"><xsl:value-of select="@importe"/></td>

    </tr>
</xsl:template>


</xsl:stylesheet><!-- Stylus Studio meta-information - (c) 2004-2009. Progress Software Corporation. All rights reserved.

<metaInformation>
	<scenarios>
		<scenario default="yes" name="Scenario1" userelativepaths="no" externalpreview="yes" url="file:///c:/AnunciosGolfo/ModeloArrendamientoCFD2/ModeloArrendamiento/JuarezHegocarAdsoft/JuarezHegocar/eFac_08dic2009/wpfEFac/bin/Debug/adesoft.xml"
		          htmlbaseurl="" outputurl="file:///c:/MESMAMENTE/bazofita.htm" processortype="saxon8" useresolver="no" profilemode="0" profiledepth="" profilelength="" urlprofilexml="" commandline="" additionalpath="" additionalclasspath=""
		          postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext="" validateoutput="no" validator="internal" customvalidator="">
			<advancedProp name="sInitialMode" value=""/>
			<advancedProp name="bXsltOneIsOkay" value="true"/>
			<advancedProp name="bSchemaAware" value="false"/>
			<advancedProp name="bXml11" value="false"/>
			<advancedProp name="iValidation" value="0"/>
			<advancedProp name="bExtensions" value="true"/>
			<advancedProp name="iWhitespace" value="0"/>
			<advancedProp name="sInitialTemplate" value=""/>
			<advancedProp name="bTinyTree" value="true"/>
			<advancedProp name="bWarnings" value="true"/>
			<advancedProp name="bUseDTD" value="false"/>
			<advancedProp name="iErrorHandling" value="fatal"/>
		</scenario>
	</scenarios>
	<MapperMetaTag>
		<MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no">
			<SourceSchema srcSchemaPath="file:///h:/cad.xml" srcSchemaRoot="Complemento" AssociatedInstance="file:///c:/AnunciosGolfo/ModeloArrendamientoCFD2/ModeloArrendamiento/JuarezHegocarAdsoft/JuarezHegocar/eFac_08dic2009/wpfEFac/bin/Debug/cad.xml"
			              loaderFunction="document" loaderFunctionUsesURI="no"/>
		</MapperInfo>
		<MapperBlockPosition>
			<template match="//cfd:Comprobante">
				<block path="html/head/title/xsl:value-of" x="171" y="144"/>
				<block path="html/head/title/xsl:value-of[1]" x="211" y="144"/>
				<block path="html/body/table/tr/td[1]/table/tr/td/xsl:value-of" x="131" y="58"/>
				<block path="html/body/table/tr/td[1]/table/tr[1]/td/xsl:value-of" x="91" y="58"/>
				<block path="html/body/table/tr/td[1]/table/tr[2]/td/xsl:value-of" x="51" y="58"/>
				<block path="html/body/table/tr/td[1]/table/tr[3]/td/xsl:value-of" x="11" y="58"/>
				<block path="html/body/table/tr/td[1]/table/tr[4]/td/xsl:value-of" x="171" y="18"/>
				<block path="html/body/table/tr[1]/td/table/tr[1]/td/xsl:value-of" x="211" y="18"/>
				<block path="html/body/table/tr[1]/td/table/tr[2]/td/xsl:value-of" x="131" y="18"/>
				<block path="html/body/table/tr[1]/td/table/xsl:apply-templates" x="91" y="98"/>
				<block path="html/body/table/tr[1]/td[1]/table/tr[1]/td/xsl:value-of" x="91" y="18"/>
				<block path="html/body/table/tr[1]/td[1]/table/tr[2]/td/xsl:value-of" x="51" y="18"/>
				<block path="html/body/table/tr[1]/td[1]/table/xsl:apply-templates" x="51" y="98"/>
				<block path="html/body/table/tr[2]/table/xsl:apply-templates" x="131" y="138"/>
				<block path="html/body/table/tr[2]/table/tr[1]/td[2]/xsl:value-of" x="11" y="98"/>
				<block path="html/body/table/tr[2]/table/xsl:apply-templates[1]" x="91" y="138"/>
				<block path="html/body/table/tr[2]/table/xsl:apply-templates[2]" x="51" y="138"/>
				<block path="html/body/table/tr[2]/table/tr[2]/td[2]/xsl:value-of" x="171" y="58"/>
				<block path="html/body/table[1]/tr[1]/td/xsl:value-of" x="11" y="138"/>
				<block path="html/body/table[1]/tr[3]/td/xsl:value-of" x="171" y="98"/>
				<block path="html/body/table[1]/tr[5]/td/xsl:value-of" x="211" y="98"/>
				<block path="html/body/table[1]/tr[7]/td/xsl:value-of" x="131" y="98"/>
				<block path="html/body/table[1]/tr[9]/td/small/small/xsl:value-of" x="211" y="58"/>
			</template>
		</MapperBlockPosition>
		<TemplateContext></TemplateContext>
		<MapperFilter side="source"></MapperFilter>
	</MapperMetaTag>
</metaInformation>
-->