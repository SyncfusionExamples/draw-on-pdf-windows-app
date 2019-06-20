using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DrawOnPDF_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            string filename = "output.pdf";
            Stream stream = pdfViewerControl.Save();
            stream.Position = 0;

            FileSavePicker savePicker = new FileSavePicker
            {
                DefaultFileExtension = ".pdf",
                SuggestedFileName = filename
            };
            savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
            StorageFile stFile = await savePicker.PickSaveFileAsync();
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream streamForWrite = fileStream.AsStreamForWrite();
                streamForWrite.SetLength(0);
                streamForWrite.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                streamForWrite.Flush();
                streamForWrite.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("File has been saved successfully.");
                IUICommand cmd = await msgDialog.ShowAsync();
            }
        }
    }

}
