// SET THESE VARIABLES USING YOUR ACCELEROMETER'S OUTPUT
const int baseX = 509;
const int baseY = 511;
const int baseZ = 608;
const int rangeX = 50;
const int rangeY = 50;
const int rangeZ = 50;

int getXInput(int xPin) {
  // Listen to touch input for short interval, recording max signal
  unsigned long startTime = millis();
  int totalSamples = 33;
  int currentSample;
  int x = 0;
  
  while (currentSample < totalSamples) {
    x += analogRead(xPin);
    currentSample++;
  }
  x /= totalSamples;

/*
  // Map and limit values
  if (x < baseX - rangeX)
    x = baseX - rangeX;
  else if (x > baseX + rangeX)
    x = baseX + rangeX;

  x = map(x, baseX - rangeX, baseX + rangeX, 0, 255);*/

  return x;
}

int getYInput(int yPin) {
  // Listen to touch input for short interval, recording max signal
  unsigned long startTime = millis();
  int totalSamples = 33;
  int currentSample;
  int y = 0;
  
  while (currentSample < totalSamples) {
    y += analogRead(yPin);
    currentSample++;
  }
  y /= totalSamples;

  /*// Map and limit values
  if (y < baseY - rangeY)
    y = baseY - rangeY;
  else if (y > baseY + rangeY)
    y = baseY + rangeY;

  y = map(y, baseY - rangeY, baseY + rangeY, 0, 255);*/

  return y;
}

int getZInput(int zPin) {
  // Listen to touch input for short interval, recording max signal
  unsigned long startTime = millis();
  int totalSamples = 33;
  int currentSample;
  int z = 0;
  
  while (currentSample < totalSamples) {
    z += analogRead(zPin);
    currentSample++;
  }
  z /= totalSamples;

  /*// Map and limit values
  if (z < baseZ - rangeZ)
    z = baseZ - rangeZ;
  else if (z > baseZ + rangeZ)
    z = baseZ + rangeZ;

  z = map(z, baseZ - rangeZ, baseZ + rangeZ, 0, 255);*/

  return z;
}
