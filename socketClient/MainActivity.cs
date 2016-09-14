using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using Sockets.Plugin;
using System.Threading.Tasks;

namespace socketClient
{
    [Activity(Label = "socketClient", MainLauncher = true, Icon = "@drawable/icon")]
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

            button.Click += delegate { sendData(); };
        }
        public async void sendData()
        {
            var address = "192.168.1.150";
            var port = 27015;
            var r = new Random();
            
            
            var client = new TcpSocketClient();
            

            // we're connected!
            for (int i = 0; i < 5; i++)
            {
                await client.ConnectAsync(address, port);
                // write to the 'WriteStream' property of the socket client to send data
                String String_SentDataContent = "hi\n";
                String_SentDataContent = i.ToString() +".from Android: "+String_SentDataContent;
                byte[] byte_SentDataContent = System.Text.Encoding.Default.GetBytes(String_SentDataContent);
                var nextByte = byte_SentDataContent;

                //client.WriteStream.WriteByte((Byte)nextByte);
                await client.WriteStream.WriteAsync(nextByte,0, nextByte.Length);

                await client.WriteStream.FlushAsync();

                // wait a little before sending the next bit of data
                await Task.Delay(TimeSpan.FromMilliseconds(3000));
                await client.DisconnectAsync();
            }

            
        }
    }
    

}

