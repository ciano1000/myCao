using System;
using System.Collections.Generic;
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
using myCao.Ads;
using myCao.Droid.CustomRenderer;
using Xamarin.Forms;


[assembly: Dependency(typeof(AdInterstitial))]
namespace myCao.Droid.CustomRenderer
{
    public class AdInterstitial : IAdInterstitial
    {
        InterstitialAd interstitialAd;
        Location location;
        const string adUnit = "ca-app-pub-1445708575343818/2531647116";
        public void ShowAd()
        {
            var context = Android.App.Application.Context;
            interstitialAd = new InterstitialAd(context);
            interstitialAd.AdUnitId = adUnit;

            var intlistener = new InterstitialListener(interstitialAd);
            intlistener.OnAdLoaded();
            interstitialAd.AdListener = intlistener;
            var requestBuilder = new AdRequest.Builder();
            location = new Location("HARDCODED");
            location.Latitude = 53.349804;
            location.Longitude = -6.260310;
            requestBuilder.SetLocation(location);
            interstitialAd.LoadAd(requestBuilder.Build());
        }

       public void Give()
        {
            if(interstitialAd.IsLoaded)
            {
                interstitialAd.Show();
            }
        }

        
    }

    public class InterstitialListener : AdListener
    {
        readonly InterstitialAd _ad;

        public InterstitialListener(InterstitialAd ad)
        {
            _ad = ad;
        }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();
        }
    }

}