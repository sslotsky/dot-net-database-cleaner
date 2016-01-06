
DECLARE @fkList TABLE (
	PKTABLE_QUALIFIER sysname collate database_default NULL,
	PKTABLE_OWNER sysname collate database_default NULL,
	PKTABLE_NAME sysname collate database_default NOT NULL,
	PKCOLUMN_NAME sysname collate database_default NOT NULL,
	FKTABLE_QUALIFIER sysname collate database_default NULL,
	FKTABLE_OWNER sysname collate database_default NULL,
	FKTABLE_NAME sysname collate database_default NOT NULL,
	FKCOLUMN_NAME sysname collate database_default NOT NULL,
	KEY_SEQ smallint NOT NULL,
	UPDATE_RULE smallint NULL,
	DELETE_RULE smallint NULL,
	FK_NAME sysname collate database_default NULL,
	PK_NAME sysname collate database_default NULL,
	DEFERRABILITY smallint null
)

insert into @fkList EXEC sp_fkeys '{0}'
select distinct FKTABLE_NAME as TableName from @fkList