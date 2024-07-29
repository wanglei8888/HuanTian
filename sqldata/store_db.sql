/*
 Navicat Premium Data Transfer

 Source Server         : myDB
 Source Server Type    : MySQL
 Source Server Version : 50742 (5.7.42)
 Source Host           : 
 Source Schema         : store_db

 Target Server Type    : MySQL
 Target Server Version : 50742 (5.7.42)
 File Encoding         : 65001

 Date: 17/04/2024 09:09:15
*/

-- CREATE DATABASE `store_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20230807065205_init_talbe', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230808083712_update_emailTemplate', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230811090029_add_systenant', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230814135811_update_business', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230825094418_add_syslog', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230828022415_add_log_table', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230828085804_add_log_table2', '7.0.5');
INSERT INTO `__EFMigrationsHistory` VALUES ('20230914025342_test_product', '7.0.5');

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '商品名称',
  `price` decimal(65, 30) NOT NULL COMMENT '商品价格',
  `inventory` int(11) NOT NULL COMMENT '商品库存',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  `tenant_id` bigint(20) NOT NULL COMMENT '租户ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 123124 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '代码生成数据详情表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES (123123, 'iphone14', 6999.000000000000000000000000000000, 3, 0, NULL, NULL, NULL, NULL, 449900948840517);

-- ----------------------------
-- Table structure for sys_apps
-- ----------------------------
DROP TABLE IF EXISTS `sys_apps`;
CREATE TABLE `sys_apps`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列名字',
  `code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `order` int(11) NOT NULL COMMENT '排序',
  `enable` tinyint(1) NOT NULL COMMENT '启用',
  `describe` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  `tenant_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '租户ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 452293334528070 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统应用表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_apps
-- ----------------------------
INSERT INTO `sys_apps` VALUES (439691352649886, '系统应用', 'System', 5, 1, '展示系统设置菜单', 0, NULL, NULL, NULL, NULL, 449900948840517);
INSERT INTO `sys_apps` VALUES (439691352649887, '业务应用', 'Business', 1, 1, '展示业务菜单', 0, NULL, NULL, NULL, NULL, 449900948840517);
INSERT INTO `sys_apps` VALUES (439691352649888, '系统应用', 'System', 5, 1, '展示系统设置菜单', 0, NULL, NULL, NULL, NULL, 450236590075973);
INSERT INTO `sys_apps` VALUES (439691352649889, '业务应用', 'Business', 1, 1, '展示业务菜单', 0, NULL, NULL, NULL, NULL, 450236590075973);

-- ----------------------------
-- Table structure for sys_code_gen
-- ----------------------------
DROP TABLE IF EXISTS `sys_code_gen`;
CREATE TABLE `sys_code_gen`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `table_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '表格名字',
  `menu_id` bigint(20) NOT NULL COMMENT '所属菜单',
  `generation_way` int(11) NOT NULL COMMENT '生成方式',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 516743354974278 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '代码生成数据表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_code_gen
-- ----------------------------

-- ----------------------------
-- Table structure for sys_code_gen_detail
-- ----------------------------
DROP TABLE IF EXISTS `sys_code_gen_detail`;
CREATE TABLE `sys_code_gen_detail`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `master_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '主表ID',
  `data_type` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列类型',
  `db_column_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列名字',
  `column_description` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '列备注',
  `drop_down_code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `query_parameters` tinyint(1) NOT NULL COMMENT '是否是查询参数',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `query_type` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '查询方式',
  `required` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否必填',
  `frontend_type` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前端显示类型',
  `order` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 458637251932231 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '代码生成数据详情表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_code_gen_detail
-- ----------------------------

-- ----------------------------
-- Table structure for sys_dept
-- ----------------------------
DROP TABLE IF EXISTS `sys_dept`;
CREATE TABLE `sys_dept`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `parent_id` bigint(20) NOT NULL COMMENT '父级部门id',
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门名字',
  `describe` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门描述',
  `enable` tinyint(1) NOT NULL COMMENT '部门启用',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `tenant_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '租户ID',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 452703954657350 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统部门表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_dept
-- ----------------------------
INSERT INTO `sys_dept` VALUES (417244902119461, 0, '财务部门', '财务部门管理公司财务', 1, 0, NULL, NULL, 450236590075973, NULL, NULL);
INSERT INTO `sys_dept` VALUES (417244902119462, 0, '人事部门', '财务部门管理所有公司人事信息', 1, 0, NULL, NULL, 449900948840517, NULL, NULL);
INSERT INTO `sys_dept` VALUES (417244902119463, 0, 'IT部门', '财务部门管理公司IT有关事宜', 1, 0, NULL, NULL, 449900948840517, NULL, NULL);
INSERT INTO `sys_dept` VALUES (437221302702149, 417244902119463, '设计部', '负责IT设计功能', 1, 0, 417244902117446, '2023-07-09 21:18:04.125393', 449900948840517, NULL, NULL);
INSERT INTO `sys_dept` VALUES (450244612444229, 0, '公关部门', '管理公司所有公关业务', 1, 0, 417244902117456, '2023-08-15 16:30:03.105822', 450236590075973, NULL, NULL);
INSERT INTO `sys_dept` VALUES (452703954657349, 417244902119462, 'sets1', '123123', 1, 0, 417244902117446, '2023-08-22 15:17:08.450000', 449900948840517, NULL, NULL);

-- ----------------------------
-- Table structure for sys_dic
-- ----------------------------
DROP TABLE IF EXISTS `sys_dic`;
CREATE TABLE `sys_dic`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典名字',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `enable` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否启用',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '系统字典表',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 435082462924870 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统字典表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_dic
-- ----------------------------
INSERT INTO `sys_dic` VALUES (4561312346542, '菜单层级', 0, 1, 'MenuLevel');
INSERT INTO `sys_dic` VALUES (4561312346543, '多语言值', 0, 1, 'languageType');
INSERT INTO `sys_dic` VALUES (435033088540741, '代码生成-前端类型', 0, 1, 'codeGenFrontendType');
INSERT INTO `sys_dic` VALUES (435082462924869, '代码生成-查询类型', 0, 1, 'codeGenQueryType');

-- ----------------------------
-- Table structure for sys_dic_detail
-- ----------------------------
DROP TABLE IF EXISTS `sys_dic_detail`;
CREATE TABLE `sys_dic_detail`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `master_id` bigint(20) NOT NULL COMMENT '主表Id',
  `value` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典值',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典名字',
  `order` int(11) NOT NULL COMMENT '排序',
  `enable` tinyint(1) NOT NULL COMMENT '是否启用',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 435104934633546 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统字典详情表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_dic_detail
-- ----------------------------
INSERT INTO `sys_dic_detail` VALUES (4561312346943, 4561312346542, 'Child', '子菜单', 10, 0, 0, NULL, NULL);
INSERT INTO `sys_dic_detail` VALUES (4561312346944, 4561312346542, 'Parent', '父菜单', 10, 0, 0, NULL, NULL);
INSERT INTO `sys_dic_detail` VALUES (4561312346945, 4561312346543, '0', '中文', 10, 0, 0, NULL, NULL);
INSERT INTO `sys_dic_detail` VALUES (4561312346946, 4561312346543, '1', '英语', 10, 0, 0, NULL, NULL);
INSERT INTO `sys_dic_detail` VALUES (435082928492613, 435082462924869, 'equal', '等于', 1, 1, 0, 417244902117446, '2023-07-03 20:17:00.109000');
INSERT INTO `sys_dic_detail` VALUES (435082928492614, 435082462924869, 'like', '模糊查询', 2, 1, 0, 417244902117446, '2023-07-03 20:17:00.109000');
INSERT INTO `sys_dic_detail` VALUES (435104934629445, 435033088540741, 'dropDown', '下拉框', 1, 1, 0, 417244902117446, '2023-07-03 21:46:32.701000');
INSERT INTO `sys_dic_detail` VALUES (435104934633541, 435033088540741, 'textBox', '文本框', 2, 1, 0, 417244902117446, '2023-07-03 21:46:32.702000');
INSERT INTO `sys_dic_detail` VALUES (435104934633542, 435033088540741, 'radio', '单选框(true/false)', 3, 1, 0, 417244902117446, '2023-07-03 21:46:32.702000');
INSERT INTO `sys_dic_detail` VALUES (435104934633544, 435033088540741, 'numBox', '数字输入框', 5, 1, 0, 417244902117446, '2023-07-03 21:46:32.702000');
INSERT INTO `sys_dic_detail` VALUES (435104934633545, 435033088540741, 'datetime', '时间选择框', 6, 1, 0, 417244902117446, '2023-07-03 21:46:32.702000');

-- ----------------------------
-- Table structure for sys_email_template
-- ----------------------------
DROP TABLE IF EXISTS `sys_email_template`;
CREATE TABLE `sys_email_template`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  `tenant_id` bigint(20) NOT NULL COMMENT '租户ID',
  `enable` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否启用',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 450924337745990 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统邮箱模板表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_email_template
-- ----------------------------
INSERT INTO `sys_email_template` VALUES (447766716219461, '客户回答模板', 0, 417244902117446, '2023-08-08 16:27:27.972000', 417244902117446, '2023-08-08 16:50:55.103000', 449900948840517, 1);
INSERT INTO `sys_email_template` VALUES (450924337745989, '用户信息模板', 0, 417244902117446, '2023-08-17 14:35:51.665000', 417244902117446, '2023-08-17 15:11:29.255000', 449900948840517, 1);

-- ----------------------------
-- Table structure for sys_log_error
-- ----------------------------
DROP TABLE IF EXISTS `sys_log_error`;
CREATE TABLE `sys_log_error`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tenant_id` bigint(20) NOT NULL COMMENT '租户ID',
  `level` int(11) NOT NULL COMMENT '日志等级 trace0、Debug1、Information2、Warning3、Error4、Critical5、None6',
  `msg` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志信息',
  `path` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '错误地址',
  `create_on` datetime(6) NOT NULL COMMENT '日志时间',
  `user_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '用户ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 429 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '错误日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_log_error
-- ----------------------------

-- ----------------------------
-- Table structure for sys_log_info
-- ----------------------------
DROP TABLE IF EXISTS `sys_log_info`;
CREATE TABLE `sys_log_info`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tenant_id` bigint(20) NOT NULL COMMENT '租户ID',
  `level` int(11) NOT NULL COMMENT '日志等级 trace0、Debug1、Information2、Warning3、Error4、Critical5、None6',
  `msg` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志信息',
  `create_on` datetime(6) NOT NULL COMMENT '日志时间',
  `user_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '用户ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 831 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '普通日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_log_info
-- ----------------------------

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `parent_id` bigint(20) NOT NULL COMMENT '菜单父级ID',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单名字',
  `path` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单地址',
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单标题',
  `keep_alive` tinyint(1) NULL DEFAULT NULL COMMENT '是否缓存',
  `icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `show` tinyint(1) NOT NULL COMMENT '是否显示菜单',
  `redirect` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单跳转地址',
  `hide_children` tinyint(1) NULL DEFAULT NULL COMMENT '隐藏子类',
  `component` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单前端绑定值',
  `menu_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单类型',
  `order` int(11) NULL DEFAULT NULL COMMENT '排序,越大越靠前',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `rout_path` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '路由标识',
  `tenant_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '租户ID',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 516751159025734 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES (417244902117886, 0, 'dashboard', NULL, 'menu.dashboard', 1, 'dashboard', 1, '/dashboard/workplace', 0, 'RouteView', 'Business', 1000, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117887, 417244902117886, 'Analysis', '/dashboard/analysis', 'menu.dashboard.analysis', 1, 'none', 1, '', 0, 'Analysis', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117889, 417244902117886, 'monitor', 'https://www.baidu.com/', 'menu.dashboard.workplace', 1, 'none', 1, '', 0, '', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117890, 417244902117894, 'advanced-form', NULL, 'menu.form.advanced-form', 1, 'none', 1, '', 0, 'AdvanceForm', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117891, 417244902117894, 'step-form', NULL, 'menu.form.step-form', 1, 'none', 1, '', 0, 'StepForm', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117892, 417244902117894, 'basic-form', NULL, 'menu.form.basic-form', 1, 'none', 1, '', 0, 'BasicForm', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117893, 417244902117886, 'workplace', NULL, 'menu.dashboard.monitor', 1, 'none', 1, '', 0, 'Workplace', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117894, 0, 'form', NULL, 'menu.form', 1, 'form', 1, '/form/base-form', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117895, 417244902117896, 'settings', NULL, 'menu.account.settings', 1, 'none', 1, '/account/settings/basic', 0, 'AccountSettings', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117896, 0, 'account', NULL, 'menu.account', 1, 'user', 1, '/account/center', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117897, 0, 'exception', NULL, 'menu.exception', 1, 'warning', 1, '/exception/403', 0, 'PageView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117898, 0, '系统管理', '/system', 'menu.system', 1, 'tool', 1, '/system/menuList', 0, 'RouteView', 'System', 900, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117899, 0, 'result', NULL, 'menu.result', 1, 'check-circle-o', 1, '/result/success', 0, 'PageView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117901, 417244902117902, 'search', NULL, 'menu.list.search-list', 1, 'none', 1, '/list/search/article', 0, 'SearchLayout', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117902, 0, 'list', NULL, 'menu.list', 1, 'table', 1, '/list/table-list', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117903, 0, 'profile', NULL, 'menu.profile', 1, 'profile', 1, '/profile/basic', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117907, 417244902117898, '角色列表', '/system/role', 'menu.system.roleList', 1, 'none', 1, '', 0, 'system/role/roleList', 'System', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117908, 417244902117898, '权限列表', '/system/perm', 'menu.system.permissionList', 1, 'none', 1, '', 0, 'system/perm/permList', 'System', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117909, 417244902117898, '用户列表', '/system/user', 'menu.system.userList', 1, 'none', 1, '', 0, 'system/user/userList', 'System', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117910, 417244902117904, 'edit-table', NULL, '内联编辑表格', 1, 'none', 1, '', 0, 'TableInnerEditList', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117911, 417244902117902, 'table-list', '/list/table-list/:pageNo([1-9]\\\\d*)?', 'menu.list.table-list', 1, 'none', 1, '', 0, 'TableList', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117912, 417244902117902, 'basic-list', NULL, 'menu.list.basic-list', 1, 'none', 1, '', 0, 'StandardList', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117913, 417244902117902, 'card', NULL, 'menu.list.card-list', 1, 'none', 1, '', 0, 'CardList', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117914, 417244902117901, 'article', NULL, 'menu.list.search-list.articles', 1, 'none', 1, '', 0, 'SearchArticles', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117915, 417244902117901, 'project', NULL, 'menu.list.search-list.projects', 1, 'none', 1, '', 0, 'SearchProjects', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117916, 417244902117901, 'application', NULL, 'menu.list.search-list.applications', 1, 'none', 1, '', 0, 'SearchApplications', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117917, 417244902117903, 'basic', NULL, 'menu.profile.basic', 1, 'none', 1, '', 0, 'ProfileBasic', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117918, 417244902117903, 'advanced', NULL, 'menu.profile.advanced', 1, 'none', 1, '', 0, 'ProfileAdvanced', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117919, 417244902117899, 'success', NULL, 'menu.result.success', 1, 'none', 1, '', 0, 'ResultSuccess', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117920, 417244902117899, 'fail', NULL, 'menu.result.fail', 1, 'none', 1, '', 0, 'ResultFail', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117921, 417244902117897, '403', NULL, 'menu.exception.not-permission', 1, 'none', 1, '', 0, 'Exception403', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117922, 417244902117897, '404', NULL, 'menu.exception.not-find', 1, 'none', 1, '', 0, 'Exception404', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117923, 417244902117896, 'center', NULL, 'menu.account.center', 1, 'none', 1, '', 0, 'AccountCenter', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117924, 417244902117895, 'BasicSettings', '/account/settings/basic', 'account.settings.menuMap.basic', 1, 'none', 1, '', 0, 'BasicSetting', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117925, 417244902117895, 'SecuritySettings', '/account/settings/security', 'account.settings.menuMap.security', 1, 'none', 1, '', 0, 'SecuritySettings', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117926, 417244902117895, 'CustomSettings', '/account/settings/custom', 'account.settings.menuMap.custom', 1, 'none', 1, '', 0, 'CustomSettings', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117927, 417244902117895, 'BindingSettings', '/account/settings/binding', 'account.settings.menuMap.binding', 1, 'none', 1, '', 0, 'BindingSettings', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117928, 417244902117895, 'NotificationSettings', '/account/settings/notification', 'account.settings.menuMap.notification', 1, 'none', 1, '', 0, 'NotificationSettings', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117967, 417244902117897, 'error', NULL, 'menu.exception.server-error', 1, 'none', 1, '', 0, 'Exception500', 'Business', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117999, 417244902117898, '菜单列表', '/system/menu', 'menu.system.menuList', 1, 'none', 1, '', 0, 'system/menu/menuList', 'System', 50, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902194761, 448839443734598, '字典列表', '/system/dic', 'menu.system.dicList', 1, 'none', 1, '', 0, 'system/dic/dicList', 'System', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902194762, 0, '代码生成', '/system/codeGen', 'menu.system.codeGen', 1, 'desktop', 1, '', 1, 'system/codeGen/codeGen', 'System', 0, 0, NULL, NULL, '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (435823827857477, 417244902117898, '部门信息', '/system/dept', 'menu.system.dept', 1, 'none', 1, NULL, 0, 'system/dept/deptList', 'System', 100, 0, 417244902117446, '2023-07-05 22:31:43.743230', '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (444148029263941, 448839443734598, '应用列表', '/system/apps', 'menu.system.apps', 1, 'none', 1, NULL, 0, 'system/apps/appsList', 'System', 100, 0, 417244902117446, '2023-07-29 11:02:59.478041', '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (447421929099333, 448839443734598, '邮件模板', '/system/emailTemplate', 'menu.system.emailTemplate', 1, 'none', 1, NULL, 0, 'system/emailTemplate/emailTemplateList', 'System', 100, 0, 417244902117446, '2023-08-07 17:04:31.429647', '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443734597, 0, '租户列表', '/system/tenant', 'menu.system.tenant', 1, 'cluster', 1, NULL, 1, 'system/tenant/tenantList', 'System', 100, 0, 417244902117446, '2023-08-11 17:12:24.338422', '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443734598, 0, '项目配置', '/setting', 'menu.setting', 0, 'setting', 1, '/system/emailTemplate', 0, 'RouteView', 'System', 200, 0, 417244902117446, '2023-08-14 16:02:20.899362', '', 449900948840517, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789157, 0, 'dashboard', NULL, 'menu.dashboard', 1, 'dashboard', 1, '/dashboard/workplace', 0, 'RouteView', 'Business', 1000, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789158, 448839443789157, 'Analysis', '/dashboard/analysis', 'menu.dashboard.analysis', 1, 'none', 1, NULL, 0, 'Analysis', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789159, 448839443789157, 'monitor', 'https://www.baidu.com/', 'menu.dashboard.workplace', 1, 'none', 1, NULL, 0, NULL, 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789160, 448839443789164, 'advanced-form', NULL, 'menu.form.advanced-form', 1, 'none', 1, NULL, 0, 'AdvanceForm', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789161, 448839443789164, 'step-form', NULL, 'menu.form.step-form', 1, 'none', 1, NULL, 0, 'StepForm', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789162, 448839443789164, 'basic-form', NULL, 'menu.form.basic-form', 1, 'none', 1, NULL, 0, 'BasicForm', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789163, 448839443789157, 'workplace', NULL, 'menu.dashboard.monitor', 1, 'none', 1, NULL, 0, 'Workplace', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789164, 0, 'form', NULL, 'menu.form', 1, 'form', 1, '/form/base-form', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789165, 448839443789166, 'settings', NULL, 'menu.account.settings', 1, 'none', 1, '/account/settings/basic', 0, 'AccountSettings', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789166, 0, 'account', NULL, 'menu.account', 1, 'user', 1, '/account/center', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789167, 0, 'exception', NULL, 'menu.exception', 1, 'warning', 1, '/exception/403', 0, 'PageView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789168, 0, '系统管理', '/system', 'menu.system', 1, 'tool', 1, '/system/menuList', 0, 'RouteView', 'System', 900, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789169, 0, 'result', NULL, 'menu.result', 1, 'check-circle-o', 1, '/result/success', 0, 'PageView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789170, 448839443789171, 'search', NULL, 'menu.list.search-list', 1, 'none', 1, '/list/search/article', 0, 'SearchLayout', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789171, 0, 'list', NULL, 'menu.list', 1, 'table', 1, '/list/table-list', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789172, 0, 'profile', NULL, 'menu.profile', 1, 'profile', 1, '/profile/basic', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789173, 448839443789168, '角色列表', '/system/role', 'menu.system.roleList', 1, 'none', 1, NULL, 0, 'system/role/roleList', 'System', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789174, 448839443789168, '权限列表', '/system/perm', 'menu.system.permissionList', 1, 'none', 1, NULL, 0, 'system/perm/permList', 'System', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789175, 448839443789168, '用户列表', '/system/user', 'menu.system.userList', 1, 'none', 1, NULL, 0, 'system/user/userList', 'System', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789196, 448839443789168, '菜单列表', '/system/menu', 'menu.system.menuList', 1, 'none', 1, NULL, 0, 'system/menu/menuList', 'System', 50, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789197, 448839443789203, '字典列表', '/system/dic', 'menu.system.dicList', 1, 'none', 1, NULL, 0, 'system/dic/dicList', 'System', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789198, 0, '代码生成', '/system/codeGen', 'menu.system.codeGen', 1, 'desktop', 1, NULL, 1, 'system/codeGen/codeGen', 'System', 0, 0, NULL, NULL, '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789199, 448839443789168, '部门信息', '/system/dept', 'menu.system.dept', 1, 'none', 1, NULL, 0, 'system/dept/deptList', 'System', 100, 0, 417244902117446, '2023-08-16 22:31:43.743230', '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789200, 448839443789203, '应用列表', '/system/apps', 'menu.system.apps', 1, 'none', 1, NULL, 0, 'system/apps/appsList', 'System', 100, 0, 417244902117446, '2023-08-16 11:02:59.478041', '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789201, 448839443789203, '邮件模板', '/system/emailTemplate', 'menu.system.emailTemplate', 1, 'none', 1, NULL, 0, 'system/emailTemplate/emailTemplateList', 'System', 100, 0, 417244902117446, '2023-08-16 17:04:31.429647', '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789202, 0, '租户列表', '/system/tenant', 'menu.system.tenant', 1, 'cluster', 1, NULL, 1, 'system/tenant/tenantList', 'System', 100, 0, 417244902117446, '2023-08-16 17:12:24.338422', '', 450236590075973, NULL, NULL);
INSERT INTO `sys_menu` VALUES (448839443789203, 0, '项目配置', '/setting', 'menu.setting', 0, 'setting', 1, '/system/emailTemplate', 0, 'RouteView', 'System', 200, 0, 417244902117446, '2023-08-16 16:02:20.899362', '', 450236590075973, NULL, NULL);

-- ----------------------------
-- Table structure for sys_menu_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu_role`;
CREATE TABLE `sys_menu_role`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `menu_id` bigint(20) NOT NULL,
  `role_id` bigint(20) NOT NULL,
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 452679851343987 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单角色表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_menu_role
-- ----------------------------
INSERT INTO `sys_menu_role` VALUES (450243269607493, 448839443789157, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264122');
INSERT INTO `sys_menu_role` VALUES (450243269607494, 448839443789158, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264202');
INSERT INTO `sys_menu_role` VALUES (450243269607495, 448839443789159, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264217');
INSERT INTO `sys_menu_role` VALUES (450243269607496, 448839443789163, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264227');
INSERT INTO `sys_menu_role` VALUES (450243269607497, 448839443789168, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264232');
INSERT INTO `sys_menu_role` VALUES (450243269607498, 448839443789173, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264241');
INSERT INTO `sys_menu_role` VALUES (450243269607499, 448839443789174, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264250');
INSERT INTO `sys_menu_role` VALUES (450243269607500, 448839443789175, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264255');
INSERT INTO `sys_menu_role` VALUES (450243269607501, 448839443789196, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264264');
INSERT INTO `sys_menu_role` VALUES (450243269607502, 448839443789199, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264282');
INSERT INTO `sys_menu_role` VALUES (450243269607503, 448839443789198, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264287');
INSERT INTO `sys_menu_role` VALUES (450243269607504, 448839443789202, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264295');
INSERT INTO `sys_menu_role` VALUES (450243269607505, 448839443789203, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264299');
INSERT INTO `sys_menu_role` VALUES (450243269607506, 448839443789197, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264308');
INSERT INTO `sys_menu_role` VALUES (450243269607507, 448839443789200, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264317');
INSERT INTO `sys_menu_role` VALUES (450243269607508, 448839443789201, 417244902134579, 0, 417244902117456, '2023-08-15 16:24:35.264321');
INSERT INTO `sys_menu_role` VALUES (452679851343941, 417244902117886, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852061');
INSERT INTO `sys_menu_role` VALUES (452679851343942, 417244902117887, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852138');
INSERT INTO `sys_menu_role` VALUES (452679851343943, 417244902117889, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852146');
INSERT INTO `sys_menu_role` VALUES (452679851343944, 417244902117893, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852160');
INSERT INTO `sys_menu_role` VALUES (452679851343945, 417244902117890, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852168');
INSERT INTO `sys_menu_role` VALUES (452679851343946, 417244902117894, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852173');
INSERT INTO `sys_menu_role` VALUES (452679851343947, 417244902117891, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852181');
INSERT INTO `sys_menu_role` VALUES (452679851343948, 417244902117892, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852189');
INSERT INTO `sys_menu_role` VALUES (452679851343949, 417244902117895, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852194');
INSERT INTO `sys_menu_role` VALUES (452679851343950, 417244902117924, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852202');
INSERT INTO `sys_menu_role` VALUES (452679851343951, 417244902117925, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852210');
INSERT INTO `sys_menu_role` VALUES (452679851343952, 417244902117926, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852214');
INSERT INTO `sys_menu_role` VALUES (452679851343953, 417244902117927, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852222');
INSERT INTO `sys_menu_role` VALUES (452679851343954, 417244902117928, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852230');
INSERT INTO `sys_menu_role` VALUES (452679851343955, 417244902117896, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852235');
INSERT INTO `sys_menu_role` VALUES (452679851343956, 417244902117923, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852244');
INSERT INTO `sys_menu_role` VALUES (452679851343957, 417244902117897, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852256');
INSERT INTO `sys_menu_role` VALUES (452679851343958, 417244902117921, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852260');
INSERT INTO `sys_menu_role` VALUES (452679851343959, 417244902117922, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852267');
INSERT INTO `sys_menu_role` VALUES (452679851343960, 417244902117967, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852271');
INSERT INTO `sys_menu_role` VALUES (452679851343961, 417244902117898, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852274');
INSERT INTO `sys_menu_role` VALUES (452679851343962, 417244902117907, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852277');
INSERT INTO `sys_menu_role` VALUES (452679851343963, 417244902117908, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852279');
INSERT INTO `sys_menu_role` VALUES (452679851343964, 417244902117909, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852283');
INSERT INTO `sys_menu_role` VALUES (452679851343965, 417244902117999, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852287');
INSERT INTO `sys_menu_role` VALUES (452679851343966, 435823827857477, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852289');
INSERT INTO `sys_menu_role` VALUES (452679851343967, 417244902117899, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852293');
INSERT INTO `sys_menu_role` VALUES (452679851343968, 417244902117919, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852297');
INSERT INTO `sys_menu_role` VALUES (452679851343969, 417244902117920, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852299');
INSERT INTO `sys_menu_role` VALUES (452679851343970, 417244902117901, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852303');
INSERT INTO `sys_menu_role` VALUES (452679851343971, 417244902117914, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852307');
INSERT INTO `sys_menu_role` VALUES (452679851343972, 417244902117915, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852309');
INSERT INTO `sys_menu_role` VALUES (452679851343973, 417244902117916, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852313');
INSERT INTO `sys_menu_role` VALUES (452679851343974, 417244902117902, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852317');
INSERT INTO `sys_menu_role` VALUES (452679851343975, 417244902117911, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852319');
INSERT INTO `sys_menu_role` VALUES (452679851343976, 417244902117912, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852322');
INSERT INTO `sys_menu_role` VALUES (452679851343977, 417244902117913, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852326');
INSERT INTO `sys_menu_role` VALUES (452679851343978, 417244902117903, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852328');
INSERT INTO `sys_menu_role` VALUES (452679851343979, 417244902117917, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852332');
INSERT INTO `sys_menu_role` VALUES (452679851343980, 417244902117918, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852337');
INSERT INTO `sys_menu_role` VALUES (452679851343981, 417244902194761, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852339');
INSERT INTO `sys_menu_role` VALUES (452679851343982, 417244902194762, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852343');
INSERT INTO `sys_menu_role` VALUES (452679851343983, 444148029263941, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852347');
INSERT INTO `sys_menu_role` VALUES (452679851343984, 448839443734597, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852349');
INSERT INTO `sys_menu_role` VALUES (452679851343985, 448839443734598, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852356');
INSERT INTO `sys_menu_role` VALUES (452679851343986, 447421929099333, 424046126026821, 0, 417244902117446, '2023-08-22 13:39:03.852358');

-- ----------------------------
-- Table structure for sys_permissions
-- ----------------------------
DROP TABLE IF EXISTS `sys_permissions`;
CREATE TABLE `sys_permissions`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `menu_id` bigint(20) NULL DEFAULT NULL COMMENT '绑定系统菜单Id',
  `code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限Code',
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限名字',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `type` int(11) NULL DEFAULT NULL COMMENT '权限类型',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 452696503009358 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_permissions
-- ----------------------------
INSERT INTO `sys_permissions` VALUES (425102445924421, 417244902117999, 'Add', '菜单新增', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924422, 417244902117999, 'Delete', '菜单删除', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924423, 417244902117999, 'Update', '菜单修改', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924424, 417244902117999, 'sysCodeGen/page', '代码生成分页', 0, 417244902117446, '2023-06-05 15:26:18.857000', 2);
INSERT INTO `sys_permissions` VALUES (443608172056645, 417244902117886, 'add', '增加按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056646, 417244902117886, 'update', '修改按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056647, 417244902117886, 'delete', '删除按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056648, 417244902117886, 'get', '查询按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (452645322899525, 417244902117909, 'add', '增加按钮', 0, 417244902117446, '2023-08-22 11:18:34.056981', 1);
INSERT INTO `sys_permissions` VALUES (452645322903621, 417244902117909, 'update', '修改按钮', 0, 417244902117446, '2023-08-22 11:18:34.057048', 1);
INSERT INTO `sys_permissions` VALUES (452645322903622, 417244902117909, 'delete', '删除按钮', 0, 417244902117446, '2023-08-22 11:18:34.057055', 1);
INSERT INTO `sys_permissions` VALUES (452645322903623, 417244902117909, 'get', '查询按钮', 0, 417244902117446, '2023-08-22 11:18:34.057058', 1);
INSERT INTO `sys_permissions` VALUES (452645322903624, 417244902117909, 'add', '增加路由', 0, 417244902117446, '2023-08-22 11:18:34.057061', 2);
INSERT INTO `sys_permissions` VALUES (452645322903625, 417244902117909, 'update', '修改路由', 0, 417244902117446, '2023-08-22 11:18:34.057063', 2);
INSERT INTO `sys_permissions` VALUES (452645322903626, 417244902117909, 'delete', '删除路由', 0, 417244902117446, '2023-08-22 11:18:34.057066', 2);
INSERT INTO `sys_permissions` VALUES (452645322903627, 417244902117909, 'get', '查询路由', 0, 417244902117446, '2023-08-22 11:18:34.057068', 2);
INSERT INTO `sys_permissions` VALUES (452645322903628, 417244902117909, 'page', '分页路由', 0, 417244902117446, '2023-08-22 11:18:34.057070', 2);
INSERT INTO `sys_permissions` VALUES (452679242018885, 417244902117907, 'add', '增加按钮', 0, 417244902117446, '2023-08-22 13:36:35.091380', 1);
INSERT INTO `sys_permissions` VALUES (452679242018886, 417244902117907, 'update', '修改按钮', 0, 417244902117446, '2023-08-22 13:36:35.091460', 1);
INSERT INTO `sys_permissions` VALUES (452679242018887, 417244902117907, 'delete', '删除按钮', 0, 417244902117446, '2023-08-22 13:36:35.091468', 1);
INSERT INTO `sys_permissions` VALUES (452679242018888, 417244902117907, 'get', '查询按钮', 0, 417244902117446, '2023-08-22 13:36:35.091479', 1);
INSERT INTO `sys_permissions` VALUES (452679242018889, 417244902117907, 'sysRole/add', '增加路由', 0, 417244902117446, '2023-08-22 13:36:35.091484', 2);
INSERT INTO `sys_permissions` VALUES (452679242018890, 417244902117907, 'sysRole/update', '修改路由', 0, 417244902117446, '2023-08-22 13:36:35.091487', 2);
INSERT INTO `sys_permissions` VALUES (452679242018891, 417244902117907, 'sysRole/delete', '删除路由', 0, 417244902117446, '2023-08-22 13:36:35.091490', 2);
INSERT INTO `sys_permissions` VALUES (452679242018892, 417244902117907, 'sysRole/get', '查询路由', 0, 417244902117446, '2023-08-22 13:36:35.091492', 2);
INSERT INTO `sys_permissions` VALUES (452679242018893, 417244902117907, 'sysRole/page', '分页路由', 0, 417244902117446, '2023-08-22 13:36:35.091495', 2);
INSERT INTO `sys_permissions` VALUES (452696503009349, 435823827857477, 'add', '部门信息-增加按钮', 0, 417244902117446, '2023-08-22 14:46:49.200674', 1);
INSERT INTO `sys_permissions` VALUES (452696503009350, 435823827857477, 'update', '部门信息-修改按钮', 0, 417244902117446, '2023-08-22 14:46:49.200745', 1);
INSERT INTO `sys_permissions` VALUES (452696503009351, 435823827857477, 'delete', '部门信息-删除按钮', 0, 417244902117446, '2023-08-22 14:46:49.200755', 1);
INSERT INTO `sys_permissions` VALUES (452696503009352, 435823827857477, 'get', '部门信息-查询按钮', 0, 417244902117446, '2023-08-22 14:46:49.200760', 1);
INSERT INTO `sys_permissions` VALUES (452696503009353, 435823827857477, 'sysDept/add', '部门信息-增加路由', 0, 417244902117446, '2023-08-22 14:46:49.200763', 2);
INSERT INTO `sys_permissions` VALUES (452696503009354, 435823827857477, 'sysDept/update', '部门信息-修改路由', 0, 417244902117446, '2023-08-22 14:46:49.200767', 2);
INSERT INTO `sys_permissions` VALUES (452696503009355, 435823827857477, 'sysDept/delete', '部门信息-删除路由', 0, 417244902117446, '2023-08-22 14:46:49.200769', 2);
INSERT INTO `sys_permissions` VALUES (452696503009356, 435823827857477, 'sysDept/get', '部门信息-查询路由', 0, 417244902117446, '2023-08-22 14:46:49.200774', 2);
INSERT INTO `sys_permissions` VALUES (452696503009357, 435823827857477, 'sysDept/page', '部门信息-分页路由', 0, 417244902117446, '2023-08-22 14:46:49.200778', 2);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色名字',
  `describe` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色描述',
  `enable` tinyint(1) NOT NULL COMMENT '角色启用',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 449891358490694 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统角色信息表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (417244902134579, '财务部长', '拥有财务相关的所有权限', 1, 0, NULL, NULL);
INSERT INTO `sys_role` VALUES (424046126026821, '系统管理员', '拥有系统所有权限', 1, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_role_permissions
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_permissions`;
CREATE TABLE `sys_role_permissions`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `role_id` bigint(20) NOT NULL,
  `permissions_id` bigint(20) NOT NULL,
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 452697010172008 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_role_permissions
-- ----------------------------
INSERT INTO `sys_role_permissions` VALUES (445269605408838, 424046126026821, 441396425613381, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408839, 424046126026821, 441396425613382, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408840, 424046126026821, 441396425613383, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408845, 424046126026821, 417244902194816, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408846, 424046126026821, 417244902194817, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408847, 424046126026821, 417244902194818, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269757751365, 417244902134579, 417244902194816, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751366, 417244902134579, 417244902194817, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751367, 417244902134579, 417244902194818, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751368, 417244902134579, 425102445924421, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751369, 417244902134579, 425102445924422, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751370, 417244902134579, 425102445924423, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751371, 417244902134579, 425102445924424, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751372, 417244902134579, 441396425613381, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751373, 417244902134579, 441396425613382, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751374, 417244902134579, 441396425613383, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751375, 417244902134579, 443608172056645, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751376, 417244902134579, 443608172056646, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757751377, 417244902134579, 443608172056647, 0, 417244902117446, '2023-08-01 15:07:18.971000');
INSERT INTO `sys_role_permissions` VALUES (445269757755461, 417244902134579, 443608172056648, 0, 417244902117446, '2023-08-01 15:07:18.972000');
INSERT INTO `sys_role_permissions` VALUES (452697010171973, 424046126026821, 425102445924421, 0, 417244902117446, '2023-08-22 14:48:53.019366');
INSERT INTO `sys_role_permissions` VALUES (452697010171974, 424046126026821, 425102445924422, 0, 417244902117446, '2023-08-22 14:48:53.019419');
INSERT INTO `sys_role_permissions` VALUES (452697010171975, 424046126026821, 425102445924423, 0, 417244902117446, '2023-08-22 14:48:53.019426');
INSERT INTO `sys_role_permissions` VALUES (452697010171976, 424046126026821, 425102445924424, 0, 417244902117446, '2023-08-22 14:48:53.019430');
INSERT INTO `sys_role_permissions` VALUES (452697010171977, 424046126026821, 443608172056645, 0, 417244902117446, '2023-08-22 14:48:53.019432');
INSERT INTO `sys_role_permissions` VALUES (452697010171978, 424046126026821, 443608172056646, 0, 417244902117446, '2023-08-22 14:48:53.019435');
INSERT INTO `sys_role_permissions` VALUES (452697010171979, 424046126026821, 443608172056647, 0, 417244902117446, '2023-08-22 14:48:53.019438');
INSERT INTO `sys_role_permissions` VALUES (452697010171980, 424046126026821, 443608172056648, 0, 417244902117446, '2023-08-22 14:48:53.019440');
INSERT INTO `sys_role_permissions` VALUES (452697010171981, 424046126026821, 452645322899525, 0, 417244902117446, '2023-08-22 14:48:53.019443');
INSERT INTO `sys_role_permissions` VALUES (452697010171982, 424046126026821, 452645322903621, 0, 417244902117446, '2023-08-22 14:48:53.019446');
INSERT INTO `sys_role_permissions` VALUES (452697010171983, 424046126026821, 452645322903622, 0, 417244902117446, '2023-08-22 14:48:53.019448');
INSERT INTO `sys_role_permissions` VALUES (452697010171984, 424046126026821, 452645322903623, 0, 417244902117446, '2023-08-22 14:48:53.019451');
INSERT INTO `sys_role_permissions` VALUES (452697010171985, 424046126026821, 452645322903624, 0, 417244902117446, '2023-08-22 14:48:53.019453');
INSERT INTO `sys_role_permissions` VALUES (452697010171986, 424046126026821, 452645322903625, 0, 417244902117446, '2023-08-22 14:48:53.019456');
INSERT INTO `sys_role_permissions` VALUES (452697010171987, 424046126026821, 452645322903626, 0, 417244902117446, '2023-08-22 14:48:53.019459');
INSERT INTO `sys_role_permissions` VALUES (452697010171988, 424046126026821, 452645322903627, 0, 417244902117446, '2023-08-22 14:48:53.019463');
INSERT INTO `sys_role_permissions` VALUES (452697010171989, 424046126026821, 452645322903628, 0, 417244902117446, '2023-08-22 14:48:53.019466');
INSERT INTO `sys_role_permissions` VALUES (452697010171990, 424046126026821, 452696503009349, 0, 417244902117446, '2023-08-22 14:48:53.019470');
INSERT INTO `sys_role_permissions` VALUES (452697010171991, 424046126026821, 452696503009350, 0, 417244902117446, '2023-08-22 14:48:53.019472');
INSERT INTO `sys_role_permissions` VALUES (452697010171992, 424046126026821, 452696503009351, 0, 417244902117446, '2023-08-22 14:48:53.019475');
INSERT INTO `sys_role_permissions` VALUES (452697010171993, 424046126026821, 452696503009352, 0, 417244902117446, '2023-08-22 14:48:53.019478');
INSERT INTO `sys_role_permissions` VALUES (452697010171994, 424046126026821, 452696503009353, 0, 417244902117446, '2023-08-22 14:48:53.019480');
INSERT INTO `sys_role_permissions` VALUES (452697010171995, 424046126026821, 452696503009354, 0, 417244902117446, '2023-08-22 14:48:53.019483');
INSERT INTO `sys_role_permissions` VALUES (452697010171996, 424046126026821, 452696503009355, 0, 417244902117446, '2023-08-22 14:48:53.019486');
INSERT INTO `sys_role_permissions` VALUES (452697010171997, 424046126026821, 452696503009356, 0, 417244902117446, '2023-08-22 14:48:53.019487');
INSERT INTO `sys_role_permissions` VALUES (452697010171998, 424046126026821, 452696503009357, 0, 417244902117446, '2023-08-22 14:48:53.019491');
INSERT INTO `sys_role_permissions` VALUES (452697010171999, 424046126026821, 452679242018885, 0, 417244902117446, '2023-08-22 14:48:53.019494');
INSERT INTO `sys_role_permissions` VALUES (452697010172000, 424046126026821, 452679242018886, 0, 417244902117446, '2023-08-22 14:48:53.019496');
INSERT INTO `sys_role_permissions` VALUES (452697010172001, 424046126026821, 452679242018887, 0, 417244902117446, '2023-08-22 14:48:53.019499');
INSERT INTO `sys_role_permissions` VALUES (452697010172002, 424046126026821, 452679242018888, 0, 417244902117446, '2023-08-22 14:48:53.019501');
INSERT INTO `sys_role_permissions` VALUES (452697010172003, 424046126026821, 452679242018889, 0, 417244902117446, '2023-08-22 14:48:53.019503');
INSERT INTO `sys_role_permissions` VALUES (452697010172004, 424046126026821, 452679242018890, 0, 417244902117446, '2023-08-22 14:48:53.019506');
INSERT INTO `sys_role_permissions` VALUES (452697010172005, 424046126026821, 452679242018891, 0, 417244902117446, '2023-08-22 14:48:53.019509');
INSERT INTO `sys_role_permissions` VALUES (452697010172006, 424046126026821, 452679242018892, 0, 417244902117446, '2023-08-22 14:48:53.019511');
INSERT INTO `sys_role_permissions` VALUES (452697010172007, 424046126026821, 452679242018893, 0, 417244902117446, '2023-08-22 14:48:53.019514');

-- ----------------------------
-- Table structure for sys_tenant
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenant`;
CREATE TABLE `sys_tenant`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tenant_admin` bigint(20) NOT NULL COMMENT '租户管理员',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '租户名字',
  `email_config` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '邮件配置',
  `enable` tinyint(1) NOT NULL COMMENT '是否启用',
  `describe` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 491539585003590 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统租户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_tenant
-- ----------------------------
INSERT INTO `sys_tenant` VALUES (449900948840517, 417244902117446, 'HuanTian项目', '271976304@qq.com;tigxqcwmuqblbhgg;smtp.qq.com;587', 1, '测试用途账套', 0, 417244902117446, '2023-08-15 15:57:24.519421', 417244902117446, '2023-08-15 15:57:46.919194');
INSERT INTO `sys_tenant` VALUES (450236590075973, 417244902117456, '测试租户', 'wangxiaopang8888@163.com;SXFDBHBLUKDQVWDR;smtp.163.com;25', 1, '主要用于测试租户', 0, 417244902117446, '2023-08-15 15:57:24.519421', NULL, NULL);

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名字',
  `avatar` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图片路径',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户名',
  `password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户密码',
  `enable` tinyint(1) NOT NULL COMMENT '启用',
  `telephone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '电话',
  `last_login_ip` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '最后登陆IP',
  `last_login_time` datetime(6) NULL DEFAULT NULL COMMENT '最后登陆时间',
  `language` int(11) NOT NULL COMMENT '系统语言',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `update_on` datetime(6) NULL DEFAULT NULL COMMENT '修改时间',
  `tenant_id` bigint(20) NOT NULL COMMENT '租户ID',
  `dept_id` bigint(20) NOT NULL DEFAULT 0 COMMENT '所属部门Id',
  `type` int(11) NOT NULL DEFAULT 0 COMMENT '账号类型',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4172449021174946 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户信息表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES (417244902117445, 'Api测试系统管理员', '/avatar2.jpg', 'test', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', 1, '123456789', '192.168.0.1', '2022-03-31 10:00:00.000000', 0, 0, 417244902117446, '2022-03-31 10:00:00.000000', NULL, NULL, 449900948840517, 417244902119463, 0);
INSERT INTO `sys_user` VALUES (417244902117446, '系统管理员', '/avatar1.jpg', 'admin', 'ADCD7048512E64B48DA55B027577886EE5A36350', 1, '15675117404', '192.168.0.2', '2022-03-31 11:00:00.000000', 0, 0, 417244902117446, '2022-03-31 11:00:00.000000', 417244902117446, '2023-07-25 09:41:45.085217', 449900948840517, 417244902119463, 2);
INSERT INTO `sys_user` VALUES (417244902117456, 'Sarah Lee', '/asd.jpg', 'wanglei', 'ADCD7048512E64B48DA55B027577886EE5A36350', 1, '111111111', '192.168.0.50', '2022-03-31 23:00:00.000000', 1, 0, 417244902117446, '2022-03-31 23:00:00.000000', NULL, NULL, 450236590075973, 417244902119462, 0);

-- ----------------------------
-- Table structure for sys_user_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NOT NULL,
  `role_id` bigint(20) NOT NULL,
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 451316677242951 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统用户权限表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_user_role
-- ----------------------------
INSERT INTO `sys_user_role` VALUES (417244902149164, 417244902117456, 417244902134579, 0, NULL, NULL);
INSERT INTO `sys_user_role` VALUES (417244902194813, 417244902117446, 417244902134579, 0, NULL, '2023-05-30 21:47:54.000000');
INSERT INTO `sys_user_role` VALUES (417244902194814, 417244902117445, 424046126026821, 0, NULL, '2023-05-30 21:47:54.000000');
INSERT INTO `sys_user_role` VALUES (417244902194815, 417244902117446, 424046126026821, 0, NULL, '2023-05-30 21:47:54.000000');

SET FOREIGN_KEY_CHECKS = 1;
