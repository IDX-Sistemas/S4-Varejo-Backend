CREATE TABLE ENTRADA_ANTECIPADA(
	sql_rowid bigint auto_increment primary key,
	sql_deleted enum('F','T') default 'F',
	NotaFiscal varchar(8) not null,
	Fornecedor varchar(4) not null,
	ClassificacaoFiscal varchar(1),
	Produto varchar(14) not null,
	Secao varchar(2) not null,
	ProdutoPrincipal varchar(14),
	DescricaoProduto varchar(30),
	DescricaoEtiqueta1 varchar(30) not null,
	DescricaoEtiqueta2 varchar(30),
	PrecoVista double,
	PrecoPrazo double,
	Loja varchar(2) not null,
	PrecoCusto double,
	Quantidade int,
	DataEntrada date
);