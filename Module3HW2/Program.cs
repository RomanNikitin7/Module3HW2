using System.Globalization;

namespace Module3HW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = new PhoneBook();
            var en = new CultureInfo(PhoneBook.EnglishCultureName);
            var uk = new CultureInfo(PhoneBook.UkrainianCultureName);

            phoneBook.Add(new Contact("John", "Doe", "342234532"), en);
            phoneBook.Add(new Contact("Anna", "Lee", "623454356"), en);
            phoneBook.Add(new Contact("James", "007", "4546547"), en);
            phoneBook.Add(new Contact("Іван", "Петлюра", "325415"), uk);
            phoneBook.Add(new Contact("Іво Бобул", "001", "15611354"), en);
            phoneBook.Add(new Contact("Paul", "McArtney", "95611456"), uk);

            foreach (var item in phoneBook)
            {
                var chapter = (KeyValuePair<string, List<IContact>>) item;
                Console.WriteLine("Chapter " + chapter.Key);
                foreach (var contact in chapter.Value)
                {
                    Console.WriteLine(((Contact)contact).FirstName + " " + contact.GetKey());
                }

                Console.WriteLine();
            }
        }
    }
}
