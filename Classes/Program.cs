namespace FileOrganizer.Classes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                FileOrganizer organizer = new FileOrganizer();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            Task.Delay(1000);

            Console.Write("\nPress any key to exit: ");

            Console.ReadKey();
        }
    }
}
