using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalPetitions.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Petitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StatusChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Petitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Signees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Creators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PetitionId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creators_Petitions_PetitionId",
                        column: x => x.PetitionId,
                        principalTable: "Petitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SigneeId = table.Column<int>(type: "integer", nullable: false),
                    PetitionId = table.Column<int>(type: "integer", nullable: false),
                    SignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signs_Petitions_PetitionId",
                        column: x => x.PetitionId,
                        principalTable: "Petitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Signs_Signees_SigneeId",
                        column: x => x.SigneeId,
                        principalTable: "Signees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "text", nullable: false),
                    PetitionId = table.Column<int>(type: "integer", nullable: false),
                    PublishedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Petitions_PetitionId",
                        column: x => x.PetitionId,
                        principalTable: "Petitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Users_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModerationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PetitionId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ModeratorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModerationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModerationResults_Petitions_PetitionId",
                        column: x => x.PetitionId,
                        principalTable: "Petitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModerationResults_Users_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Petitions",
                columns: new[] { "Id", "Body", "CreatedAt", "Status", "StatusChangedAt", "StatusDeadline", "Title" },
                values: new object[,]
                {
                    { 1, "Ця петиція закликає до впровадження суворих природоохоронних заходів для захисту зникаючих видів у Королівстві Дикої Природи та Лісів. Вона закликає уряд виділити ресурси на збереження середовища існування, ініціативи по боротьбі з браконьєрством і кампанії з підвищення обізнаності громадськості для збереження біорізноманіття Королівства", new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3696), 0, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3700), null, "Збереження видів, що перебувають під загрозою зникнення" },
                    { 2, "Ця петиція вимагає негайної заборони вирубки лісів у Королівстві. У ній підкреслюється важливість лісів для підтримки екологічної рівноваги, боротьби зі зміною клімату та забезпечення середовища проживання незліченних видів. Петиція закликає Уряд сприяти стійким альтернативам і застосовувати суворі покарання за незаконну вирубку.", new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3750), 0, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3751), null, "Заборона вирубки лісів" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam quis vehicula eros. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae dui turpis. Donec eu consectetur massa, sed vestibulum nisl. Ut id turpis eu orci mollis eleifend. Morbi leo ex, suscipit id porttitor quis, pharetra eget enim. Vivamus eget ex eu arcu suscipit lobortis ut vel sem. Curabitur sollicitudin quam id eleifend mollis. Fusce vulputate a risus ut ornare. Etiam aliquam ut tortor eu vehicula. Duis aliquam placerat blandit. Praesent eu diam sed nibh imperdiet iaculis. Donec massa magna, condimentum eu iaculis eu, lobortis sed nisi. Mauris aliquet aliquet.", new DateTime(2023, 6, 1, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3755), 1, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3754), null, "Lorem Ipsum #1" },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam vitae condimentum purus. Proin ullamcorper nisl libero, ac tincidunt metus mollis ac. In iaculis tempor rutrum. Aenean dapibus, magna sed feugiat eleifend, quam neque interdum turpis, iaculis pretium arcu velit quis tortor. Aliquam pulvinar finibus ipsum at hendrerit. Morbi interdum egestas porttitor. Nam viverra sem ut ornare elementum. Fusce id odio nibh. Quisque volutpat dictum purus. Nulla ornare ut ex vel sodales. Integer volutpat ipsum eget odio bibendum sollicitudin. Nulla facilisi. In hac habitasse platea dictumst. Donec felis dolor, pretium vel erat ac, rhoncus elementum felis. Cras malesuada magna vel vehicula.", new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3765), 1, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3764), null, "Lorem Ipsum #2" },
                    { 5, "Ця петиція закликає до впровадження суворих правил полювання в Королівстві. У петиції наголошується на необхідності запобігання надмірному полюванню, захисту ключових видів і підтримки балансу екосистем. Петиція закликає Уряд забезпечити дотримання ліцензій на полювання, обмежити ліміти багажу, а також посилити моніторинг і правозастосування.", new DateTime(2023, 5, 31, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3847), 2, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3767), new DateTime(2023, 8, 31, 20, 59, 59, 999, DateTimeKind.Utc), "Суворе регулювання полювання" },
                    { 6, "Ця петиція підкреслює важливість розширення заповідних територій у Королівстві Дикої Природи та Лісів. Вона закликає Уряд створити нові національні парки, заповідники та заповідники, щоб захистити критичні середовища існування та забезпечити безпечні гавані для дикої природи. У петиції наголошується на екологічних та економічних перевагах природоохоронних територій.", new DateTime(2023, 5, 29, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3853), 2, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3850), new DateTime(2023, 8, 31, 20, 59, 59, 999, DateTimeKind.Utc), "Розширення заповідних територій" },
                    { 7, "Ця петиція виступає за сприяння сталим методам управління лісами в Королівстві. Вона закликає до впровадження відповідальних методів рубки, ініціатив з лісовідновлення та захисту старих лісів. Петиція закликає Уряд підтримувати стійкі галузі промисловості, які покладаються на лісові ресурси.", new DateTime(2023, 5, 28, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3857), 2, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3855), new DateTime(2023, 8, 31, 20, 59, 59, 999, DateTimeKind.Utc), "Сприяння сталому управлінню лісами" },
                    { 8, "Ця петиція закликає Уряд ввести повну заборону на торгівлю дикими тваринами в Королівстві. Вона підкреслює згубний вплив незаконної торгівлі дикими тваринами на біорізноманіття, добробут тварин і здоров’я населення. Петиція закликає до суворих заходів примусу, проведення інформаційно-просвітницьких кампаній та підтримки альтернативних засобів існування.", new DateTime(2023, 2, 11, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3860), 3, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3860), null, "Заборона торгівлі дикими тваринами" },
                    { 9, "Ця петиція закликає збільшити інвестиції в центри реабілітації дикої природи в Королівстві. Петиція наголошує на необхідності створення засобів для порятунку, реабілітації та звільнення поранених або осиротілих диких тварин назад у їх природне середовище існування. У петиції закликають уряд виділити кошти на розширення та покращення цих центрів.", new DateTime(2023, 2, 22, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3863), 3, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3862), null, "Інвестиції в центри реабілітації диких тварин" },
                    { 10, "Ця петиція виступає за популяризацію екотуризму як сталого засобу отримання доходу в Королівстві Дикої Природи та Лісів. Петиція закликає до розвитку практики відповідального туризму, яка надає пріоритет збереженню навколишнього середовища, підтримує місцеві громади та інформує відвідувачів про важливість біорізноманіття та захисту лісів.", new DateTime(2023, 5, 8, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3865), 4, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3865), null, "Сприяння екотуризму" },
                    { 11, "Ця петиція підкреслює необхідність створення коридорів дикої природи в Королівстві Дикої Природи та Лісів. Петиція закликає уряд створити захищені шляхи, які з’єднують фрагментовані середовища існування, забезпечуючи вільне пересування дикої природи та запобігаючи генетичній ізоляції. У петиції наголошується на ролі коридорів дикої природи у підтримці здорових екосистем.", new DateTime(2023, 5, 13, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3869), 4, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3868), null, "Створення коридорів дикої природи" },
                    { 12, "Ця петиція виступає за посилення підтримки та визнання прав корінних і місцевих громад у Королівстві Дикої Природи та Лісів. Вона закликає до їх залучення до процесів прийняття рішень щодо управління землею та ресурсами, забезпечуючи дотримання їхніх традиційних знань і стійких практик та інтеграцію в зусилля щодо збереження.", new DateTime(2023, 4, 30, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3872), 5, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3871), null, "Підтримка корінних і місцевих громад" },
                    { 13, "Ця петиція підкреслює важливість екологічної освіти в Королівстві Дикої Природи та Лісів. Вона закликає до інтеграції комплексних екологічних навчальних програм у школах, кампанії з підвищення обізнаності громадськості та громадські ініціативи, які сприяють розвитку почуття відповідальності та піклування про довкілля.", new DateTime(2023, 4, 8, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3874), 5, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3874), null, "Освіта для збереження довкілля" }
                });

            migrationBuilder.InsertData(
                table: "Signees",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "zoe@test.localhost", "Zoe", "Watson" },
                    { 2, "dave@test.localhost", "Dave", "Wilson" },
                    { 3, "sophie@test.localhost", "Sophie", "Cameron" },
                    { 4, "oliver@test.localhost", "Oliver", "Davies" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "PasswordHash", "Role" },
                values: new object[] { 1, "Admin", "admin@localhost", "$2a$13$kZ2jaYEA/mQKZB02XcKSo.8cKKhZQwdJe/i99BppCr0GAkMD.4RdG", 1 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Body", "CreatedAt", "PetitionId", "PublishedById" },
                values: new object[,]
                {
                    { 1, "Відповідь Уряду на петицію №12: «Підтримка корінних і місцевих громад»:\r\n\r\nУряд визнає важливість підтримки корінних і місцевих громад у Королівстві Дикої Природи та Лісів і визнає їхній цінний внесок у зусилля щодо збереження. У відповідь на цю петицію Уряд окреслює комплексний план вирішення виниклих проблем і посилення підтримки цих громад:\r\n\r\n1. Інклюзивне прийняття рішень: Уряд зобов’язується залучати корінні та місцеві громади до процесів прийняття рішень, пов’язаних з управлінням землею та ресурсами. Їхні традиційні знання, стійкі практики та перспективи будуть активно шукатися та інтегруватися в стратегії збереження.\r\n\r\n2. Права на землю та гарантії володіння: Уряд працюватиме над забезпеченням прав на землю та гарантій володіння для корінних і місцевих громад. За допомогою законодавчої бази та політики земельні спори вирішуватимуться, а також будуть встановлені чіткі вказівки для захисту їхніх прав і забезпечення практики сталого землекористування.\r\n\r\n3. Доступ до базових послуг: Уряд надасть пріоритет покращенню доступу до основних послуг для корінних і місцевих громад. Буде докладено зусиль, щоб забезпечити медичні заклади, освіту, чисту воду та інфраструктуру каналізації, забезпечуючи їхнє благополуччя та якість життя.\r\n\r\n4. Збереження та визнання культури: Уряд цінує культурну спадщину корінних і місцевих громад і зобов’язується її зберігати. Будуть докладені зусилля для збереження традиційних практик, мов і звичаїв, сприяння культурному різноманіттю та визнання їхнього внеску в ідентичність Королівства.\r\n\r\n5. Обізнаність і освіта: Уряд надаватиме пріоритет громадським кампаніям і освітнім програмам, які сприятимуть розумінню, повазі та вдячності до корінних і місцевих громад. Ці ініціативи будуть спрямовані на виховання емпатії та подолання культурних розривів у суспільстві.\r\n\r\nЗа допомогою цих заходів Уряд має намір вирішити проблеми, викладені в петиції №12. Підтримуючи та надаючи повноваження корінним і місцевим громадам, Уряд прагне сприяти сталому розвитку, зберігати біорізноманіття та забезпечувати справедливість у Королівстві Дикої Природи та Лісів.", new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4053), 12, 1 },
                    { 2, "У відповідь на петицію №11: «Освіта для збереження довкілля» Уряд визнає важливість екологічної освіти та зобов’язується вжити таких заходів:\r\n\r\n1. Інтеграція навчальної програми: Уряд надасть пріоритет інтеграції комплексної екологічної освіти в шкільну програму на всіх рівнях. Тісно співпрацюючи з освітніми експертами та зацікавленими сторонами, Уряд перегляне існуючі навчальні програми, щоб забезпечити включення тем збереження довкілля до відповідних предметів. Така інтеграція допоможе учням розвинути глибоке розуміння екологічних проблем і виховувати почуття відповідальності перед навколишнім середовищем.\r\n\r\n2. Підготовка вчителів та професійний розвиток: Уряд інвестуватиме кошти в програми навчання та професійного розвитку вчителів, щоб покращити їхні знання та навички з екологічної освіти. Будуть організовані спеціалізовані семінари та курси, щоб у вчителів були необхідні інструменти та методології навчання для ефективного впровадження концепцій збереження довкілля в класі.\r\n\r\n3. Моніторинг та оцінка: Уряд зобов'язується здійснювати моніторинг та оцінку ефективності екологічних освітніх ініціатив. За допомогою регулярних оцінок і механізмів зворотного зв’язку Уряд оцінюватиме вплив цих програм, визначатиме напрямки для покращення та вноситиме необхідні корективи для забезпечення постійного прогресу в екологічній освіті.\r\n\r\nВпроваджуючи ці заходи, Уряд прагне виховати покоління екологічно свідомих громадян, які розуміють важливість збереження довкілля та мають знання та навички, щоб зробити внесок у стале майбутнє.", new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4056), 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "Creators",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PetitionId" },
                values: new object[,]
                {
                    { 1, "zoe@test.localhost", "Zoe", "Watson", 1 },
                    { 2, "dave@test.localhost", "Dave", "Wilson", 2 },
                    { 3, "sophie@test.localhost", "Sophie", "Cameron", 3 },
                    { 4, "oliver@test.localhost", "Oliver", "Davies", 4 },
                    { 5, "lily@test.localhost", "Lily", "Dickens", 5 },
                    { 6, "zoe@test.localhost", "Zoe", "Watson", 6 },
                    { 7, "amelia@test.localhost", "Amelia", "Quinn", 7 },
                    { 8, "frank@test.localhost", "Frank", "McLean", 8 },
                    { 9, "oliver@test.localhost", "Oliver", "Davies", 9 },
                    { 10, "victor@test.localhost", "Victor", "Hudson", 10 },
                    { 11, "sophie@test.localhost", "Sophie", "Cameron", 11 },
                    { 12, "leonard@test.localhost", "Leonard", "Parsons", 12 },
                    { 13, "lily@test.localhost", "Lily", "Dickens", 13 }
                });

            migrationBuilder.InsertData(
                table: "ModerationResults",
                columns: new[] { "Id", "CreatedAt", "ModeratorId", "PetitionId", "Reason", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3907), 1, 3, "Порушення правил чи норм", 0 },
                    { 2, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3914), 1, 4, "Порушення правил чи норм", 0 },
                    { 3, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3915), 1, 5, null, 1 },
                    { 4, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3917), 1, 6, null, 1 },
                    { 5, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3918), 1, 7, null, 1 },
                    { 6, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3919), 1, 8, null, 1 },
                    { 7, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3921), 1, 9, null, 1 },
                    { 8, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3923), 1, 10, null, 1 },
                    { 9, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3924), 1, 11, null, 1 },
                    { 10, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3926), 1, 12, null, 1 },
                    { 11, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(3927), 1, 13, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Signs",
                columns: new[] { "Id", "PetitionId", "SignedAt", "SigneeId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4010), 1 },
                    { 2, 2, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4012), 1 },
                    { 3, 3, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4013), 1 },
                    { 4, 4, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4014), 1 },
                    { 5, 5, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4015), 1 },
                    { 6, 5, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4016), 2 },
                    { 8, 6, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4017), 2 },
                    { 9, 6, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4018), 3 },
                    { 11, 7, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4019), 1 },
                    { 12, 8, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4019), 1 },
                    { 13, 8, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4020), 2 },
                    { 14, 9, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4021), 1 },
                    { 15, 10, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4022), 1 },
                    { 16, 10, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4023), 2 },
                    { 17, 10, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4024), 3 },
                    { 18, 10, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4025), 4 },
                    { 19, 11, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4025), 1 },
                    { 20, 11, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4027), 2 },
                    { 21, 11, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4027), 3 },
                    { 22, 12, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4028), 1 },
                    { 23, 12, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4029), 2 },
                    { 24, 12, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4030), 3 },
                    { 25, 13, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4030), 1 },
                    { 26, 13, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4031), 2 },
                    { 27, 13, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4032), 3 },
                    { 28, 13, new DateTime(2023, 6, 2, 12, 5, 41, 612, DateTimeKind.Utc).AddTicks(4033), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PetitionId",
                table: "Answers",
                column: "PetitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PublishedById",
                table: "Answers",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Creators_PetitionId",
                table: "Creators",
                column: "PetitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModerationResults_ModeratorId",
                table: "ModerationResults",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationResults_PetitionId",
                table: "ModerationResults",
                column: "PetitionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Signs_PetitionId",
                table: "Signs",
                column: "PetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Signs_SigneeId",
                table: "Signs",
                column: "SigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Creators");

            migrationBuilder.DropTable(
                name: "ModerationResults");

            migrationBuilder.DropTable(
                name: "Signs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Petitions");

            migrationBuilder.DropTable(
                name: "Signees");
        }
    }
}
