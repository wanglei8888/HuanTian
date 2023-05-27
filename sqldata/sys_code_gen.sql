/*
 Navicat Premium Data Transfer

 Source Server         : 175.178.42.232
 Source Server Type    : MySQL
 Source Server Version : 50742 (5.7.42)
 Source Host           : 175.178.42.232:3306
 Source Schema         : store_db

 Target Server Type    : MySQL
 Target Server Version : 50742 (5.7.42)
 File Encoding         : 65001

 Date: 27/05/2023 15:06:22
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_code_gen
-- ----------------------------
DROP TABLE IF EXISTS `sys_code_gen`;
CREATE TABLE `sys_code_gen`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `table_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '表格名字',
  `data_type` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列类型',
  `db_column_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列名字',
  `column_description` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列备注',
  `drop_down_list` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '下拉框绑定字典值 如果没有就是不是下拉框',
  `query_parameters` tinyint(1) NOT NULL COMMENT '是否是查询参数',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 417244902111098 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_code_gen
-- ----------------------------
INSERT INTO `sys_code_gen` VALUES (417244902111091, 'sys_role', 'bigint', 'id', 'Id', '', 1, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111092, 'sys_role', 'varchar', 'role_name', '角色名字', '', 1, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111093, 'sys_role', 'varchar', 'describe', '角色描述', '', 0, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111094, 'sys_role', 'int', 'status', '角色状态', '', 0, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111095, 'sys_role', 'tinyint', 'deleted', '软删除', '', 0, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111096, 'sys_role', 'bigint', 'create_by', '创建人', '', 0, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902111097, 'sys_role', 'datetime', 'create_on', '创建时间', '', 0, 0, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
