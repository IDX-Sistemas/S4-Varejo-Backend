CREATE TABLE cad_opr(
	sql_rowid bigint auto_increment primary key,
	sql_deleted enum('F','T') default 'F',
	COD_CLI varchar(5) not null,
	DES_OPR varchar(50) not null,
	TAX_OPR double,
	PAR_INI int,
	PAR_FIN int,
	FLG_STA varchar(1),
	TIP_VEN int
)