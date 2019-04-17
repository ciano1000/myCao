using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;
using Google.MobileAds;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using myCao.Controls;

[assembly: ExportRenderer(typeof(myCao.Controls.AdControlView),typeof(myCao.iOS.CustomRenderer.AdViewRenderer))]
namespace myCao.iOS.CustomRenderer
{
    public class AdViewRenderer: ViewRenderer<AdControlView, BannerView>
    {
        int adUnitPage;
        string adUnitId;
        BannerView adView;

        BannerView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            adUnitPage = ((AdControlView)Element).AdUnitID;
            adUnitId = AdId(adUnitPage);

            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new BannerView(size: AdSizeCons.SmartBannerPortrait,
                                           origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
            {
                AdUnitID = adUnitId,
                RootViewController = GetVisibleViewController()
            };
            
            // Wire AdReceived event to know when the Ad is ready to be displayed
            adView.AdReceived += (object sender, EventArgs e) =>
            {
                //ad has come in
            };
            

            adView.LoadRequest(GetRequest());
            return adView;
        }

        Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            request.SetLocation((nfloat)53.349804, (nfloat)(-6.260310),1000);
            // Requests test ads on devices you specify. Your test device ID is printed to the console when
            // an ad request is made. GADBannerView automatically returns test ads when running on a
            // simulator. After you get your device ID, add it here
            //request.TestDevices = new [] { Request.SimulatorId.ToString () };
            return request;
        }

        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.Windows[0].RootViewController;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController)
            {
                return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController)
            {
                return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
            }

            return rootController.PresentedViewController;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);


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