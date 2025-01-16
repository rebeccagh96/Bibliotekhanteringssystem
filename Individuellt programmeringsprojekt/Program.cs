using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Runtime.CompilerServices;
using Individuellt_programmeringsprojekt;//Här ger jag program.cs tillgång till klassen Bibliotek som finns i Bibliotek.cs.

namespace Individuellt_programmeringsprojekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ändrar färger i fönstret.
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            //Lägger till några böcker i databasen.
            AddBibliotek("Harry Potter", "J.K Rowling", "123456789", true);
            AddBibliotek("Pippi Långstrump", "Astrid Lindgren", "987654321", true);
            AddBibliotek("Bamse", "Rune Andréasson", "987123456", true);
            //Här skapar jag en lista som innehåller stringarrays och namnger den inlogg.
            //Här sparas alla inlogg för användare.
            List<string[]> inlogg = new List<string[]>();
            //Här skapar jag en lista som innehåller stringarrays och namnger den inloggAdmin.
            //Här sparas alla inlogg för administratörer.
            List<string[]> inloggAdmin = new List<string[]>();

            //Här skapar jag en bool för menyn som är true ända tills användaren väljer avsluta i menyn,
            //då blir den false och programmet avslutas.
            bool meny1 = true;
            while (meny1)
            {
                //Här är den yttersta(första) menyn, där man kan logga in som användare eller administratör eller registrera sig eller avsluta programmet.
                Console.Clear();
                Console.WriteLine("Välkommen till biblioteket!");
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("[1] Logga in som användare");
                Console.WriteLine("[2] Logga in som administratör");
                Console.WriteLine("[3] Registrera dig");
                Console.WriteLine("[4] Avsluta");
                Int32.TryParse(Console.ReadLine(), out int menyval1);

                switch (menyval1)
                {
                    case 1:
                        //Menyval 1 där användaren kan logga in genom att skriva in först sitt
                        //användarnamn och sedan sitt lösenord.
                        Console.Clear();
                        Console.WriteLine("Logga in");
                        Console.Write("Användarnamn: ");
                        string username = Console.ReadLine();
                        Console.Write("Lösenord: ");
                        string password = Console.ReadLine();
                        bool login = false;//En bool som bli true när användaren är inloggad.
                        for (int i = 0; i < inlogg.Count; i++)//For-loop som kontrollerar om användarnamnet och lösenordet finns i listan inlogg.
                        {
                            if (inlogg[i][0] == username)
                            {
                                if (inlogg[i][1] == password)
                                {
                                    //Här blir login true då användaren är inloggad.
                                    //använder samma bool för menyn då menyn ska köras hela tiden som användaren är inloggad och inte trycker på [4] Avsluta.
                                    login = true;
                                    //Denna meny visas för användare.
                                    while (login)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Välkommen till biblioteket!");
                                        Console.WriteLine("Vad vill du göra idag?");
                                        Console.WriteLine("[1] Låna en bok");
                                        Console.WriteLine("[2] Återlämna en bok");
                                        Console.WriteLine("[3] Söka efter bok");
                                        Console.WriteLine("[4] Avsluta");
                                        Int32.TryParse(Console.ReadLine(), out int menyval);

                                        switch (menyval)
                                        {
                                            //Menyval 1 där användaren kan låna en bok.
                                            case 1:
                                                Console.Clear();
                                                Console.Write("Skriv titeln på den bok du vill låna: ");
                                                var input = Console.ReadLine();
                                                Låna(input);//Anropar metoden Låna som hanterar låneprocessen.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 2 där användaren kan återlämna en bok.
                                            case 2:
                                                Console.Clear();
                                                Console.Write("Skriv in titeln på den bok du vill återlämna: ");
                                                var återlämnaBok = Console.ReadLine();
                                                Återlämna(återlämnaBok);//Anropar metoden Återlämna som hanterar återlämningsprocessen.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 3 där användaren kan söka efter boktitel, författare eller ISBN.
                                            case 3:
                                                Console.Clear();
                                                Console.WriteLine("Skriv titel, författare eller ISBN för den bok du söker.");
                                                string sökord = Console.ReadLine();
                                                Sök(sökord);//Anropar metoden Sök som söker efter titel, författare eller ISBN i databasen.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 4 som avslutar programmet.
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine("Tack för att du besökte biblioteket!");
                                                Console.ReadKey();
                                                login = false;
                                                break;

                                            // Felmeddelande som skrivs ut om användaren skriver något annat än siffrorna 1-4 vid menyvalet.
                                            default:
                                                Console.WriteLine("Du måste skriva en siffra som finns i menyvalen.");
                                                Console.ReadKey();
                                                break;
                                        }
                                    }
                                }
                            }
                            //Om användaren skriver in ett inlogg som inte finns.
                            else if (login == false)
                            {
                                Console.WriteLine("Ogiltigt inlogg till biblioteket.");
                            }
                            Console.ReadKey();
                            break;
                        }
                        break;

                    //Menyval 2 i första menyn, inloggning för administratörer.
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Logga in");
                        Console.Write("Användarnamn: ");
                        string usernameAdmin = Console.ReadLine();
                        Console.Write("Lösenord: ");
                        string passwordAdmin = Console.ReadLine();
                        bool loginAdmin = false;
                        for (int i = 0; i < inloggAdmin.Count; i++)//For-loop som kontrollerar om användarnamnet och lösenordet finns i listan inloggAdmin.
                        {
                            if (inloggAdmin[i][0] == usernameAdmin)
                            {
                                if (inloggAdmin[i][1] == passwordAdmin)
                                {
                                    //Boolen för inloggningen blir nu true vilket innebär att användaren nu är inloggad som administratör.
                                    loginAdmin = true;
                                    //Meny för administratörer.
                                    while (loginAdmin)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Välkommen till biblioteket!");
                                        Console.WriteLine("Vad vill du göra idag?");
                                        Console.WriteLine("[1] Lägga till en bok i biblioteket");
                                        Console.WriteLine("[2] Ta bort bok i biblioteket");
                                        Console.WriteLine("[3] Uppdatera bokinformation");
                                        Console.WriteLine("[4] Söka i biblioteket");
                                        Console.WriteLine("[5] Visa alla böcker i biblioteket.");
                                        Console.WriteLine("[6] Avsluta");
                                        Int32.TryParse(Console.ReadLine(), out int menyval2);

                                        switch (menyval2)
                                        {
                                            //Menyval 1 där administratören kan lägga till böcker i biblioteket.
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine("Lägg till bok här: ");
                                                Console.Write("Skriv bokens titel: ");
                                                string titel = Console.ReadLine();
                                                Console.Write("Skriv bokens författare: ");
                                                string författare = Console.ReadLine();
                                                Console.Write("Skriv bokens ISBN: ");
                                                string isbn = Console.ReadLine();
                                                bool tillgänglig = true;
                                                if (titel != "" && författare != "" && isbn != "")//Kontrollerar att titel, författare och isbn inte är tomma innan boken läggs till i databasen.
                                                {
                                                    AddBibliotek(titel, författare, isbn, tillgänglig);//Anropar metoden AddBibliotek som lägger till ett objekt(bok) i databasen.
                                                    Console.WriteLine("Boken har lagts till i biblioteket!");
                                                }
                                                else 
                                                {
                                                    Console.Write("Något gick fel, försök att lägga till boken på nytt.");
                                                }
                                                Console.ReadKey();
                                                break;

                                            //Menyval 2 där administratörer kan ta bort en bok från biblioteket.
                                            case 2:
                                                Console.Clear();
                                                Console.Write("Skriv in titeln på den bok du vill ta bort: ");
                                                string remove = Console.ReadLine();
                                                RemoveBok(remove);//Anropar metoden RemoveBok som tar bort en bok från biblioteket.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 3 där administratörer kan uppdatera information om en bok som redan finns i biblioteket.
                                            case 3:
                                                Console.Clear();
                                                Console.Write("Vilken titel, författare eller ISBN vill du ändra/uppdatera? ");
                                                string input = Console.ReadLine();
                                                Console.Write("Vad vill du ändra till? ");
                                                string input2 = Console.ReadLine();
                                                Uppdatera(input, input2);//Anropar metoden Uppdatera som ändrar ett visst element i databasen.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 4 där administratörer kan söka i biblioteket.
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine("Skriv titel, författare eller ISBN för den bok du söker.");
                                                string sökord = Console.ReadLine();
                                                Sök(sökord);//Anropar metoden Sök som söker efter titel, författare eller ISBN i databasen.
                                                Console.ReadKey();
                                                break;

                                            //Menyval 5 skriver ut alla objekt i databasen genom metoden PrintAll.
                                            case 5:
                                                Console.Clear();
                                                PrintAll();
                                                Console.ReadKey();
                                                break;

                                            //Menyval 6 där administratören avslutar programmet och boolen loginAdmin blir false.
                                            case 6:
                                                Console.Clear();
                                                Console.WriteLine("Tack för att du besökte biblioteket!");
                                                Console.ReadKey();
                                                loginAdmin = false;
                                                break;

                                            // Felmeddelande som skrivs ut om administratören skriver något annat än siffrorna 1-6 vid menyvalet.
                                            default:
                                                Console.WriteLine("Du måste skriva en siffra som finns i menyvalen.");
                                                Console.ReadKey();
                                                break;
                                        }
                                    }
                                }
                            }
                            else if (loginAdmin == false)//Om användarnamnet och/eller lösenordet inte hittas i listan inloggAdmin skrivs meddelandet nedan ut.
                            {
                                Console.WriteLine("Ogiltigt inlogg till biblioteket.");
                            }
                        }
                        Console.ReadKey();
                        break;

                    //Menyval 3 där användaren får registrera ett användarnamn och
                    //lösenord som sedan sparas som en stringarray vid namn användare i listan inlogg eller som administratör vid namn admin i listan inloggAdmin.
                    case 3:
                        string[] användare = new string[2];
                        string[] admin = new string[2];
                        Console.Clear();
                        Console.WriteLine("Registrera dig här!");
                        Console.WriteLine("Vill du registrera dig som användare [1] eller administratör [2]?");
                        Int32.TryParse(Console.ReadLine(), out int registrering);
                        switch (registrering)
                        {
                            //Regeistrering för användare.
                            case 1:
                                Console.Write("Användarnamn: ");
                                användare[0] = Console.ReadLine();
                                Console.Write("Lösenord: ");
                                användare[1] = Console.ReadLine();
                                if (användare[0] == "" || användare[1] == "")//Felhantering om användaren inte skriver in något användarnamn eler lösenord.
                                    {
                                        Console.WriteLine("Du måste skriva in ett användarnamn och lösenord.");
                                    }
                                else//Om användare skrivit in ett användarnamn och lösenord.
                                {
                                    inlogg.Add(användare);//Lägger till användarens användarnamn och lösenord i listan inlogg.
                                    SortInlogg(inlogg);//Anropar metoden SortInlogg som sorterar listan inlogg efter användarnamn.
                                    Console.Clear();
                                    Console.WriteLine("Ditt konto har blivit registrerat!");
                                }
                                Console.ReadKey();
                                break;
                            //Registrering för administratörer.
                            case 2:
                                Console.Write("Användarnamn: ");
                                admin[0] = Console.ReadLine();
                                Console.Write("Lösenord: ");
                                admin[1] = Console.ReadLine();
                                if (admin[0] == "" || admin[1] == "")//Felhantering om användaren inte skriver in något användarnamn eller lösenord.
                                {
                                    Console.WriteLine("Du måste skriva in ett användarnamn och lösenord.");
                                }
                                else//Om användaren skrivit in ett användarnamn och ett lösenord.
                                {
                                    inloggAdmin.Add(admin);//Lägger till administratörens användarnamn och lösenord i listan inloggAdmin.
                                    SortInloggAdmin(inloggAdmin);//Anropar metoden SortInloggAdmin som sorterar listan inloggAdmin efter användarnamn.
                                    Console.Clear();
                                    Console.WriteLine("Ditt konto har blivit registrerat!");
                                }
                                Console.ReadKey();
                                break;

                            // Felmeddelande som skrivs ut om det skrivs in något annat än 1 eller 2 vid första frågan i menyval 3.
                            default:
                                Console.WriteLine("Du måste skriva 1 eller 2.");
                                Console.ReadKey();
                                break;
                        }
                        break;

                    //Menyval 4 där användaren avslutar programmet.
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Tack för att du besökte biblioteket!");
                        Console.ReadKey();
                        meny1 = false;
                        break;

                    // Felmeddelande som skrivs ut om användaren skriver något annat än siffrorna 1-4 vid menyvalet.
                    default:
                        Console.WriteLine("Du måste skriva en siffra som finns i menyvalen.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        //--------METODER--------//
        //Metod för att lägga till en bok i databasen.
        static void AddBibliotek(string titel, string författare, string isbn, bool tillgänglig)
        {
            using (var context = new BibliotekContext())
            {
                var bok = new Bibliotek
                {
                    Titel = titel,
                    Författare = författare,
                    Isbn = isbn,
                    Tillgänglig = tillgänglig
                };
                context.bibliotek.Add(bok);//Lägger till boken i databasen.
                context.SaveChanges();//Sparar ändringarna som gjorts i databasen.
            }
        }
        //Metod för att låna en bok.
        static void Låna(string input)
        {
            using (var context = new BibliotekContext())
            {
                var bok = context.bibliotek.FirstOrDefault(b => b.Titel.ToLower() == input.ToLower());
                if (bok == null)
                {
                    Console.WriteLine("Boken du försöker låna finns inte!");//Meddelande om det användaren försöker låna inte hittas i databasen.
                }
                else if (bok != null && bok.Titel.ToLower() == input.ToLower() && bok.Tillgänglig == true)//Om det användaren försöker låna hittats i databasen och boolen Tillgänglig är true, alltså att boken inte redan är utlånad.
                {
                    bok.Tillgänglig = false;//Gör boolen Tillgänglig till false vilket innebär att boken nu är utlånad.
                    context.bibliotek.AddOrUpdate(bok);//Lägger till ändringen av boolen i databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du lånar nu boken " + input + ".");
                }
                else if (bok != null && bok.Titel == input && bok.Tillgänglig == false)//Om det användaren försöker låna hittas i databasen men boolen Tillgänglig är false vilket innebär att boken redan är utlånad.
                {
                    Console.WriteLine("Boken du vill låna är redan utlånad!");
                }
            }
        }
        //Metod för att återlämna en bok.
        static void Återlämna(string återlämnaBok)
        {
            using (var context = new BibliotekContext())
            {
                var bok = context.bibliotek.FirstOrDefault(b => b.Titel.ToLower() == återlämnaBok.ToLower());//Söker efter bokens titel i databasen.
                if (bok == null)//Om det användaren försöker återlämna inte hittas i databasen.
                {
                    Console.WriteLine("Det du försöker återlämna hittas inte!");
                }
                else if (bok != null && bok.Titel.ToLower() == återlämnaBok.ToLower() && bok.Tillgänglig == false)//Om det användaren försöker återlämna hittats i databasen och boolen för just den boken är false alltså utlånad.
                {
                    bok.Tillgänglig = true;//Ändrar boolen Tillgänglig till true då boken nu är återlämnad och därmed tillgänglig för att lånas igen.
                    context.bibliotek.AddOrUpdate(bok);//Lägger till ändringen i databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du har nu återlämnat boken " + återlämnaBok + ".");
                }
                else if (bok != null && bok.Titel.ToLower() == återlämnaBok.ToLower() && bok.Tillgänglig == true)//Om det användaren försöker återlämna hittas i databasen och boolen Tillgänglig är true alltså att boken inte är utlånad.
                {
                    Console.WriteLine("Du har inte lånat den här boken så därför kan du inte återlämna den.");
                }
            }
        }
        //Metod för sökning i databasen.
        static void Sök(string sökord)
        {
            using (var context = new BibliotekContext())
            {
                var bokT = context.bibliotek.FirstOrDefault(b => b.Titel.ToLower() == sökord.ToLower());//Söker igenom alla titlar i databasen.
                var bokF = context.bibliotek.FirstOrDefault(b => b.Författare.ToLower() == sökord.ToLower());//Söker igenom alla författare i databasen.
                var bokI = context.bibliotek.FirstOrDefault(b => b.Isbn == sökord);//Söker igenom alla ISBN i databasen.
                if (bokT == null && bokF == null && bokI == null)//Om det användaren söker efter inte hittas i databasen.
                {
                    Console.WriteLine("Det du söker finns inte i biblioteket");
                }
                else if (bokT != null && bokT.Titel.ToLower() == sökord.ToLower())//Om det användaren söker efter hittas bland titlar i databasen.
                {
                    Console.WriteLine("Det du söker finns i bilioteket!");
                }
                else if (bokF != null && bokF.Författare.ToLower() == sökord.ToLower())//Om det användaren söker hittas bland författare i databasen.
                {
                    Console.WriteLine("Det du söker finns i bilioteket!");
                }
                else if (bokI != null && bokI.Isbn == sökord)//Om det användaren söker efter hittas bland ISBN i databasen.
                {
                    Console.WriteLine("Det du söker finns i bilioteket!");
                }
            }
        }
        //Metod för att uppdatera en bok i databasen.
        static void Uppdatera(string input, string input2)
        {
            using (var context = new BibliotekContext())
            {
                var bokT = context.bibliotek.FirstOrDefault(b => b.Titel.ToLower() == input.ToLower());//Söker bland titlar i databasen efter det som administratören vill ändra.
                var bokF = context.bibliotek.FirstOrDefault(b => b.Författare.ToLower() == input.ToLower());//Söker bland författare i databasen efter det som administratören vill ändra.
                var bokI = context.bibliotek.FirstOrDefault(b => b.Isbn == input);//Söder bland ISBN i databasen efter det som administratören vill ändra.
                if (bokT == null && bokF == null && bokI == null)//Om det administratören vill ändra inte hittas i databasen.
                {
                    Console.WriteLine("Det du vill ändra finns inte i biblioteket.");
                }
                else if (bokT != null && bokT.Titel.ToLower() == input.ToLower() && input2 != "")//Om det administratören vill ändra hittats bland titlar i databasen och input2 inte är tomt.
                {
                    bokT.Titel = input2;//Ändrar titeln på det som administratören vill ändra på till input2 som är det administratören vill ändra till.
                    context.bibliotek.AddOrUpdate(bokT);//Lägger till ändringen i databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du har nu ändrat titeln på boken " + input + " till " + input2 + ".");
                }
                else if (bokF != null && bokF.Författare.ToLower() == input.ToLower() && input2 != "")//Om det administratören vill ändra hittats bland författare i databasen och input2 inte är tomt.
                {
                    bokF.Författare = input2;//Ändrar författaren på det som administratören vill ändra på till input2 som är det som administratören vill ändra till.
                    context.bibliotek.AddOrUpdate(bokF);//Lägger till ändringen i databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du har nu ändrat författare för boken " + bokF.Titel + " från " + input + " till " + input2 + ".");
                }
                else if (bokI != null && bokI.Isbn == input && input2 != "")//Om det administratören vill ändra hittats bland ISBN i databasen och input2 inte är tomt.
                {
                    bokI.Isbn = input2;//Ändrar ISBN på det som administratören vill ändra på till input2 som är det som administratören vill ändra till.
                    context.bibliotek.AddOrUpdate(bokI);//Lägger till ändringen i databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du har nu ändrat ISBN för boken " + bokI.Titel + " från " + input + " till " + input2 + ".");
                }
                else if (input2 == "")//Felhantering om input2 är tomt.
                {
                    Console.WriteLine("Det du vill ändra till är ogiltigt, försök på nytt.");
                }
            }
        }
        //Metod för att skriva ut alla böcker i databasen med titel, författare och ISBN.
        static void PrintAll()
        {
            using (var context = new BibliotekContext())
            {
                foreach (var bok in context.bibliotek)
                {
                    Console.WriteLine(bok.Titel + ", " + bok.Författare + ", " + bok.Isbn);
                }
            }
        }
        //Metod för att ta bort en bok från databasen.
        static void RemoveBok(string remove)
        {
            using (var context = new BibliotekContext())
            {
                var bok = context.bibliotek.FirstOrDefault(b => b.Titel.ToLower() == remove.ToLower());//Söker efter den titel som administratören vill ta bort.
                if (bok == null)//Om det administratören vill ta bort inte hittas i databasen.
                {
                    Console.WriteLine("Det du vill ta bort hittas inte i biblioteket!");
                }
                else if (remove != null && bok.Titel.ToLower() == remove.ToLower())//Om det administratören vill ta bort hittats bland titlar i databasen.
                {
                    context.bibliotek.Remove(bok);//Tar bort hela boken från databasen.
                    context.SaveChanges();//Sparar ändringen i databasen.
                    Console.WriteLine("Du har nu tagit bort boken " + bok.Titel + ".");
                }
            }
        }
        //Metod för sortering av listan inlogg.
        static void SortInlogg(List<string[]> inlogg)
        {
            inlogg = inlogg.OrderBy(a => a[0]).ThenBy(a => a[1]).ToList();//Sorterar listan inlogg efter användarnamn.
        }
        //Metod för sortering av listan inloggAdmin.
        static void SortInloggAdmin(List<string[]> inloggAdmin)
        {
            inloggAdmin = inloggAdmin.OrderBy(a => a[0]).ThenBy(a => a[1]).ToList();//Sorterar listan inloggAdmin efter användarnamn.
        }
    }
}
