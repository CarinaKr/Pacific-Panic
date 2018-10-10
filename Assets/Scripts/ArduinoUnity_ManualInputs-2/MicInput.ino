int getMicInput(int analogPin) {
  // Listen to mic for short interval, recording min & max signal
  unsigned int signalMin = 1023, signalMax = 0;
  unsigned long startTime = millis();
  
  while ((millis() - startTime) < 33) {
    int sample = analogRead(analogPin);
    if (sample < signalMin) signalMin = sample;
    if (sample > signalMax) signalMax = sample;
  }

  int peakToPeak = signalMax - signalMin; // Max - min = peak-peak amplitude
  int n = (peakToPeak - 10) / 4;          // Remove low-level noise, lower gain
  if (n > 255)    n = 255;                // Limit to valid PWM range
  else if (n < 0) n = 0;

  return n;
}

