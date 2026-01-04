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
                     @PersonName nvarchar(100),
                     @Email nvarchar(100),
                     @DateOfBirth datetime2(7), 
                        @Gender nvarchar(50),
                        @Address nvarchar(100),
                        @CountryId  )
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
