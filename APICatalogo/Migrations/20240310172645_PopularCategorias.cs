using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Migrations;
using APICatalogo.Models;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into \"Categorias\" (\"Nome\", \"ImagemUrl\")  values ('Bebidas', 'bebidas.jpg');");
            migrationBuilder.Sql("Insert into \"Categorias\" (\"Nome\", \"ImagemUrl\")  values ('Lanches', 'lanches.jpg');");
            migrationBuilder.Sql("Insert into \"Categorias\" (\"Nome\", \"ImagemUrl\")  values ('Sobremesas', 'sobremesas.jpg');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from \"Categorias\";");
        }
    }
}
