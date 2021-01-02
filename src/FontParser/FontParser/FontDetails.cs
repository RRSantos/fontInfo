using static FontParser.Constants.Numbers;
using FontParser.Extension;
using FontParser.StringExtractor;
using System.Collections.Generic;
using System.IO;
using System;
using FontParser.Records;
using FontParser.Tables;

namespace FontParser
{
    public class FontDetails
    {

        private void initInteralFields(NamingTable namingTable, HeadTable headTable)
        {
            MajorVersion = headTable.MajorVersion;
            MinorVersion = headTable.MinorVersion;
            FontRevision = headTable.FontRevision;

            foreach (NameRecord record in namingTable.NameRecords)
            {   
                switch (record.NameID)
                {
                    case NameID.CompatibleFullName:
                        CompatibleFullName = record.ExtractedData;
                        break;
                    case NameID.Copyright:
                        Copyright = record.ExtractedData;
                        break;
                    case NameID.DarkBackgroundPalette:
                        DarkBackgroundPalette = record.ExtractedData;
                        break;
                    case NameID.Description:
                        Description = record.ExtractedData;
                        break;
                    case NameID.Designer:
                        Designer = record.ExtractedData;
                        break;
                    case NameID.Family:
                        Family = record.ExtractedData;
                        break;
                    case NameID.FullName:
                        FullName = record.ExtractedData;
                        break;
                    case NameID.LicenseDescription:
                        LicenseDescription = record.ExtractedData;
                        break;
                    case NameID.LicenseURL:
                        LicenseURL = record.ExtractedData;
                        break;
                    case NameID.LightBackgroundPalette:
                        LightBackgroundPalette = record.ExtractedData;
                        break;
                    case NameID.Manufacturer:
                        Manufacturer = record.ExtractedData;
                        break;
                    case NameID.PostScriptCID:
                        PostScriptCID = record.ExtractedData;
                        break;
                    case NameID.PostScriptName:
                        PostScriptName = record.ExtractedData;
                        break;
                    case NameID.PostScriptNamePrefix:
                        PostScriptNamePrefix = record.ExtractedData;
                        break;
                    case NameID.SampleText:
                        SampleText = record.ExtractedData;
                        break;
                    case NameID.Subfamily:
                        Subfamily = record.ExtractedData;
                        break;
                    case NameID.Trademark:
                        Trademark = record.ExtractedData;
                        break;
                    case NameID.TypographicFamily:
                        TypographicFamily = record.ExtractedData;
                        break;
                    case NameID.TypographicSubfamily:
                        TypographicSubfamily = record.ExtractedData;
                        break;
                    case NameID.UniqueID:
                        UniqueID = record.ExtractedData;
                        break;
                    case NameID.URLDesigner:
                        URLDesigner = record.ExtractedData;
                        break;
                    case NameID.URLVendor:
                        URLVendor = record.ExtractedData;
                        break;
                    case NameID.Version:
                        Version = record.ExtractedData;
                        break;
                    case NameID.WWSFamily:
                        WWSFamily = record.ExtractedData;
                        break;
                    case NameID.WWSSubfamily:
                        WWSSubfamily = record.ExtractedData;
                        break;
                    default:
                        break;
                }
            }
        }        

        public string Copyright { get; private set; } 
        public string Family { get; private set; }
        public string Subfamily { get; private set; }
        public string UniqueID { get; private set; }
        public string FullName { get; private set; }
        public string Version { get; private set; }
        public string PostScriptName { get; private set; } 
        public string Trademark { get; private set; }
        public string Manufacturer { get; private set; }
        public string Designer { get; private set; }
        public string Description { get; private set; }
        public string URLVendor { get; private set; }
        public string URLDesigner { get; private set; }
        public string LicenseDescription { get; private set; }
        public string LicenseURL { get; private set; }
        public string TypographicFamily { get; private set; }
        public string TypographicSubfamily { get; private set; }
        public string CompatibleFullName { get; private set; }
        public string SampleText { get; private set; } = string.Empty;
        public string PostScriptCID { get; private set; }
        public string WWSFamily { get; private set; }
        public string WWSSubfamily { get; private set; }
        public string LightBackgroundPalette { get; private set; }
        public string DarkBackgroundPalette { get; private set; }
        public string PostScriptNamePrefix { get; private set; }
        public ushort MajorVersion { get; private set; }
        public ushort MinorVersion { get; private set; }
        public double FontRevision { get; private set; }

        internal FontDetails(NamingTable namingTable, HeadTable headTable)
        {
            initInteralFields(namingTable, headTable);
        }

        
    }
}
