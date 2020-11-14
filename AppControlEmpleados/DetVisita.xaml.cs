using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.AudioRecorder;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControlEmpleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetVisita : ContentPage
    {
        public DetVisita()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            btnCamara.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.camera.png");
            btnAudio.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.microphone.png");
        }

        private async void btnCamara_Clicked(object sender, EventArgs e)
        {
            var photo = await TakePhoto(); /*await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });*/

            //if (photo != null)
            //    ImagenCamara.Source = ImageSource.FromStream(() => { return photo.GetStream(); });

        }

        public async Task<ImageSource> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable ||
                    !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera",
                            "Sorry! No camera available.", "OK");
                return null;
            }

            var isPermissionGranted = await RequestCameraAndGalleryPermissions();
            if (!isPermissionGranted)
                return null;

            var file = await CrossMedia.Current.TakePhotoAsync(new
                StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return null;

            var imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            return imageSource;
        }

        private async Task<bool> RequestCameraAndGalleryPermissions()
        {
            var cameraStatus = await CrossPermissions.Current.
            CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.
            CheckPermissionStatusAsync(Permission.Storage);
            var photosStatus = await CrossPermissions.Current.
            CheckPermissionStatusAsync(Permission.Photos);

            if (
            cameraStatus != PermissionStatus.Granted ||
            storageStatus != PermissionStatus.Granted ||
            photosStatus != PermissionStatus.Granted)
            {
                var permissionRequestResult = await CrossPermissions.Current.
                RequestPermissionsAsync(
                    new Permission[]
                    {
                Permission.Camera,
                Permission.Storage,
                Permission.Photos
                    });

                var cameraResult = permissionRequestResult[Permission.Camera];
                var storageResult = permissionRequestResult[Permission.Storage];
                var photosResults = permissionRequestResult[Permission.Photos];

                return (
                    cameraResult != PermissionStatus.Denied &&
                    storageResult != PermissionStatus.Denied &&
                    photosResults != PermissionStatus.Denied);
            }

            return true;
        }

        private async Task<bool> RequesAudioAndGalleryPermission()
        {
            var micStatus = await CrossPermissions.Current.
            CheckPermissionStatusAsync(Permission.Microphone);
            var storageStatus = await CrossPermissions.Current.
            CheckPermissionStatusAsync(Permission.Storage);
            if (
            micStatus != PermissionStatus.Granted ||
            storageStatus != PermissionStatus.Granted)
            {
                var permissionRequestResult = await CrossPermissions.Current.
                RequestPermissionsAsync(
                    new Permission[]
                    {
                Permission.Microphone,
                Permission.Storage,
                    });

                var micResult = permissionRequestResult[Permission.Microphone];
                var storageResult = permissionRequestResult[Permission.Storage];

                return (
                    micResult != PermissionStatus.Denied &&
                    storageResult != PermissionStatus.Denied
                    );
            }

            return true;
        }

        AudioRecorderService recorder = new AudioRecorderService();
        public async Task GrabarAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                    await recorder.StartRecording();
                    btnAudio.BackgroundColor = Color.FromHex("#d32f2f");
                }
                else
                {
                    await recorder.StopRecording();
                    btnAudio.BackgroundColor = Color.FromHex("#43a047");
                }
            }
            catch (Exception ex)
            {
	        }
            recorder.GetAudioFilePath();
        }

        public async Task ReproducirAudio()
        {
            AudioPlayer player = new AudioPlayer();
            var filePath =  recorder.GetAudioFilePath();
            player.Play(filePath);
        }

        public async void btnAudio_Clicked(object sender, EventArgs e)
        {
            await GrabarAudio();
        }

        void Finish_Playing(object sender, EventArgs e)
        {
            btnAudio.BackgroundColor = Color.FromHex("#43a047");
        }


    }
}