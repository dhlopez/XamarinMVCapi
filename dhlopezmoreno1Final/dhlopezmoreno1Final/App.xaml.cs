using dhlopezmoreno1Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace dhlopezmoreno1Final
{
    public partial class App : Application
    {
        public List<Movement> ActiveMovements;
        public List<Artists> ActiveArtists;
        public bool needPaintingRefresh;
        public App()
        {
            InitializeComponent();

            MainPage = new dhlopezmoreno1Final.MainPage();
            var nav = new NavigationPage(new MainPage());
            MainPage = nav;
        }
       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
