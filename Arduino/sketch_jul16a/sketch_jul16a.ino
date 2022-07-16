
int number = 0;

bool canPrint = false;

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  if(canPrint) {
    Serial.println(number);
    canPrint = false;
  } else {
    canPrint = true;
  }

  number = number + 1;

  delay(1000);
}
