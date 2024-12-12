using StaffingSolution.Models;
using System.Collections.Generic;

namespace StaffingSolution.Controllers
{
    public class JobController
    {
        public List<Job> GetJobs()
        {
            return new List<Job>
            {
               new Job
            {
                Title = "Lagerarbetare",
                Location = "Vällingby",
                Description = "Arbeta på lager i Vällingby.",
                ImageUrl = "/imagesJob/Lagerarbetare.png",
                Responsibilities = "• Packa och organisera varor\n• Hantera inkommande och utgående leveranser\n• Säkerställa att lagret är rent och organiserat",
                Requirements = "• Minst 1 års erfarenhet av lagerarbete\n• Fysisk styrka och förmåga att lyfta tunga föremål\n• God kommunikationsförmåga och lagarbete\n• B-körkort"
            },
            new Job
            {
                Title = "Ekonom",
                Location = "Stockholm",
                Description = "Ansvar för ekonomi och bokföring.",
                ImageUrl = "/imagesJob/Ekonom.png",
                Responsibilities = "• Skapa och hantera ekonomiska rapporter\n• Utföra bokföring och budgetering\n• Analysera finansiella data för beslutsfattande",
                Requirements = "• Kandidatexamen i ekonomi eller liknande\n• Minst 2 års erfarenhet inom ekonomisk förvaltning\n• Kunskap i ekonomiska program som Excel och Visma"
            },
            new Job
            {
                Title = "Programmerare",
                Location = "Göteborg",
                Description = "Utveckla programvara för olika system.",
                ImageUrl = "/imagesJob/Programmerare.png",
                Responsibilities = "• Utveckla och underhålla applikationer\n• Skriva ren och effektiv kod\n• Arbeta med teamet för att identifiera och lösa buggar",
                Requirements = "• Erfarenhet av programmeringsspråk som C#, Python eller Java\n• Kunskap om versionskontroll som Git\n• Problemlösningsförmåga och god kommunikation"
            },
            new Job
            {
                Title = "HR-specialist",
                Location = "Malmö",
                Description = "Rekrytera och hantera personalfrågor.",
                ImageUrl = "/imagesJob/HR-specialist.png",
                Responsibilities = "• Ansvara för rekryteringsprocesser\n• Genomföra intervjuer och utvärderingar\n• Utveckla strategier för personalhantering",
                Requirements = "• Minst 2 års erfarenhet av HR-arbete\n• Stark förmåga att kommunicera och organisera\n• Kandidatexamen inom HR eller motsvarande"
            },
            new Job
            {
                Title = "Marknadsförare",
                Location = "Stockholm",
                Description = "Skapa och implementera marknadsföringsstrategier.",
                ImageUrl = "/imagesJob/Marknadsförare.png",
                Responsibilities = "• Skapa marknadsföringskampanjer\n• Analysera kundbeteende och marknadstrender\n• Samarbeta med andra team för att utveckla marknadsföringsmaterial",
                Requirements = "• Erfarenhet av digital marknadsföring\n• Kunskap om SEO och sociala medier\n• Kreativ och analytisk förmåga"
            },
            new Job
            {
                Title = "Projektledare",
                Location = "Uppsala",
                Description = "Leda och koordinera projektteam.",
                ImageUrl = "/imagesJob/Projektledare.png",
                Responsibilities = "• Planera och leda projekt från start till mål\n• Säkerställa att projekten hålls inom budget och tidsramar\n• Motivera och stötta teammedlemmar",
                Requirements = "• Erfarenhet av projektledning\n• Stark ledarskapsförmåga\n• Certifiering i projektledning är en fördel\n• B-körkort"
            },
            new Job
            {
                Title = "Butikssäljare",
                Location = "Nacka",
                Description = "Sälja produkter och hjälpa kunder i butiken.",
                ImageUrl = "/imagesJob/Butikssäljare.png",
                Responsibilities = "• Assistera kunder och ge produktinformation\n• Hantera kassatransaktioner\n• Upprätthålla en organiserad och attraktiv butik",
                Requirements = "• Erfarenhet av kundservice eller försäljning\n• Positiv attityd och kommunikationsförmåga\n• Flexibilitet att arbeta på varierande tider"
            },
            new Job
            {
                Title = "Webbutvecklare",
                Location = "Stockholm",
                Description = "Bygga och underhålla webbapplikationer.",
                ImageUrl = "/imagesJob/Webbutvecklare.png",
                Responsibilities = "• Designa och utveckla responsiva webbsidor\n• Samarbeta med team för att implementera funktioner\n• Testa och optimera webbplatser för prestanda",
                Requirements = "• Erfarenhet av HTML, CSS, JavaScript och frameworks som React\n• Kunskap om backend-teknologier är ett plus\n• Stark problemlösningsförmåga"
            },
            new Job
            {
                Title = "Account Manager",
                Location = "Göteborg",
                Description = "Bygga relationer och hantera kundkonton.",
                ImageUrl = "/imagesJob/Account_Manager.png",
                Responsibilities = "• Utveckla och upprätthålla kundrelationer\n• Identifiera och möta kunders behov\n• Förhandla och hantera kontrakt",
                Requirements = "• Erfarenhet av försäljning eller kundhantering\n• Stark förmåga att bygga relationer\n• Resultatorienterad och självgående\n• B-körkort"
            },
            new Job
            {
                Title = "Systemadministratör",
                Location = "Västerås",
                Description = "Underhålla och optimera företagets IT-system.",
                ImageUrl = "/imagesJob/Systemadministratör.png",
                Responsibilities = "• Installera och konfigurera programvara och hårdvara\n• Övervaka system och nätverk för att säkerställa drift\n• Hantera säkerhetskopior och felsökning",
                Requirements = "• Kunskap om operativsystem och nätverk\n• Erfarenhet av IT-support\n• Förmåga att prioritera och lösa problem snabbt"
            }
            };
        }
    }
}