using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace YoutubeSearch
{
	public partial class App : Application
	{
        public static string directory;

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("#2196F3"), BarTextColor = Color.White };
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
