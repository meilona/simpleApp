
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace simpleApp
{
    [Activity(Label = "firstActivity", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class firstActivity : Activity
    {
        EditText etUser;
        Button btnGo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_first);

            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            etUser = FindViewById<EditText>(Resource.Id.etUser);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("Username", FileCreationMode.Private); ;
            String username = pref.GetString("Username", String.Empty);

            if (username != string.Empty)
            {
                // no saved credentials
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
                this.Finish();
            }

            btnGo.Click += buttonClicked;
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            String username = etUser.Text;
            if (username != string.Empty)
            {
                ISharedPreferences pref = Application.Context.GetSharedPreferences("Username", FileCreationMode.Private); ;
                ISharedPreferencesEditor editor = pref.Edit();
                editor.PutString("Username", username);
                editor.Apply();        // applies changes asynchronously on newer APIs
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
                this.Finish();
            }
            else
            {
                var toastConfig = new ToastConfig("Fill username!");
                toastConfig.SetDuration(3000);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));

                UserDialogs.Instance.Toast(toastConfig);
            }

        }
    }
}
