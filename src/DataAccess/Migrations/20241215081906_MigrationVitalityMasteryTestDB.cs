using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MigrationVitalityMasteryTestDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    achievements_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    achievements_text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    achievements_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Achievem__833913349530C80D", x => x.achievements_id);
                });

            migrationBuilder.CreateTable(
                name: "Dialogs",
                columns: table => new
                {
                    dialogs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text_dialogs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    time_create = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dialogs__39AA56DBFF015D08", x => x.dialogs_id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Calories = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProteinPer = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FatPer = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CarbsPer = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VitaminsAndMinerals = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NutritionalValue = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6EDBA594E9A", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3213E83FAF59EC6E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    trainer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trainer__65A4B6296B04858D", x => x.trainer_id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    training_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    DurationMinutes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CaloriesBurned = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrainingType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Training__2F28D08F96433C89", x => x.training_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    role_user = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    user_img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    phone_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F812C2756", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Nutrition",
                columns: table => new
                {
                    nutrition_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product = table.Column<int>(type: "int", nullable: false),
                    mean_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    mean_deacription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_nutrition = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Nutritio__147CC3A2FD0AE619", x => x.nutrition_id);
                    table.ForeignKey(
                        name: "FK_Nutration",
                        column: x => x.Product,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    training_id = table.Column<int>(type: "int", nullable: false),
                    trainer_id = table.Column<int>(type: "int", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    day_of_week = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Schedule__C46A8A6F1508589D", x => x.schedule_id);
                    table.ForeignKey(
                        name: "FK_Schedule_Trainer",
                        column: x => x.trainer_id,
                        principalTable: "Trainer",
                        principalColumn: "trainer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Training",
                        column: x => x.training_id,
                        principalTable: "Training",
                        principalColumn: "training_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comments_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    item_type = table.Column<int>(type: "int", nullable: false),
                    comments_text = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    comments_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__E059CA999E03E383", x => x.comments_id);
                    table.ForeignKey(
                        name: "FK_Comments",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    friend_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id_1 = table.Column<int>(type: "int", nullable: false),
                    user_id_2 = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Friend__3FA1E1551E575765", x => x.friend_id);
                    table.ForeignKey(
                        name: "FK_Friend_1",
                        column: x => x.user_id_1,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Friend_2",
                        column: x => x.user_id_2,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    groups_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    owner_groups = table.Column<int>(type: "int", nullable: false),
                    groups_name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    update_groups = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__54CA4F672F0409BA", x => x.groups_id);
                    table.ForeignKey(
                        name: "FK_Groups_Owner",
                        column: x => x.owner_groups,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageUsers",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    message_content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_message = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    date_up_message = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MessageU__C87C037C6F25066E", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_1",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Message_2",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "PhotoUsers",
                columns: table => new
                {
                    PhotoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    PhotoLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    upload_photo = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhotoUse__21B7B582572DA409", x => x.PhotoID);
                    table.ForeignKey(
                        name: "Fk_PhotoUser",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    publications_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    publication_text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    publication_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    publications_image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publicat__CE2F37DC4BA8743B", x => x.publications_id);
                    table.ForeignKey(
                        name: "FK_Publication",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_to_achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    achievements_id = table.Column<int>(type: "int", nullable: false),
                    get_date_achievements = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_to___3213E83FC07128E9", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_1",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievements_2",
                        column: x => x.achievements_id,
                        principalTable: "Achievements",
                        principalColumn: "achievements_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_to_dialogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dialog_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    time_create = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_to___3213E83F52D018B5", x => x.id);
                    table.ForeignKey(
                        name: "FK_Dialogs",
                        column: x => x.dialog_id,
                        principalTable: "Dialogs",
                        principalColumn: "dialogs_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDialogs",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_to_rule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_to___3213E83F1630FB83", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_user",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_role",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTraining",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    training_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    trainer_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    day_of_week = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    start_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    end_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    duration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    training_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserTrai__3213E83F47334E14", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserTraining_1",
                        column: x => x.trainer_id,
                        principalTable: "Trainer",
                        principalColumn: "trainer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTraining_2",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_UserTraining_3",
                        column: x => x.training_id,
                        principalTable: "Training",
                        principalColumn: "training_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNutrition",
                columns: table => new
                {
                    user_nutrition_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    nutrition_id = table.Column<int>(type: "int", nullable: false),
                    date_of_admission = table.Column<DateOnly>(type: "date", nullable: false),
                    appointment_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    nutrition_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    report = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserNutr__53483C7116CD73D3", x => x.user_nutrition_id);
                    table.ForeignKey(
                        name: "FK_UserNutration_1",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNutration_2",
                        column: x => x.nutrition_id,
                        principalTable: "Nutrition",
                        principalColumn: "nutrition_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainersSchedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule_id = table.Column<int>(type: "int", nullable: false),
                    trainer_id = table.Column<int>(type: "int", nullable: false),
                    type_of_training = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trainers__3213E83F7B8BE60E", x => x.id);
                    table.ForeignKey(
                        name: "FK_TrainersSchedule_1",
                        column: x => x.trainer_id,
                        principalTable: "Trainer",
                        principalColumn: "trainer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainersSchedule_2",
                        column: x => x.schedule_id,
                        principalTable: "Schedule",
                        principalColumn: "schedule_id");
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    groups_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GroupMem__54CA4F67706920EB", x => x.groups_id);
                    table.ForeignKey(
                        name: "Fk_GroupMember_1",
                        column: x => x.groups_id,
                        principalTable: "Groups",
                        principalColumn: "groups_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Fk_GroupMember_2",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Comments__E059CA98090CAFEB",
                table: "Comments",
                column: "comments_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friend_user_id_1",
                table: "Friend",
                column: "user_id_1");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_user_id_2",
                table: "Friend",
                column: "user_id_2");

            migrationBuilder.CreateIndex(
                name: "UQ__Friend__38E9CCFB5FF403B2",
                table: "Friend",
                columns: new[] { "friend_id", "user_id_1", "user_id_2" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_user_id",
                table: "GroupMembers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Groups__FEBE0EE7039561F3",
                table: "Groups",
                column: "owner_groups",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageUsers_ReceiverID",
                table: "MessageUsers",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageUsers_SenderID",
                table: "MessageUsers",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_Product",
                table: "Nutrition",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUsers_user_id",
                table: "PhotoUsers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Products__A2A64E92B2373B4D",
                table: "Products",
                column: "Product",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publication_users_id",
                table: "Publication",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Publicat__CE2F37DD62E113B6",
                table: "Publication",
                column: "publications_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__3213E83E906D64D5",
                table: "Roles",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_trainer_id",
                table: "Schedule",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_training_id",
                table: "Schedule",
                column: "training_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Trainer__44B3C35458D50DCC",
                table: "Trainer",
                columns: new[] { "trainer_id", "email", "phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainersSchedule_schedule_id",
                table: "TrainersSchedule",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrainersSchedule_trainer_id",
                table: "TrainersSchedule",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Training__2F28D08E145B3212",
                table: "Training",
                column: "training_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_to_achievements_achievements_id",
                table: "User_to_achievements",
                column: "achievements_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_to_achievements_user_id",
                table: "User_to_achievements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_to_dialogs_dialog_id",
                table: "User_to_dialogs",
                column: "dialog_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_to_dialogs_user_id",
                table: "User_to_dialogs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_to_rule_role_id",
                table: "User_to_rule",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_to_rule_user_id",
                table: "User_to_rule",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__User_to___3213E83EE0E80FDA",
                table: "User_to_rule",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNutrition_nutrition_id",
                table: "UserNutrition",
                column: "nutrition_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserNutrition_user_id",
                table: "UserNutrition",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__98A94272BDEB79D7",
                table: "Users",
                columns: new[] { "user_id", "email", "phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_trainer_id",
                table: "UserTraining",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_training_id",
                table: "UserTraining",
                column: "training_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_user_id",
                table: "UserTraining",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "MessageUsers");

            migrationBuilder.DropTable(
                name: "PhotoUsers");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "TrainersSchedule");

            migrationBuilder.DropTable(
                name: "User_to_achievements");

            migrationBuilder.DropTable(
                name: "User_to_dialogs");

            migrationBuilder.DropTable(
                name: "User_to_rule");

            migrationBuilder.DropTable(
                name: "UserNutrition");

            migrationBuilder.DropTable(
                name: "UserTraining");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Dialogs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Nutrition");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}