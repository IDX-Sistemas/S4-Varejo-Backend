CREATE TABLE tmp_con_rec (
	sql_rowid bigint auto_increment primary key,
	sql_deleted enum('F','T') default 'F',
	COD_CLI varchar(5) not null,
	LOC_PAG varchar(2) not null,
	NUM_DOC VARCHAR(6),
	COD_VEN VARCHAR(2),
	NUM_DUP VARCHAR(8),
	VAL_DUP double,
	NUM_FAT varchar(6),
	VAL_FAT double,
	VAL_JUR double,
	VAL_DES double,
	DAT_EMI date,
	DAT_VEN date,
	NUM__CI varchar(6),
	VAL_TOT double,
	FLA_ENT varchar(1)	
);