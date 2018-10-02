using System;

namespace AlbionTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var albionTracker = new AlbionTracker())
                {
                    albionTracker.Start();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}
