using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using System.ComponentModel;
using Android.Graphics;
using myCao.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(Button),typeof(CustomButton))]
namespace myCao.Droid.CustomRenderer
{
    public class CustomButton : ButtonRenderer
    {
        public CustomButton(Context context) : base(context)
        {

        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}