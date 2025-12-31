using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { new Guid("07481067-c679-4a48-9823-4b04f7e353e0"), "Pakistan" },
                    { new Guid("57643ccc-1371-4445-b92c-c74a6e428ed1"), "Maldives" },
                    { new Guid("7da058f2-dfbe-446b-bd9f-c68338399aed"), "Srilanka" },
                    { new Guid("a055fa40-94c0-43ae-83b0-86138b267297"), "India" },
                    { new Guid("a91f3c12-2b8d-4d5e-9f3c-1e2b7c8d9a45"), "China" },
                    { new Guid("b7078c84-62dd-4551-ba74-dad88e597492"), "Nepal" },
                    { new Guid("c2b1d34f-7a5c-4e6b-b2d3-9f1a7b2c3d4e"), "Japan" },
                    { new Guid("c85c09ff-9da2-44f8-aa86-59502fc3737f"), "Bhutan" },
                    { new Guid("d12a7f42-8b3e-4f76-9c3b-2f17e0c7a1b5"), "Afghanistan" },
                    { new Guid("e7f2b910-0c4d-4b5a-84d5-3c1f9d2b7e88"), "Germany" },
                    { new Guid("f19a3d2b-6e7c-4f5d-bfa1-8d9c7e1b4a90"), "USA" },
                    { new Guid("fefc9d80-fb10-4114-ac9c-3f0f9e999974"), "Bangladesh" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "Gender", "PersonName" },
                values: new object[,]
                {
                    { new Guid("072413c7-2185-4e42-8259-823c03697463"), "Delhi", new Guid("a055fa40-94c0-43ae-83b0-86138b267297"), new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anita.sharma@email.com", "Female", "Anita Sharma" },
                    { new Guid("1f2a3b4c-5d6e-7f81-9012-3456789abcde"), "Mumbai", new Guid("a055fa40-94c0-43ae-83b0-86138b267297"), new DateTime(1989, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "rajesh.kumar@email.com", "Male", "Rajesh Kumar" },
                    { new Guid("2b3c4d5e-6f70-8192-0345-6789abcde12f"), "Pokhara", new Guid("b7078c84-62dd-4551-ba74-dad88e597492"), new DateTime(1994, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "sita.lama@email.com", "Female", "Sita Lama" },
                    { new Guid("3c4d5e6f-7081-9234-5678-9abcde123456"), "Karachi", new Guid("07481067-c679-4a48-9823-4b04f7e353e0"), new DateTime(1987, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "muhammad.ali@email.com", "Male", "Muhammad Ali" },
                    { new Guid("4d5e6f70-8192-3456-789a-bcde12345678"), "Dhaka", new Guid("fefc9d80-fb10-4114-ac9c-3f0f9e999974"), new DateTime(1996, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "fatima.noor@email.com", "Female", "Fatima Noor" },
                    { new Guid("5e6f7081-9234-5678-9abc-de1234567890"), "Galle", new Guid("7da058f2-dfbe-446b-bd9f-c68338399aed"), new DateTime(1988, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "kamal.perera@email.com", "Male", "Kamal Perera" },
                    { new Guid("6f708192-3456-789a-bcde-123456789abc"), "Delhi", new Guid("a055fa40-94c0-43ae-83b0-86138b267297"), new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "leela.sharma@email.com", "Female", "Leela Sharma" },
                    { new Guid("70819234-5678-9abc-de12-3456789abcde"), "Paro", new Guid("c85c09ff-9da2-44f8-aa86-59502fc3737f"), new DateTime(1986, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "tashi.wangchuk@email.com", "Male", "Tashi Wangchuk" },
                    { new Guid("8107934e-75d4-4f75-b630-6b88f6777447"), "Thimphu", new Guid("c85c09ff-9da2-44f8-aa86-59502fc3737f"), new DateTime(1988, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "tenzin.dorji@email.com", "Male", "Tenzin Dorji" },
                    { new Guid("81923456-789a-bcde-1234-56789abcdef0"), "Islamabad", new Guid("07481067-c679-4a48-9823-4b04f7e353e0"), new DateTime(1993, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "nadia.khan@email.com", "Female", "Nadia Khan" },
                    { new Guid("8e9482b0-4440-45cc-a4bf-641327c40440"), "Colombo", new Guid("7da058f2-dfbe-446b-bd9f-c68338399aed"), new DateTime(1991, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "nimal.perera@email.com", "Male", "Nimal Perera" },
                    { new Guid("92345678-9abc-de12-3456-789abcdef012"), "Lalitpur", new Guid("b7078c84-62dd-4551-ba74-dad88e597492"), new DateTime(1990, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "ramesh.thapa@email.com", "Male", "Ramesh Thapa" },
                    { new Guid("a3456789-abcd-e123-4567-89abcdef0123"), "Mumbai", new Guid("a055fa40-94c0-43ae-83b0-86138b267297"), new DateTime(1995, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "priya.singh@email.com", "Female", "Priya Singh" },
                    { new Guid("a6a41716-c23b-4772-bb40-0da9ad0606e8"), "Male", new Guid("57643ccc-1371-4445-b92c-c74a6e428ed1"), new DateTime(1993, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "aisha.ali@email.com", "Female", "Aisha Ali" },
                    { new Guid("b456789a-bcde-f123-4567-89abcdef0123"), "Thimphu", new Guid("c85c09ff-9da2-44f8-aa86-59502fc3737f"), new DateTime(1989, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "choden.zangmo@email.com", "Female", "Choden Zangmo" },
                    { new Guid("c321d7e8-f400-4cc2-815b-280946a07903"), "Kathmandu", new Guid("b7078c84-62dd-4551-ba74-dad88e597492"), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@email.com", "Male", "John Doe" },
                    { new Guid("c56789ab-cdef-1234-5678-9abcdef01234"), "Male", new Guid("57643ccc-1371-4445-b92c-c74a6e428ed1"), new DateTime(1991, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "mohamed.faisal@email.com", "Male", "Mohamed Faisal" },
                    { new Guid("cbf1b428-a099-489d-a66a-6023514fa346"), "Dhaka", new Guid("fefc9d80-fb10-4114-ac9c-3f0f9e999974"), new DateTime(1995, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "rahima.begum@email.com", "Female", "Rahima Begum" },
                    { new Guid("d0a8ee7c-9df4-4277-862e-bf418c953e81"), "Lahore", new Guid("07481067-c679-4a48-9823-4b04f7e353e0"), new DateTime(1985, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.khan@email.com", "Male", "Ahmed Khan" },
                    { new Guid("d6789abc-def1-2345-6789-abcdef012345"), "Colombo", new Guid("7da058f2-dfbe-446b-bd9f-c68338399aed"), new DateTime(1994, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.perera@email.com", "Female", "Sara Perera" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
