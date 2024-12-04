using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon;
using NAudio.Wave; // Ensure you have NAudio installed via NuGet

namespace OpenWeatherPolly
{
    public partial class Form1 : Form
    {
        private string openWeatherMapApiKey = "70dfbed2258ef2cd49e778a6bfd9276a";
        private string awsAccessKey = "AKIA3CMCCLZZOPI36XQ4";
        private string awsSecretKey = "gJFK2Al5GIPagJWb2RTNIHnSSH1vdk4VDvW6m8Q1";
        private string awsRegion = "us-east-1"; // Change based on your region

        public Form1()
        {
            InitializeComponent();
        }

        private async Task<string> GetWeatherData(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={openWeatherMapApiKey}&units=imperial"; // 'imperial' for Fahrenheit

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject weatherJson = JObject.Parse(responseBody);
                    string description = weatherJson["weather"][0]["description"].ToString();
                    string temperature = weatherJson["main"]["temp"].ToString();

                    return $"The weather in {city} is {description} with a temperature of {temperature}°F.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching weather data: {ex.Message}");
                    return null;
                }
            }
        }

        private async Task<Stream> GenerateSpeech(string text)
        {
            try
            {
                using (var client = new AmazonPollyClient(awsAccessKey, awsSecretKey, RegionEndpoint.GetBySystemName(awsRegion)))
                {
                    var request = new SynthesizeSpeechRequest
                    {
                        Text = text,
                        OutputFormat = OutputFormat.Mp3,
                        VoiceId = VoiceId.Joanna
                    };

                    var response = await client.SynthesizeSpeechAsync(request);
                    MemoryStream memoryStream = new MemoryStream();
                    await response.AudioStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0; // Reset stream position for playback

                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating speech: {ex.Message}");
                return null;
            }
        }

        private async void btnFetchWeather_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text;
            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Please enter a city name.");
                return;
            }

            string weatherData = await GetWeatherData(city);
            if (weatherData != null)
            {
                lblWeather.Text = weatherData;
                WeatherBackgroundColor();
            }

            string textToNarrate = lblWeather.Text;
            if (string.IsNullOrWhiteSpace(textToNarrate))
            {
                MessageBox.Show("No weather data to narrate.");
                return;
            }

            Stream audioStream = await GenerateSpeech(textToNarrate);
            if (audioStream != null)
            {
                try
                {
                    // Convert MP3 to WAV and play
                    using (var mp3Reader = new Mp3FileReader(audioStream))
                    using (var waveStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader))
                    using (var waveOutEvent = new WaveOutEvent())
                    {
                        waveOutEvent.Init(waveStream);
                        waveOutEvent.Play();
                        while (waveOutEvent.PlaybackState == PlaybackState.Playing)
                        {
                            await Task.Delay(500); // Wait while audio is playing
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error playing audio: {ex.Message}");
                }
            }
        }

        private void WeatherBackgroundColor()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblWeather.Text))
                {
                    return;
                }

                // Extract weather condition from the label text
                string weatherCondition = lblWeather.Text.ToLower();

                // Get the current time
                int currentHour = DateTime.Now.Hour;

                // Determine if it's day or night
                bool isDaytime = currentHour >= 6 && currentHour < 18;

                // Change background color based on weather condition and time
                if (weatherCondition.Contains("clear"))
                {
                    this.BackColor = isDaytime ? Color.SkyBlue : Color.MidnightBlue;
                }
                else if (weatherCondition.Contains("cloud"))
                {
                    this.BackColor = isDaytime ? Color.LightGray : Color.DarkSlateGray;
                }
                else if (weatherCondition.Contains("rain"))
                {
                    this.BackColor = isDaytime ? Color.LightSlateGray : Color.DarkBlue;
                }
                else if (weatherCondition.Contains("snow"))
                {
                    this.BackColor = isDaytime ? Color.WhiteSmoke : Color.LightSlateGray;
                }
                else
                {
                    // Default background for unknown conditions
                    this.BackColor = isDaytime ? Color.LightYellow : Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing background color: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void txtCity_Text_Click(object sender, EventArgs e)
        {
            if (txtCity.Text.Contains("City Name"))
            {
                txtCity.Text = string.Empty;
            } 
        }
    }
}
