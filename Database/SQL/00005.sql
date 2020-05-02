CREATE TABLE CONDICAO_PAGAMENTO(
	sql_rowid BIGINT AUTO_INCREMENT PRIMARY KEY,
	sql_deleted enum('F','T') DEFAULT 'F',
	Descricao varchar(100) NOT NULL,
	Intervalo int,
	Parcelas int,
	ComEntrada varchar(1) NOT NULL
);

ALTER TABLE CONDICAO_PAGAMENTO ADD COLUMN Codigo VARCHAR(4) NOT NULL;