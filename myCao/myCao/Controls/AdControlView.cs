using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace myCao.Controls
{
    public class AdControlView: Xamarin.Forms.View
    {
        public static readonly BindableProperty AdUnitIDProperty = BindableProperty.Create(nameof(AdUnitID),typeof(int), typeof(AdControlView),default(int));

        public int AdUnitID { get { return (int)GetValue(AdUnitIDProperty); } set { SetValue(AdUnitIDProperty, value); } }

    }
}
