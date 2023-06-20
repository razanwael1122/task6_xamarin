using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace task6
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage()
        {
            InitializeComponent();
            {
                
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            
            var file = await MediaPicker.PickPhotoAsync();
            if (file == null)
                return;
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            var httpClient=new HttpClient();
            var response = await httpClient.PostAsync("http://192.168.248.1:7252/UploadFile", content);
       


            StatusLabel.Text=response.StatusCode.ToString();
            
        }

    }
}





