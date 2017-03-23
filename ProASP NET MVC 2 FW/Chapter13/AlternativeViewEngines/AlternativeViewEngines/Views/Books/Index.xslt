<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <h1>My Favorite Books</h1>
    <ol>
      <xsl:for-each select="Books/Book">
        <li>
          <b>
            <xsl:value-of select="@title"/>
          </b>
          <xsl:text> by </xsl:text>
          <xsl:value-of select="@author"/>
        </li>
      </xsl:for-each>
    </ol>
  </xsl:template>
</xsl:stylesheet>