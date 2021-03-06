﻿using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using XPlatformMenus.Core.ViewModels;
using XPlatformMenus.Droid.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.Fragging.Caching;
using MvvmCross.Platform;

namespace XPlatformMenus.Droid.Activities
{
    [Activity(
        Label = "Main Activity",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        Name = "xplatformmenus.droid.activities.MainActivity"
    )]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout;

        /*private static readonly Dictionary<string, CustomFragmentInfo> MyFragmentsInfo = new Dictionary<string, CustomFragmentInfo>()
        {
            {typeof(MenuViewModel).Name, new CustomFragmentInfo(typeof(MenuViewModel).Name, typeof(MenuFragment), typeof(MenuViewModel))},
            {typeof(HomeViewModel).Name, new CustomFragmentInfo( typeof(HomeViewModel).Name, typeof(HomeFragment), typeof(HomeViewModel), isRoot: true)},
            {typeof(ExampleViewPagerViewModel).Name, new CustomFragmentInfo( typeof(ExampleViewPagerViewModel).Name, typeof(ExampleViewPagerFragment), typeof(ExampleViewPagerViewModel), isRoot: true)},
            {typeof(ExampleRecyclerViewModel).Name, new CustomFragmentInfo( typeof(ExampleRecyclerViewModel).Name, typeof(ExampleRecyclerViewFragment), typeof(ExampleRecyclerViewModel), isRoot: true)},
            {typeof(SettingsViewModel).Name, new CustomFragmentInfo( typeof(SettingsViewModel).Name, typeof(SettingsFragment), typeof(SettingsViewModel), isRoot: true)}
        };*/

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
                ViewModel.ShowMenu();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        /*public override void OnFragmentCreated(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            var myCustomInfo = (CustomFragmentInfo)fragmentInfo;

            // You can do fragment + transaction based configurations here.
            // Note that, the cached fragment might be reused in another transaction afterwards.
        }*/

        /*private void CheckIfMenuIsNeeded(CustomFragmentInfo myCustomInfo)
        {
            //If not root, we will block the menu sliding gesture and show the back button on top
            if (myCustomInfo.IsRoot)
            {
                ShowHamburguerMenu();
            }
            else
            {
                ShowBackButton();
            }
        }
*/
        private void ShowBackButton()
        {
            //TODO Tell the toggle to set the indicator off
            //this.DrawerToggle.DrawerIndicatorEnabled = false;

            //Block the menu slide gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        private void ShowHamburguerMenu()
        {
            //TODO set toggle indicator as enabled 
            //this.DrawerToggle.DrawerIndicatorEnabled = true;

            //Unlock the menu sliding gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public bool Show(MvxViewModelRequest request, Bundle bundle)
        {
            if (request.ViewModelType == typeof(MenuViewModel))
            {
                ShowFragment(request.ViewModelType.Name, Resource.Id.navigation_frame, bundle);
                return true;
            }
            
            ShowFragment(request.ViewModelType.Name, Resource.Id.content_frame, bundle, true);
            return true;
        }

        /*public override void OnFragmentChanged(IMvxCachedFragmentInfo fragmentInfo)
        {
            var myCustomInfo = (CustomFragmentInfo)fragmentInfo;
            CheckIfMenuIsNeeded(myCustomInfo);
        }*/

        public bool Close(IMvxViewModel viewModel)
        {
            CloseFragment(viewModel.GetType().Name, Resource.Id.content_frame);
            return true;
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}