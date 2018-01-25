# DoctorSlots

## Solution structure

The solution consists on three main projects:
* **DoctorSlots.Api:** REST API using the .NET Core 2.0 framework.
* **DoctorSlots.Tests:** Library class project with unit tests for the API REST.
* **DoctorSlots.Frontend:** Angular 5 project created with Angular CLI.

## API configuration & running
1. Navigate to *DoctorSlots.Api folder*.
1. Copy the provided ***private-settings.json*** (contact the repository owner) file into the root directory.
2. Run the project in Visual Studio (F5).

## Frontend configuration & running
1. Navigate to *DoctorSlots.Frontend* folder.
2. Run the command `npm install`.
3. Edit the file ***proxy.conf.json*** and update the *target* field with the *localhost:port* value assigned to the Api project by Visual Studio.
4. Run the command `npm run start`.
5. Browse to http://localhost:4200

## Run API unit tests
1. Open the Visual Studio solution.
2. Build the project *DoctorSlots.Api*.
3. Press the keys `CTRL + R,A` (from Visual Studio) to run all unit tests.

## Run Angular unit tests
1. Navigate to *DoctorSlots.Frontend* folder.
2. Run the command `npm run test`.
