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

     <fieldset>
   <body background="fondo.jpg">



    
     <table width="100%" border="0">
       <tr>
         <td>
           <center>
           <img src="logo.jpg" height="150" align="150"/>
           </center>
         </td>

         <td>
           <center>           
            <img src="muestrafiscalsinvalor.jpg" height="80" align="300"/>
           </center>
         </td>


         <td>

       <td colspan="2" align="right">
         
         <table border="0" bordercolor="blue">
           <tr>
             <td class="h2" align="center">
               <center>
                 <font color="#0B0B6" face="arial" size="6">
                   <xsl:value-of select="document('cad.xml')/Complemento/TipoComprobante"/>
                 </font>
               </center>
             </td>
           </tr>

           <tr>
             
               <th class="h1">Serie</th>
               <td class="h1">
                 <xsl:value-of select="@serie"/>
               </td>
            
           </tr>

           
             <tr>
               <th class="h1">Folio</th>
               <td class="h1">
                 <xsl:value-of select="@folio"/>
               </td>
             </tr>
          
             <tr>
             <th class="h1">Fecha</th>
             <td class="h1">
               <xsl:value-of select="@fecha"/>
             </td>
           </tr>
           <tr>
             <th class="h1">Aprobacion</th>
             <td class="h1">
		<xsl:value-of select="@anoAprobacion"/>/
               <xsl:value-of select="@noAprobacion"/>
             </td>
             
           </tr>
         </table>
           
       </td>
         </td>
           </tr>   
       
     </table>

    
       <table width="100%">

         <tr>
           <td width="50%">

            
             <fieldset>

             <table width="100%" border="0">

               <tr>
                 <th colspan="2" class="h1">Emisor</th>
               </tr>

               <tr width="100%">
                 
		<td>RFC : 
                 


                     <xsl:value-of select="cfd:Emisor/@rfc"/>

                 </td>
               </tr>

               <tr width="100%">
                 <td>Nombre :


                     <xsl:value-of select="cfd:Emisor/@nombre"/>

                 </td>
               </tr>

                 <xsl:apply-templates select="//cfd:DomicilioFiscal"/>


             </table>
             </fieldset>
           </td>




           <td width="50%">
             <fieldset>
             <table width="100%" border="0">
               <tr>
                 <th colspan="2" class="h1">CLIENTE</th>
               </tr>

               <tr width ="100%">
                 <th width="25%">RFC</th>
                 <td width="75%">

                     <xsl:value-of select="cfd:Receptor/@rfc"/>

                 </td>
               </tr>
               

		<tr>
                 <th>Nombre</th>
                 <td>
                   <font color="#0B0B6" face="Courier New" size="4">
                     <xsl:value-of select="cfd:Receptor/@nombre"/>
                   </font>
                 </td>
               </tr>
               
		<font color="#0B0B6" face="Courier New" size="4">
                 <xsl:apply-templates select="//cfd:Domicilio"/>
               </font>


             </table>
             </fieldset>
           </td>
         </tr>


       </table>

    
       <table width="100%" border="0">
             <tr><th>
               <font color="#0B0B6" face="Century Gothic" size="4">
                 Cantidad
               </font>
                 </th>
               
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="4">
                     Descripcion
                   </font>
                     
                     </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="4">
                     Precio
                   </font>
                                  
                 </th>
                 <th>
                   <font color="#0B0B6" face="Century Gothic" size="4">
                     Importe
                   </font>
                     </th>
             </tr>

         <font color="#0B0B6" face="Courier New" size="6">
           <xsl:apply-templates select="//cfd:Concepto"/>
           <xsl:for-each select="Concepto">
           </xsl:for-each>

         </font>
         
	 <tr>

		<td> </td>

		 <td align="left">
		
		<pre> <xsl:value-of select="document('cad.xml')/Complemento/Comentarios"/> </pre>
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
               <font color="#0B0B6" face="Courier New" size="5">
                 <p style="word-spacing: 2em;">
                   SUBTOTAL:     
                 </p>
               </font>
                 </td>
             <td align="right" >
               <font color="#0B0B6" face="Courier New" size="5">
                 <xsl:value-of select='format-number(@subTotal,"$###,###,###.00")'/>
               </font>
             </td>
           </tr>

           <font color="#0B0B6" face="Courier New" size="5">
             <xsl:apply-templates select="//cfd:Traslado"/>
           </font>

           <font color="#0B0B6" face="Courier New" size="5">
             <xsl:for-each select="Traslado">
             </xsl:for-each>
           </font>

           <font color="#0B0B6" face="Courier New" size="4">
             <xsl:apply-templates select="//cfd:Retencion"/>
             <xsl:for-each select="Retencion">
             </xsl:for-each>
           </font>

           <tr>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="5">
                 <b>
                   TOTAL: 
                 </b>
               </font>
                 </td>
             <td align="right">
               <font color="#0B0B6" face="Courier New" size="5">
                 <b>
                   <xsl:value-of select='format-number(@total,"$###,###,###.00")'/>
                 </b>
               </font>
             </td>
           </tr>




         </table>




       </table>
         
	
       
        <hr/>
        
       <table width="100%" border="0">
         
	

         <tr><th>Cantidad Letra</th></tr>
            <tr><td>		<xsl:value-of select="document('cad.xml')/Complemento/CantidadLetra"/></td></tr>
 

            <tr><th>Numero de serie del Certificado</th></tr>
            <tr><td><font color="#0B0B61" face="courier"><xsl:value-of select="@noCertificado"/></font></td></tr>
            <tr><th>Cadena Original</th></tr>
            <tr><td> 
		<font color="#0B0B61" face="courier"><xsl:value-of select="document('cad.xml')/Complemento/Cadena"/></font>

		</td></tr>
            <tr><th>Sello Digital</th></tr>

            <tr><td><font color="#0B0B61" face="courier">
              <xsl:value-of select="@sello"/>
            </font></td></tr>
        </table>
  <br></br>
  
        <center><font color="#0B0B61" face="courier">
        <b>Este documento es una impresion de un comprobante fiscal digital</b>
        </font> </center>
		
		<center>
		<table border="0">
		 <tr>
		 <td>
       <img src="myfacturae.png" height="300" align="1000"/>
		 </td>

	 <td>
       <img src="taxitel.jpg" height="400" align="1000"/>
		 </td>

     </tr>
		 </table>
         </center>

    </body>
     </fieldset>
	    </html>
</xsl:template>
 
 
<xsl:template match="//cfd:DomicilioFiscal">
  

<tr>
	<th colspan="2" class="h2">Domicilio</th></tr>
    	<tr><td colspan="2">

        <xsl:value-of select="@calle"/> <pre> </pre> <xsl:value-of select="@noExterior"/>
        <pre> </pre><xsl:value-of select="@noInterior"/>

        </td>
</tr>
   
<tr>

	<td>

	        <xsl:value-of select="@colonia"/> ,








        <xsl:value-of select="@municipio"/> ,












        	 <xsl:value-of select="@estado"/> ,
	




	         <xsl:value-of select="@pais"/> ,







              C.P. <xsl:value-of select="@codigoPostal"/>


        
         </td>


</tr>

<tr>
<td> <pre> </pre></td>
</tr>

<tr>
	<td width="100%">
		<center>

		Tel./Fax 01 (272)-7250787

		</center>
        </td>

</tr>
<tr>
	<td width="100%">
		<center>

		e-Mail: adolfo.centeno@adesoft.com.mx

		</center>
        </td>

</tr>


   
</xsl:template>









  
    <xsl:template match="//cfd:Domicilio">
 
    <tr><th colspan="2" class="h2">Domicilio</th></tr>
    <tr><td colspan="2">
      <font color="#0B0B6" face="Courier New" size="4">
        <xsl:value-of select="@calle"/> <pre> </pre> <xsl:value-of select="@noExterior"/>
        <pre> </pre> <xsl:value-of select="@noInterior"/>
      </font>
        </td>
	</tr>
      
    <tr><td colspan="2">
      <font color="#0B0B6" face="Courier New" size="4">
        <xsl:value-of select="@colonia"/>
      </font>
        </td></tr>

      <tr>
        <td colspan="2">
          <font color="#0B0B6" face="Courier New" size="4">
            <xsl:value-of select="@municipio"/>
          </font>
        </td>
      </tr>
      
	
    <tr><td colspan="2">
      <font color="#0B0B6" face="Courier New" size="4">
        <xsl:value-of select="@estado"/>
      </font>
        </td></tr>
    <tr><td colspan="2">
      <font color="#0B0B6" face="Courier New" size="4">
        <xsl:value-of select="@pais"/>
      </font>
    </td></tr>

<tr>
        <td colspan="2">
          <font color="#0B0B6" face="Courier New" size="4">
            <xsl:if test="@codigoPostal">
              C.P. <xsl:value-of select="@codigoPostal"/>
            </xsl:if>
          </font>
        </td></tr>
</xsl:template>
    
 
<xsl:template match="//cfd:Concepto">

    <tr>
      <td align="center">
        <font color="#0B0B6" face="Courier New" size="4">
          <xsl:value-of select="@cantidad"/>
        </font>
      </td>
      <td>
        <font color="#0B0B6" face="Courier New" size="4">
          <xsl:value-of select="@descripcion"/>
          <pre></pre>
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="4">
          <xsl:value-of select="@valorUnitario"/>
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="4">
          <xsl:value-of select="@importe"/>
        </font>
      </td>
    </tr>
  
</xsl:template>





  
  <xsl:template match="//cfd:Traslado">
 
  
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="5">
          <xsl:value-of select="@impuesto"/>
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="5">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")' />
        </font>
      </td>
    </tr>
  </xsl:template>


  <xsl:template match="//cfd:Retencion">
    <tr>

      <td align="right">
        <font color="#0B0B6" face="Courier New" size="5">
          RET. <xsl:value-of select="@impuesto"/>
        </font>
      </td>
      <td align="right">
        <font color="#0B0B6" face="Courier New" size="5">
          <xsl:value-of select='format-number(@importe,"$###,###,###.00")'/>
        </font>
      </td>

    </tr>
  </xsl:template>  
  
  
  
  

 




</xsl:stylesheet><!-- Stylus Studio meta-information - (c) 2004-2009. Progress Software Corporation. All rights reserved.

<metaInformation>
	<scenarios>
		<scenario default="yes" name="Scenario1" userelativepaths="no" externalpreview="yes" url="file:///c:/MESMAMENTE/XML_100_DIBD5912169C9_20101219_031140_OK.xml" htmlbaseurl="" outputurl="file:///c:/MESMAMENTE/bazofita.htm" processortype="saxon8"
		          useresolver="no" profilemode="0" profiledepth="" profilelength="" urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath=""
		          postprocessgeneratedext="" validateoutput="no" validator="internal" customvalidator="">
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