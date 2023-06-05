using DigitalPetitions.Domain;
using DigitalPetitions.Domain.AdminNS;
using DigitalPetitions.Domain.Petitions;
using Microsoft.EntityFrameworkCore;

namespace DigitalPetitions.Infrastructure.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Email = "admin@localhost",
                PasswordHash = "$2a$13$kZ2jaYEA/mQKZB02XcKSo.8cKKhZQwdJe/i99BppCr0GAkMD.4RdG" // admin
            });


            modelBuilder.Entity<Petition>().HasData(
                new Petition { 
                    Id = 1, 
                    Title = "Збереження видів, що перебувають під загрозою зникнення", 
                    Body = "Ця петиція закликає до впровадження суворих природоохоронних заходів для захисту зникаючих видів у Королівстві Дикої Природи та Лісів. Вона закликає уряд виділити ресурси на збереження середовища існування, ініціативи по боротьбі з браконьєрством і кампанії з підвищення обізнаності громадськості для збереження біорізноманіття Королівства", 
                    Status = PetitionStatus.Created, 
                },
                new Petition { 
                    Id = 2, 
                    Title = "Заборона вирубки лісів", 
                    Body = "Ця петиція вимагає негайної заборони вирубки лісів у Королівстві. У ній підкреслюється важливість лісів для підтримки екологічної рівноваги, боротьби зі зміною клімату та забезпечення середовища проживання незліченних видів. Петиція закликає Уряд сприяти стійким альтернативам і застосовувати суворі покарання за незаконну вирубку.", 
                    Status = PetitionStatus.Created, 
                },
                new Petition { 
                    Id = 3, 
                    Title = "Lorem Ipsum #1", 
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam quis vehicula eros. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae dui turpis. Donec eu consectetur massa, sed vestibulum nisl. Ut id turpis eu orci mollis eleifend. Morbi leo ex, suscipit id porttitor quis, pharetra eget enim. Vivamus eget ex eu arcu suscipit lobortis ut vel sem. Curabitur sollicitudin quam id eleifend mollis. Fusce vulputate a risus ut ornare. Etiam aliquam ut tortor eu vehicula. Duis aliquam placerat blandit. Praesent eu diam sed nibh imperdiet iaculis. Donec massa magna, condimentum eu iaculis eu, lobortis sed nisi. Mauris aliquet aliquet.", 
                    Status = PetitionStatus.Declined, 
                    CreatedAt = DateTime.UtcNow.AddDays(-1)

                },
                new Petition { 
                    Id = 4, 
                    Title = "Lorem Ipsum #2", 
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam vitae condimentum purus. Proin ullamcorper nisl libero, ac tincidunt metus mollis ac. In iaculis tempor rutrum. Aenean dapibus, magna sed feugiat eleifend, quam neque interdum turpis, iaculis pretium arcu velit quis tortor. Aliquam pulvinar finibus ipsum at hendrerit. Morbi interdum egestas porttitor. Nam viverra sem ut ornare elementum. Fusce id odio nibh. Quisque volutpat dictum purus. Nulla ornare ut ex vel sodales. Integer volutpat ipsum eget odio bibendum sollicitudin. Nulla facilisi. In hac habitasse platea dictumst. Donec felis dolor, pretium vel erat ac, rhoncus elementum felis. Cras malesuada magna vel vehicula.", 
                    Status = PetitionStatus.Declined, 
                    CreatedAt = DateTime.UtcNow
                },
                new Petition { 
                    Id = 5, 
                    Title = "Суворе регулювання полювання", 
                    Body = "Ця петиція закликає до впровадження суворих правил полювання в Королівстві. У петиції наголошується на необхідності запобігання надмірному полюванню, захисту ключових видів і підтримки балансу екосистем. Петиція закликає Уряд забезпечити дотримання ліцензій на полювання, обмежити ліміти багажу, а також посилити моніторинг і правозастосування.", 
                    Status = PetitionStatus.Signing, 
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                },
                new Petition { 
                    Id = 6, 
                    Title = "Розширення заповідних територій", 
                    Body = "Ця петиція підкреслює важливість розширення заповідних територій у Королівстві Дикої Природи та Лісів. Вона закликає Уряд створити нові національні парки, заповідники та заповідники, щоб захистити критичні середовища існування та забезпечити безпечні гавані для дикої природи. У петиції наголошується на екологічних та економічних перевагах природоохоронних територій.", 
                    Status = PetitionStatus.Signing, 
                    CreatedAt = DateTime.UtcNow.AddDays(-4),
                },
                new Petition { 
                    Id = 7, 
                    Title = "Сприяння сталому управлінню лісами", 
                    Body = "Ця петиція виступає за сприяння сталим методам управління лісами в Королівстві. Вона закликає до впровадження відповідальних методів рубки, ініціатив з лісовідновлення та захисту старих лісів. Петиція закликає Уряд підтримувати стійкі галузі промисловості, які покладаються на лісові ресурси.",
                    Status = PetitionStatus.Signing, 
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                },
                new Petition { 
                    Id = 8, 
                    Title = "Заборона торгівлі дикими тваринами", 
                    Body = "Ця петиція закликає Уряд ввести повну заборону на торгівлю дикими тваринами в Королівстві. Вона підкреслює згубний вплив незаконної торгівлі дикими тваринами на біорізноманіття, добробут тварин і здоров’я населення. Петиція закликає до суворих заходів примусу, проведення інформаційно-просвітницьких кампаній та підтримки альтернативних засобів існування.", 
                    Status = PetitionStatus.NotEnoughSigns, 
                    CreatedAt = DateTime.UtcNow.AddDays(-111)
                },
                new Petition { 
                    Id = 9, 
                    Title = "Інвестиції в центри реабілітації диких тварин", 
                    Body = "Ця петиція закликає збільшити інвестиції в центри реабілітації дикої природи в Королівстві. Петиція наголошує на необхідності створення засобів для порятунку, реабілітації та звільнення поранених або осиротілих диких тварин назад у їх природне середовище існування. У петиції закликають уряд виділити кошти на розширення та покращення цих центрів.", 
                    Status = PetitionStatus.NotEnoughSigns, 
                    CreatedAt = DateTime.UtcNow.AddDays(-100)
                },
                new Petition { 
                    Id = 10, 
                    Title = "Сприяння екотуризму", 
                    Body = "Ця петиція виступає за популяризацію екотуризму як сталого засобу отримання доходу в Королівстві Дикої Природи та Лісів. Петиція закликає до розвитку практики відповідального туризму, яка надає пріоритет збереженню навколишнього середовища, підтримує місцеві громади та інформує відвідувачів про важливість біорізноманіття та захисту лісів.", 
                    Status = PetitionStatus.WaitingForAnswer, 
                    CreatedAt = DateTime.UtcNow.AddDays(-25)
                },
                new Petition { 
                    Id = 11, 
                    Title = "Створення коридорів дикої природи", 
                    Body = "Ця петиція підкреслює необхідність створення коридорів дикої природи в Королівстві Дикої Природи та Лісів. Петиція закликає уряд створити захищені шляхи, які з’єднують фрагментовані середовища існування, забезпечуючи вільне пересування дикої природи та запобігаючи генетичній ізоляції. У петиції наголошується на ролі коридорів дикої природи у підтримці здорових екосистем.", 
                    Status = PetitionStatus.WaitingForAnswer, 
                    CreatedAt = DateTime.UtcNow.AddDays(-20)
                },
                new Petition { 
                    Id = 12, 
                    Title = "Підтримка корінних і місцевих громад", 
                    Body = "Ця петиція виступає за посилення підтримки та визнання прав корінних і місцевих громад у Королівстві Дикої Природи та Лісів. Вона закликає до їх залучення до процесів прийняття рішень щодо управління землею та ресурсами, забезпечуючи дотримання їхніх традиційних знань і стійких практик та інтеграцію в зусилля щодо збереження.", 
                    Status = PetitionStatus.Answered, 
                    CreatedAt = DateTime.UtcNow.AddDays(-33)
                },
                new Petition { 
                    Id = 13, 
                    Title = "Освіта для збереження довкілля", 
                    Body = "Ця петиція підкреслює важливість екологічної освіти в Королівстві Дикої Природи та Лісів. Вона закликає до інтеграції комплексних екологічних навчальних програм у школах, кампанії з підвищення обізнаності громадськості та громадські ініціативи, які сприяють розвитку почуття відповідальності та піклування про довкілля.",
                    Status = PetitionStatus.Answered, 
                    CreatedAt = DateTime.UtcNow.AddDays(-55)
                }
                );

            modelBuilder.Entity<ModerationResult>().HasData(
                new ModerationResult { Id = 1, Status = ModerationStatus.Declined, PetitionId = 3, ModeratorId = 1, Reason = "Порушення правил чи норм" },
                new ModerationResult { Id = 2, Status = ModerationStatus.Declined, PetitionId = 4, ModeratorId = 1, Reason = "Порушення правил чи норм" },
                new ModerationResult { Id = 3, Status = ModerationStatus.Accepted, PetitionId = 5, ModeratorId = 1 },
                new ModerationResult { Id = 4, Status = ModerationStatus.Accepted, PetitionId = 6, ModeratorId = 1 },
                new ModerationResult { Id = 5, Status = ModerationStatus.Accepted, PetitionId = 7, ModeratorId = 1 },
                new ModerationResult { Id = 6, Status = ModerationStatus.Accepted, PetitionId = 8, ModeratorId = 1 },
                new ModerationResult { Id = 7, Status = ModerationStatus.Accepted, PetitionId = 9, ModeratorId = 1 },
                new ModerationResult { Id = 8, Status = ModerationStatus.Accepted, PetitionId = 10, ModeratorId = 1 },
                new ModerationResult { Id = 9, Status = ModerationStatus.Accepted, PetitionId = 11, ModeratorId = 1 },
                new ModerationResult { Id = 10, Status = ModerationStatus.Accepted, PetitionId = 12, ModeratorId = 1 },
                new ModerationResult { Id = 11, Status = ModerationStatus.Accepted, PetitionId = 13, ModeratorId = 1 }
                );

            modelBuilder.Entity<Creator>().HasData(
                new Creator { Id = 1, PetitionId = 1, Email = "zoe@test.localhost", FirstName = "Zoe", LastName = "Watson" },
                new Creator { Id = 2, PetitionId = 2, Email = "dave@test.localhost", FirstName = "Dave", LastName = "Wilson" },
                new Creator { Id = 3, PetitionId = 3, Email = "sophie@test.localhost", FirstName = "Sophie", LastName = "Cameron" },
                new Creator { Id = 4, PetitionId = 4, Email = "oliver@test.localhost", FirstName = "Oliver", LastName = "Davies" },
                new Creator { Id = 5, PetitionId = 5, Email = "lily@test.localhost", FirstName = "Lily", LastName = "Dickens" },
                new Creator { Id = 6, PetitionId = 6, Email = "zoe@test.localhost", FirstName = "Zoe", LastName = "Watson" },
                new Creator { Id = 7, PetitionId = 7, Email = "amelia@test.localhost", FirstName = "Amelia", LastName = "Quinn" },
                new Creator { Id = 8, PetitionId = 8, Email = "frank@test.localhost", FirstName = "Frank", LastName = "McLean" },
                new Creator { Id = 9, PetitionId = 9, Email = "oliver@test.localhost", FirstName = "Oliver", LastName = "Davies" },
                new Creator { Id = 10, PetitionId = 10, Email = "victor@test.localhost", FirstName = "Victor", LastName = "Hudson" },
                new Creator { Id = 11, PetitionId = 11, Email = "sophie@test.localhost", FirstName = "Sophie", LastName = "Cameron" },
                new Creator { Id = 12, PetitionId = 12, Email = "leonard@test.localhost", FirstName = "Leonard", LastName = "Parsons" },
                new Creator { Id = 13, PetitionId = 13, Email = "lily@test.localhost", FirstName = "Lily", LastName = "Dickens" }
                );

            modelBuilder.Entity<Signee>().HasData(
                new Signee { Id = 1, Email = "zoe@test.localhost", FirstName = "Zoe", LastName = "Watson" },
                new Signee { Id = 2, Email = "dave@test.localhost", FirstName = "Dave", LastName = "Wilson" },
                new Signee { Id = 3, Email = "sophie@test.localhost", FirstName = "Sophie", LastName = "Cameron" },
                new Signee { Id = 4, Email = "oliver@test.localhost", FirstName = "Oliver", LastName = "Davies" }
                );

            modelBuilder.Entity<Sign>().HasData(
                new Sign { Id = 1, PetitionId = 1, SigneeId = 1 },
                new Sign { Id = 2, PetitionId = 2, SigneeId = 1 },
                new Sign { Id = 3, PetitionId = 3, SigneeId = 1 },
                new Sign { Id = 4, PetitionId = 4, SigneeId = 1 },
                new Sign { Id = 5, PetitionId = 5, SigneeId = 1 },
                new Sign { Id = 6, PetitionId = 5, SigneeId = 2 },
                new Sign { Id = 8, PetitionId = 6, SigneeId = 2 },
                new Sign { Id = 9, PetitionId = 6, SigneeId = 3 },
                new Sign { Id = 11, PetitionId = 7, SigneeId = 1 },
                new Sign { Id = 12, PetitionId = 8, SigneeId = 1 },
                new Sign { Id = 13, PetitionId = 8, SigneeId = 2 },
                new Sign { Id = 14, PetitionId = 9, SigneeId = 1 },
                new Sign { Id = 15, PetitionId = 10, SigneeId = 1 },
                new Sign { Id = 16, PetitionId = 10, SigneeId = 2 },
                new Sign { Id = 17, PetitionId = 10, SigneeId = 3 },
                new Sign { Id = 18, PetitionId = 10, SigneeId = 4 },
                new Sign { Id = 19, PetitionId = 11, SigneeId = 1 },
                new Sign { Id = 20, PetitionId = 11, SigneeId = 2 },
                new Sign { Id = 21, PetitionId = 11, SigneeId = 3 },
                new Sign { Id = 22, PetitionId = 12, SigneeId = 1 },
                new Sign { Id = 23, PetitionId = 12, SigneeId = 2 },
                new Sign { Id = 24, PetitionId = 12, SigneeId = 3 },
                new Sign { Id = 25, PetitionId = 13, SigneeId = 1 },
                new Sign { Id = 26, PetitionId = 13, SigneeId = 2 },
                new Sign { Id = 27, PetitionId = 13, SigneeId = 3 },
                new Sign { Id = 28, PetitionId = 13, SigneeId = 4 }
                );

            modelBuilder.Entity<Answer>().HasData(
                    new Answer { Id = 1, Body = "Відповідь Уряду на петицію №12: «Підтримка корінних і місцевих громад»:\r\n\r\nУряд визнає важливість підтримки корінних і місцевих громад у Королівстві Дикої Природи та Лісів і визнає їхній цінний внесок у зусилля щодо збереження. У відповідь на цю петицію Уряд окреслює комплексний план вирішення виниклих проблем і посилення підтримки цих громад:\r\n\r\n1. Інклюзивне прийняття рішень: Уряд зобов’язується залучати корінні та місцеві громади до процесів прийняття рішень, пов’язаних з управлінням землею та ресурсами. Їхні традиційні знання, стійкі практики та перспективи будуть активно шукатися та інтегруватися в стратегії збереження.\r\n\r\n2. Права на землю та гарантії володіння: Уряд працюватиме над забезпеченням прав на землю та гарантій володіння для корінних і місцевих громад. За допомогою законодавчої бази та політики земельні спори вирішуватимуться, а також будуть встановлені чіткі вказівки для захисту їхніх прав і забезпечення практики сталого землекористування.\r\n\r\n3. Доступ до базових послуг: Уряд надасть пріоритет покращенню доступу до основних послуг для корінних і місцевих громад. Буде докладено зусиль, щоб забезпечити медичні заклади, освіту, чисту воду та інфраструктуру каналізації, забезпечуючи їхнє благополуччя та якість життя.\r\n\r\n4. Збереження та визнання культури: Уряд цінує культурну спадщину корінних і місцевих громад і зобов’язується її зберігати. Будуть докладені зусилля для збереження традиційних практик, мов і звичаїв, сприяння культурному різноманіттю та визнання їхнього внеску в ідентичність Королівства.\r\n\r\n5. Обізнаність і освіта: Уряд надаватиме пріоритет громадським кампаніям і освітнім програмам, які сприятимуть розумінню, повазі та вдячності до корінних і місцевих громад. Ці ініціативи будуть спрямовані на виховання емпатії та подолання культурних розривів у суспільстві.\r\n\r\nЗа допомогою цих заходів Уряд має намір вирішити проблеми, викладені в петиції №12. Підтримуючи та надаючи повноваження корінним і місцевим громадам, Уряд прагне сприяти сталому розвитку, зберігати біорізноманіття та забезпечувати справедливість у Королівстві Дикої Природи та Лісів.", PetitionId = 12, PublishedById = 1 },
                    new Answer { Id = 2, Body = "У відповідь на петицію №11: «Освіта для збереження довкілля» Уряд визнає важливість екологічної освіти та зобов’язується вжити таких заходів:\r\n\r\n1. Інтеграція навчальної програми: Уряд надасть пріоритет інтеграції комплексної екологічної освіти в шкільну програму на всіх рівнях. Тісно співпрацюючи з освітніми експертами та зацікавленими сторонами, Уряд перегляне існуючі навчальні програми, щоб забезпечити включення тем збереження довкілля до відповідних предметів. Така інтеграція допоможе учням розвинути глибоке розуміння екологічних проблем і виховувати почуття відповідальності перед навколишнім середовищем.\r\n\r\n2. Підготовка вчителів та професійний розвиток: Уряд інвестуватиме кошти в програми навчання та професійного розвитку вчителів, щоб покращити їхні знання та навички з екологічної освіти. Будуть організовані спеціалізовані семінари та курси, щоб у вчителів були необхідні інструменти та методології навчання для ефективного впровадження концепцій збереження довкілля в класі.\r\n\r\n3. Моніторинг та оцінка: Уряд зобов'язується здійснювати моніторинг та оцінку ефективності екологічних освітніх ініціатив. За допомогою регулярних оцінок і механізмів зворотного зв’язку Уряд оцінюватиме вплив цих програм, визначатиме напрямки для покращення та вноситиме необхідні корективи для забезпечення постійного прогресу в екологічній освіті.\r\n\r\nВпроваджуючи ці заходи, Уряд прагне виховати покоління екологічно свідомих громадян, які розуміють важливість збереження довкілля та мають знання та навички, щоб зробити внесок у стале майбутнє.", PetitionId = 13, PublishedById = 1 }
                );
        }
    }
}
