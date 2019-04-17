using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using myCao.Ads;
using myCao.iOS.CustomRenderer;
using UIKit;
using Google.MobileAds;

[assembly: Xamarin.Forms.Dependency(typeof(AdInterstitial))]
namespace myCao.iOS.CustomRenderer
{
    public class AdInterstitial : IAdInterstitial
    {
        Interstitial interstitialAd;
        const string adUnit = "ca-app-pub-1445708575343818/2966825029";
        
        public void Give()
        {
            if (interstitialAd.IsReady)
            {
                interstitialAd.AdReceived += (sender, args) =>
                {
                    if (interstitialAd.IsReady)
                    {
                        var window = UIApplication.SharedApplication.KeyWindow;
                        var vc = window.RootViewController;
                        while (vc.PresentedViewController != null)
                        {
                            vc = vc.PresentedViewController;
                        }
                        interstitialAd.PresentFromRootViewController(vc);
                    }
                };
            }
        }

        public void ShowAd()
        {
            interstitialAd = new Interstitial(adUnit);
            var request = Request.GetDefaultRequest();
            request.SetLocation((nfloat)53.349804, (nfloat)(-6.260310), 1000);
            interstitialAd.LoadRequest(request);
        }
    }
}