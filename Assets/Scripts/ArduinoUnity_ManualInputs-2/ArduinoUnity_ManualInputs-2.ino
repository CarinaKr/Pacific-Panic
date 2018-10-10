#include <SoftReset.h>

// DON'T FORGET TO SET THIS!
const int totalInputs = 18;

int inputs[totalInputs];
int inByte;

// Store the acc reading
int x,y,z;
// store the volume
int volume;

//shift register
int latchPin = 8;
int clockPin = 12;
int dataPin = 11;
int bytes[8];

void setup() {
  pinMode(2, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(10, INPUT_PULLUP);
  pinMode(11, INPUT_PULLUP);
  pinMode(12, INPUT_PULLUP);
  pinMode(13, INPUT_PULLUP);

  //PUT IN FOR ARDUINO 1; TAKE OUT FOR ARDUINO 2!!
   //set pins to output so you can control the shift register
  /*pinMode(latchPin, OUTPUT);
  pinMode(clockPin, OUTPUT);
  pinMode(dataPin, OUTPUT);
*/
  Serial.begin(9600);
  while (!Serial) {
    ;
  }
  establishContact();
}

void loop() {

  x = getXInput(A0);
  y = getYInput(A1);
  z = getZInput(A2);
  volume=getMicInput(A3);
  sendToUnity();
  buzzMotors();
  output();
}

//
void establishContact() {
  while (Serial.available() <= 0) {
    Serial.print('A');   // send a capital A
    delay(600); // Slow this down to eliminate the extra '65' in Unity
  }
}

void sendToUnity () {
  if (Serial.available() > 0) {
    inByte = Serial.read();
    if (inByte == 'Z') {
      softReset();
    }
    else if ( inByte == 'A') {
      // Always analog inputs first...
      //Accelerometer
      inputs[0] = x / 4;
      inputs[1] = y / 4;
      inputs[2] = z / 4;
      inputs[3]=volume;
      //Poti
      //inputs[3]=analogRead(A3)/4;

      // then digital inputs
      inputs[4]=0;
      inputs[5]=0;
      inputs[6] = digitalRead(2);
      inputs[7] = digitalRead(3);
      inputs[8] = digitalRead(4);
      inputs[9] = digitalRead(5);
      inputs[10] = digitalRead(6);
      inputs[11] = digitalRead(7);
      inputs[12]= digitalRead(8);
      inputs[13]= digitalRead(9);
      inputs[14]= digitalRead(10);
      inputs[15]= digitalRead(11);
      inputs[16]= digitalRead(12);
      inputs[17]= digitalRead(13);
      

      for (int i = 0; i < totalInputs; i++) {
        Serial.write(inputs[i]);
        //Serial.println(inputs[i]);
      }
    }

    else
    {
      readByte(inByte);
    }
    Serial.flush();
  }
}

void softReset() {
  //asm volatile ("  jmp 0");
  soft_restart();
}
