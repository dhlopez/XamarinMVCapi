using dhlopezmoreno1Final.Helpers;
using dhlopezmoreno1Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace dhlopezmoreno1Final
{
    public partial class PaintDetails : ContentPage
    {
        Painting paint;
        App thisApp;

        public PaintDetails()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            paint = (Painting)this.BindingContext;
            setMovements();
            setArtists();
            if (paint.ID == 0)//Adding New
            {
                this.Title = "Add New Painting";
                btnDelete.IsEnabled = false;
            }
            else
            {
                this.Title = "Edit Painting Details";
                btnDelete.IsEnabled = true;
            }
        }

        void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void setMovements()
        {
            int assignedMov = 0;
            int count = 0;
            foreach (Movement m in thisApp.ActiveMovements.OrderBy(m => m.Name))
            {
                ddlMovement.Items.Add(m.Name);
                if (m.ID == paint.ID)
                {
                    assignedMov = count;
                }
                count++;
            }
            ddlMovement.SelectedIndex = assignedMov;
        }
        void setArtists()
        {
            int assignedArtist = 0;
            int count = 0;
            foreach (Artists a in thisApp.ActiveArtists.OrderBy(a => a.Name))
            {
                ddlArtist.Items.Add(a.Name);
                if (a.ID == paint.ArtistID)
                {
                    assignedArtist = count;
                }
                count++;
            }
            ddlArtist.SelectedIndex = assignedArtist;
        }

        int getMovement()
        {
            string selMov = ddlMovement.Items[ddlMovement.SelectedIndex];
            int id = thisApp.ActiveMovements.Where(d => d.Name == selMov).SingleOrDefault().ID;
            return id;
        }
        int getArtist()
        {
            string selArt = ddlArtist.Items[ddlArtist.SelectedIndex];
            int id = thisApp.ActiveArtists.Where(d => d.Name == selArt).SingleOrDefault().ID;
            return id;
        }
        async void SaveClicked(object sender, EventArgs e)
        {
            try
            {
                paint.Movement = null;
                paint.Artist = null;
                paint.MovementID = getMovement();
                paint.ArtistID = getArtist();
                ArtRepo er = new ArtRepo();
                if (paint.ID == 0)
                {
                    await er.AddPainting(paint);
                }
                else
                {
                    await er.UpdatePainting(paint);
                }
                thisApp.needPaintingRefresh = true;
                await Navigation.PopAsync();
            }
            catch (AggregateException ex)
            {
                string errMsg = "";
                foreach (var exception in ex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                await DisplayAlert("One or more exceptions has occurred:", errMsg, "Ok");
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                thisApp.needPaintingRefresh = true;
                await DisplayAlert("Problem Saving the Painting:", sb.ToString(), "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not complete operation.", "Ok");
            }
        }

        async void DeleteClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + paint.Name + "?", "Yes", "No");
            if (answer == true)
            {
                try
                {
                    paint.Movement = null;
                    ArtRepo er = new ArtRepo();
                    await er.DeletePainting(paint);
                    thisApp.needPaintingRefresh = true;
                    await Navigation.PopAsync();
                }
                catch (AggregateException ex)
                {
                    string errMsg = "";
                    foreach (var exception in ex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    await DisplayAlert("One or more exceptions has occurred:", errMsg, "Ok");
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    await DisplayAlert("Problem Saving the Painting:", sb.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("connection with the server"))
                    {
                        await DisplayAlert("Error", "No connection with the server.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Could not complete operation.", "Ok");
                    }
                }
            }
        }
    }
}
