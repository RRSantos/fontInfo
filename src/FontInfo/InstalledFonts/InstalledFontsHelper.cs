using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FontInfo.InstalledFonts
{
    static class FontsHelper
    {
        private static List<string> getFontNamesFromPath(List<string> pathList)
        {
            List<string> fontNames = new List<string>();
            foreach (string p in pathList)
            {
                string[] ttfFonts = Directory.GetFiles(p, "*.ttf", SearchOption.AllDirectories);
                string[] otfFonts = Directory.GetFiles(p, "*.otf", SearchOption.AllDirectories);
                fontNames.AddRange(ttfFonts.Union(otfFonts));
            }

            return fontNames;

        }


        private static async Task<IReadOnlyCollection<Font>> createFontList(List<string> fontNames)
        {
            List<Font> allFonts = new List<Font>();
            foreach (string f in fontNames)
            {
                Font font = await Font.CreateAsync(f);
                allFonts.Add(font);
            }

            return allFonts;
        }

        public static async Task<IReadOnlyCollection<Font>> GetFontsAsync(List<string> pathList)
        {
            List<string> fontNames = getFontNamesFromPath(pathList);

            return await createFontList(fontNames);
        }


    }
}
