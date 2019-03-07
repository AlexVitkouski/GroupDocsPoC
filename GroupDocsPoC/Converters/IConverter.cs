using System.Drawing;

namespace GroupDocsPoC.Converters
{
    interface IConverter
    {
        Image Convert(string filePath);
    }
}
