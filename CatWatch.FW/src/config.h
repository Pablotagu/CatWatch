#include "secrets.h"


// DEBUG ONLY
// This is used to wait for the USB port to be free after uploading, so the serial monitor can display the initial debug messages.
const int DEBUG_START_WAIT_TIME = 5000;  // MILLISECONDS

const bool DEBUG = true;
const int BUS_IDX = 5; 
const int BAUDS = 115200;
const int WIFI_CONNECTION_RETRY_DELAY = 500; // MILLISECONDS
const int WIFI_CONNECTION_MAX_ATTEMPTS = 20;
const long LOOP_WAIT_TIME = 5LL * 60 * 1000 * 1000; // MICROSECONDS

const char* WIFI_SSID = S_WIFI_SSID;
const char* WIFI_PASSWORD = S_WIFI_PASSWORD;
const char* API_URL = S_API_URL;
const char* PROBE_ID = S_PROBE_ID;
const char* PROBE_API_KEY = S_PROBE_API_KEY;