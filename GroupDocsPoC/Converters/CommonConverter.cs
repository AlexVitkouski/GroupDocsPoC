using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupDocs.Metadata.Preview;

namespace GroupDocsPoC.Converters
{
    class CommonConverter : IConverter
    {
        public Image Convert(string filePath)
        {
            Image image = null;
            int pageNumber = 0;
            int resolution = 80;
            using (FileStream fs = File.OpenRead(filePath))
            {
                using (PreviewHandler handler = PreviewFactory.Load(fs))
                {
                    PreviewImageData[] pagePreviews = handler.GetPageImage(0, resolution);
                    var imageBytes = pagePreviews[pageNumber].Contents;
                    image = new Bitmap(new MemoryStream(imageBytes));
                }
            }
            return image;
        }
    }
}
