using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myCao.AppPurchases
{
     public class PurchaseService
    {
        public async Task<bool> RestorePurchase(string productID)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

                if(!connected)
                {
                    return false;
                }

                var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

                if(purchases?.Any(p=> p.ProductId == productID) ?? false)
                {
                    Application.Current.Properties["AdsDisabled"] = true;
                    await Application.Current.SavePropertiesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Issue Connecting: " + ex);
                return false;
            }
            finally
            {
                await billing.DisconnectAsync();
            }
        }

        public async Task<bool> MakePurchase(string productID)
        {
            if (!CrossInAppBilling.IsSupported)
                return false;

            var alreadyPurchased = await RestorePurchase(productID);

            if (alreadyPurchased)
                return true;

            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

               

                if (!connected)
                {
                    Debug.WriteLine("Could not connect");
                    return false;
                }

                    var purchase = await CrossInAppBilling.Current.PurchaseAsync(productID, ItemType.InAppPurchase,"apppayload");
                    if(purchase ==null)
                    {
                        return false;
                    }
                    else if(purchase.State == PurchaseState.Purchased)
                    {
                        return true;
                    }
                return false;
            }
            catch(InAppBillingPurchaseException purchaseEx)
            {
                Debug.WriteLine("Error: " + purchaseEx);
                return false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Issue Connecting: " + ex);
                return false;
            }
            finally
            {
                await billing.DisconnectAsync();
            }
        }
    }
}
