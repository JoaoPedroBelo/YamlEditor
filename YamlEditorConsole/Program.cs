using System;
using Logging;

namespace YamlEditorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Recorder = new Logging.DateRecorderDecorator(new CounterDecorator(new ConsoleRecorder()));
            var configuration = new YamlFile("C:/Users/Dfmar/source/repos/YamlEditor/Examples/Apocrathia/homeassistant/configuration.yaml");

            //configuration.SaveFile();
            //configuration.WriteToConsole();
            Console.ReadKey();
        }
    }
}