CREATE TABLE par_met (
    sql_rowid bigint AUTO_INCREMENT PRIMARY KEY,
    sql_deleted enum('F','T') DEFAULT 'F',
    DES_PAR varchar(254) NOT NULL,
    VARIAVEL varchar(254) ,
    VAL_PAR varchar(254) 
);