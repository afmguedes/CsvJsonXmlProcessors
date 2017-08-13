# CsvJsonXmlProcessors

A set of different file processors that are responsible to read different lists of users from 3 different file formats, ```CSV```, ```JSON``` and ```XML```, and write the agregated and ordered list of users back to the different file formats.


## Requirements

* Read the users from the xml, csv and json files within the `data` directory
* Merge all users into a single list and sort them by their `userId` in ascending order
* Write the ordered results to new xml, csv and json files, see the `examples` directory
  * Results should use the same structure as the source files they were parsed from
  * The exception is for `lastLoginTime` where an `ISO 8601` date format is preferred for output

  
## Deployment

Instructions on how to deploy and run the application.

Please make sure you have .NET Core 1.1.2 installed.

Get the solution from this repository.


### From Visual Studio

Open the solution on Visual Studio 2015.

Press Crtl+F5.


### From the Command Prompt

Navigate to the solution folder.

Run the application with the following command:

``` dotnet src\CsvJsonXmlProcessors\bin\Release\PublishOutput\CsvJsonXmlProcessors.dll ```
