# FontInfo
Library to read basic informations from TTF/OTF files. 


![CI build](https://github.com/RRSantos/fontInfo/workflows/CI%20build/badge.svg)


![Package publish](https://github.com/RRSantos/fontInfo/workflows/Package%20publish/badge.svg)

## Installing

Install using NuGet

```powershell
PM > Install-Package FontInfo
``` 
## Usage

### Creating font object

To create an instace of Font, use the code below.

```csharp
using FontInfo;

[...]

// Create new instace of Font class for font 'myFontName.ttf' asynchronously
Font font = await Font.CreateAsync("myFontName.ttf");

// Accessing property FullName (FontDetails class)
Console.WriteLine($"Font fullname: {font.Details.FullName}");

// Accessing property LineSpacing (FontMetrics  class)
Console.WriteLine($"Font fullname: {font.Metrics.LineSpacing}");

```
### Loading fonts from given paths

To get all Fonts on a given directory, use the code below.

```csharp
using FontInfo;

[...]

List<string> fontDirectories = new List<string>();
fontDirectories.Add(@"C:\temp\myFonts\"); //some random directory
fontDirectories.Add(@"C:\Windows\Fonts\"); // Windows Font directory

// Load all fonts asynchronously from directories in fontDirectories list 
IReadonlyCollection<Font> loadedFonts = await Font.GetFontsAsync(fontDirectories);

// Writing FullName for all loaded fonts 
foreach(Font f in loadedFonts)
{
    Console.WriteLine($"Font fullname: {f.Details.FullName}");
}

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
