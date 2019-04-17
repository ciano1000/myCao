using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using myCao.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(myCao.Controls.AdControlView),typeof(myCao.Droid.CustomRenderer.AdViewRenderer))]
namespace myCao.Droid.CustomRenderer
{
    public class AdViewRenderer : ViewRenderer<AdControlView, AdView>
    {
        
        
        //Note you may want to adjust this, see further down.
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        Location location;
        int adUnitPage;
        string adUnitId;

        public AdViewRenderer(Context context):base(context)
        {
            /*adUnitPage = ((AdControlView)Element).AdUnitID;
            adUnitId = AdId(adUnitPage);*/
            
        }



        AdView CreateNativeAdControl()
        {
            adUnitPage = ((AdControlView)Element).AdUnitID;
            adUnitId = AdId(adUnitPage);
            location = new Location("HARDCODED");
            location.Latitude = 53.349804;
            location.Longitude = -6.260310;

            
            if (adView != null)
                return adView;

            adView = new AdView(Android.App.Application.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;
            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;
            var request = new AdRequest.Builder();
            request.SetLocation(location);
            
            adView.LoadAd(request.Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
        {
            
            base.OnElementChanged(e);
        if(e.NewElement!=null && Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
                
            
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == nameof(AdControlView.AdUnitID))
            {
                Control.AdUnitId = AdId(Element.AdUnitID);
            }
        }

       private string AdId(int page)
        {
            switch (page)
            {
                case 1:
                    return "ca-app-pub-1445708575343818/9316784999";
                    
                case 2:
                    return "ca-app-pub-1445708575343818/4272535398";
                    
                case 3:
                    return "ca-app-pub-1445708575343818/9674155446";
                   
                case 4:
                    return "ca-app-pub-1445708575343818/5394045375";
                    
                default:
                    return null;
                    
            }
        }

    }
}
