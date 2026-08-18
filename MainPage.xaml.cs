
using GeoPlotter.Automation;
using System.Data;
using GeoPlotter.Clases;

namespace GeoPlotter
{
    public partial class MainPage : ContentPage
    {
        JsonDataList jsonlist = new JsonDataList(); 
        private Picker _labelPicker;
        private Entry _newLabelEntry;



        public MainPage()
        {
            InitializeComponent();

            // resolve controls by name (safe if XAML source-gen hasn't created fields)
            _labelPicker = this.FindByName<Picker>("LabelPicker");
            _newLabelEntry = this.FindByName<Entry>("NewLabelEntry");
            

        }

        private async void PlotOnCounterClicked(object? sender, EventArgs e)
        {
            JsonData tmp = new JsonData();

            // handle label selection / new label entry
            var selected = _labelPicker?.SelectedItem?.ToString();
            if (selected == "New")
            {
                var text = _newLabelEntry?.Text?.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    tmp.label = text;
                    // add new option to picker so it persists
                    _labelPicker?.Items.Add(text);
                    // select the newly added item
                    if (_labelPicker != null)
                        _labelPicker.SelectedIndex = _labelPicker.Items.Count - 1;
                }
            }
            else if (!string.IsNullOrEmpty(selected) && selected != "None")
            {
                tmp.label = selected;
            }

            // get location (static method)
            await Geography.GetCurrentLocation(tmp);
            jsonlist.Add(tmp);

            DataLabel.Text += tmp.ToString() + "\n";

            SemanticScreenReader.Announce(Plotbtn.Text);
        }

        private void LabelPicker_SelectionChanged(object? sender, EventArgs e)
        {
            if (_labelPicker?.SelectedItem?.ToString() == "New")
            {
                if (_newLabelEntry != null)
                {
                    _newLabelEntry.IsEnabled = true;
                    _newLabelEntry.Focus();
                }
            }
            else
            {
                if (_newLabelEntry != null)
                {
                    _newLabelEntry.Text = string.Empty;
                    _newLabelEntry.IsEnabled = false;
                }
            }
        }
        /// <summary>
        /// This method executes oncce Save button is cicked, it will get the current location and save it to a json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SavedOnCounterClicked(object? sender, EventArgs e)
        {
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
            LoadData();

            SemanticScreenReader.Announce(Loadbtn.Text);
        }
        private async void ShareOnCounterClicked(object? sender, EventArgs e)
        {
            var sh = new Automation.Sharing();
            await sh.ShareText(jsonlist.exportJson(), "GeoPlotter Data");
            SemanticScreenReader.Announce(Sharebtn.Text);
        }


        private void LoadData()
        {
            JsonManager.loadJson(out JsonDataList JsonDataListLoaded, "data.json");

            // populate picker with labels from loaded data
            if (JsonDataListLoaded != null && JsonDataListLoaded.DataList != null)
            {
                foreach (var item in JsonDataListLoaded.DataList)
                {
                    var lbl = item.label?.Trim();
                    if (!string.IsNullOrEmpty(lbl))
                    {
                        // avoid duplicates
                        if (_labelPicker != null && !_labelPicker.Items.Contains(lbl))
                        {
                            _labelPicker.Items.Add(lbl);
                        }
                    }
                    DataLabel.Text += item.ToString() + "\n";

                }
                
            }

        }
    }
}
