using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string sp_CreatePerson = @"
                create procedure [dbo].[CreatePerson]
                   ( @PersonId uniqueidentifier,
                    @PersonName nvarchar(max),
                    @Email nvarchar(max),
                    @DateOfBirth datetime2(7),
                    @Gender nvarchar(max),
                    @Address nvarchar(max),
                    @CountryId uniqueidentifier
                   as begin 
                    insert into [dbo].[Persons](PersonId,PersonName,Email,DateOfBirth,Gender,Address,CountryId)
                    values(@PersonId,@PersonName,@Email,@DateOfBirth,@Gender,@Address,@CountryId);
                    end  
             ";
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_CreatePerson = @"
                drop procedure [dbo].[CreatePerson]
               ";

        }
    }
}
