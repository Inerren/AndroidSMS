using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using static App1.SMSHelper;
using Android.Database;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += delegate { GetAllSMS("inbox"); };
        }

        public List<SMS> GetAllSMS(string folderName)
        {
            List<SMS> SMSList = new List<SMS>();
            SMS SMS = new SMS();
            Android.Net.Uri hh = Android.Provider.Telephony.Sms.Inbox.ContentUri;
            ICursor Cursor = ContentResolver.Query(hh, null, null, null, null);
            StartManagingCursor(Cursor);
            int TotalCount = Cursor.Count;

            if (Cursor.MoveToFirst())
            {
                do
                {
                    SMS = new SMS();
                    SMS.setId(Cursor.GetString(Cursor.GetColumnIndexOrThrow("_id")));
                    SMS.setAddress(Cursor.GetString(Cursor
                            .GetColumnIndexOrThrow("address")));
                    SMS.setMsg(Cursor.GetString(Cursor.GetColumnIndexOrThrow("body")));
                    SMS.setReadState(Cursor.GetString(Cursor.GetColumnIndex("read")));
                    SMS.setTime(Cursor.GetString(Cursor.GetColumnIndexOrThrow("date")));

                    SMSList.Add(SMS);
                } while (Cursor.MoveToNext());
            }
            
            return SMSList;
        }
    }
}

