using dhlopezmoreno1Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Xamarin.Forms;

namespace dhlopezmoreno1Final
{
    public partial class MainPage : ContentPage
    {
        App thisApp;
        public MainPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            thisApp.needPaintingRefresh = false;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await showMovement();
            await showArtist();
            if (thisApp.needPaintingRefresh)
            {
                refreshPaintings();
            }
            else
            {
                moveList.IsVisible = true;
            }
            moveList.SelectedItem = null;

        }

        void AddClicked(object sender, EventArgs e)
        {
            Painting newPaint = new Painting();
            //newPaint.Dated = //DateTime.Now.AddYears(-16);
            var paintDetailPage = new PaintDetails();
            paintDetailPage.BindingContext = newPaint;
            moveList.IsVisible = false;
            Navigation.PushAsync(paintDetailPage);

        }
        void moveSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var painting = (Painting)e.SelectedItem;
                var paintDetailPage = new PaintDetails();
                paintDetailPage.BindingContext = painting;
                moveList.IsVisible = false;
                Navigation.PushAsync(paintDetailPage);
            }
        }

        private void refreshPaintings()
        {
            string selMov = ddlMoveStyle.Items[ddlMoveStyle.SelectedIndex];
            if (selMov == "All Movement/Style")
            {
                showPaintings(null);
            }
            else
            {
                int? id = thisApp.ActiveMovements.Where(d => d.Name == selMov).SingleOrDefault().ID;
                showPaintings(id.GetValueOrDefault());
            }
            thisApp.needPaintingRefresh = false;
        }
        private void ddlMoveStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshPaintings();
        }
        private async Task showMovement()
        {
            if (!(thisApp.ActiveMovements?.Count > 0))
            {
                //Get the departments
                MovementRepo r = new MovementRepo();
                try
                {
                    //Add the All option and then add the rest in order
                    ddlMoveStyle.Items.Add("All Movement/Style");

                    thisApp.ActiveMovements = await r.GetMovements();
                    foreach (Movement m in thisApp.ActiveMovements.OrderBy(m => m.Name))
                    {
                        ddlMoveStyle.Items.Add(m.Name);
                    }
                    ddlMoveStyle.SelectedIndex = 0;
                }

                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("connection with the server"))
                        {

                            await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                        }
                    }
                    else
                    {
                        if (ex.Message.Contains("NameResolutionFailure"))
                        {
                            await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Utility.DBUri.ToString(), "Ok");
                        }
                        else
                        {
                            await DisplayAlert("General Error ", ex.Message, "Ok");
                        }

                    }
                }
            }

        }
        private async Task showArtist()
        {
            if (!(thisApp.ActiveArtists?.Count > 0))
            {
                //Get the departments
                ArtistsRepo r = new ArtistsRepo();
                try
                {
                    //Add the All option and then add the rest in order
                    //ddlMoveStyle.Items.Add("All Movement/Style");

                    thisApp.ActiveArtists = await r.GetArtists();
                    //foreach (Artists a in thisApp.ActiveArtists.OrderBy(a => a.Name))
                    //{
                    //    ddlMoveStyle.Items.Add(a.Name);
                    //}
                    //ddlMoveStyle.SelectedIndex = 0;
                }

                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("connection with the server"))
                        {

                            await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                        }
                    }
                    else
                    {
                        if (ex.Message.Contains("NameResolutionFailure"))
                        {
                            await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Utility.DBUri.ToString(), "Ok");
                        }
                        else
                        {
                            await DisplayAlert("General Error ", ex.Message, "Ok");
                        }

                    }
                }
            }

        }
        private async void showPaintings(int? ID)
        {
            //Get the departments
            ArtRepo r = new ArtRepo();
            try
            {
                List<Painting> paintings;
                if (ID.GetValueOrDefault() > 0)
                {
                    paintings = await r.GetPaintingsByMovement(ID.GetValueOrDefault());
                }
                else
                {
                    paintings = await r.GetPaintings();
                }
                moveList.ItemsSource = paintings;
                moveList.IsVisible = true;

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
                }

            }

        }
    }
}
