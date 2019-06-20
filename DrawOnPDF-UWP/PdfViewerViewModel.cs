using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace DrawOnPDF_UWP
{
    class PdfViewerViewModel : INotifyPropertyChanged
    {
        private Stream documentStream;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Stream object to be bound to the ItemsSource of the PDF Viewer
        /// </summary>
        public Stream DocumentStream
        {
            get
            {
                return documentStream;
            }
            set
            {
                documentStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public PdfViewerViewModel()
        {
            //Loads the stream from the embedded resource.
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            DocumentStream = assembly.GetManifestResourceStream("DrawOnPDF_UWP.Assets.Invoice.pdf");
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
