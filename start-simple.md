---
description: >-
  for each desired change, make the change easy (warning: this may be hard),
  then make the easy change— Kent Beck (@KentBeck) September 25, 2012
---

# Start Simple

## Example 1

A simple example from one of my project where based on the command line input, it decides which part of the program needs to be executed

```csharp

class Program
{
    static int Main(string[] args)
    {
        try
        {
            if (FeatureToggler.Feature1234)
            {
                if (args.Length == 1 && args[0] == "ABC")
                    LaunchFunctionalityX();
                else if (args.Length == 1 && args[0] == "LMN")
                    LaunchFunctionalityY();
                else if (args.Length == 1 && args[0] == "XYZ")
                    LaunchFunctionalityZ();
                else
                    Console.WriteLine("Incorrect Arguments");
            }
            return 0;
        }
        catch (Exception ex)
        {
            Trace.SendMail("Processing Error", "Error in Main", ex);
        }
        return 1;
    }

    private static void LaunchFunctionalityX()
    {
        var instanceProvider = DependencyContainerFactory.GetContainer(
                                ContainerType.Windsor, ApplicationName.APP_NAME);
        var appServiceX = instanceProvider.Resolve<IAppServiceX>();
        var logger = instanceProvider.Resolve<IDetailedLogger>();

        try
        {
            appServiceX.DoFunctionalityX();
        }
        catch (Exception)
        {
            logger.WriteProcessLog("Functionality X :", "Error description...");
            throw;
        }
    }

    private static void LaunchFunctionalityY()
    {
        var instanceProvider = DependencyContainerFactory.GetContainer(
                                ContainerType.Windsor, ApplicationName.APP_NAME);
        var appServiceY = instanceProvider.Resolve<IAppServiceY>();
        var logger = instanceProvider.Resolve<IDetailedLogger>();

        try
        {
            appServiceY.DoFunctionalityY(new Guid());
        }
        catch (Exception ex)
        {
            logger.WriteProcessLog("Functionality Y :", 
                EnumHelper.EnumToDescription(ProcessStatus.Failure));
            Trace.SendMail("Functionality Y  Error", "...", ex);
            throw;
        }
    }

    private static void LaunchFunctionalityZ()
    {
        var instanceProvider = DependencyContainerFactory.GetContainer(
                                ContainerType.Windsor, ApplicationName.APP_NAME);
        var appServiceZ = instanceProvider.Resolve<IAppServiceZ>();
        var logger = instanceProvider.Resolve<IDetailedLogger>();

        try
        {
            appServiceZ.DoFunctionalityZ();
        }
        catch (Exception ex)
        {
            logger.WriteProcessLog("Functionality Z  :", 
            EnumHelper.EnumToDescription(ProcessStatus.Failure));
            throw;
        }
    }
}
```

Once you're strong enough, save the world:

{% code title="hello.sh" %}
```bash
# Ain't no code for that yet, sorry
echo 'You got to trust me on this, I saved the world'
```
{% endcode %}



