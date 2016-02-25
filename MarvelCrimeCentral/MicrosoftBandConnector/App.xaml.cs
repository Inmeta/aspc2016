﻿using Microsoft.Band;
using Microsoft.Band.Tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace MicrosoftBandConnector
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            //SetBandTile();
            GetBandNotifications();
        }

        private async void deleteExistingTile(Guid guid)
        {
            var bandManager = BandClientManager.Instance;
            // query the service for paired devices
            var pairedBands = await bandManager.GetBandsAsync();
            // connect to the first device
            var bandInfo = pairedBands.FirstOrDefault();

            using (var bandClient = await bandManager.ConnectAsync(bandInfo))
            {
                var tiles = await bandClient.TileManager.GetTilesAsync();
                await bandClient.TileManager.RemoveTileAsync(guid);
            }
        }

        private async void SetBandTile()
        {
            var bandManager = BandClientManager.Instance;
            // query the service for paired devices
            var pairedBands = await bandManager.GetBandsAsync();
            // connect to the first device
            var bandInfo = pairedBands.FirstOrDefault();

            var bandClient = await bandManager.ConnectAsync(bandInfo);

            //RETRIEVING THE BAND VERSION INFORMATION
            string fwVersion;//"2.0.3923.0"
            string hwVersion;//"26"
            try
            {
                fwVersion = await bandClient.GetFirmwareVersionAsync();
                hwVersion = await bandClient.GetHardwareVersionAsync();
                // do work with firmware & hardware versions
            }
            catch (BandException ex)
            {
                // handle any BandExceptions
            }
            ///////////////////////////////////////





            //just vibration because of reasons - no message
            //await bandClient.NotificationManager.VibrateAsync(Microsoft.Band.Notifications.VibrationType.NotificationAlarm);
            //    bandClient.TileManager.TileButtonPressed += TileManager_TileButtonPressed;
            //await bandClient.TileManager.StartReadingsAsync();

            var smallIconFile = await StorageFile.GetFileFromApplicationUriAsync(
                                    new Uri("ms-appx:///Assets/logo_small.png"));


            using (var smallStream = await smallIconFile.OpenReadAsync())
            {
                var largeBitmap = new WriteableBitmap(48, 48);
                largeBitmap.SetSource(smallStream);
                var largeIcon = largeBitmap.ToBandIcon();

                var guid = Guid.NewGuid();//{be8a038f-2532-4be7-ac5e-aacf1771c788}

                var added = await bandClient.TileManager.AddTileAsync(
                  new BandTile(guid)
                  {
                      Name = "MarvelCrimeCenterUnit",
                      TileIcon = largeIcon,
                      SmallIcon = largeIcon
                  }
                );
                if (added)
                {
                    // NB: This call will return back to us *before* the
                    // user has acknowledged the dialog on their device -
                    // we don't get to know their answer here.
                    //await bandClient.NotificationManager.ShowDialogAsync(guid, "Test", "Hello Mario");

                    //PageData pageContent = new PageData(guid,
                    //    0, // index of our (only) layout 
                    //    new TextButtonData((short)1, "CALL SUPERHEROES!"));

                    //await bandClient.TileManager.SetPagesAsync(guid, pageContent);

                }

            }

            bandClient.TileManager.TileOpened += TileManager_TileOpened;

            await bandClient.TileManager.StartReadingsAsync();

        }

        private async void GetBandNotifications()
        {
            var bandManager = BandClientManager.Instance;
            // query the service for paired devices
            var pairedBands = await bandManager.GetBandsAsync();
            // connect to the first device
            var bandInfo = pairedBands.FirstOrDefault();

            var bandClient = await bandManager.ConnectAsync(bandInfo);

            var tiles = await bandClient.TileManager.GetTilesAsync();

            var tile = tiles.First();

            bandClient.TileManager.TileOpened += TileManager_TileOpened;

            await bandClient.TileManager.StartReadingsAsync();


        }

        private async void TileManager_TileOpened(object sender, BandTileEventArgs<IBandTileOpenedEvent> e)
        {
            //alert!
            //Create new alert in the alerts list
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            var result = await client.GetDataAsync(27);
            await client.CloseAsync();


            //Start sending live data from band to a new event list in sharepoint

        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}