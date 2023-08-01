/*
 Navicat Premium Data Transfer

 Source Server Type    : MySQL
 Source Server Version : 50742 (5.7.42)
 Source Schema         : store_db

 Target Server Type    : MySQL
 Target Server Version : 50742 (5.7.42)
 File Encoding         : 65001

 Date: 01/08/2023 15:53:01
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------

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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 439691352649888 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统应用表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_apps
-- ----------------------------
INSERT INTO `sys_apps` VALUES (439691352649886, '系统应用', 'System', 5, 1, '展示系统设置菜单', 0, NULL, NULL, NULL, NULL);
INSERT INTO `sys_apps` VALUES (439691352649887, '业务应用', 'Business', 1, 1, '展示业务菜单', 0, NULL, NULL, NULL, NULL);

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
) ENGINE = InnoDB AUTO_INCREMENT = 443511585738822 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '代码生成数据表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_code_gen
-- ----------------------------
INSERT INTO `sys_code_gen` VALUES (417244902194618, '系统角色页面', 'sys_role', 0, 0, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (417244902194619, '系统权限页面', 'sys_permissions', 0, 1, 0, NULL, NULL);
INSERT INTO `sys_code_gen` VALUES (436088202891333, '系统部门列表', 'sys_dept', 0, 1, 0, 417244902117446, '2023-07-06 16:27:31.415000');
INSERT INTO `sys_code_gen` VALUES (443511585738821, '菜单应用列表', 'sys_apps', 417244902117898, 0, 0, 417244902117446, '2023-07-27 15:53:19.319000');

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
) ENGINE = InnoDB AUTO_INCREMENT = 443511599190090 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '代码生成数据详情表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_code_gen_detail
-- ----------------------------
INSERT INTO `sys_code_gen_detail` VALUES (417244902111091, 417244902194618, 'bigint', 'id', 'Id', '', 1, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111092, 417244902194618, 'varchar', 'role_name', '角色名字', '', 1, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111093, 417244902194618, 'varchar', 'describe', '角色描述', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111094, 417244902194618, 'int', 'status', '角色状态', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111095, 417244902194618, 'tinyint', 'deleted', '软删除', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111096, 417244902194618, 'bigint', 'create_by', '创建人', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111097, 417244902194618, 'datetime', 'create_on', '创建时间', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111098, 417244902194619, 'bigint', 'id', 'Id', '', 1, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111099, 417244902194619, 'varchar', 'code', '权限code', '', 1, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111100, 417244902194619, 'varchar', 'name', '权限名称', '', 1, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111101, 417244902194619, 'tinyint', 'deleted', '软删除', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111102, 417244902194619, 'bigint', 'create_by', '创建人', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111103, 417244902194619, 'datetime', 'create_on', '创建时间', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111104, 417244902194619, 'bigint', 'menu_id', '菜单id', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (417244902111105, 417244902194619, 'int', 'type', '权限类型', '', 0, 0, NULL, NULL, '', 0, '', 0);
INSERT INTO `sys_code_gen_detail` VALUES (436088221413445, 436088202891333, 'bigint', 'parent_id', '父级部门id', NULL, 0, 0, 417244902117446, '2023-07-06 16:27:32.951000', NULL, 1, 'textBox', 0);
INSERT INTO `sys_code_gen_detail` VALUES (436088221413446, 436088202891333, 'varchar', 'name', '部门名字', '', 1, 0, 417244902117446, '2023-07-06 16:27:32.951000', 'like', 1, 'textBox', 1);
INSERT INTO `sys_code_gen_detail` VALUES (436088221413447, 436088202891333, 'varchar', 'describe', '部门描述', NULL, 0, 0, 417244902117446, '2023-07-06 16:27:32.951000', NULL, 1, 'textBox', 2);
INSERT INTO `sys_code_gen_detail` VALUES (436088221413448, 436088202891333, 'tinyint', 'enable', '部门启用', NULL, 1, 0, 417244902117446, '2023-07-06 16:27:32.951000', NULL, 1, 'radio', 3);
INSERT INTO `sys_code_gen_detail` VALUES (443511599190085, 443511585738821, 'varchar', 'name', '应用名称', NULL, 1, 0, 417244902117446, '2023-07-27 15:53:21.041477', 'equal', 0, 'textBox', 1);
INSERT INTO `sys_code_gen_detail` VALUES (443511599190086, 443511585738821, 'varchar', 'code', '应用编码', NULL, 1, 0, 417244902117446, '2023-07-27 15:53:21.041536', 'equal', 0, 'textBox', 2);
INSERT INTO `sys_code_gen_detail` VALUES (443511599190087, 443511585738821, 'int', 'order', '排序', NULL, 0, 0, 417244902117446, '2023-07-27 15:53:21.041541', NULL, 0, 'textBox', 3);
INSERT INTO `sys_code_gen_detail` VALUES (443511599190088, 443511585738821, 'tinyint', 'enable', '启用', NULL, 0, 0, 417244902117446, '2023-07-27 15:53:21.041548', NULL, 0, 'radio', 4);
INSERT INTO `sys_code_gen_detail` VALUES (443511599190089, 443511585738821, 'varchar', 'describe', '描述', NULL, 0, 0, 417244902117446, '2023-07-27 15:53:21.041553', NULL, 0, 'textBox', 5);

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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 437221302702150 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统部门表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_dept
-- ----------------------------
INSERT INTO `sys_dept` VALUES (417244902119461, 0, '财务部门', '财务部门管理公司财务', 1, 0, NULL, NULL);
INSERT INTO `sys_dept` VALUES (417244902119462, 0, '人事部门', '财务部门管理所有公司人事信息', 1, 0, NULL, NULL);
INSERT INTO `sys_dept` VALUES (417244902119463, 0, 'IT部门', '财务部门管理公司IT有关事宜', 1, 0, NULL, NULL);
INSERT INTO `sys_dept` VALUES (437221302702149, 417244902119463, '设计部', '负责IT设计功能', 1, 0, 417244902117446, '2023-07-09 21:18:04.125393');

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
) ENGINE = InnoDB AUTO_INCREMENT = 435082462924870 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统字典表' ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB AUTO_INCREMENT = 435104934633546 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统字典详情表' ROW_FORMAT = Dynamic;

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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 444148029263942 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES (417244902117886, 0, 'dashboard', NULL, 'menu.dashboard', 0, 'dashboard', 1, '/dashboard/workplace', 0, 'RouteView', 'Business', 1000, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117887, 417244902117886, 'Analysis', '/dashboard/analysis', 'menu.dashboard.analysis', 0, 'none', 1, '', 0, 'Analysis', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117889, 417244902117886, 'monitor', 'https://www.baidu.com/', 'menu.dashboard.workplace', 0, 'none', 1, '', 0, '', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117890, 417244902117894, 'advanced-form', NULL, 'menu.form.advanced-form', 0, 'none', 1, '', 0, 'AdvanceForm', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117891, 417244902117894, 'step-form', NULL, 'menu.form.step-form', 0, 'none', 1, '', 0, 'StepForm', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117892, 417244902117894, 'basic-form', NULL, 'menu.form.basic-form', 0, 'none', 1, '', 0, 'BasicForm', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117893, 417244902117886, 'workplace', NULL, 'menu.dashboard.monitor', 0, 'none', 1, '', 0, 'Workplace', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117894, 0, 'form', NULL, 'menu.form', 0, 'form', 1, '/form/base-form', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117895, 417244902117896, 'settings', NULL, 'menu.account.settings', 0, 'none', 1, '/account/settings/basic', 0, 'AccountSettings', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117896, 0, 'account', NULL, 'menu.account', 0, 'user', 1, '/account/center', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117897, 0, 'exception', NULL, 'menu.exception', 0, 'warning', 1, '/exception/403', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117898, 0, '系统管理', '/system', 'menu.system', 0, 'tool', 1, '/sysManage/menuList', 0, 'RouteView', 'System', 900, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117899, 0, 'result', NULL, 'menu.result', 0, 'check-circle-o', 1, '/result/success', 0, 'PageView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117901, 417244902117902, 'search', NULL, 'menu.list.search-list', 0, 'none', 1, '/list/search/article', 0, 'SearchLayout', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117902, 0, 'list', NULL, 'menu.list', 0, 'table', 1, '/list/table-list', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117903, 0, 'profile', NULL, 'menu.profile', 0, 'profile', 1, '/profile/basic', 0, 'RouteView', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117907, 417244902117898, '角色列表', '/system/roleList', 'menu.system.roleList', 0, 'none', 1, '', 0, 'system/role/roleList', 'System', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117908, 417244902117898, '权限列表', '/system/permList', 'menu.system.permissionList', 0, 'none', 1, '', 0, 'system/perm/permList', 'System', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117909, 417244902117898, '用户列表', '/system/userList', 'menu.system.userList', 0, 'none', 1, '', 0, 'system/user/userList', 'System', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117910, 417244902117904, 'edit-table', NULL, '内联编辑表格', 0, 'none', 1, '', 0, 'TableInnerEditList', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117911, 417244902117902, 'table-list', '/list/table-list/:pageNo([1-9]\\\\d*)?', 'menu.list.table-list', 0, 'none', 1, '', 0, 'TableList', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117912, 417244902117902, 'basic-list', NULL, 'menu.list.basic-list', 0, 'none', 1, '', 0, 'StandardList', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117913, 417244902117902, 'card', NULL, 'menu.list.card-list', 0, 'none', 1, '', 0, 'CardList', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117914, 417244902117901, 'article', NULL, 'menu.list.search-list.articles', 0, 'none', 1, '', 0, 'SearchArticles', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117915, 417244902117901, 'project', NULL, 'menu.list.search-list.projects', 0, 'none', 1, '', 0, 'SearchProjects', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117916, 417244902117901, 'application', NULL, 'menu.list.search-list.applications', 0, 'none', 1, '', 0, 'SearchApplications', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117917, 417244902117903, 'basic', NULL, 'menu.profile.basic', 0, 'none', 1, '', 0, 'ProfileBasic', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117918, 417244902117903, 'advanced', NULL, 'menu.profile.advanced', 0, 'none', 1, '', 0, 'ProfileAdvanced', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117919, 417244902117899, 'success', NULL, 'menu.result.success', 0, 'none', 1, '', 0, 'ResultSuccess', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117920, 417244902117899, 'fail', NULL, 'menu.result.fail', 0, 'none', 1, '', 0, 'ResultFail', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117921, 417244902117897, '403', NULL, 'menu.exception.not-permission', 0, 'none', 1, '', 0, 'Exception403', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117922, 417244902117897, '404', NULL, 'menu.exception.not-find', 0, 'none', 1, '', 0, 'Exception404', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117923, 417244902117896, 'center', NULL, 'menu.account.center', 0, 'none', 1, '', 0, 'AccountCenter', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117924, 417244902117895, 'BasicSettings', '/account/settings/basic', 'account.settings.menuMap.basic', 0, 'none', 1, '', 0, 'BasicSetting', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117925, 417244902117895, 'SecuritySettings', '/account/settings/security', 'account.settings.menuMap.security', 0, 'none', 1, '', 0, 'SecuritySettings', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117926, 417244902117895, 'CustomSettings', '/account/settings/custom', 'account.settings.menuMap.custom', 0, 'none', 1, '', 0, 'CustomSettings', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117927, 417244902117895, 'BindingSettings', '/account/settings/binding', 'account.settings.menuMap.binding', 0, 'none', 1, '', 0, 'BindingSettings', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117928, 417244902117895, 'NotificationSettings', '/account/settings/notification', 'account.settings.menuMap.notification', 0, 'none', 1, '', 0, 'NotificationSettings', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117967, 417244902117897, 'error', '', 'menu.exception.server-error', 0, 'none', 1, '', 0, 'Exception500', 'Business', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902117999, 417244902117898, '菜单列表', '/system/menuList', 'menu.system.menuList', 0, 'none', 1, '', 0, 'system/menu/menuList', 'System', 50, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902194761, 417244902117898, '字典列表', '/system/dicList', 'menu.system.dicList', 0, 'none', 1, '', 0, 'system/dic/dicList', 'System', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (417244902194762, 417244902117898, '代码生成', '/system/codeGen', 'menu.system.codeGen', 0, 'none', 1, '', 0, 'system/codeGen/codeGen', 'System', 0, 0, NULL, NULL, '');
INSERT INTO `sys_menu` VALUES (435823827857477, 417244902117898, '部门信息', '/system/deptList', 'menu.system.dept', 0, 'none', 1, NULL, 0, 'system/dept/deptList', 'System', 100, 0, 417244902117446, '2023-07-05 22:31:43.743230', '');
INSERT INTO `sys_menu` VALUES (444148029263941, 417244902117898, '应用列表', '/system/sysApps', 'menu.system.sysApps', 0, 'none', 1, NULL, 0, 'system/sysApps/sysAppsList', 'System', 100, 0, 417244902117446, '2023-07-29 11:02:59.478041', '');

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
) ENGINE = InnoDB AUTO_INCREMENT = 444213516976239 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu_role
-- ----------------------------
INSERT INTO `sys_menu_role` VALUES (440952917856325, 417244902117898, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.981000');
INSERT INTO `sys_menu_role` VALUES (440952917868613, 417244902117907, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868614, 417244902117908, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868615, 417244902117909, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868616, 417244902117999, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868617, 417244902194761, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868618, 417244902194762, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (440952917868619, 435823827857477, 417244902134579, 0, 417244902117446, '2023-07-20 10:22:02.984000');
INSERT INTO `sys_menu_role` VALUES (444213516972101, 417244902117886, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976197, 417244902117887, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976198, 417244902117889, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976199, 417244902117893, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976200, 417244902117898, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976201, 417244902117907, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976202, 417244902117908, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976203, 417244902117909, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976204, 417244902117999, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976205, 417244902194761, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976206, 417244902194762, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976207, 435823827857477, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976208, 444148029263941, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976209, 417244902117897, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976210, 417244902117921, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976211, 417244902117922, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976212, 417244902117967, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976213, 417244902117896, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976214, 417244902117895, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976215, 417244902117924, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976216, 417244902117925, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976217, 417244902117926, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976218, 417244902117927, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976219, 417244902117928, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976220, 417244902117923, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976221, 417244902117894, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976222, 417244902117890, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976223, 417244902117891, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976224, 417244902117892, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976225, 417244902117899, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976226, 417244902117919, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976227, 417244902117920, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976228, 417244902117902, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976229, 417244902117901, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976230, 417244902117914, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976231, 417244902117915, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976232, 417244902117916, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976233, 417244902117911, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976234, 417244902117912, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976235, 417244902117913, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976236, 417244902117903, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976237, 417244902117917, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');
INSERT INTO `sys_menu_role` VALUES (444213516976238, 417244902117918, 424046126026821, 0, 417244902117446, '2023-07-29 15:29:27.688000');

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
) ENGINE = InnoDB AUTO_INCREMENT = 443608172056649 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_permissions
-- ----------------------------
INSERT INTO `sys_permissions` VALUES (417244902194816, 417244902117909, 'Add', '用户新增', 0, NULL, NULL, 1);
INSERT INTO `sys_permissions` VALUES (417244902194817, 417244902117909, 'Delete', '用户删除', 0, NULL, NULL, 1);
INSERT INTO `sys_permissions` VALUES (417244902194818, 417244902117909, 'Update', '用户修改', 0, NULL, NULL, 1);
INSERT INTO `sys_permissions` VALUES (425102445924421, 417244902117999, 'Add', '菜单新增', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924422, 417244902117999, 'Delete', '菜单删除', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924423, 417244902117999, 'Update', '菜单修改', 0, 417244902117446, '2023-06-05 15:26:18.857000', 1);
INSERT INTO `sys_permissions` VALUES (425102445924424, 417244902117999, 'sysCodeGen/page', '代码生成分页', 0, 417244902117446, '2023-06-05 15:26:18.857000', 2);
INSERT INTO `sys_permissions` VALUES (441396425613381, 417244902117907, 'add', '新增', 0, 417244902117446, '2023-07-21 16:26:41.242000', 1);
INSERT INTO `sys_permissions` VALUES (441396425613382, 417244902117907, 'update', '修改', 0, 417244902117446, '2023-07-21 16:26:41.242000', 1);
INSERT INTO `sys_permissions` VALUES (441396425613383, 417244902117907, 'sysRole/add', '增加路由', 0, 417244902117446, '2023-07-21 16:26:41.242000', 2);
INSERT INTO `sys_permissions` VALUES (443608172056645, 417244902117886, 'add', '增加按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056646, 417244902117886, 'update', '修改按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056647, 417244902117886, 'delete', '删除按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);
INSERT INTO `sys_permissions` VALUES (443608172056648, 417244902117886, 'get', '查询按钮', 0, 417244902117446, '2023-07-27 22:26:18.401000', 1);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色名字',
  `describe` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色描述',
  `enable` tinyint(1) NOT NULL COMMENT '角色启用',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 445269808595014 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统角色信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (417244902134579, '系统管理员', '管理员拥有所有权限', 1, 0, NULL, NULL);
INSERT INTO `sys_role` VALUES (424046126026821, '财务部长', '拥有财务部门的所有权限', 1, 0, 417244902117445, '2023-06-02 15:48:08.257375');

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
) ENGINE = InnoDB AUTO_INCREMENT = 445269757755462 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role_permissions
-- ----------------------------
INSERT INTO `sys_role_permissions` VALUES (445269605408837, 424046126026821, 425102445924424, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408838, 424046126026821, 441396425613381, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408839, 424046126026821, 441396425613382, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408840, 424046126026821, 441396425613383, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408841, 424046126026821, 443608172056645, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408842, 424046126026821, 443608172056646, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408843, 424046126026821, 443608172056647, 0, 417244902117446, '2023-08-01 15:06:41.778000');
INSERT INTO `sys_role_permissions` VALUES (445269605408844, 424046126026821, 443608172056648, 0, 417244902117446, '2023-08-01 15:06:41.778000');
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
) ENGINE = InnoDB AUTO_INCREMENT = 417244902117457 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES (417244902117445, 'Api测试系统管理员', '/avatar2.jpg', 'test', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', 1, '123456789', '192.168.0.1', '2022-03-31 10:00:00.000000', 0, 0, 417244902117446, '2022-03-31 10:00:00.000000', NULL, NULL, 417244902117458, 417244902119463, 0);
INSERT INTO `sys_user` VALUES (417244902117446, '系统管理员', '/avatar1.jpg', 'admin', 'ADCD7048512E64B48DA55B027577886EE5A36350', 1, '15675117404', '192.168.0.2', '2022-03-31 11:00:00.000000', 0, 0, 417244902117446, '2022-03-31 11:00:00.000000', 417244902117446, '2023-07-25 09:41:45.085217', 417244902117458, 417244902119463, 2);
INSERT INTO `sys_user` VALUES (417244902117456, 'Sarah Lee', '', 'humin', 'ADCD7048512E64B48DA55B027577886EE5A36350', 1, '111111111', '192.168.0.50', '2022-03-31 23:00:00.000000', 1, 0, 417244902117446, '2022-03-31 23:00:00.000000', NULL, NULL, 417244902117458, 417244902119462, 0);

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
) ENGINE = InnoDB AUTO_INCREMENT = 417244902194816 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统用户权限表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user_role
-- ----------------------------
INSERT INTO `sys_user_role` VALUES (417244902194813, 417244902117446, 417244902134579, 0, NULL, '2023-05-30 21:47:54.000000');
INSERT INTO `sys_user_role` VALUES (417244902194814, 417244902117445, 424046126026821, 0, NULL, '2023-05-30 21:47:54.000000');
INSERT INTO `sys_user_role` VALUES (417244902194815, 417244902117446, 424046126026821, 0, NULL, '2023-05-30 21:47:54.000000');

SET FOREIGN_KEY_CHECKS = 1;
