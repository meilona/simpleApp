
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace simpleApp
{
    [Activity(Label = "firstActivity")]
    public class firstActivity : Activity
    {
        private Context mContext;
        EditText etUser;
        Button btnGo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_first);

            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            etUser = FindViewById<EditText>(Resource.Id.etUser);
            btnGo.Click += buttonClicked;
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            String username = etUser.Text;
            if (username != null)
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("username", username);
                editor.Apply();        // applies changes asynchronously on newer APIs
                var intent = new Intent(this, typeof(MainActivity));
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
