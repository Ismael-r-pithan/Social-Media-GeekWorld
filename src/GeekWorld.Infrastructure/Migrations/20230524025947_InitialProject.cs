using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekWorld.Infrastructure.Migrations
{
    public partial class InitialProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    full_name = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    nickname = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: true, defaultValue: ""),
                    date_of_birth = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    cep = table.Column<string>(type: "VARCHAR", maxLength: 8, nullable: false),
                    password = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    image_profile = table.Column<string>(type: "VARCHAR", maxLength: 512, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    friend_id = table.Column<Guid>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.id);
                    table.ForeignKey(
                        name: "FK_Friendship_User_friend_id",
                        column: x => x.friend_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendship_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    content = table.Column<string>(type: "VARCHAR", maxLength: 512, nullable: false),
                    visibility = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: false),
                    created_at = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    author_id = table.Column<Guid>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.id);
                    table.ForeignKey(
                        name: "FK_Post_User_author_id",
                        column: x => x.author_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    content = table.Column<string>(type: "VARCHAR", maxLength: 512, nullable: false),
                    created_at = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    author_id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    post_id = table.Column<Guid>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post_post_id",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_author_id",
                        column: x => x.author_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikePost",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    value = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    user_id = table.Column<Guid>(type: "VARCHAR", nullable: false),
                    post_id = table.Column<Guid>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePost", x => x.id);
                    table.ForeignKey(
                        name: "FK_LikePost_Post_post_id",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikePost_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_author_id",
                table: "Comment",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_post_id",
                table: "Comment",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_friend_id",
                table: "Friendship",
                column: "friend_id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_user_id",
                table: "Friendship",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_LikePost_post_id",
                table: "LikePost",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_LikePost_user_id",
                table: "LikePost",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_author_id",
                table: "Post",
                column: "author_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "LikePost");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
