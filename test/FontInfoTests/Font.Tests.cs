using Xunit;
using FontInfo;

namespace FontInfoTests
{
    public class FontTests
    {   

        [Fact]
        public void ShouldReadDetailsForTTFFont()
        {
            Font font = new Font(Constants.TTFFontFilename);
            
            
            Assert.Equal("Copyright 2011 Google Inc. All Rights Reserved.", font.Details.Copyright);
            Assert.Equal("Google", font.Details.Designer);
            Assert.Equal("Christian Robertson", font.Details.URLDesigner);
            Assert.Equal("Roboto", font.Details.Family);
            Assert.Equal("Regular", font.Details.Subfamily);
            Assert.Equal("Roboto", font.Details.FullName);
            Assert.Equal("Licensed under the Apache License, Version 2.0", font.Details.LicenseDescription);
            Assert.Equal("http://www.apache.org/licenses/LICENSE-2.0", font.Details.LicenseURL);
            Assert.Equal("Google.com", font.Details.URLVendor);
            Assert.Equal("Roboto-Regular", font.Details.PostScriptName);
            Assert.Equal("Roboto is a trademark of Google.", font.Details.Trademark);
            Assert.Equal("Roboto", font.Details.UniqueID);
            Assert.Equal("Version 2.137; 2017", font.Details.Version);
            Assert.Equal(2.137, font.Details.FontRevision);
            Assert.Equal(1, font.Details.MajorVersion);
            Assert.Equal(0, font.Details.MinorVersion);
        }

        [Fact]
        public void ShouldReadMetricsForTTFFont()
        {
            Font font = new Font(Constants.TTFFontFilename);
            
            Assert.Equal((uint)1946, font.Metrics.Ascender);
            Assert.Equal((uint)512, font.Metrics.Descender);
            Assert.Equal((uint)1946 + 512, font.Metrics.Height);
            Assert.Equal((uint)1946 + 512 + 102, font.Metrics.LineSpacing);
            Assert.Equal((uint)2048, font.Metrics.UnitsPerEm);

        }


        [Fact]
        public void ShouldReadDetailsForOTFFont()
        {
            Font font = new Font(Constants.OTFFontFilename);

            Assert.Equal("Copyright (c) 2014 by Julieta Ulanovsky. All rights reserved, Design Modifications and new weights 2015, Jasper @ Cannot Into Space Fonts", font.Details.Copyright);
            Assert.Equal("Julieta Ulanovsky", font.Details.Designer);
            Assert.Equal("http://www.zkysky.com.ar/", font.Details.URLDesigner);
            Assert.Equal("Trueno", font.Details.Family);
            Assert.Equal("Rg", font.Details.Subfamily);
            Assert.Equal("Trueno", font.Details.FullName);
            Assert.Equal("This Font Software is licensed under the SIL Open Font License, Version 1.1. This license is available with a FAQ at: http://scripts.sil.org/OFL", font.Details.LicenseDescription);
            Assert.Equal("http://scripts.sil.org/OFL", font.Details.LicenseURL);
            Assert.Equal("Julieta Ulanovsky", font.Details.Manufacturer);            
            Assert.Equal("http://www.zkysky.com.ar/", font.Details.URLVendor);
            Assert.Equal("TruenoRg", font.Details.PostScriptName);            
            Assert.Equal("Trueno v3.001b", font.Details.UniqueID);
            Assert.Equal("Version 3.001b ", font.Details.Version);
            Assert.Equal(1, font.Details.FontRevision);
            Assert.Equal(1, font.Details.MajorVersion);
            Assert.Equal(0, font.Details.MinorVersion);
        }

        [Fact]
        public void ShouldReadMetricsForOTFFont()
        {
            Font font = new Font(Constants.OTFFontFilename);

            Assert.Equal((uint)1006, font.Metrics.Ascender);
            Assert.Equal((uint)194, font.Metrics.Descender);
            Assert.Equal((uint)1006 + 194, font.Metrics.Height);
            Assert.Equal((uint)1006 + 194 + 0, font.Metrics.LineSpacing);
            Assert.Equal((uint)1000, font.Metrics.UnitsPerEm);

        }

    }
}