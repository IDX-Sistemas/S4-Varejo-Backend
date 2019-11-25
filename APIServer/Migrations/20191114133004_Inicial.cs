using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "age_loj",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NOM_ITE = table.Column<string>(maxLength: 40, nullable: true),
                    END_ITE = table.Column<string>(maxLength: 40, nullable: true),
                    BAI_ITE = table.Column<string>(maxLength: 15, nullable: true),
                    CID_ITE = table.Column<string>(maxLength: 15, nullable: true),
                    TEL_ITE = table.Column<string>(maxLength: 15, nullable: true),
                    EMA_ITE = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_age_loj", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "arq_mov",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_TRA = table.Column<string>(maxLength: 2, nullable: false),
                    COD_ITE = table.Column<string>(maxLength: 14, nullable: false),
                    LOC_SAI = table.Column<string>(maxLength: 2, nullable: true),
                    LOC_ENT = table.Column<string>(maxLength: 2, nullable: true),
                    DAT_MOV = table.Column<DateTime>(type: "date", nullable: false),
                    HIS_MOV = table.Column<string>(maxLength: 20, nullable: true),
                    NUM_DOC = table.Column<string>(maxLength: 10, nullable: true),
                    QUT_ATU = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arq_mov", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_ace",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NOM_USR = table.Column<string>(maxLength: 10, nullable: false),
                    SEN_USR = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_ace", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_cli",
                columns: table => new
                {
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    NOM_CLI = table.Column<string>(maxLength: 40, nullable: true),
                    END_CLI = table.Column<string>(maxLength: 40, nullable: true),
                    BAI_CLI = table.Column<string>(maxLength: 18, nullable: true),
                    CID_CLI = table.Column<string>(maxLength: 20, nullable: true),
                    CEP_CLI = table.Column<string>(maxLength: 8, nullable: true),
                    EST_CLI = table.Column<string>(maxLength: 2, nullable: true),
                    REG_CLI = table.Column<string>(maxLength: 13, nullable: true),
                    CIC_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    TEL_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    DAT_NAS = table.Column<DateTime>(nullable: true),
                    EST_CIV = table.Column<string>(maxLength: 15, nullable: true),
                    CNJ_CLI = table.Column<string>(maxLength: 40, nullable: true),
                    QUT_FIL = table.Column<int>(nullable: true),
                    DAT_CAD = table.Column<DateTime>(nullable: true),
                    DAT_ULT = table.Column<DateTime>(nullable: true),
                    VAL_ULT = table.Column<double>(nullable: true),
                    VAL_CRE = table.Column<double>(nullable: true),
                    CON_CEI = table.Column<string>(maxLength: 2, nullable: true),
                    OBS_1 = table.Column<string>(maxLength: 25, nullable: true),
                    OBS_2 = table.Column<string>(maxLength: 25, nullable: true),
                    CLI_SPC = table.Column<string>(maxLength: 1, nullable: true),
                    TIP_CLI = table.Column<string>(maxLength: 1, nullable: true),
                    EMA_CLI = table.Column<string>(maxLength: 35, nullable: true),
                    EMP_CLI = table.Column<string>(maxLength: 40, nullable: true),
                    END_EMP = table.Column<string>(maxLength: 40, nullable: true),
                    CID_EMP = table.Column<string>(maxLength: 20, nullable: true),
                    TEL_EMP = table.Column<string>(maxLength: 15, nullable: true),
                    CAR_EMP = table.Column<string>(maxLength: 15, nullable: true),
                    SAL_EMP = table.Column<double>(nullable: true),
                    CAR_001 = table.Column<string>(maxLength: 1, nullable: true),
                    CAR_002 = table.Column<string>(maxLength: 2, nullable: true),
                    DAT_ALT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_cli", x => x.COD_CLI);
                    table.UniqueConstraint("AK_cad_cli_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_con",
                columns: table => new
                {
                    COD_CON = table.Column<string>(maxLength: 4, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NOM_CON = table.Column<string>(maxLength: 15, nullable: false),
                    NOM_BAN = table.Column<string>(maxLength: 15, nullable: true),
                    DES_CON = table.Column<string>(maxLength: 20, nullable: true),
                    VAL_SAL = table.Column<double>(nullable: true),
                    ULT_LAN = table.Column<DateTime>(type: "date", nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_con", x => x.COD_CON);
                    table.UniqueConstraint("AK_cad_con_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_des",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_DES = table.Column<string>(maxLength: 2, nullable: false),
                    NOM_DES = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_des", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_for",
                columns: table => new
                {
                    COD_FOR = table.Column<string>(maxLength: 4, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    RAZ_SOC = table.Column<string>(maxLength: 30, nullable: true),
                    END_FOR = table.Column<string>(maxLength: 40, nullable: true),
                    BAI_FOR = table.Column<string>(maxLength: 15, nullable: true),
                    CID_FOR = table.Column<string>(maxLength: 20, nullable: true),
                    CEP_FOR = table.Column<string>(maxLength: 9, nullable: true),
                    EST_FOR = table.Column<string>(maxLength: 2, nullable: true),
                    CGC_FOR = table.Column<string>(maxLength: 18, nullable: true),
                    INS_FOR = table.Column<string>(maxLength: 15, nullable: true),
                    TEL_FOR = table.Column<string>(maxLength: 15, nullable: true),
                    FAX_FOR = table.Column<string>(maxLength: 15, nullable: true),
                    NOM_REP = table.Column<string>(maxLength: 30, nullable: true),
                    END_REP = table.Column<string>(maxLength: 40, nullable: true),
                    BAI_REP = table.Column<string>(maxLength: 15, nullable: true),
                    CID_REP = table.Column<string>(maxLength: 20, nullable: true),
                    EST_REP = table.Column<string>(maxLength: 2, nullable: true),
                    CEP_REP = table.Column<string>(maxLength: 9, nullable: true),
                    TEL_REP = table.Column<string>(maxLength: 15, nullable: true),
                    FAX_REP = table.Column<string>(maxLength: 15, nullable: true),
                    CON_COM = table.Column<string>(maxLength: 30, nullable: true),
                    DES_COM = table.Column<string>(maxLength: 30, nullable: true),
                    DAT_CAD = table.Column<DateTime>(type: "date", nullable: true),
                    OBS_001 = table.Column<string>(maxLength: 30, nullable: true),
                    OBS_002 = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_for", x => x.COD_FOR);
                    table.UniqueConstraint("AK_cad_for_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_fun",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: false),
                    NOM_CLI = table.Column<string>(maxLength: 40, nullable: false),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    REG_CLI = table.Column<string>(maxLength: 13, nullable: true),
                    CIC_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    DAT_NAS = table.Column<DateTime>(type: "date", nullable: true),
                    END_CLI = table.Column<string>(maxLength: 40, nullable: true),
                    BAI_CLI = table.Column<string>(maxLength: 18, nullable: true),
                    CID_CLI = table.Column<string>(maxLength: 20, nullable: true),
                    EST_CLI = table.Column<string>(maxLength: 2, nullable: true),
                    CEP_CLI = table.Column<string>(maxLength: 8, nullable: true),
                    TEL_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    DAT_CAD = table.Column<DateTime>(type: "date", nullable: true),
                    CAR_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    CRP_CLI = table.Column<string>(maxLength: 15, nullable: true),
                    VAL_SAL = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_fun", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_his",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_HIS = table.Column<string>(maxLength: 2, nullable: false),
                    NOM_HIS = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_his", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_ite",
                columns: table => new
                {
                    COD_ITE = table.Column<string>(maxLength: 14, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    DES_ITE = table.Column<string>(maxLength: 30, nullable: true),
                    UNI_ITE = table.Column<string>(maxLength: 2, nullable: true),
                    DES_CD1 = table.Column<string>(maxLength: 18, nullable: true),
                    COD_TAB = table.Column<string>(maxLength: 4, nullable: true),
                    DES_CD2 = table.Column<string>(maxLength: 18, nullable: true),
                    VAL_001 = table.Column<double>(nullable: true),
                    VAL_002 = table.Column<double>(nullable: true),
                    VAL_CUS = table.Column<double>(nullable: true),
                    STA_ITE = table.Column<string>(maxLength: 1, nullable: true),
                    VAL_PRO = table.Column<double>(nullable: true),
                    ULT_FOR = table.Column<string>(maxLength: 4, nullable: true),
                    ULT_COM = table.Column<DateTime>(type: "date", nullable: true),
                    CON_MED = table.Column<int>(nullable: true),
                    ITE_PRI = table.Column<string>(maxLength: 14, nullable: true),
                    CUS_MED = table.Column<double>(nullable: true),
                    VEN_MES = table.Column<int>(nullable: true),
                    COM_MES = table.Column<int>(nullable: true),
                    TRA_MES = table.Column<int>(nullable: true),
                    COD_SEC = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_ite", x => x.COD_ITE);
                    table.UniqueConstraint("AK_cad_ite_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_loc",
                columns: table => new
                {
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NOM_LOC = table.Column<string>(maxLength: 15, nullable: false),
                    MOV_EST = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_loc", x => x.COD_LOC);
                    table.UniqueConstraint("AK_cad_loc_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_mar",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_MAR = table.Column<string>(maxLength: 3, nullable: false),
                    NOM_MAR = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_mar", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_ord",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_ORD = table.Column<string>(maxLength: 3, nullable: false),
                    NOM_ORD = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_ord", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_sec",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_SEC = table.Column<string>(maxLength: 2, nullable: false),
                    NOM_SEC = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_sec", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_tab",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_TAB = table.Column<string>(maxLength: 4, nullable: false),
                    NOM_TAB = table.Column<string>(maxLength: 30, nullable: true),
                    DAT_TAB = table.Column<DateTime>(type: "date", nullable: true),
                    ULT_ALT = table.Column<DateTime>(type: "date", nullable: true),
                    MAR_GEM = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_tab", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_ven",
                columns: table => new
                {
                    COD_VEN = table.Column<string>(maxLength: 2, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NOM_VEN = table.Column<string>(maxLength: 20, nullable: false),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    DAT_CAD = table.Column<DateTime>(type: "date", nullable: true),
                    TAX_COM = table.Column<double>(nullable: true),
                    VAL_ADI = table.Column<double>(nullable: true),
                    VAL_COT = table.Column<double>(nullable: true),
                    VAL_FIX = table.Column<double>(nullable: true),
                    PRAZO = table.Column<double>(nullable: true),
                    VISTA = table.Column<double>(nullable: true),
                    COD_SE1 = table.Column<string>(maxLength: 2, nullable: true),
                    COD_SE2 = table.Column<string>(maxLength: 2, nullable: true),
                    COD_SE3 = table.Column<string>(maxLength: 2, nullable: true),
                    COD_SE4 = table.Column<string>(maxLength: 4, nullable: true),
                    COD_SE5 = table.Column<string>(maxLength: 2, nullable: true),
                    COD_SE6 = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_ven", x => x.COD_VEN);
                    table.UniqueConstraint("AK_cad_ven_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cep_rua",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(maxLength: 8, nullable: false),
                    RUA = table.Column<string>(maxLength: 55, nullable: true),
                    BAI = table.Column<string>(maxLength: 18, nullable: true),
                    CID = table.Column<string>(maxLength: 20, nullable: true),
                    EST = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cep_rua", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "CONDICAO_PAGAMENTO",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 4, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Intervalo = table.Column<int>(nullable: false),
                    Parcelas = table.Column<int>(nullable: false),
                    ComEntrada = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONDICAO_PAGAMENTO", x => x.Codigo);
                    table.UniqueConstraint("AK_CONDICAO_PAGAMENTO_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "pag_doc",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    DAT_PAG = table.Column<DateTime>(type: "date", nullable: true),
                    NUM_DOC = table.Column<string>(nullable: true),
                    LOC_PAG = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pag_doc", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "par_met",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    DES_PAR = table.Column<string>(nullable: false),
                    VARIAVEL = table.Column<string>(nullable: true),
                    VAL_PAR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_par_met", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "TMP_HISTORICO_CLIENTE",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Cliente = table.Column<string>(nullable: true),
                    NumeroDuplicata = table.Column<string>(nullable: true),
                    Loja = table.Column<string>(nullable: true),
                    NumeroDocumento = table.Column<string>(nullable: true),
                    TipoVenda = table.Column<string>(nullable: true),
                    DataEmissao = table.Column<DateTime>(nullable: true),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    ValorDuplicata = table.Column<double>(nullable: true),
                    ValorPago = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMP_HISTORICO_CLIENTE", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "cad_opr",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(nullable: true),
                    DES_OPR = table.Column<string>(maxLength: 50, nullable: true),
                    TAX_OPR = table.Column<double>(nullable: true),
                    PAR_INI = table.Column<int>(nullable: false),
                    PAR_FIN = table.Column<int>(nullable: false),
                    FLG_STA = table.Column<string>(maxLength: 1, nullable: true),
                    TIP_VEN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_opr", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_opr_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "con_rec",
                columns: table => new
                {
                    LOC_PAG = table.Column<string>(maxLength: 2, nullable: false),
                    NUM_DUP = table.Column<string>(maxLength: 8, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: true),
                    NUM_DOC = table.Column<string>(maxLength: 6, nullable: true),
                    COD_VEN = table.Column<string>(maxLength: 2, nullable: true),
                    VAL_DUP = table.Column<double>(nullable: true),
                    NUM_FAT = table.Column<string>(maxLength: 6, nullable: true),
                    VAL_FAT = table.Column<double>(nullable: true),
                    VAL_JUR = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    DAT_EMI = table.Column<DateTime>(type: "DATE", nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "DATE", nullable: true),
                    NUM__CI = table.Column<string>(maxLength: 6, nullable: true),
                    VAL_TOT = table.Column<double>(nullable: true),
                    FLA_PAG = table.Column<short>(nullable: true),
                    TIP_DUP = table.Column<string>(maxLength: 1, nullable: true),
                    FLA_ENT = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_con_rec", x => new { x.NUM_DUP, x.LOC_PAG });
                    table.UniqueConstraint("AK_con_rec_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_con_rec_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tmp_con_rec",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: false),
                    LOC_PAG = table.Column<string>(maxLength: 2, nullable: false),
                    NUM_DOC = table.Column<string>(maxLength: 6, nullable: true),
                    COD_VEN = table.Column<string>(maxLength: 2, nullable: true),
                    NUM_DUP = table.Column<string>(maxLength: 8, nullable: true),
                    VAL_DUP = table.Column<double>(nullable: true),
                    NUM_FAT = table.Column<string>(maxLength: 6, nullable: true),
                    VAL_FAT = table.Column<double>(nullable: true),
                    VAL_JUR = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    DAT_EMI = table.Column<DateTime>(type: "DATE", nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "DATE", nullable: false),
                    NUM__CI = table.Column<string>(maxLength: 6, nullable: true),
                    VAL_TOT = table.Column<double>(nullable: true),
                    FLA_PAG = table.Column<short>(nullable: true),
                    TIP_DUP = table.Column<string>(maxLength: 1, nullable: true),
                    FLA_ENT = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmp_con_rec", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_tmp_con_rec_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "con_cor",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CON = table.Column<string>(maxLength: 4, nullable: false),
                    DEB_CRE = table.Column<string>(maxLength: 1, nullable: false),
                    NUM_CHE = table.Column<string>(maxLength: 6, nullable: true),
                    NOM_HIS = table.Column<string>(maxLength: 29, nullable: true),
                    DAT_LAN = table.Column<DateTime>(type: "date", nullable: false),
                    NUM_DUP = table.Column<string>(maxLength: 8, nullable: true),
                    COD_FOR = table.Column<string>(maxLength: 4, nullable: true),
                    VAL_LAN = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_con_cor", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_con_cor_cad_con_COD_CON",
                        column: x => x.COD_CON,
                        principalTable: "cad_con",
                        principalColumn: "COD_CON",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_con_cor_cad_for_COD_FOR",
                        column: x => x.COD_FOR,
                        principalTable: "cad_for",
                        principalColumn: "COD_FOR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "con_pag",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NUM_DOC = table.Column<string>(maxLength: 6, nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: true),
                    NUM_CHE = table.Column<string>(maxLength: 6, nullable: true),
                    COD_FOR = table.Column<string>(maxLength: 4, nullable: true),
                    NUM_NOT = table.Column<string>(maxLength: 8, nullable: true),
                    DAT_EMI = table.Column<DateTime>(type: "date", nullable: true),
                    CLA_FIS = table.Column<string>(maxLength: 1, nullable: true),
                    NUM_DUP = table.Column<string>(maxLength: 8, nullable: true),
                    VAL_DUP = table.Column<double>(nullable: true),
                    HIS_DUP = table.Column<string>(maxLength: 30, nullable: true),
                    VAL_JUR = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_PAG = table.Column<double>(nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "date", nullable: true),
                    DAT_REC = table.Column<DateTime>(type: "date", nullable: true),
                    DAT_PAG = table.Column<DateTime>(type: "date", nullable: true),
                    COD_CON = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_con_pag", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_con_pag_cad_for_COD_FOR",
                        column: x => x.COD_FOR,
                        principalTable: "cad_for",
                        principalColumn: "COD_FOR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tmp_con_pag",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NUM_DOC = table.Column<string>(maxLength: 6, nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: true),
                    NUM_CHE = table.Column<string>(maxLength: 6, nullable: true),
                    COD_FOR = table.Column<string>(maxLength: 4, nullable: true),
                    NUM_NOT = table.Column<string>(maxLength: 8, nullable: true),
                    DAT_EMI = table.Column<DateTime>(type: "date", nullable: true),
                    CLA_FIS = table.Column<string>(maxLength: 1, nullable: true),
                    NUM_DUP = table.Column<string>(maxLength: 8, nullable: true),
                    VAL_DUP = table.Column<double>(nullable: true),
                    HIS_DUP = table.Column<string>(maxLength: 30, nullable: true),
                    VAL_JUR = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_PAG = table.Column<double>(nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "date", nullable: true),
                    DAT_REC = table.Column<DateTime>(type: "date", nullable: true),
                    DAT_PAG = table.Column<DateTime>(type: "date", nullable: true),
                    COD_CON = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmp_con_pag", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_tmp_con_pag_cad_for_COD_FOR",
                        column: x => x.COD_FOR,
                        principalTable: "cad_for",
                        principalColumn: "COD_FOR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTRADA_ANTECIPADA",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NotaFiscal = table.Column<string>(maxLength: 8, nullable: false),
                    Fornecedor = table.Column<string>(maxLength: 4, nullable: false),
                    ClassificacaoFiscal = table.Column<string>(maxLength: 1, nullable: true),
                    Produto = table.Column<string>(maxLength: 14, nullable: false),
                    Secao = table.Column<string>(maxLength: 2, nullable: false),
                    ProdutoPrincipal = table.Column<string>(maxLength: 14, nullable: true),
                    DescricaoProduto = table.Column<string>(maxLength: 30, nullable: false),
                    DescricaoEtiqueta1 = table.Column<string>(maxLength: 20, nullable: false),
                    DescricaoEtiqueta2 = table.Column<string>(maxLength: 20, nullable: true),
                    PrecoVista = table.Column<double>(nullable: true),
                    PrecoPrazo = table.Column<double>(nullable: true),
                    Loja = table.Column<string>(maxLength: 2, nullable: false),
                    PrecoCusto = table.Column<double>(nullable: true),
                    Quantidade = table.Column<int>(nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTRADA_ANTECIPADA", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_ENTRADA_ANTECIPADA_cad_for_Fornecedor",
                        column: x => x.Fornecedor,
                        principalTable: "cad_for",
                        principalColumn: "COD_FOR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ENTRADA_ANTECIPADA_cad_ite_Produto",
                        column: x => x.Produto,
                        principalTable: "cad_ite",
                        principalColumn: "COD_ITE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cad_can",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    DAT_LAN = table.Column<DateTime>(type: "date", nullable: false),
                    NOM_HIS = table.Column<string>(maxLength: 25, nullable: true),
                    PEN_CAN = table.Column<string>(maxLength: 1, nullable: true),
                    NUM_DOC = table.Column<string>(maxLength: 6, nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_can", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_can_cad_loc_COD_LOC",
                        column: x => x.COD_LOC,
                        principalTable: "cad_loc",
                        principalColumn: "COD_LOC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cad_che",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    DAT_LAN = table.Column<DateTime>(type: "date", nullable: true),
                    BAN_CHE = table.Column<string>(maxLength: 3, nullable: false),
                    AGE_CHE = table.Column<string>(maxLength: 3, nullable: false),
                    CON_CHE = table.Column<string>(maxLength: 12, nullable: false),
                    NUM_CHE = table.Column<string>(maxLength: 7, nullable: false),
                    VAL_CHE = table.Column<double>(nullable: true),
                    NUM_ETP = table.Column<string>(maxLength: 1, nullable: false),
                    DAT_VEN = table.Column<DateTime>(type: "date", nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_che", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_che_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cad_che_cad_loc_COD_LOC",
                        column: x => x.COD_LOC,
                        principalTable: "cad_loc",
                        principalColumn: "COD_LOC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loc_ite",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_ITE = table.Column<string>(maxLength: 14, nullable: false),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    EST_ATU = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loc_ite", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_loc_ite_cad_loc_COD_LOC",
                        column: x => x.COD_LOC,
                        principalTable: "cad_loc",
                        principalColumn: "COD_LOC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mov_cai",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_LOC = table.Column<string>(nullable: true),
                    DAT_LAN = table.Column<DateTime>(type: "date", nullable: false),
                    VAL_LAN = table.Column<double>(nullable: false),
                    NOM_HIS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mov_cai", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_mov_cai_cad_loc_COD_LOC",
                        column: x => x.COD_LOC,
                        principalTable: "cad_loc",
                        principalColumn: "COD_LOC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cad_ctl",
                columns: table => new
                {
                    NUM_VEN = table.Column<string>(maxLength: 6, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: true),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: true),
                    COD_VE1 = table.Column<string>(maxLength: 2, nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "date", nullable: true),
                    VAL_VEN = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_ACR = table.Column<double>(nullable: true),
                    NUM_DOC = table.Column<string>(nullable: true),
                    FLA_ENT = table.Column<string>(nullable: true),
                    FLG_CON = table.Column<string>(nullable: true),
                    FLG_FIN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_ctl", x => x.NUM_VEN);
                    table.UniqueConstraint("AK_cad_ctl_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_ctl_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cad_ctl_cad_ven_COD_VE1",
                        column: x => x.COD_VE1,
                        principalTable: "cad_ven",
                        principalColumn: "COD_VEN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cad_com",
                columns: table => new
                {
                    NUM_NOT = table.Column<string>(maxLength: 8, nullable: false),
                    COD_FOR = table.Column<string>(nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NUM_DUP = table.Column<string>(maxLength: 6, nullable: true),
                    CLA_FIS = table.Column<string>(maxLength: 3, nullable: true),
                    NUM_SER = table.Column<string>(maxLength: 3, nullable: true),
                    NAT_OPE = table.Column<string>(maxLength: 3, nullable: true),
                    DAT_COM = table.Column<DateTime>(type: "date", nullable: true),
                    DAT_ENT = table.Column<DateTime>(type: "date", nullable: true),
                    BAS_CAL = table.Column<double>(nullable: true),
                    VAL_ICM = table.Column<double>(nullable: true),
                    VAL_IPI = table.Column<double>(nullable: true),
                    VAL_TOT = table.Column<double>(nullable: true),
                    COD_LOC = table.Column<string>(nullable: false),
                    CON_PAG = table.Column<string>(maxLength: 20, nullable: true),
                    STA_COM = table.Column<string>(nullable: true),
                    CONDICAO_PAGAMENTO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_com", x => new { x.NUM_NOT, x.COD_FOR });
                    table.UniqueConstraint("AK_cad_com_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_com_CONDICAO_PAGAMENTO_CONDICAO_PAGAMENTO",
                        column: x => x.CONDICAO_PAGAMENTO,
                        principalTable: "CONDICAO_PAGAMENTO",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cad_com_cad_for_COD_FOR",
                        column: x => x.COD_FOR,
                        principalTable: "cad_for",
                        principalColumn: "COD_FOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cad_not",
                columns: table => new
                {
                    NUM_VEN = table.Column<string>(maxLength: 6, nullable: false),
                    COD_LOC = table.Column<string>(maxLength: 2, nullable: false),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_CLI = table.Column<string>(maxLength: 5, nullable: true),
                    DAT_VEN = table.Column<DateTime>(type: "date", nullable: true),
                    TIP_VEN = table.Column<string>(nullable: true),
                    NUM_PAR = table.Column<int>(nullable: true),
                    FLA_ENT = table.Column<string>(nullable: true),
                    VAL_VEN = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_ACR = table.Column<double>(nullable: true),
                    NUM_DOC = table.Column<string>(nullable: true),
                    COD_CAR = table.Column<long>(nullable: true),
                    VAL_ENT = table.Column<double>(nullable: true),
                    NOT_FAT = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_not", x => new { x.NUM_VEN, x.COD_LOC });
                    table.UniqueConstraint("AK_cad_not_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_cad_not_cad_cli_COD_CLI",
                        column: x => x.COD_CLI,
                        principalTable: "cad_cli",
                        principalColumn: "COD_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cad_not_cad_opr_COD_CAR",
                        column: x => x.COD_CAR,
                        principalTable: "cad_opr",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fil_rec",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NUM_DUP = table.Column<string>(nullable: true),
                    DAT_PAG = table.Column<DateTime>(type: "date", nullable: true),
                    VAL_PAG = table.Column<double>(nullable: true),
                    NUM_DOC = table.Column<string>(nullable: true),
                    COD_LOC = table.Column<string>(nullable: true),
                    VAL_JUR = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fil_rec", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_fil_rec_con_rec_NUM_DUP_COD_LOC",
                        columns: x => new { x.NUM_DUP, x.COD_LOC },
                        principalTable: "con_rec",
                        principalColumns: new[] { "NUM_DUP", "LOC_PAG" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ite_ctl",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    NUM_VEN = table.Column<string>(nullable: true),
                    COD_LOC = table.Column<string>(nullable: true),
                    COD_ITE = table.Column<string>(nullable: true),
                    VAL_UNI = table.Column<double>(nullable: true),
                    QUT_ITE = table.Column<int>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_ACR = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ite_ctl", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_ite_ctl_cad_ctl_NUM_VEN",
                        column: x => x.NUM_VEN,
                        principalTable: "cad_ctl",
                        principalColumn: "NUM_VEN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ite_com",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NUM_NOT = table.Column<string>(maxLength: 8, nullable: false),
                    COD_FOR = table.Column<string>(nullable: false),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_ITE = table.Column<string>(maxLength: 14, nullable: false),
                    VAL_UNI = table.Column<double>(nullable: false),
                    QUT_ITE = table.Column<int>(nullable: false),
                    COD_LOC = table.Column<string>(nullable: false),
                    DAT_ENT = table.Column<DateTime>(type: "date", nullable: false),
                    CLA = table.Column<string>(nullable: true),
                    ENT_ANT = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ite_com", x => new { x.NUM_NOT, x.COD_FOR, x.sql_rowid });
                    table.UniqueConstraint("AK_ite_com_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_ite_com_cad_ite_COD_ITE",
                        column: x => x.COD_ITE,
                        principalTable: "cad_ite",
                        principalColumn: "COD_ITE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ite_com_cad_com_NUM_NOT_COD_FOR",
                        columns: x => new { x.NUM_NOT, x.COD_FOR },
                        principalTable: "cad_com",
                        principalColumns: new[] { "NUM_NOT", "COD_FOR" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ite_not",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NUM_VEN = table.Column<string>(nullable: false),
                    COD_LOC = table.Column<string>(nullable: false),
                    sql_deleted = table.Column<string>(nullable: true),
                    COD_ITE = table.Column<string>(nullable: false),
                    VAL_UNI = table.Column<double>(nullable: true),
                    VAL_DES = table.Column<double>(nullable: true),
                    VAL_ACR = table.Column<double>(nullable: true),
                    QUT_ITE = table.Column<int>(nullable: true),
                    TRO_CA = table.Column<string>(nullable: true),
                    COD_VEN = table.Column<string>(nullable: true),
                    COD_SEC = table.Column<string>(nullable: true),
                    NUM_CTL = table.Column<string>(maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ite_not", x => new { x.NUM_VEN, x.COD_LOC, x.sql_rowid });
                    table.UniqueConstraint("AK_ite_not_sql_rowid", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_ite_not_cad_ite_COD_ITE",
                        column: x => x.COD_ITE,
                        principalTable: "cad_ite",
                        principalColumn: "COD_ITE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ite_not_cad_ven_COD_VEN",
                        column: x => x.COD_VEN,
                        principalTable: "cad_ven",
                        principalColumn: "COD_VEN",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ite_not_cad_not_NUM_VEN_COD_LOC",
                        columns: x => new { x.NUM_VEN, x.COD_LOC },
                        principalTable: "cad_not",
                        principalColumns: new[] { "NUM_VEN", "COD_LOC" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_can_COD_LOC",
                table: "cad_can",
                column: "COD_LOC");

            migrationBuilder.CreateIndex(
                name: "IX_cad_che_COD_CLI",
                table: "cad_che",
                column: "COD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_cad_che_COD_LOC",
                table: "cad_che",
                column: "COD_LOC");

            migrationBuilder.CreateIndex(
                name: "IX_cad_cli_COD_CLI",
                table: "cad_cli",
                column: "COD_CLI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_com_CONDICAO_PAGAMENTO",
                table: "cad_com",
                column: "CONDICAO_PAGAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_cad_com_COD_FOR",
                table: "cad_com",
                column: "COD_FOR");

            migrationBuilder.CreateIndex(
                name: "IX_cad_ctl_COD_CLI",
                table: "cad_ctl",
                column: "COD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_cad_ctl_COD_VE1",
                table: "cad_ctl",
                column: "COD_VE1");

            migrationBuilder.CreateIndex(
                name: "IX_cad_for_COD_FOR",
                table: "cad_for",
                column: "COD_FOR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_ite_COD_ITE",
                table: "cad_ite",
                column: "COD_ITE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_not_COD_CLI",
                table: "cad_not",
                column: "COD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_cad_not_COD_CAR",
                table: "cad_not",
                column: "COD_CAR");

            migrationBuilder.CreateIndex(
                name: "IX_cad_opr_COD_CLI",
                table: "cad_opr",
                column: "COD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_con_cor_COD_CON",
                table: "con_cor",
                column: "COD_CON");

            migrationBuilder.CreateIndex(
                name: "IX_con_cor_COD_FOR",
                table: "con_cor",
                column: "COD_FOR");

            migrationBuilder.CreateIndex(
                name: "IX_con_pag_COD_FOR",
                table: "con_pag",
                column: "COD_FOR");

            migrationBuilder.CreateIndex(
                name: "IX_con_rec_COD_CLI",
                table: "con_rec",
                column: "COD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_ENTRADA_ANTECIPADA_Fornecedor",
                table: "ENTRADA_ANTECIPADA",
                column: "Fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_ENTRADA_ANTECIPADA_Produto",
                table: "ENTRADA_ANTECIPADA",
                column: "Produto");

            migrationBuilder.CreateIndex(
                name: "IX_fil_rec_NUM_DUP_COD_LOC",
                table: "fil_rec",
                columns: new[] { "NUM_DUP", "COD_LOC" });

            migrationBuilder.CreateIndex(
                name: "IX_ite_com_COD_ITE",
                table: "ite_com",
                column: "COD_ITE");

            migrationBuilder.CreateIndex(
                name: "IX_ite_ctl_NUM_VEN",
                table: "ite_ctl",
                column: "NUM_VEN");

            migrationBuilder.CreateIndex(
                name: "IX_ite_not_COD_ITE",
                table: "ite_not",
                column: "COD_ITE");

            migrationBuilder.CreateIndex(
                name: "IX_ite_not_COD_VEN",
                table: "ite_not",
                column: "COD_VEN");

            migrationBuilder.CreateIndex(
                name: "IX_loc_ite_COD_LOC",
                table: "loc_ite",
                column: "COD_LOC");

            migrationBuilder.CreateIndex(
                name: "IX_mov_cai_COD_LOC",
                table: "mov_cai",
                column: "COD_LOC");

            migrationBuilder.CreateIndex(
                name: "IX_tmp_con_pag_COD_FOR",
                table: "tmp_con_pag",
                column: "COD_FOR");

            migrationBuilder.CreateIndex(
                name: "IX_tmp_con_rec_COD_CLI",
                table: "tmp_con_rec",
                column: "COD_CLI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "age_loj");

            migrationBuilder.DropTable(
                name: "arq_mov");

            migrationBuilder.DropTable(
                name: "cad_ace");

            migrationBuilder.DropTable(
                name: "cad_can");

            migrationBuilder.DropTable(
                name: "cad_che");

            migrationBuilder.DropTable(
                name: "cad_des");

            migrationBuilder.DropTable(
                name: "cad_fun");

            migrationBuilder.DropTable(
                name: "cad_his");

            migrationBuilder.DropTable(
                name: "cad_mar");

            migrationBuilder.DropTable(
                name: "cad_ord");

            migrationBuilder.DropTable(
                name: "cad_sec");

            migrationBuilder.DropTable(
                name: "cad_tab");

            migrationBuilder.DropTable(
                name: "cep_rua");

            migrationBuilder.DropTable(
                name: "con_cor");

            migrationBuilder.DropTable(
                name: "con_pag");

            migrationBuilder.DropTable(
                name: "ENTRADA_ANTECIPADA");

            migrationBuilder.DropTable(
                name: "fil_rec");

            migrationBuilder.DropTable(
                name: "ite_com");

            migrationBuilder.DropTable(
                name: "ite_ctl");

            migrationBuilder.DropTable(
                name: "ite_not");

            migrationBuilder.DropTable(
                name: "loc_ite");

            migrationBuilder.DropTable(
                name: "mov_cai");

            migrationBuilder.DropTable(
                name: "pag_doc");

            migrationBuilder.DropTable(
                name: "par_met");

            migrationBuilder.DropTable(
                name: "tmp_con_pag");

            migrationBuilder.DropTable(
                name: "tmp_con_rec");

            migrationBuilder.DropTable(
                name: "TMP_HISTORICO_CLIENTE");

            migrationBuilder.DropTable(
                name: "cad_con");

            migrationBuilder.DropTable(
                name: "con_rec");

            migrationBuilder.DropTable(
                name: "cad_com");

            migrationBuilder.DropTable(
                name: "cad_ctl");

            migrationBuilder.DropTable(
                name: "cad_ite");

            migrationBuilder.DropTable(
                name: "cad_not");

            migrationBuilder.DropTable(
                name: "cad_loc");

            migrationBuilder.DropTable(
                name: "CONDICAO_PAGAMENTO");

            migrationBuilder.DropTable(
                name: "cad_for");

            migrationBuilder.DropTable(
                name: "cad_ven");

            migrationBuilder.DropTable(
                name: "cad_opr");

            migrationBuilder.DropTable(
                name: "cad_cli");
        }
    }
}
