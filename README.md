# **OpenWeatherPolly**

OpenWeatherPollyApp is an accessible, dynamic weather application that integrates real-time data, voice narration, and a dynamic UI.
---

## **Features**
- **Real-Time Weather Updates**: Retrieves the current weather conditions, temperature, and time of day for any city using the OpenWeatherMap API.
- **Voice Narration**: Converts weather updates into speech using Amazon Polly.
- **Weather Tips**: Displays and narrates tips based on weather conditions (e.g., "Carry an umbrella today; it might rain").
- **Dynamic Weather Icons**: Updates weather icons to match current conditions (e.g., sunny, rainy, cloudy).
- **Time Zone Support**: Adjusts the weather and greeting to the city's local time.

---

1. **Development Environment**: 
   - Visual Studio 2022 or later
   - .NET 6 or later
2. **APIs**:
   - OpenWeatherMap API Key: [Get your API Key here](https://openweathermap.org/api)
   - Amazon Polly Access Key and Secret Key: [Get your AWS credentials here](https://aws.amazon.com/polly/)
3. **NuGet Packages**:
   - `Newtonsoft.Json` for JSON parsing
   - `NAudio` for audio playback
   - `AWSSDK.Polly` for Amazon Polly API integration
