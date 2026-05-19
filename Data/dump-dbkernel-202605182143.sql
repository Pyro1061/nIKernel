/*M!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19-11.7.2-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: dbkernel
-- ------------------------------------------------------
-- Server version	12.2.2-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;

--
-- Table structure for table `tb_cl_clientes`
--

DROP TABLE IF EXISTS `tb_cl_clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_cl_clientes` (
  `CL_id` int(11) NOT NULL AUTO_INCREMENT,
  `CL_cpf_cnpj` varchar(18) NOT NULL,
  `CL_rg_ie` varchar(20) DEFAULT NULL,
  `CL_nome` varchar(500) DEFAULT NULL,
  `CL_apelido` varchar(500) DEFAULT NULL,
  `CL_status` char(1) NOT NULL,
  `CL_data_inclusao` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`CL_id`),
  UNIQUE KEY `uk_cpf_cnpj` (`CL_cpf_cnpj`),
  UNIQUE KEY `uk_rg_ie` (`CL_rg_ie`),
  CONSTRAINT `chk_status` CHECK (`CL_status` in ('A','I','B'))
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_cl_clientes`
--

LOCK TABLES `tb_cl_clientes` WRITE;
/*!40000 ALTER TABLE `tb_cl_clientes` DISABLE KEYS */;
INSERT INTO `tb_cl_clientes` VALUES
(2,'10.234.567/0001-89','109283745','Logística Rápida S.A.','LogRapid','B','2026-04-28 22:30:23'),
(3,'789.456.123-10','45.678.901-2','Bar do Seu Toba','TOBA','A','2026-04-28 22:30:23'),
(5,'34563456','234234','w2345','werwe','A','2026-05-04 23:54:11'),
(7,'42136400860','34535346','Herdeiro','Júlio','A','2026-05-12 23:55:40');
/*!40000 ALTER TABLE `tb_cl_clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_log_sistema`
--

DROP TABLE IF EXISTS `tb_log_sistema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_log_sistema` (
  `LOG_ID` int(11) NOT NULL AUTO_INCREMENT,
  `UCN_ID` int(11) NOT NULL,
  `LOG_TYP` int(11) NOT NULL,
  `LOG_POS` varchar(200) NOT NULL,
  `LOG_DSC` varchar(500) NOT NULL,
  `LOG_DTA_INC` datetime(6) NOT NULL,
  PRIMARY KEY (`LOG_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_log_sistema`
--

LOCK TABLES `tb_log_sistema` WRITE;
/*!40000 ALTER TABLE `tb_log_sistema` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_log_sistema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_obj_objeto_sistema`
--

DROP TABLE IF EXISTS `tb_obj_objeto_sistema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_obj_objeto_sistema` (
  `OBJ_ID` int(11) NOT NULL AUTO_INCREMENT,
  `OBJ_NAM` varchar(200) NOT NULL,
  `OBJ_DSC` varchar(400) NOT NULL,
  `OBJ_STA` char(1) NOT NULL,
  PRIMARY KEY (`OBJ_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_obj_objeto_sistema`
--

LOCK TABLES `tb_obj_objeto_sistema` WRITE;
/*!40000 ALTER TABLE `tb_obj_objeto_sistema` DISABLE KEYS */;
INSERT INTO `tb_obj_objeto_sistema` VALUES
(1,'Usuarios','/Admin/Usuarios','A'),
(2,'Produtos','/Produtos','A'),
(3,'Perfis','/Admin/Perfis','A'),
(4,'Conectados','/Admin/Conectados','A'),
(5,'Clientes','/Clientes','A');
/*!40000 ALTER TABLE `tb_obj_objeto_sistema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_obj_prf_objeto_perfil`
--

DROP TABLE IF EXISTS `tb_obj_prf_objeto_perfil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_obj_prf_objeto_perfil` (
  `OBJ_ID` int(11) NOT NULL,
  `PRF_ID` int(11) NOT NULL,
  `OBJ_PRF_CNT` char(1) NOT NULL,
  `OBJ_PRF_INP` char(1) NOT NULL,
  `OBJ_PRF_UPT` char(1) NOT NULL,
  `OBJ_PRF_DEL` char(1) NOT NULL,
  `OBJ_PRF_PRT` char(1) NOT NULL,
  `OBJ_PRF_OBS` varchar(100) DEFAULT NULL,
  KEY `TB_OBJ_PRF_OBJETO_PERFIL_TB_OBJ_OBJETO_SISTEMA_FK` (`OBJ_ID`),
  KEY `TB_OBJ_PRF_OBJETO_PERFIL_TB_PRF_PERFIL_ACESSO_FK` (`PRF_ID`),
  CONSTRAINT `TB_OBJ_PRF_OBJETO_PERFIL_TB_OBJ_OBJETO_SISTEMA_FK` FOREIGN KEY (`OBJ_ID`) REFERENCES `tb_obj_objeto_sistema` (`OBJ_ID`),
  CONSTRAINT `TB_OBJ_PRF_OBJETO_PERFIL_TB_PRF_PERFIL_ACESSO_FK` FOREIGN KEY (`PRF_ID`) REFERENCES `tb_prf_perfil_acesso` (`PRF_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_obj_prf_objeto_perfil`
--

LOCK TABLES `tb_obj_prf_objeto_perfil` WRITE;
/*!40000 ALTER TABLE `tb_obj_prf_objeto_perfil` DISABLE KEYS */;
INSERT INTO `tb_obj_prf_objeto_perfil` VALUES
(1,1,'S','S','S','S','S',NULL),
(2,1,'S','S','S','S','S',NULL),
(3,1,'S','S','S','S','S',NULL),
(4,1,'S','S','S','S','S',NULL),
(1,3,'S','N','N','N','N','Perfil somente leitura'),
(2,3,'S','N','N','N','N','Perfil somente leitura'),
(3,3,'S','N','N','N','N','Perfil somente leitura'),
(4,3,'S','N','N','N','N','Perfil somente leitura'),
(5,3,'S','N','N','N','N','Perfil somente leitura'),
(5,1,'S','S','S','S','S',NULL),
(1,4,'S','N','N','N','N','Consulta'),
(2,4,'S','N','N','N','N','Consulta'),
(3,4,'S','N','N','N','N','Consulta'),
(4,4,'S','N','N','N','N','Consulta'),
(5,4,'S','N','N','N','N','Consulta'),
(1,5,'N','S','N','N','N','Insercao'),
(2,5,'N','S','N','N','N','Insercao'),
(3,5,'N','S','N','N','N','Insercao'),
(4,5,'N','S','N','N','N','Insercao'),
(5,5,'N','S','N','N','N','Insercao'),
(1,6,'N','N','S','N','N','Atualizacao'),
(2,6,'N','N','S','N','N','Atualizacao'),
(3,6,'N','N','S','N','N','Atualizacao'),
(4,6,'N','N','S','N','N','Atualizacao'),
(5,6,'N','N','S','N','N','Atualizacao'),
(1,7,'N','N','N','S','N','Exclusao'),
(2,7,'N','N','N','S','N','Exclusao'),
(3,7,'N','N','N','S','N','Exclusao'),
(4,7,'N','N','N','S','N','Exclusao'),
(5,7,'N','N','N','S','N','Exclusao'),
(1,8,'N','N','N','N','S','Impressao'),
(2,8,'N','N','N','N','S','Impressao'),
(3,8,'N','N','N','N','S','Impressao'),
(4,8,'N','N','N','N','S','Impressao'),
(5,8,'N','N','N','N','S','Impressao');
/*!40000 ALTER TABLE `tb_obj_prf_objeto_perfil` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_prd_produtos`
--

DROP TABLE IF EXISTS `tb_prd_produtos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_prd_produtos` (
  `prd_id` int(11) DEFAULT NULL,
  `prd_cod` varchar(50) DEFAULT NULL,
  `prd_gtin_ean` varchar(14) DEFAULT NULL,
  `prd_descricao` varchar(255) DEFAULT NULL,
  `prd_un_compra` varchar(10) DEFAULT NULL,
  `prd_un_venda` varchar(10) DEFAULT NULL,
  `prd_preco_compra` decimal(18,4) DEFAULT NULL,
  `prd_margem_venda` decimal(10,2) DEFAULT NULL,
  `prd_preco_venda` decimal(18,4) DEFAULT NULL,
  `prd_ativo` char(1) DEFAULT NULL,
  `prd_data_criacao` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_prd_produtos`
--

LOCK TABLES `tb_prd_produtos` WRITE;
/*!40000 ALTER TABLE `tb_prd_produtos` DISABLE KEYS */;
INSERT INTO `tb_prd_produtos` VALUES
(1,'PRD001','7891234567890','Café Torrado e Moído 500g','UN','UN',12.5000,25.00,15.6250,'S','2026-05-08 00:56:12'),
(2,'PRD002','7899876543210','Leite Integral 1L','CX','UN',4.2500,35.00,5.7400,'S','2026-05-08 00:56:12');
/*!40000 ALTER TABLE `tb_prd_produtos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_prf_perfil_acesso`
--

DROP TABLE IF EXISTS `tb_prf_perfil_acesso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_prf_perfil_acesso` (
  `PRF_ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRF_DSC` varchar(60) NOT NULL,
  `PRF_STA` char(1) NOT NULL,
  PRIMARY KEY (`PRF_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_prf_perfil_acesso`
--

LOCK TABLES `tb_prf_perfil_acesso` WRITE;
/*!40000 ALTER TABLE `tb_prf_perfil_acesso` DISABLE KEYS */;
INSERT INTO `tb_prf_perfil_acesso` VALUES
(1,'Administrador','A'),
(2,'Administrador','A'),
(3,'Estagiario','A'),
(4,'Perfil Consulta','A'),
(5,'Perfil Insercao','I'),
(6,'Perfil Atualizacao','A'),
(7,'Perfil Exclusao','A'),
(8,'Perfil Impressao','A');
/*!40000 ALTER TABLE `tb_prf_perfil_acesso` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_ucn_usuarios_conectados`
--

DROP TABLE IF EXISTS `tb_ucn_usuarios_conectados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_ucn_usuarios_conectados` (
  `UCN_ID` int(11) NOT NULL AUTO_INCREMENT,
  `USU_ID` int(11) NOT NULL,
  `UCN_DTA_INC` datetime(6) NOT NULL,
  `UCN_AGT` varchar(500) NOT NULL,
  `UCN_DAT_OFF` datetime(6) DEFAULT NULL,
  `UCN_SESSION_ID` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`UCN_ID`),
  KEY `IDX_UCN_USUARIO` (`USU_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_ucn_usuarios_conectados`
--

LOCK TABLES `tb_ucn_usuarios_conectados` WRITE;
/*!40000 ALTER TABLE `tb_ucn_usuarios_conectados` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_ucn_usuarios_conectados` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_usu_usuarios`
--

DROP TABLE IF EXISTS `tb_usu_usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_usu_usuarios` (
  `USU_ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRF_ID` int(11) NOT NULL,
  `USU_LOG` varchar(20) NOT NULL,
  `USU_PWD` varchar(255) NOT NULL,
  `USU_NAM` varchar(80) NOT NULL,
  `USU_DTA_INC` date NOT NULL,
  `USU_DTA_FIN` date DEFAULT NULL,
  `USU_STA` char(1) NOT NULL,
  `USU_CNT` char(1) DEFAULT NULL,
  `USU_EMAIL` varchar(100) DEFAULT NULL,
  `USU_CEL` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`USU_ID`),
  KEY `FK_TB_USU_USUARIOS_PRF` (`PRF_ID`),
  CONSTRAINT `FK_TB_USU_USUARIOS_PRF` FOREIGN KEY (`PRF_ID`) REFERENCES `tb_prf_perfil_acesso` (`PRF_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_usu_usuarios`
--

LOCK TABLES `tb_usu_usuarios` WRITE;
/*!40000 ALTER TABLE `tb_usu_usuarios` DISABLE KEYS */;
INSERT INTO `tb_usu_usuarios` VALUES
(1,1,'admin','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4','Administrador','2026-04-16',NULL,'A','N',NULL,NULL),
(4,3,'estagiario','fe2592b42a727e977f055947385b709cc82b16b9a87f88c6abf3900d65d0cdc3','estagiario','2026-05-11',NULL,'A','N','estagiario@empresa.com','(11)99999-9999'),
(5,4,'usuario_consulta','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','Usuario Consulta','2026-05-12',NULL,'A',NULL,NULL,NULL),
(6,5,'usuario_insercao','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','Usuario Insercao','2026-05-12',NULL,'A',NULL,NULL,NULL),
(7,6,'usuario_atualizacao','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','Usuario Atualizacao','2026-05-12',NULL,'A',NULL,NULL,NULL),
(8,7,'usuario_exclusao','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','Usuario Exclusao','2026-05-12',NULL,'A',NULL,NULL,NULL),
(9,8,'usuario_impressao','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','Usuario Impressao','2026-05-12',NULL,'A',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tb_usu_usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'dbkernel'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;

-- Dump completed on 2026-05-18 21:43:59
