/*
 * Serial test program
 * Used to make sure that serial communication between C# and Arduino 
 * is working properly
 * 
 * Comments: Three LEDs should light up according to the if-statement
 * when sending a byte with write() or writeline in C#. Remember to sync
 * Baudrates if any problems.
 */
 

const int LEDpin1 = 2;   //red
const int LEDpin2 = 3;   //yellow
const int LEDpin3 = 4;   //green
byte incomingByte = 0;

void setup()
{
  pinMode(LEDpin1,OUTPUT);
  pinMode(LEDpin2,OUTPUT);
  pinMode(LEDpin3,OUTPUT);
 
  Serial.begin(115200);
}

void loop()
{
  if (Serial.available() > 1){
    incomingByte = Serial.read();
    incomingByte = Serial.read();
    if(incomingByte == 0){
        digitalWrite(LEDpin1,LOW);
        digitalWrite(LEDpin2,HIGH);
        digitalWrite(LEDpin3,HIGH);
    }else if (incomingByte == 44){
        digitalWrite(LEDpin2,LOW);
        digitalWrite(LEDpin1,HIGH);
        digitalWrite(LEDpin3,HIGH);
    } else {
        digitalWrite(LEDpin3,LOW);
        digitalWrite(LEDpin1,HIGH);
        digitalWrite(LEDpin2,HIGH);
    }  
  }  
}
