using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetAllPersons_StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string sp_GetAllThePersons = @"
                      
            Create procedure [dbo].[GetAllPersons]
             as begin 
              select  * from [dbo].[Persons];
               end
            ";
            migrationBuilder.Sql(sp_GetAllThePersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            string sp_GetAllThePersons = @"
                      
            Drop procedure [dbo].[GetAllPersons]
             ";
            migrationBuilder.Sql(sp_GetAllThePersons);
        }

    }
}
