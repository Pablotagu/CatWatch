#include <WiFi.h>
#include <HTTPClient.h>


void sendReading(float temperature)
{
  if (WiFi.status() != WL_CONNECTED) 
  {
    if(DEBUG)
    {
      Serial.println("WiFi not connected. Cannot send reading.");
    }
    return;
  }

  HTTPClient http;
  http.begin(API_URL);
  
  http.addHeader("Content-Type", "application/json");
  http.addHeader("x-api-key", PROBE_API_KEY);
  
  String payload = "{\"probeId\":\"" + String(PROBE_ID) + "\",\"temperature\":" + String(temperature) + "}";
  int httpResponseCode = http.POST(payload);
  if (DEBUG) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
  }
  http.end();
  
} 
void connectToWiFi(){
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

  if(DEBUG)
  {
    Serial.print("Connecting to WiFi...");
  }
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    if(DEBUG)
    {
      Serial.print(".");
    }
  }
  if(DEBUG)
  {
    Serial.println("Connected!");
  }
  
}
