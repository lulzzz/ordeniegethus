using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "integration",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_integration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    license_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_license_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "national_component",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_national_component", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    organization_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sector",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sector", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "standard",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_standard", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dcat_catalog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true),
                    homepage = table.Column<string>(nullable: true),
                    issued = table.Column<DateTime>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    license = table.Column<string>(nullable: true),
                    modified = table.Column<DateTime>(nullable: true),
                    organization_id = table.Column<int>(nullable: false),
                    themes_taxonomy = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dcat_catalog", x => x.id);
                    table.ForeignKey(
                        name: "fk_dcat_catalog_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dataset",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    access_right = table.Column<int>(nullable: false),
                    data_location = table.Column<string>(nullable: true),
                    date_created = table.Column<DateTime>(nullable: true),
                    date_modified = table.Column<DateTime>(nullable: true),
                    dcat_catalog_id = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    has_master_data = table.Column<bool>(nullable: false),
                    has_personal_data = table.Column<bool>(nullable: false),
                    has_sensitive_personal_data = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    published_to_shared_data_catalog = table.Column<DateTime>(nullable: true),
                    purpose = table.Column<string>(nullable: true),
                    user_created = table.Column<string>(nullable: true),
                    user_modified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dataset", x => x.id);
                    table.ForeignKey(
                        name: "fk_dataset_dcat_catalog_dcat_catalog_id",
                        column: x => x.dcat_catalog_id,
                        principalTable: "dcat_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "access_right_comment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    access_right_comment_field = table.Column<string>(nullable: true),
                    dataset_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_right_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_access_right_comment_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dcat_concept",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dataset_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dcat_concept", x => x.id);
                    table.ForeignKey(
                        name: "fk_dcat_concept_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "distribution",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dataset_id = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    license_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_distribution", x => x.id);
                    table.ForeignKey(
                        name: "fk_distribution_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_distribution_license_document_license_id",
                        column: x => x.license_id,
                        principalTable: "license_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "field",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dataset_id = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    is_personal_data = table.Column<bool>(nullable: false),
                    is_sensitive_personal_data = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_field", x => x.id);
                    table.ForeignKey(
                        name: "fk_field_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "identifier",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dataset_id = table.Column<int>(nullable: true),
                    identifier_field = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identifier", x => x.id);
                    table.ForeignKey(
                        name: "fk_identifier_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "keyword",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dataset_id = table.Column<int>(nullable: true),
                    keyword_field = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_keyword", x => x.id);
                    table.ForeignKey(
                        name: "fk_keyword_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "format",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    distribution_id = table.Column<int>(nullable: true),
                    format_field = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_format", x => x.id);
                    table.ForeignKey(
                        name: "fk_format_distribution_distribution_id",
                        column: x => x.distribution_id,
                        principalTable: "distribution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "application_dataset",
                columns: table => new
                {
                    application_id = table.Column<int>(nullable: false),
                    dataset_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_dataset", x => new { x.application_id, x.dataset_id });
                    table.ForeignKey(
                        name: "fk_application_dataset_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_national_component",
                columns: table => new
                {
                    application_id = table.Column<int>(nullable: false),
                    national_component_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_national_component", x => new { x.application_id, x.national_component_id });
                    table.ForeignKey(
                        name: "fk_application_national_component_national_component_national_component_id",
                        column: x => x.national_component_id,
                        principalTable: "national_component",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_standard",
                columns: table => new
                {
                    application_id = table.Column<int>(nullable: false),
                    standard_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_standard", x => new { x.application_id, x.standard_id });
                    table.ForeignKey(
                        name: "fk_application_standard_standard_standard_id",
                        column: x => x.standard_id,
                        principalTable: "standard",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_supported_integration",
                columns: table => new
                {
                    application_id = table.Column<int>(nullable: false),
                    supported_integration_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_supported_integration", x => new { x.application_id, x.supported_integration_id });
                    table.ForeignKey(
                        name: "fk_application_supported_integration_integration_supported_integration_id",
                        column: x => x.supported_integration_id,
                        principalTable: "integration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    application_id = table.Column<int>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    full_name = table.Column<string>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    password_hash = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    security_stamp = table.Column<string>(nullable: true),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    annual_fee = table.Column<decimal>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: true),
                    date_modified = table.Column<DateTime>(nullable: true),
                    hosting_location = table.Column<string>(nullable: true),
                    hosting_vendor = table.Column<string>(nullable: true),
                    initial_cost = table.Column<decimal>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    number_of_users = table.Column<int>(nullable: false),
                    organization_id = table.Column<int>(nullable: true),
                    system_owner_id = table.Column<string>(nullable: true),
                    user_created = table.Column<string>(nullable: true),
                    user_modified = table.Column<string>(nullable: true),
                    vendor = table.Column<string>(nullable: true),
                    version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_application_asp_net_users_system_owner_id",
                        column: x => x.system_owner_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_administrators",
                columns: table => new
                {
                    organization_id = table.Column<int>(nullable: false),
                    application_user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_administrators", x => new { x.organization_id, x.application_user_id });
                    table.ForeignKey(
                        name: "fk_organization_administrators_asp_net_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_administrators_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_application_user",
                columns: table => new
                {
                    organization_id = table.Column<int>(nullable: false),
                    application_user_id = table.Column<string>(nullable: false),
                    role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_application_user", x => new { x.organization_id, x.application_user_id, x.role });
                    table.ForeignKey(
                        name: "fk_organization_application_user_asp_net_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_application_user_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resource_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    application_id = table.Column<int>(nullable: true),
                    dataset_id = table.Column<int>(nullable: true),
                    dataset_id1 = table.Column<int>(nullable: true),
                    dataset_id2 = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    distribution_id = table.Column<int>(nullable: true),
                    sector_id = table.Column<int>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resource_link", x => x.id);
                    table.ForeignKey(
                        name: "fk_resource_link_application_application_id",
                        column: x => x.application_id,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resource_link_dataset_dataset_id",
                        column: x => x.dataset_id,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resource_link_dataset_dataset_id1",
                        column: x => x.dataset_id1,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resource_link_dataset_dataset_id2",
                        column: x => x.dataset_id2,
                        principalTable: "dataset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resource_link_distribution_distribution_id",
                        column: x => x.distribution_id,
                        principalTable: "distribution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resource_link_sector_sector_id",
                        column: x => x.sector_id,
                        principalTable: "sector",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sector_application",
                columns: table => new
                {
                    sector_id = table.Column<int>(nullable: false),
                    application_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sector_application", x => new { x.sector_id, x.application_id });
                    table.ForeignKey(
                        name: "fk_sector_application_application_application_id",
                        column: x => x.application_id,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sector_application_sector_sector_id",
                        column: x => x.sector_id,
                        principalTable: "sector",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_access_right_comment_dataset_id",
                table: "access_right_comment",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_organization_id",
                table: "application",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_system_owner_id",
                table: "application",
                column: "system_owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_dataset_dataset_id",
                table: "application_dataset",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_national_component_national_component_id",
                table: "application_national_component",
                column: "national_component_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_standard_standard_id",
                table: "application_standard",
                column: "standard_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_supported_integration_supported_integration_id",
                table: "application_supported_integration",
                column: "supported_integration_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "asp_net_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_application_id",
                table: "asp_net_users",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "asp_net_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "asp_net_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dataset_dcat_catalog_id",
                table: "dataset",
                column: "dcat_catalog_id");

            migrationBuilder.CreateIndex(
                name: "ix_dcat_catalog_organization_id",
                table: "dcat_catalog",
                column: "organization_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dcat_concept_dataset_id",
                table: "dcat_concept",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_distribution_dataset_id",
                table: "distribution",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_distribution_license_id",
                table: "distribution",
                column: "license_id");

            migrationBuilder.CreateIndex(
                name: "ix_field_dataset_id",
                table: "field",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_format_distribution_id",
                table: "format",
                column: "distribution_id");

            migrationBuilder.CreateIndex(
                name: "ix_identifier_dataset_id",
                table: "identifier",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_keyword_dataset_id",
                table: "keyword",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_administrators_application_user_id",
                table: "organization_administrators",
                column: "application_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_application_user_application_user_id",
                table: "organization_application_user",
                column: "application_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_application_id",
                table: "resource_link",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_dataset_id",
                table: "resource_link",
                column: "dataset_id");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_dataset_id1",
                table: "resource_link",
                column: "dataset_id1");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_dataset_id2",
                table: "resource_link",
                column: "dataset_id2");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_distribution_id",
                table: "resource_link",
                column: "distribution_id");

            migrationBuilder.CreateIndex(
                name: "ix_resource_link_sector_id",
                table: "resource_link",
                column: "sector_id");

            migrationBuilder.CreateIndex(
                name: "ix_sector_application_application_id",
                table: "sector_application",
                column: "application_id");

            migrationBuilder.AddForeignKey(
                name: "fk_application_dataset_application_application_id",
                table: "application_dataset",
                column: "application_id",
                principalTable: "application",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_national_component_application_application_id",
                table: "application_national_component",
                column: "application_id",
                principalTable: "application",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_standard_application_application_id",
                table: "application_standard",
                column: "application_id",
                principalTable: "application",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_supported_integration_application_application_id",
                table: "application_supported_integration",
                column: "application_id",
                principalTable: "application",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_application_application_id",
                table: "asp_net_users",
                column: "application_id",
                principalTable: "application",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_application_organization_organization_id",
                table: "application");

            migrationBuilder.DropForeignKey(
                name: "fk_application_asp_net_users_system_owner_id",
                table: "application");

            migrationBuilder.DropTable(
                name: "access_right_comment");

            migrationBuilder.DropTable(
                name: "application_dataset");

            migrationBuilder.DropTable(
                name: "application_national_component");

            migrationBuilder.DropTable(
                name: "application_standard");

            migrationBuilder.DropTable(
                name: "application_supported_integration");

            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "dcat_concept");

            migrationBuilder.DropTable(
                name: "field");

            migrationBuilder.DropTable(
                name: "format");

            migrationBuilder.DropTable(
                name: "identifier");

            migrationBuilder.DropTable(
                name: "keyword");

            migrationBuilder.DropTable(
                name: "organization_administrators");

            migrationBuilder.DropTable(
                name: "organization_application_user");

            migrationBuilder.DropTable(
                name: "resource_link");

            migrationBuilder.DropTable(
                name: "sector_application");

            migrationBuilder.DropTable(
                name: "national_component");

            migrationBuilder.DropTable(
                name: "standard");

            migrationBuilder.DropTable(
                name: "integration");

            migrationBuilder.DropTable(
                name: "asp_net_roles");

            migrationBuilder.DropTable(
                name: "distribution");

            migrationBuilder.DropTable(
                name: "sector");

            migrationBuilder.DropTable(
                name: "dataset");

            migrationBuilder.DropTable(
                name: "license_document");

            migrationBuilder.DropTable(
                name: "dcat_catalog");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "asp_net_users");

            migrationBuilder.DropTable(
                name: "application");
        }
    }
}
