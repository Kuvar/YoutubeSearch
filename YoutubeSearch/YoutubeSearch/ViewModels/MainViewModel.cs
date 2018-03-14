using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeSearch.Models;
using YoutubeSearch.Services;
using YoutubeSearch.ViewModels.Base;

namespace YoutubeSearch.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged(() => SearchString);
            }
        }

        private ObservableCollection<VideoInformation> _informationList;
        public ObservableCollection<VideoInformation> InformationList
        {
            get { return _informationList; }
            set
            {
                _informationList = value;
                RaisePropertyChanged(() => InformationList);
            }
        }

        public MainViewModel()
        {
            
        }

        //DownloadCommand

        public ICommand SearchVideoCommand => new Command(async () => await SearchVideoAsync());

        public ICommand DownloadCommand => new Command(async () => await DownloadAsync());

        private async Task DownloadAsync()
        {
            YouTubeMp3 youTube = new YouTubeMp3();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            var lst = InformationList.Where(c => c.IsSelected == true);

            foreach (var item in lst)
            {
                await youTube.DownloadMP3(item.Url, Path.Combine(App.directory, item.Title + ".mp3"));
            }

            //var destination = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "music.mp3");
            //await new WebClient().DownloadFileTaskAsync( new Uri("http://www.xyz.com/music.mp3"), destination);

            //System.IO.File.WriteAllBytes(destination + "FullName", vid.GetBytes());
        }

        private async Task SearchVideoAsync()
        {
            await ServiceHandler.SearchQuery(SearchString, 3).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {

                }
                else
                {
                    var data = t.Result;
                    if (data != null)
                    {                       
                        InformationList = new ObservableCollection<VideoInformation>(data);
                        RaisePropertyChanged(() => InformationList);
                    }
                }
            });
        }
    }
}
