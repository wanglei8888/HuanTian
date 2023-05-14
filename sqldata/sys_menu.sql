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

 Date: 14/05/2023 17:38:39
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

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
  `redirect` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '菜单跳转地址',
  `hide_children` tinyint(1) NULL DEFAULT NULL COMMENT '隐藏子类',
  `component` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '菜单类型',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 417244902117887 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES (417244902117886, 0, 'dashboard', NULL, 'menu.dashboard', NULL, 'dashboard', 1, '/dashboard/workplace', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117887, 417244902117886, 'Analysis', '/dashboard/analysis', 'menu.dashboard.analysis', NULL, '', 1, '', 0, 'Analysis', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117889, 417244902117886, 'monitor', 'https://www.baidu.com/', 'menu.dashboard.workplace', NULL, '', 1, '', 0, '', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117890, 417244902117894, 'advanced-form', NULL, 'menu.form.advanced-form', NULL, '', 1, '', 0, 'AdvanceForm', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117891, 417244902117894, 'step-form', NULL, 'menu.form.step-form', NULL, '', 1, '', 0, 'StepForm', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117892, 417244902117894, 'basic-form', NULL, 'menu.form.basic-form', NULL, '', 1, '', 0, 'BasicForm', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117893, 417244902117886, 'workplace', NULL, 'menu.dashboard.monitor', NULL, '', 1, '', 0, 'Workplace', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117894, 0, 'form', NULL, 'menu.form', NULL, 'form', 1, '/form/base-form', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117895, 417244902117896, 'settings', NULL, 'menu.account.settings', NULL, '', 1, '/account/settings/basic', 0, 'AccountSettings', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117896, 0, 'account', NULL, 'menu.account', NULL, 'user', 1, '/account/center', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117897, 0, 'exception', NULL, 'menu.exception', NULL, 'warning', 1, '/exception/403', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117898, 0, 'userInfo', NULL, 'menu.userInfo', NULL, 'table', 1, '/userInfo/userList', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117899, 0, 'result', NULL, 'menu.result', NULL, 'check-circle-o', 1, '/result/success', 0, 'PageView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117900, 0, 'other', NULL, '其他组件', NULL, 'slack', 1, '/other/icon-selector', 0, 'PageView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117901, 417244902117902, 'search', NULL, 'menu.list.search-list', NULL, '', 1, '/list/search/article', 0, 'SearchLayout', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117902, 0, 'list', NULL, 'menu.list', NULL, 'table', 1, '/list/table-list', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117903, 0, 'profile', NULL, 'menu.profile', NULL, 'profile', 1, '/profile/basic', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117904, 417244902117900, 'list', NULL, '业务布局', NULL, '', 1, '/other/list/tree-list', 0, 'RouteView', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117905, 417244902117898, 'tree-list', NULL, '树目录表格', NULL, '', 1, '', 0, 'TreeList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117906, 417244902117898, 'user-list', NULL, '用户列表', NULL, '', 1, '', 0, 'UserList2', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117907, 417244902117898, 'role-list', NULL, '角色列表', NULL, '', 1, '', 0, 'RoleList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117908, 417244902117898, 'permission-list', NULL, '权限列表', NULL, '', 1, '', 0, 'PermissionList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117909, 417244902117898, 'userList', NULL, 'menu.userInfo.userList', NULL, '', 1, '', 0, 'UserList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117910, 417244902117904, 'edit-table', NULL, '内联编辑表格', NULL, '', 1, '', 0, 'TableInnerEditList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117911, 417244902117902, 'table-list', '/list/table-list/:pageNo([1-9]\\\\d*)?', 'menu.list.table-list', NULL, '', 1, '', 0, 'TableList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117912, 417244902117902, 'basic-list', NULL, 'menu.list.basic-list', NULL, '', 1, '', 0, 'StandardList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117913, 417244902117902, 'card', NULL, 'menu.list.card-list', NULL, '', 1, '', 0, 'CardList', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117914, 417244902117901, 'article', NULL, 'menu.list.search-list.articles', NULL, '', 1, '', 0, 'SearchArticles', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117915, 417244902117901, 'project', NULL, 'menu.list.search-list.projects', NULL, '', 1, '', 0, 'SearchProjects', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117916, 417244902117901, 'application', NULL, 'menu.list.search-list.applications', NULL, '', 1, '', 0, 'SearchApplications', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117917, 417244902117903, 'basic', NULL, 'menu.profile.basic', NULL, '', 1, '', 0, 'ProfileBasic', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117918, 417244902117903, 'advanced', NULL, 'menu.profile.advanced', NULL, '', 1, '', 0, 'ProfileAdvanced', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117919, 417244902117899, 'success', NULL, 'menu.result.success', NULL, '', 1, '', 0, 'ResultSuccess', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117920, 417244902117899, 'fail', NULL, 'menu.result.fail', NULL, '', 1, '', 0, 'ResultFail', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117921, 417244902117897, '403', NULL, 'menu.exception.not-permission', NULL, '', 1, '', 0, 'Exception403', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117922, 417244902117897, '404', NULL, 'menu.exception.not-find', NULL, '', 1, '', 0, 'Exception404', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117923, 417244902117896, 'center', NULL, 'menu.account.center', NULL, '', 1, '', 0, 'AccountCenter', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117924, 417244902117895, 'BasicSettings', '/account/settings/basic', 'account.settings.menuMap.basic', NULL, '', 1, '', 0, 'BasicSetting', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117925, 417244902117895, 'SecuritySettings', '/account/settings/security', 'account.settings.menuMap.security', NULL, '', 1, '', 0, 'SecuritySettings', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117926, 417244902117895, 'CustomSettings', '/account/settings/custom', 'account.settings.menuMap.custom', NULL, '', 1, '', 0, 'CustomSettings', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117927, 417244902117895, 'BindingSettings', '/account/settings/binding', 'account.settings.menuMap.binding', NULL, '', 1, '', 0, 'BindingSettings', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117928, 417244902117895, 'NotificationSettings', '/account/settings/notification', 'account.settings.menuMap.notification', NULL, '', 1, '', 0, 'NotificationSettings', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117967, 417244902117897, '500', NULL, 'menu.exception.server-error', NULL, '', 1, '', 0, 'Exception500', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES (417244902117976, 417244902117900, 'icon-selector', NULL, 'menu.other.IconSelector', NULL, '', 1, '', 0, 'IconSelectorView', 0, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
