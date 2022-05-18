<xsl:stylesheet version = '1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:cfd='http://www.sat.gob.mx/cfd/2'>
 
<xsl:output method = "text" /> 
 
<xsl:template match="/">|<xsl:apply-templates select="//cfd:Comprobante"/>||</xsl:template>
 
<xsl:template match="cfd:Comprobante">
      <xsl:if test="@version">|<xsl:value-of select="@version"/></xsl:if>
      <xsl:if test="@serie">|<xsl:value-of select="@serie"/></xsl:if>
      <xsl:if test="@folio">|<xsl:value-of select="@folio"/></xsl:if>
      <xsl:if test="@fecha">|<xsl:value-of select="@fecha"/></xsl:if>
      <xsl:if test="@noAprobacion">|<xsl:value-of select="@noAprobacion"/></xsl:if>
      <xsl:if test="@anoAprobacion">|<xsl:value-of select="@anoAprobacion"/></xsl:if>
      <xsl:if test="@tipoDeComprobante">|<xsl:value-of select="@tipoDeComprobante"/></xsl:if>
      <xsl:if test="@formaDePago">|<xsl:value-of select="@formaDePago"/></xsl:if>
      <xsl:if test="@condicionesDePago">|<xsl:value-of select="@condicionesDePago"/></xsl:if>
      <xsl:if test="@subTotal">|<xsl:value-of select="@subTotal"/></xsl:if>
      <xsl:if test="@descuento">|<xsl:value-of select="@descuento"/></xsl:if>
      <xsl:if test="@total">|<xsl:value-of select="@total"/></xsl:if>
      <xsl:apply-templates select="//cfd:Emisor"/>
      <xsl:apply-templates select="//cfd:DomicilioFiscal"/>
      <xsl:apply-templates select="//cfd:ExpedidoEn"/>
      <xsl:apply-templates select="//cfd:Receptor"/>
      <xsl:apply-templates select="//cfd:Domicilio"/>
      <xsl:apply-templates select="//cfd:Concepto"/>
      <xsl:apply-templates select="//cfd:Impuestos"/>
</xsl:template>
 
<xsl:template match="cfd:Emisor">
      <xsl:if test="@rfc">|<xsl:value-of select="@rfc"/></xsl:if>
      <xsl:if test="@nombre">|<xsl:value-of select="@nombre"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:DomicilioFiscal">
      <xsl:if test="@calle">|<xsl:value-of select="@calle"/></xsl:if>
      <xsl:if test="@noExterior">|<xsl:value-of select="@noExterior"/></xsl:if>
      <xsl:if test="@noInterior">|<xsl:value-of select="@noInterior"/></xsl:if>
      <xsl:if test="@colonia">|<xsl:value-of select="@colonia"/></xsl:if>
      <xsl:if test="@localidad">|<xsl:value-of select="@localidad"/></xsl:if>
      <xsl:if test="@referencia">|<xsl:value-of select="@referencia"/></xsl:if>
      <xsl:if test="@municipio">|<xsl:value-of select="@municipio"/></xsl:if>
      <xsl:if test="@estado">|<xsl:value-of select="@estado"/></xsl:if>
      <xsl:if test="@pais">|<xsl:value-of select="@pais"/></xsl:if>
      <xsl:if test="@codigoPostal">|<xsl:value-of select="@codigoPostal"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:ExpedidoEn">
      <xsl:if test="@calle">|<xsl:value-of select="@calle"/></xsl:if>
      <xsl:if test="@noExterior">|<xsl:value-of select="@noExterior"/></xsl:if>
      <xsl:if test="@noInterior">|<xsl:value-of select="@noInterior"/></xsl:if>
      <xsl:if test="@colonia">|<xsl:value-of select="@colonia"/></xsl:if>
      <xsl:if test="@localidad">|<xsl:value-of select="@localidad"/></xsl:if>
      <xsl:if test="@refrencia">|<xsl:value-of select="@referencia"/></xsl:if>
      <xsl:if test="@municipio">|<xsl:value-of select="@municipio"/></xsl:if>
      <xsl:if test="@estado">|<xsl:value-of select="@estado"/></xsl:if>
      <xsl:if test="@pais">|<xsl:value-of select="@pais"/></xsl:if>
      <xsl:if test="@codigoPostal">|<xsl:value-of select="@codigoPostal"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:Receptor">
      <xsl:if test="@rfc">|<xsl:value-of select="@rfc"/></xsl:if>
      <xsl:if test="@nombre">|<xsl:value-of select="@nombre"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:Domicilio">
      <xsl:if test="@calle">|<xsl:value-of select="@calle"/></xsl:if>
      <xsl:if test="@noExterior">|<xsl:value-of select="@noExterior"/></xsl:if>
      <xsl:if test="@noInterior">|<xsl:value-of select="@noInterior"/></xsl:if>
      <xsl:if test="@colonia">|<xsl:value-of select="@colonia"/></xsl:if>
      <xsl:if test="@localidad">|<xsl:value-of select="@localidad"/></xsl:if>
      <xsl:if test="@refrencia">|<xsl:value-of select="@referencia"/></xsl:if>
      <xsl:if test="@municipio">|<xsl:value-of select="@municipio"/></xsl:if>
      <xsl:if test="@estado">|<xsl:value-of select="@estado"/></xsl:if>
      <xsl:if test="@pais">|<xsl:value-of select="@pais"/></xsl:if>
      <xsl:if test="@codigoPostal">|<xsl:value-of select="@codigoPostal"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:Concepto">
      <xsl:if test="@cantidad">|<xsl:value-of select="@cantidad"/></xsl:if>
      <xsl:if test="@unidad">|<xsl:value-of select="@unidad"/></xsl:if>
      <xsl:if test="@noIdentificacion">|<xsl:value-of select="@noIdentificacion"/></xsl:if>
      <xsl:if test="@descripcion">|<xsl:value-of select="@descripcion"/></xsl:if>
      <xsl:if test="@valorUnitario">|<xsl:value-of select="@valorUnitario"/></xsl:if>
      <xsl:if test="@importe">|<xsl:value-of select="@importe"/></xsl:if>
      <xsl:apply-templates select="InformacionAduanera"/>
</xsl:template>
 
<xsl:template match="cfd:Impuestos">
      <xsl:apply-templates select="//cfd:Retencion"/>
      <xsl:if test="@totalImpuestosRetenidos">|<xsl:value-of select="@totalImpuestosRetenidos"/></xsl:if>
      <xsl:apply-templates select="//cfd:Traslado"/>
      <xsl:if test="@totalImpuestosTrasladados">|<xsl:value-of select="@totalImpuestosTrasladados"/></xsl:if>
  </xsl:template>
 
<xsl:template match="cfd:InformacionAduanera">
      <xsl:if test="@numero">|<xsl:value-of select="@numero"/></xsl:if>
      <xsl:if test="@fecha">|<xsl:value-of select="@fecha"/></xsl:if>
      <xsl:if test="@aduana">|<xsl:value-of select="@aduana"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:Retencion">
      <xsl:if test="@impuesto">|<xsl:value-of select="@impuesto"/></xsl:if>
      <xsl:if test="@importe">|<xsl:value-of select="@importe"/></xsl:if>
</xsl:template>
 
<xsl:template match="cfd:Traslado">
      <xsl:if test="@impuesto">|<xsl:value-of select="@impuesto"/></xsl:if>
      <xsl:if test="@tasa">|<xsl:value-of select="@tasa"/></xsl:if>
      <xsl:if test="@importe">|<xsl:value-of select="@importe"/></xsl:if>
</xsl:template>
 
</xsl:stylesheet>