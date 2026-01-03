using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_UpdatePerson = @"
                create procedure [dbo].[UpdatePerson]
                    (@PersonId uniqueidentifier,
                     @PersonName nvarchar(max),
                     @Email nvarchar(max),
                     @DateOfBirth datetime2(7), 
                        @Gender nvarchar(max),
                        @Address nvarchar(max),
                        @CountryId uniqueidentifier)
                     as begin 
                     update [dbo].[Persons]
                      set 
                        PersonName=@PersonName,
                        Email=@Email,
                        DateOfBirth=@DateOfBirth,
                        Gender=@Gender,
                        Address=@Address,
                        CountryId=@CountryId
                        where PersonId=@PersonId;
                     end
                         ";
            migrationBuilder.Sql(sp_UpdatePerson);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            string sp_UpdatePerson = @"
                  drop procedure [dbo].[UpdatePerson]
               ";

        }
    }
}
