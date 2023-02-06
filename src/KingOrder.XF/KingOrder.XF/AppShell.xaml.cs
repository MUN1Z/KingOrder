using KingOrder.XF.ViewModels;
using KingOrder.XF.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KingOrder.XF
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(NewProductPage), typeof(NewProductPage));
        }

    }
}
