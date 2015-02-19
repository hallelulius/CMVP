/*
 * Old communication program when LP-filter was used
 *
 */


//Which pins on the arduino that are used to control the transmitter
const int ThrottlePin = 9; 
const int SteeringPin = 10;

//Some testing variables 
//NOT TESTED SOME OF THE VALUES MIGHT RESULT IN TO HIGH VOLTAGES RESULTING IN DAMGING THE TRANSMITTER
const byte TP_val_init = 125;          // value output to the PWM pin 0-255. Sets the dutycycle
const byte SP_val_init = 76;   
const byte TP_val_zero = 0;                 
const byte SP_val_zero = 0;           
const byte TP_val_high = 255;   
const byte SP_val_high = 255;   
const byte TP_val_low = 10;   
const byte SP_val_low = 10;   

byte TP_val;
byte SP_val;
byte prev_TP_val = 255;
byte prev_SP_val= 255;


//Setup
void setup() 
{
  
  Serial.begin(115200);
  //Serial.begin(9600);
  
  // set the potentiometer pins as outputs:
  pinMode(ThrottlePin, OUTPUT);
  pinMode(SteeringPin, OUTPUT);
  
  // reset all pins to "zero"
  analogWrite(ThrottlePin , TP_val_init);
  analogWrite(SteeringPin, SP_val_init);
 
  delay(1000);
  Serial.println("Setup complete");
}

//Main Loop
void loop() 
{
 test();
}

void test(){
  
  analogWrite(ThrottlePin , TP_val_zero);
  analogWrite(SteeringPin, SP_val_zero);
  Serial.println("Output set to zero"); 
  delay(5000);
  
  analogWrite(ThrottlePin , TP_val_low);
  analogWrite(SteeringPin, SP_val_low);
  Serial.println("Output set to low"); 
  delay(5000);
  
  analogWrite(ThrottlePin , TP_val_low);
  analogWrite(SteeringPin, SP_val_low);
  Serial.println("Steering set to high"); 
  delay(5000);
}


//from old project
void program(){
 //Read Buffer
  if (Serial.available()==2)
  {
    TP_val = Serial.read();
    SP_val = Serial.read();

    if (TP_val != prev_TP_val || SP_val != prev_SP_val)
    {
      analogWrite(ThrottlePin, TP_val);
      analogWrite(SteeringPin, SP_val);
      TP_val = prev_TP_val;
      SP_val = prev_SP_val;
    }
  }
}

