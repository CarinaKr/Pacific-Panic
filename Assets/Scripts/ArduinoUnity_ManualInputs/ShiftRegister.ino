

int numberToDisplay;

void readByte(byte inByte)
{
  
  if(inByte=='a')
  {
    bytes[0]=(bytes[0]+1)%2;
  }
  else if(inByte=='b')
  {
    bytes[1]=(bytes[1]+1)%2;
  }
  else if(inByte=='c')
  {
    bytes[2]=(bytes[2]+1)%2;
  }
  else if(inByte=='d')
  {
    bytes[3]=(bytes[3]+1)%2;
  }
  else if(inByte=='e')
  {
    bytes[4]=(bytes[4]+1)%2;
  }
  else if(inByte=='f')
  {
    //bytes[5]=(bytes[0]+1)%2;
    enableMotor(0);
  }
  else if(inByte=='g')
  {
    //bytes[6]=(bytes[0]+1)%2;
    enableMotor(1);
  }
  else if(inByte=='h')
  {
    //bytes[7]=(bytes[0]+1)%2;
    enableMotor(2);
  }
  
  

}

void calculateNumber()
{
  numberToDisplay=0;
  numberToDisplay=bytes[0]
                  +(bytes[1]*2)
                  +(bytes[2]*4)
                  +(bytes[3]*8)
                  +(bytes[4]*16)
                  +(bytes[5]*32)
                  +(bytes[6]*64)
                  +(bytes[7]*128);
}

void output() {
    calculateNumber();
    //Serial.print("Number");
    //Serial.println(numberToDisplay);
    // take the latchPin low so 
    // the LEDs don't change while you're sending in bits:
    digitalWrite(latchPin, LOW);
    // shift out the bits:
    shiftOut(dataPin, clockPin, MSBFIRST, numberToDisplay);  
    //take the latch pin high so the LEDs will light up:
    digitalWrite(latchPin, HIGH);
    delay(50);
}
