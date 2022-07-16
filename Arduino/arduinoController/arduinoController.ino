// configurazione pin bottoni
const int buttonPin_0 = 10;
const int buttonPin_1 = 11;
const int buttonPin_2 = 2;
const int buttonPin_3 = 3;
const int buttonPin_4 = 4;
const int buttonPin_5 = 5;
const int buttonPin_6 = 6;
const int buttonPin_7 = 7;
const int buttonPin_8 = 8;

// configurazione pin motori
const int vibrationMotor_1 = 12;
const int vibrationMotor_2 = 12;

// stato bottoni
int buttonState_0 = 0;
int buttonState_1 = 0;
int buttonState_2 = 0;
int buttonState_3 = 0;
int buttonState_4 = 0;
int buttonState_5 = 0;
int buttonState_6 = 0;
int buttonState_7 = 0;
int buttonState_8 = 0;

// buttons current configuration
String buttonState = "";
// latest button configuration
String latestButtonState = "";

// variable to store incoming serial data
int incomingByte = 0;

// arduino configuration
int baudRate = 9600;

void setup()
{
  Serial.begin(9600);
  pinMode(buttonPin_0, INPUT);
  pinMode(buttonPin_1, INPUT);
  pinMode(buttonPin_2, INPUT);
  pinMode(buttonPin_3, INPUT);
  pinMode(buttonPin_4, INPUT);
  pinMode(buttonPin_5, INPUT);
  pinMode(buttonPin_6, INPUT);
  pinMode(buttonPin_7, INPUT);
  pinMode(buttonPin_8, INPUT);
}

void loop()
{

  // check if there is an incoming string in the Serial port
  if(Serial.available() > 0) 
  {
    // read the incoming byte
    int buttonID = (Serial.readString()).toInt();

    if(buttonID == 2)
    {
      makeVibrateMotors(0);
    }
    if(buttonID == 3)
    {
      makeVibrateMotors(1);
    }
    if(buttonID == 5)
    {
      makeVibrateMotors(2);
    }
    
  }
  
  // read the state of buttons
  buttonState_0 = digitalRead(buttonPin_0);
  buttonState_1 = digitalRead(buttonPin_1);
  buttonState_2 = digitalRead(buttonPin_2);
  buttonState_3 = digitalRead(buttonPin_3);
  buttonState_4 = digitalRead(buttonPin_4);
  buttonState_5 = digitalRead(buttonPin_5);
  buttonState_6 = digitalRead(buttonPin_6);
  buttonState_7 = digitalRead(buttonPin_7);
  buttonState_8 = digitalRead(buttonPin_8);

  // create the buttonState string for state comparison
  buttonState = String(buttonState_0) + "," +
                String(buttonState_1) + "," +
                String(buttonState_2) + "," +
                String(buttonState_3) + "," +
                String(buttonState_4) + "," +
                String(buttonState_5) + "," +
                String(buttonState_6) + "," +
                String(buttonState_7) + "," +
                String(buttonState_8);

  // check if the previous state is equal to the current one
  if(buttonState != latestButtonState)
  {
    Serial.println(buttonState);
  }

  // update latest button state to the current one
  latestButtonState = buttonState;
}

void makeVibrateMotors(int configuration) 
{
  if(configuration == 0)
  {
    analogWrite(vibrationMotor_1, 255);
    delay(3000);
    analogWrite(vibrationMotor_1, 0);
  }
  else if(configuration == 1)
  {
    analogWrite(vibrationMotor_2, 255);
    delay(3000);
    analogWrite(vibrationMotor_2, 0);
  }
  else if(configuration == 2)
  {
    analogWrite(vibrationMotor_1, 255);
    analogWrite(vibrationMotor_2, 255);
    delay(3000);
    analogWrite(vibrationMotor_1, 0);
    analogWrite(vibrationMotor_2, 0);
  }
}
