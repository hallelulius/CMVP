/*
 * Communication program
 * This program is used to send the steer and throttle signals to the transmitter
 * Could change binary to hexadecimal for better reading but whatever
 *
 * Comments: Baud rate sets the transmission speed for the bits. Set to as high as possible.
 * Lower it if any problems.
 * Make sure to check the output voltages with a multimeter or something to prevent burning the transmitter
 * 
 * Circuit info: 
 * TODO
 */

#include <SPI.h>
#include "pins_arduino.h"

// Load pin
const int loadPin = 9;

//addresses for the DACs
const byte throttleA = 0b00000000;    // DAC A gain 1
const byte throttleB = 0b00000010;    // DAC B gain 1
const byte steeringA = 0b00000100;    // DAC C gain 1
const byte steeringB = 0b00000110;    // DAC D gain 1

//alternative addressing not used
const byte DAC_Array[] = {0b00000000,0b00000010,0b00000100,0b00000110};

//default values for testing
const byte dac_off = 0b00000000; //output = 0V
const byte dac_on = 0b11111111;  //output = Vref

//Variables used to debug and trim  Vref=3.3V
const byte outMax_Throttle = 0b00111001;      //output = 0.74V
const byte outNeutral_Throttle = 0b01101111;  //output = 1.44V
const byte outReverse_Throttle = 0b10011101;  //output = 2.03V
const byte outNeutral_Steering = 0b01110010;  //output = 1.47V
const byte outLeft_Steering = 0b11011010;     //output = 2.82V
const byte outRight_Steering = 0b00000111;    //output = 0.09V

byte incomingByte = 0;
//Setup
void setup() 
{

  digitalWrite(SS, LOW);      // may be unneccesary, is the clock running without this setting?
  pinMode(loadPin, OUTPUT);   
  digitalWrite(loadPin,HIGH);

  SPI.begin();
  delay(500); 
  SPI.setDataMode(SPI_MODE1);           // Settings from the datasheet, reads at negative edge, clock=0 is idle
  SPI.setClockDivider(SPI_CLOCK_DIV16); // Match clock speed with DAC
  SPI.setBitOrder(MSBFIRST);            // Most significant bit should be sent first

  Serial.begin(115200);
  delay(1000);
  Serial.println("Setup complete");
}

//Main Loop
void loop() 
{ 
  steeringTest();
  delay(100);
  throttleTest();
  delay(100);
 }

void changeDAC(byte DAC, byte value){
  SPI.transfer(DAC);
  SPI.transfer(value);
  digitalWrite(loadPin,LOW);
  delay(6);
  digitalWrite(loadPin,HIGH);
}

  /**
   * This method will handle the serial inputs from the CMVP program
   */

void program(){
  if (Serial.available() > 1){ //if at least two bytes available
    incomingByte = Serial.read();
    if (incomingByte == throttleA){
       changeDAC(throttleA,Serial.read());
     }else if (incomingByte == throttleB){
       changeDAC(throttleB,Serial.read());
     }else if (incomingByte == steeringA){
       changeDAC(steeringA,Serial.read());
     }else if (incomingByte == steeringB){
       changeDAC(steeringB,Serial.read());
    } else {
       // DAC not reconigzed 
    }
  } 
}

  /**
   * A methods used to test that the DACS and transmitter works properly
   **/
   
void debug_delayed_updates(){
  
  changeDAC(throttleA, dac_on);
  delay(1500);
  changeDAC(throttleB, dac_off);
  delay(1500);
  changeDAC(throttleA, 0b10000000);
  delay(1500);
}

void debug_fast_updates(){
  for(int i=0; i<200; i++)
    changeDAC(throttleA, dac_on);
  for(int i=0; i<200; i++)
    changeDAC(throttleA, dac_off);
  for(int i=0; i<200; i++)
    changeDAC(throttleA, 0b10000000);
}

void test_DAC(){
  changeDAC(throttleA,outNeutral_Throttle);
  changeDAC(throttleB,0b00000000);
  changeDAC(steeringA,outRight_Steering);
  changeDAC(steeringB,0b00000000);
  while(1);
 }

//steering test for one car
void steeringTest(){
    changeDAC(throttleA,outNeutral_Throttle);  
   for (byte i= outRight_Steering; i<outLeft_Steering; i++){
     changeDAC(steeringA, i);
   }
  
  for (byte i= outLeft_Steering; i>outRight_Steering; i--){
     changeDAC(steeringA, i);
   } 
}
//throttle test for one car
void throttleTest(){
  changeDAC(throttleA,outNeutral_Throttle);  
   for (byte i= outNeutral_Throttle; i>outMax_Throttle; i--){
     changeDAC(throttleA, i);
   }
   for (byte i= outMax_Throttle; i>outNeutral_Throttle; i++){
     changeDAC(throttleA, i);
   }
}
