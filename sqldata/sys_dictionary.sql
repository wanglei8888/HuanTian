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

 Date: 26/05/2023 23:00:39
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_dictionary
-- ----------------------------
DROP TABLE IF EXISTS `sys_dictionary`;
CREATE TABLE `sys_dictionary`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '系统字典表',
  `value` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典值',
  `name` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典名字',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4561312346545 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统字典表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_dictionary
-- ----------------------------
INSERT INTO `sys_dictionary` VALUES (4561312346541, 'SysMenuType', 'System', '系统菜单', 0, NULL, NULL);
INSERT INTO `sys_dictionary` VALUES (4561312346542, 'SysMenuType', 'Business', '业务菜单', 0, NULL, NULL);
INSERT INTO `sys_dictionary` VALUES (4561312346543, 'SysMenuChild', 'Child', '子菜单', 0, NULL, NULL);
INSERT INTO `sys_dictionary` VALUES (4561312346544, 'SysMenuChild', 'Parent', '父菜单', 0, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
