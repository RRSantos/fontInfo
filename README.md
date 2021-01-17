# FontInfo
Library to read basic informations from TTF/OTF files. 


![CI](https://github.com/RRSantos/fontInfo/workflows/CI%20for%20FontInfo/badge.svg)


![Publish package](https://github.com/RRSantos/fontInfo/workflows/Build%20and%20Publish%20package/badge.svg)

## Usage

```csharp
using FontInfo;

[...]

// Create new instace of Font class for font myFontName.ttf
Font font = new Font("myFontName.ttf");

// Accessing property FullName (FontDetails class)
Console.WriteLine($"Font fullname: {font.Details.FullName}");

// Accessing property LineSpacing (FontMetrics  class)
Console.WriteLine($"Font fullname: {font.Metrics.LineSpacing}");

```

## Available properties 
All font informations can be accessed by ``Details`` and ``Metrics`` groups.

|Group|Property|
|---|---|
| Details |CompatibleFullName|
| Details |Copyright |
| Details |DarkBackgroundPalette |
| Details |Description |
| Details |Designer |
| Details |Family |
| Details |FontRevision |
| Details |FullName |
| Details |LicenseDescription |
| Details |LicenseURL |
| Details |LightBackgroundPalette |
| Details |MajorVersion |
| Details |Manufacturer |
| Details |MinorVersion |
| Details |PostScriptCID |
| Details |PostScriptName |
| Details |PostScriptNamePrefix  |
| Details |SampleText |
| Details |Subfamily |
| Details |Trademark |
| Details |TypographicFamily | 
| Details |TypographicSubfamily |
| Details |URLDesigner |
| Details |URLVendor |
| Details |UniqueID |
| Details |Version |
| Details |WWSFamily |
| Details |WWSSubfamily |
| Metrics |UnitsPerEm |
| Metrics |Ascender |
| Metrics |Descender |
| Metrics |Height |
| Metrics |LineSpacing |
