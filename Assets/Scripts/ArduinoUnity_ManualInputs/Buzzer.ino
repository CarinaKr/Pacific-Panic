int motorEnabled[3];
int motorBuzzing[3];
int startTime[3];
int buzzTime=600;
int pauseTime=600;

void enableMotor(int motor)
{
  motorEnabled[motor]=(motorEnabled[motor]+1)%2;
  startTime[motor]=millis();
  motorBuzzing[motor]=motorEnabled[motor];
}

void buzzMotors()
{
  for(int i=0;i<3;i++)
  {
    if(motorEnabled[i]==1) //motor is enables
    {
      if(millis()-startTime[i]<buzzTime)
      {
        Serial.print("motor buzzing:");
        Serial.println(i);
        motorBuzzing[i]=1;
      }
      else if(millis()-startTime[i]<buzzTime+pauseTime)
      {
        motorBuzzing[i]=0;
      }
      else if(millis()-startTime[i]>buzzTime+pauseTime)
      {
        startTime[i]=millis();
      }
    }
    bytes[5+i]=motorBuzzing[i];   //enable/disable byte for shift register
  }
}

