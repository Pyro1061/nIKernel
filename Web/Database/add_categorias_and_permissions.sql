-- Script: add_categorias_and_permissions.sql
-- Cria tabela de categorias, altera produtos e adiciona objeto/permissões

START TRANSACTION;

-- 1) Criar tabela de categorias se não existe
CREATE TABLE IF NOT EXISTS tb_ctg_categorias (
  CTG_ID INT(11) NOT NULL AUTO_INCREMENT,
  CTG_DCC VARCHAR(30) NOT NULL,
  CTG_STA CHAR(1) NOT NULL DEFAULT 'S',
  PRIMARY KEY (CTG_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 2) Adicionar coluna prd_ctg_id na tabela de produtos se não existe
ALTER TABLE tb_prd_produtos
  ADD COLUMN IF NOT EXISTS prd_ctg_id INT NULL;

-- 3) Inserir objeto 'Categorias' em tb_obj_objeto_sistema caso não exista
INSERT INTO tb_obj_objeto_sistema (OBJ_NAM, OBJ_DSC, OBJ_STA)
SELECT 'Categorias','/Categorias','A'
WHERE NOT EXISTS (SELECT 1 FROM tb_obj_objeto_sistema WHERE OBJ_NAM = 'Categorias');

-- 4) Recuperar OBJ_ID da entrada recém-criada (ou existente)
SET @objId = (SELECT OBJ_ID FROM tb_obj_objeto_sistema WHERE OBJ_NAM = 'Categorias' LIMIT 1);

-- 5) Limpar entradas existentes de permissão para este objeto (para evitar duplicatas)
DELETE FROM tb_obj_prf_objeto_perfil WHERE OBJ_ID = @objId;

-- 6) Inserir permissões padrão por perfil
-- As permissões seguem o padrão do dump:
-- OBJ_PRF_CNT (Consulta), OBJ_PRF_INP (Inserir), OBJ_PRF_UPT (Atualizar), OBJ_PRF_DEL (Excluir), OBJ_PRF_PRT (Imprimir)

-- Administrador(s) - acesso total (PRF_ID = 1,2 conforme base)
INSERT INTO tb_obj_prf_objeto_perfil (OBJ_ID, PRF_ID, OBJ_PRF_CNT, OBJ_PRF_INP, OBJ_PRF_UPT, OBJ_PRF_DEL, OBJ_PRF_PRT, OBJ_PRF_OBS)
VALUES
(@objId, 1, 'S','S','S','S','S', NULL),
(@objId, 2, 'S','S','S','S','S', NULL);

-- Estagiario / Perfil leitura (PRF_ID = 3) -> somente consulta
INSERT INTO tb_obj_prf_objeto_perfil (OBJ_ID, PRF_ID, OBJ_PRF_CNT, OBJ_PRF_INP, OBJ_PRF_UPT, OBJ_PRF_DEL, OBJ_PRF_PRT, OBJ_PRF_OBS)
VALUES
(@objId, 3, 'S','N','N','N','N', 'Perfil somente leitura');

-- Perfis específicos (consulta, insercao, atualizacao, exclusao, impressao)
INSERT INTO tb_obj_prf_objeto_perfil (OBJ_ID, PRF_ID, OBJ_PRF_CNT, OBJ_PRF_INP, OBJ_PRF_UPT, OBJ_PRF_DEL, OBJ_PRF_PRT, OBJ_PRF_OBS)
VALUES
(@objId, 4, 'S','N','N','N','N','Consulta'),
(@objId, 5, 'N','S','N','N','N','Insercao'),
(@objId, 6, 'N','N','S','N','N','Atualizacao'),
(@objId, 7, 'N','N','N','S','N','Exclusao'),
(@objId, 8, 'N','N','N','N','S','Impressao');

COMMIT;

-- Observações:
-- - Verifique os PRF_IDs existentes em tb_prf_perfil_acesso do seu banco antes de executar.
-- - Se a sua versão do MySQL não aceita "ADD COLUMN IF NOT EXISTS", remova o "IF NOT EXISTS" ou use um bloco condicional.
-- - Execute este script em ambiente de desenvolvimento/backup antes de aplicar em produção.
