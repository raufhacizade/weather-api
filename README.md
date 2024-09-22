### Weather API

#### Overview
This Weather API provides current, historical, and geographical data using four different third-party APIs:
1. **Open-Meteo API** (`https://archive-api.open-meteo.com/`)
2. **OpenWeatherMap API** (`https://api.openweathermap.org/`)
3. **Tomorrow.io API** (`https://api.tomorrow.io/`)
4. **Visual Crossing Weather API** (`https://weather.visualcrossing.com/`)

The API features built-in caching to optimize performance and reduce redundant external API calls:
- **Latitude and Longitude Caching**: When a city and country combination is requested, the coordinates are cached to avoid repeated requests.
- **Current Weather Caching**: Weather data for the requested location is cached until the end of the day, ensuring efficient data retrieval.

#### Deployment
This API is deployed on an **Azure Web App** running on a **Linux system**. The secrets for the third-party APIs are securely stored in **Azure KeyVault** to protect sensitive credentials.

#### Local Development
To run the API locally, you can use the included **Dockerfile**. However, ensure that the application settings (such as third-party API keys) are properly configured in your local environment.

#### Endpoints Overview
Here is a brief description of the available endpoints based on the attached Postman collection:

1. **GET `/api/weather/detailed`**
   - Retrieves detailed weather information for a specified city, country, and date.
   - Example: 
     ```
     GET /api/weather/detailed?city=budapest&country=hungary&date=2020-03-20
     ```

2. **GET `/api/weather/today`**
   - Fetches the current day's weather information for a given city and country.
   - Example:
     ```
     GET /api/weather/today?city=budapest&country=hungary
     ```

3. **GET `/api/weather/archived`**
   - Provides archived weather data for a given city, country, and specific date.
   - Example:
     ```
     GET /api/weather/archived?city=budapest&country=hungary&date=2020-03-20
     ```

4. **GET `/api/geolocation`**
   - Returns the latitude and longitude for a specified city and country.
   - Example:
     ```
     GET /api/geolocation?city=budapest&country=hungary
     ```

#### Running the API Locally
- **Docker**: Use the provided Dockerfile to run the API locally.
- **App Settings**: Ensure that API keys and other environment variables (such as API secrets) are set in your local settings for proper operation.

For more detailed usage and development setup, refer to the [Postman collection](https://github.com/raufhacizade/weather-api/blob/post-man/WeatherApi.postman_collection.json).