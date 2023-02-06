using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Vonage;

namespace KingOrder.XF.ViewModels
{
    public class VonageViewModel : BaseViewModel
    {
        #region Private Members

        private string _btnCallText;

        #endregion

        #region Properties

        public string BtnCallText
        {
            get => _btnCallText;
            set => SetProperty(ref _btnCallText, value);
        }

        #endregion

        #region Commands

        public Command BtnCallCommand { get; }

        #endregion

        #region Constructors

        public VonageViewModel()
        {
            Title = "Vonage";
            BtnCallText = "Start Call";
            BtnCallCommand = new Command(OnStartCallCommand);
        }

        #endregion

        #region Commands Implementations

        private void OnStartCallCommand(object sender)
        {
            try
            {
                if (!CrossVonage.Current.IsSessionStarted)
                {
                    Analytics.TrackEvent("VonageViewModel StartCall");
                    BtnCallText = "End Call";
                    CrossVonage.Current.TextMessageReceived += OnMessageReceived;

                    if (!CrossVonage.Current.TryStartSession())
                    {
                        return;
                    }
                }
                else
                    EndCall();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void EndCall()
        {
            try
            {
                Analytics.TrackEvent("VonageViewModel EndCall");
                BtnCallText = "Start Call";
                CrossVonage.Current.EndSession();
                CrossVonage.Current.TextMessageReceived -= OnMessageReceived;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        private void OnMessageReceived(object sender, VonageTextMessageReceivedEventArgs e)
            => App.Current.MainPage.DisplayAlert($"Message with type: {e.MessageType}", e.Message, "OK");

        #endregion
    }
}