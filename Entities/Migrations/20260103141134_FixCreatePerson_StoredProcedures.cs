using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatePerson_StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string sp_CreatePerson = @"
                create procedure [dbo].[CreatePerson]
                   ( @PersonId uniqueidentifier,
                    @PersonName nvarchar(100),
                    @Email nvarchar(100),
                    @DateOfBirth datetime2(7),
                    @Gender nvarchar(50),
                    @Address nvarchar(100),
                    @CountryId uniqueidentifier)
                   as begin 
                    insert into [dbo].[Persons](PersonId,PersonName,Email,DateOfBirth,Gender,Address,CountryId)
                    values(@PersonId,@PersonName,@Email,@DateOfBirth,@Gender,@Address,@CountryId);
                    end  
             ";

            migrationBuilder.Sql(sp_CreatePerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_CreatePerson = @"
                drop procedure [dbo].[CreatePerson]
               ";

            migrationBuilder.Sql(sp_CreatePerson);
        }
    }
}
