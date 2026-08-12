using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPlotter.Automation
{
    internal class Sharing
    {
        public async Task ShareText(string text, string title="Share Text")
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = title
            });
        }
    }
}
