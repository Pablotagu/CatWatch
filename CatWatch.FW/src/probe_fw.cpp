#include <OneWire.h>
#include <DallasTemperature.h>
#include "config.h"
#include "utils.hpp"

OneWire oneWire(BUS_IDX);
DallasTemperature sensors(&oneWire);


void setup() {
  if(DEBUG)
  {
    delay(DEBUG_START_WAIT_TIME);
  }

  Serial.begin(BAUDS); 
  sensors.begin();
}


void loop() {
  sensors.requestTemperatures(); 
  float temperature = sensors.getTempCByIndex(0);
  
  if(DEBUG)
  {
    Serial.print("Temperature: ");
    Serial.print(temperature);
    Serial.println(" ºC");

    Serial.println(WiFi.status());
  }

  sendReading(temperature);
  esp_deep_sleep(LOOP_WAIT_TIME);
}
