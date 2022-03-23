using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taste.DataAccess.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_MenueItems_MenuItemId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orderHeaders_OrderId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderHeaders_AspNetUsers_UserId",
                table: "orderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderHeaders",
                table: "orderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderDetails",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "orderHeaders",
                newName: "OrderHeaders");

            migrationBuilder.RenameTable(
                name: "orderDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_orderHeaders_UserId",
                table: "OrderHeaders",
                newName: "IX_OrderHeaders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_MenuItemId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_MenuItemId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_MenueItems_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId",
                principalTable: "MenueItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_MenueItems_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderHeaders",
                newName: "orderHeaders");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "orderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeaders_UserId",
                table: "orderHeaders",
                newName: "IX_orderHeaders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "orderDetails",
                newName: "IX_orderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_MenuItemId",
                table: "orderDetails",
                newName: "IX_orderDetails_MenuItemId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderHeaders",
                table: "orderHeaders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderDetails",
                table: "orderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_MenueItems_MenuItemId",
                table: "orderDetails",
                column: "MenuItemId",
                principalTable: "MenueItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orderHeaders_OrderId",
                table: "orderDetails",
                column: "OrderId",
                principalTable: "orderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderHeaders_AspNetUsers_UserId",
                table: "orderHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
