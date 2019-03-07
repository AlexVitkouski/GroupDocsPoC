using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GroupDocsPoC.Converters;
using GroupDocsPoC.Logger;
using Image = System.Drawing.Image;

namespace GroupDocsPoC
{
    class TestRunner
    {
        private readonly ILogger Log;
        private readonly string PathToFolder;
        private readonly string PreviewFolderPath;
        private readonly PictureBox Picture;

        public void Run()
        {
            CreatePreviews(PathToFolder, Picture, PreviewFolderPath);
        }

        private static string CreateFolder(string fodlerName, string pathToFolder, string time)
        {
            var pathToNewFolder = Path.Combine(pathToFolder, $"{fodlerName}-{time}");
            if (!Directory.Exists(pathToNewFolder))
            {
                Directory.CreateDirectory(pathToNewFolder);
            }
            return pathToNewFolder;
        }

        private static void CreateFolders(string pathToSrcFolder, string time, out string logFolderPath, out string previewFolderPath)
        {
            previewFolderPath = CreateFolder("previews", pathToSrcFolder, time);
            logFolderPath = CreateFolder("logs", pathToSrcFolder, time);
        }

        public TestRunner(string pathToFolder, PictureBox pictureBox)
        {
            PathToFolder = pathToFolder;
            Picture = pictureBox;
            var time = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            CreateFolders(pathToFolder, time, out var logFolderPath, out var previewFolderPath);
            PreviewFolderPath = previewFolderPath;
            Log = new Logger.Logger(logFolderPath, time);
        }

        public void CreatePreviews(string pathToFolder, PictureBox pictureBox, string previewFolderPath)
        {
            var converterProvider = new ConverterProvider();
            
            foreach (var filePath in Directory.EnumerateFiles(pathToFolder))
            {
                var image = GenerateImagePreviews(filePath, converterProvider, out long conversionTime);

                long renderTime = 0;
                string status = image != null ? "Ok" : "Error";
                if (image != null)
                {
                    status = "Ok";
                    ShowPreview(pictureBox, image, out renderTime);
                    SavePreview(image, filePath, previewFolderPath);
                    image.Dispose();
                }

                var totalTime = conversionTime + renderTime;
                var fileName = Path.GetFileName(filePath);
                Log.LogInfo($"{fileName}, {conversionTime}, {renderTime}, {totalTime}, {status}");
            }
        }

        private static void ShowPreview(PictureBox pictureBox, Image image, out long renderTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            pictureBox.Image = image;
            pictureBox.Refresh();
            sw.Stop();
            renderTime = sw.ElapsedMilliseconds;
        }

        private static void SavePreview(Image image, string srcFileName, string previewFolderPath)
        {
            var previewFileName = $"{Path.GetFileNameWithoutExtension(srcFileName)}.png";
            var previewPath = Path.Combine(previewFolderPath, previewFileName);
            image.Save(previewPath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public Image GenerateImagePreviews(string filePath, ConverterProvider converterProvider, out long conversionTime)
        {
            Image result = null;
            var sw = new Stopwatch();
            conversionTime = 0;
            try
            {
                sw.Start();
                var converter = converterProvider.GetConverter(filePath);
                if (converter != null)
                    result = converter.Convert(filePath);
                else
                    Log.LogError($"Converter not found for {Path.GetFileName(filePath)}");
                sw.Stop();
                conversionTime = sw.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Log.LogError($"{ex.Message} - {Path.GetFileName(filePath)}");
            }
            return result;
        }
    }
}
