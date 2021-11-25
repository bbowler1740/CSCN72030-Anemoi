using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    public enum Weather
    {
        InsufficientSensors,
        Night,
        Sunny,
        Cloudy,
        Overcast,
        Rain,
        Snow,
        Thunderstorm,
        Hurricane,
        Tornado
    }

    class WeatherConditions
    {
        private const float FULL_SUN = 100000; //lux
        private const float CLOUDY_LOWER_THRESHOLD = 1000; //lux
        private const float LOW_PRESSURE_THRESHOLD = 1010; //mb
        private const float HIGH_PRESSURE_THRESHOLD = 1022; //mb
        private const float RAIN_HUMIDITY_THRESHOLD = 90; //%
        private const float STORM_WINDS = 20; //mph
        private const float HURRICANE_WINDS = 74; //mph
        private const float TORNADO_WINDS = 158; //mph
        private const string SUNRISE_STRING = "8:00";
        private const string SUNSET_STRING = "19:00";

        /// <summary>
        /// Determine the current weather based on the sensors provided
        /// </summary>
        /// <param name="sensors">collection of sensors used to determine weather</param>
        /// <returns>Weather enum representing the current weather condition</returns>
        public static Weather GetCurrentWeather(List<Sensor> sensors)
        {
            var currentWeather = Weather.InsufficientSensors;
            var lightSensor = sensors.Where(s => s.GetType() == typeof(Sunlight)).FirstOrDefault();
            var precipitationSensor = sensors.Where(s => s.GetType() == typeof(Precipitation)).FirstOrDefault();
            var thermometer = sensors.Where(s => s.GetType() == typeof(Temperature)).FirstOrDefault();
            var barometer = sensors.Where(s => s.GetType() == typeof(AirPressure)).FirstOrDefault();
            var humiditySensor = sensors.Where(s => s.GetType() == typeof(Humidity)).FirstOrDefault();
            var anemometer = sensors.Where(s => s.GetType() == typeof(WindSpeed)).FirstOrDefault();

            if (lightSensor != null)
            {
                if (lightSensor.SensorData >= FULL_SUN) //Best determinate of sunny
                {
                    currentWeather = Weather.Sunny;
                }
                else if (lightSensor.SensorData > CLOUDY_LOWER_THRESHOLD) //Best determinate of cloudy
                {
                    currentWeather = Weather.Cloudy;
                }
                else if (lightSensor.SensorData > 1) //Best determinate of overcast
                {
                    currentWeather = Weather.Overcast;
                }
                else
                {
                    currentWeather = Weather.Night;
                }
            }
            else //no lightSensor, use other sensors
            {
                if (DateTime.Now.TimeOfDay < TimeSpan.Parse(SUNRISE_STRING) || DateTime.Now.TimeOfDay > TimeSpan.Parse(SUNSET_STRING))
                {
                    currentWeather = Weather.Night;
                }
                if (barometer != null)
                {
                    if (barometer.SensorData > HIGH_PRESSURE_THRESHOLD && currentWeather != Weather.Night) //barometer.Data > HIGH_PRESSURE_THRESHOLD means clear skies
                    {
                        currentWeather = Weather.Sunny;
                    }
                    else if (barometer.SensorData < HIGH_PRESSURE_THRESHOLD)
                    {
                        currentWeather = Weather.Cloudy;
                    }
                }
            }

            if (precipitationSensor != null)
            {
                if (precipitationSensor.SensorData > 0) //Best determinate of rain
                {
                    currentWeather = Weather.Rain;
                }
            }
            else if (humiditySensor != null)
            {
                if (humiditySensor.SensorData > RAIN_HUMIDITY_THRESHOLD && currentWeather >= Weather.Cloudy) //determine of rain without precipitation sensor
                {
                    currentWeather = Weather.Rain;
                }
            }

            if (thermometer != null)
            {
                if (thermometer.SensorData <= 0 && currentWeather == Weather.Rain) //Best determinate of snow
                {
                    currentWeather = Weather.Snow;
                }
            }

            if (barometer != null)
            {
                if (barometer.SensorData < LOW_PRESSURE_THRESHOLD && (currentWeather == Weather.Rain || currentWeather == Weather.Snow))
                {
                    currentWeather = Weather.Thunderstorm;
                }
            }

            if (anemometer != null)
            {
                if (anemometer.SensorData > TORNADO_WINDS)
                {
                    currentWeather = Weather.Tornado;
                }
                else if (anemometer.SensorData > HURRICANE_WINDS)
                {
                    currentWeather = Weather.Hurricane;
                }
                else if(anemometer.SensorData > STORM_WINDS && (currentWeather == Weather.Rain || currentWeather == Weather.Snow))
                {
                    currentWeather = Weather.Thunderstorm;
                }
            }

            return currentWeather;
        }

        /// <summary>
        /// Create a list of typical sensor readings for the given weather condition
        /// </summary>
        /// <param name="weather">enum representing a weather condition</param>
        /// <returns>collection of values representing sensor readings</returns>
        public static List<float> GetWeatherReadings(Weather weather)
        {
            List<float> conditionsList;
            switch (weather)
            {
                case Weather.Night:
                    conditionsList = new List<float>()
                    {
                        1,                              //sunlight
                        0,                              //precipitation
                        HIGH_PRESSURE_THRESHOLD + 1,    //air pressure
                        60,                             //humidity
                        STORM_WINDS - 5,                //wind
                        20                              //temperature
                    };
                    break;
                case Weather.Sunny:
                    conditionsList = new List<float>()
                    {
                        FULL_SUN,
                        0,
                        HIGH_PRESSURE_THRESHOLD + 1,
                        60,
                        STORM_WINDS - 5,
                        20
                    };
                    break;
                case Weather.Cloudy:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD + 1,
                        0,
                        LOW_PRESSURE_THRESHOLD + 1,
                        60,
                        STORM_WINDS - 5,
                        20
                    };
                    break;
                case Weather.Overcast:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        0,
                        LOW_PRESSURE_THRESHOLD + 1,
                        60,
                        STORM_WINDS - 5,
                        20
                    };
                    break;
                case Weather.Rain:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        10,
                        LOW_PRESSURE_THRESHOLD + 1,
                        RAIN_HUMIDITY_THRESHOLD + 1,
                        STORM_WINDS - 5,
                        20
                    };
                    break;
                case Weather.Snow:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        10,
                        LOW_PRESSURE_THRESHOLD + 1,
                        RAIN_HUMIDITY_THRESHOLD + 1,
                        STORM_WINDS - 5,
                        -1
                    };
                    break;
                case Weather.Thunderstorm:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        10,
                        LOW_PRESSURE_THRESHOLD - 1,
                        RAIN_HUMIDITY_THRESHOLD + 1,
                        STORM_WINDS + 1,
                        20
                    };
                    break;
                case Weather.Hurricane:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        50,
                        LOW_PRESSURE_THRESHOLD - 1,
                        RAIN_HUMIDITY_THRESHOLD + 1,
                        HURRICANE_WINDS + 1,
                        20
                    };
                    break;
                case Weather.Tornado:
                    conditionsList = new List<float>()
                    {
                        CLOUDY_LOWER_THRESHOLD - 1,
                        10,
                        LOW_PRESSURE_THRESHOLD - 1,
                        RAIN_HUMIDITY_THRESHOLD + 1,
                        TORNADO_WINDS + 1,
                        20
                    };
                    break;
                case Weather.InsufficientSensors:
                default:
                    conditionsList = new List<float>();
                    break;
            }
            return conditionsList;
        }
        //static?
        //hardcode the weather conditions and the sensor conditions that create the weather
        //sunny, rainy, cloudy, snow, hail, windy, thunderstorm, tornado
        //needs to determine if sensors produce weather
        //needs to get conditions from weather
            //Get(Sunny) - (FULL_SUN, 0, HIGH_PRESSURE_THRESHOLD, <RAIN_HUMIDITY_THRESHOLD, <STORM_WINDS)
            //Get(Thunderstorm) - (<CLOUDY_LOWER_THRESHOLD, >5, <LOW_PRESSURE_THRESHOLD, >RAIN_HUMIDITY_THRESHOLD, >STORM_WINDS)



        /* midday Sunlight - Light sensor (lux)
         * 100,000+ direct light
         * 1000-2000 overcast
         * ~200 thick storm clouds
         * 40 fully overcast
         * 
         * <1 nighttime
         */

        /* atmosphere pressure (millibar [mb])
         * high pressure - clear skies/calm weather 
         * low pressure - high winds, warm air, rain
         * normal - 1009-1022mb
         */

        /* precipitation 
         * Light rain — < 2.5 mm (0.098 in) per hour
         * Moderate rain — 2.5 mm (0.098 in) – 10 mm (0.39 in) per hour
         * Heavy rain — 10 mm (0.39 in) and 50 mm (2.0 in) per hour
         * Violent rain — >50 mm (2.0 in) per hour
         * 
         * <0 - snow
         */

        /* Humidity (%)
         * 90+ rain
         */

        /* wind (mph)
         * breeze - 1-24
         * gale - 25-46
         * storm - 47-72
         * 
         * Tornado (mph)
         * F0 40-72
         * F1 73-112
         * F2 113-157
         * F3 158-206
         * F4 207-260
         * F5 261-318
         *
         * Hurricane (mph)
         * cat1 74-95 (damage to roof, Large branches of trees will snap)
         * cat2 96-110
         * cat3 111-129 (major)
         * cat4 130-156
         * cat5 157+
         */

        /* Thunderstorm (real data)
         * 31kmph (19mph)
         * 94%
         * 1004mb
         * 15-20mm
         * 
         * 
         * 44km/h  (27mph)
         * 87%
         * 1007mb
         * 25-30mm
         */

        /* updrafts cause thunderstorm growth, and hail */

        /* partly cloudy (real data)
         * 14kmph (8.6 mph)
         * 87%
         * 1016mb
         * 0mm
         */

        /* overcast (edge of dissipating hurricane) (real data)
         * 57kmph (35mph)
         * 63%
         * 1003mb
         * 0mm
         * 
         * 67kmph (42mph)
         * 93%
         * 1002mb
         * ~10mm
         */
    }
}
