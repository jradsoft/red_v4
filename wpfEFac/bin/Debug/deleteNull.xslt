<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
<xsl:template match="/">
  <xsl:apply-templates select="node()" />
</xsl:template>
<xsl:template match="/*">

  <xsl:copy>
    <xsl:apply-templates select="*|@*" />
  </xsl:copy>
</xsl:template>
<xsl:template match="node()">
  <xsl:if test="not(normalize-space(text()) = '' and count(./@*[normalize-space()!='']|.//*/@*[normalize-space()!='']) = 0)">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:if>
</xsl:template>

<xsl:template match="@*">
  <xsl:if test="normalize-space() != ''">
    <xsl:copy-of select="." />
  </xsl:if>
</xsl:template>
</xsl:stylesheet>

