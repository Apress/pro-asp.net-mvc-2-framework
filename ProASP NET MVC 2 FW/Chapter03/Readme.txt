To run IntegrationTestingExample, follow these steps:

[1] If you want to edit the .feature file in IntegrationTestingExample, you must first install SpecFlow from http://www.specflow.org/
[2] Open IntegrationTestingExample.sln in Visual Studio 2010 and build it (Ctrl-Shift-B, or choose Build -> Build Solution)
[2] Run the IntegrationTestingExample web application by pressing Ctrl-F5 or choosing Debug -> Start Without Debugging. This will launch the application on port 8080.
[4] You can now use NUnit GUI (downloadable from www.nunit.org) to run the WatiN tests and SpecFlow feature assemblies that appeared in IntegrationTestingExample.WatiNTests\bin\Debug\IntegrationTestingExample.WatiNTests.dll and IntegrationTestingExample.SpecFlowSpec\bin\Debug\IntegrationTestingExample.SpecFlowSpec.dll when you compiled