# CodeKata

In response to https://github.com/thanh81/CodeKata, created this project to implement the problem statement using a Console application using Dotnet Core3.1 framework.

The code will process an input file and produce a console output summarising the driver and trip.

Usage: ConsoleApp C:/input.txt

Each line in the input file will start with a command. There are two possible commands.
The first command is Driver, which will register a new Driver in the app. Example:
Driver Dan
The second command is Trip, which will record a trip attributed to a driver. The line will be space delimited with the following fields: the command (Trip), driver name, start time, stop time, miles driven. Times will be given in the format of hours:minutes. We'll use a 24-hour clock and will assume that drivers never drive past midnight (the start time will always be before the end time). Example:
Trip Dan 07:15 07:45 17.3
Discard any trips that average a speed of less than 5 mph or greater than 100 mph.
Generate a report containing each driver with total miles driven and average speed. Sort the output by most miles driven to least. Round miles and miles per hour to the nearest integer.

Example input:

Trip Alex 12:01 24:10 42.0
Driver Dan
Trip Dan 07:15 07:45 17.3
Driver Alex
Trip Dan 06:12 06:32 21.8
Driver Bob

Trip Dan 07:15 07:45 17.3

Trip Dan 06:12 06:32 21.8

Trip Alex 12:01 13:16 42.0




Expected output:

Dan: 78 miles @ 47 mph

Alex: 42 miles @ 34 mph

Bob: 0 miles
