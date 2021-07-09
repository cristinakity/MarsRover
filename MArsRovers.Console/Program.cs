namespace MarsRovers.Console
{
    using MarsRovers.Core;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            string inputData = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n3 3 E\nMMRMMRMRRMMMMM\n3 3 E\nMMLMMRMLMLMMRMMMRMMRMRRM\n3 3 E\nMMRMMRMRRM";
            bool showLog = true;
            var navigation = new Navigation(showLog);
            Console.WriteLine(navigation.Navigate(inputData));
        }
    }
}
