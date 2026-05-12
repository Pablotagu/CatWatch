#include <WiFi.h>
#include <HTTPClient.h>

WiFiClient wifiClient;
HTTPClient httpClient;

void sendReading(float temperature)
{
  connectWifi();
  if (WiFi.status() != WL_CONNECTED) 
  {
    if(DEBUG)
    {
      Serial.println("WiFi not connected. Cannot send reading.");
    }
    return;
  }

  openConnection();  
  postReading(temperature);
  closeConnection();
  disconnectWifi();  
} 


void postReading(float temperature) 
{
  int httpResponseCode = httpClient.POST(getReadingAsPayload(temperature));
  if (DEBUG) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
  }
}

String getReadingAsPayload(float temperature)
{
  return "{\"probeId\":\"" + String(PROBE_ID) + "\",\"temperature\":" + String(temperature) + "}";
}


void openConnection()
{
  httpClient.begin(wifiClient, API_URL); 
  httpClient.addHeader("Content-Type", "application/json");
  httpClient.addHeader("x-api-key", PROBE_API_KEY);
}


void closeConnection() 
{
  httpClient.end();
}


void connectWifi()
{
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  int attempts = 0;
  while (WiFi.status() != WL_CONNECTED && attempts < WIFI_CONNECTION_MAX_ATTEMPTS) 
  {
    delay(WIFI_CONNECTION_RETRY_DELAY);
    attempts++;
  }
}


void disconnectWifi()
{
  WiFi.disconnect(true);
}