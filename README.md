Parking System
This is a simple parking system program that allows you to manage a parking lot. It provides various commands to create parking lots, park vehicles, remove vehicles, and display the current status of the parking lot.

Commands
create_parking_lot <n>: Create a parking lot with <n> slots.
park <registration_number> <color>: Create a new vehicle parking entry with the given registration number and color.
leave <slot_number>: Remove a vehicle from the parking lot at the specified slot number.
status: Display the current status of the parking lot.
exit: Close the program.
Usage
Run the program.
Enter the desired command from the list above.
Follow the command format and provide the required information.
View the output or follow the instructions displayed.
For more detailed command usage and examples, please refer to the full command documentation available in the doc.txt file.


$ create_parking_lot 5
Created a parking lot with 5 slots

$ park ABC-123 Red
Allocated slot number: 1

$ status
Slot     Registration No.     Color
1        ABC-123              Red

$ leave 1
Slot number 1 is free

$ status
Slot     Registration No.     Color


Notes
Registration numbers and colors are case-insensitive.
Slot numbers start from 1 and increment sequentially.
The program will prompt for user input and display output accordingly.
Thank you for using the parking system!
