
using GeoPlotter.Automation;
using System.Data;
using GeoPlotter.Clases;

namespace GeoPlotter
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        JsonDataList jsonlist = new JsonDataList(); 

        public MainPage()
        {
            InitializeComponent();

        }
        /// <summary>
        /// This method executes oncce Save button is cicked, it will get the current location and save it to a json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SavedOnCounterClicked(object? sender, EventArgs e)
        {
            count++;
            JsonData tmp = new JsonData();

            jsonlist.Add(await Geography.GetCurrentLocation(tmp));
            JsonManager.saveJson(jsonlist, "data.json");

            SemanticScreenReader.Announce(Savebtn.Text);
        }
        /// <summary>
        /// This method executes once Load button is clicked, it will load the data from the json file and display it in the DataLabel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadOnCounterClicked(object? sender, EventArgs e)
        {
            JsonManager.loadJson(out JsonDataList JsonDataListLoaded, "data.json");
            DataLabel.Text  = JsonDataListLoaded.ToString();
            SemanticScreenReader.Announce(Loadbtn.Text);
        }
    }
}
