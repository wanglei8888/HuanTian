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

 Date: 26/05/2023 23:00:13
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
  `redirect` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单跳转地址',
  `hide_children` tinyint(1) NULL DEFAULT NULL COMMENT '隐藏子类',
  `component` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单前端绑定值',
  `deleted` tinyint(1) NOT NULL COMMENT '软删除',
  `create_by` bigint(20) NULL DEFAULT NULL COMMENT '创建人',
  `create_on` datetime(6) NULL DEFAULT NULL COMMENT '创建时间',
  `menu_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单类型',
  `order` int(11) NULL DEFAULT NULL COMMENT '排序,越大越靠前',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 421232401293382 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统菜单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES (417244902114614, 0, '用户管理', NULL, 'menu.userManage', NULL, 'team', 1, '/userManage/userList', 1, 'RouteView', 0, NULL, NULL, 'Business', 800);
INSERT INTO `sys_menu` VALUES (417244902117886, 0, 'dashboard', NULL, 'menu.dashboard', NULL, 'dashboard', 1, '/dashboard/workplace', 0, 'RouteView', 0, NULL, NULL, 'Business', 1000);
INSERT INTO `sys_menu` VALUES (417244902117887, 417244902117886, 'Analysis', '/dashboard/analysis', 'menu.dashboard.analysis', NULL, 'none', 1, '', 0, 'Analysis', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117889, 417244902117886, 'monitor', 'https://www.baidu.com/', 'menu.dashboard.workplace', NULL, 'none', 1, '', 0, '', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117890, 417244902117894, 'advanced-form', NULL, 'menu.form.advanced-form', NULL, 'none', 1, '', 0, 'AdvanceForm', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117891, 417244902117894, 'step-form', NULL, 'menu.form.step-form', NULL, 'none', 1, '', 0, 'StepForm', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117892, 417244902117894, 'basic-form', NULL, 'menu.form.basic-form', NULL, 'none', 1, '', 0, 'BasicForm', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117893, 417244902117886, 'workplace', NULL, 'menu.dashboard.monitor', NULL, 'none', 1, '', 0, 'Workplace', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117894, 0, 'form', NULL, 'menu.form', NULL, 'form', 1, '/form/base-form', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117895, 417244902117896, 'settings', NULL, 'menu.account.settings', NULL, 'none', 1, '/account/settings/basic', 0, 'AccountSettings', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117896, 0, 'account', NULL, 'menu.account', NULL, 'user', 1, '/account/center', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117897, 0, 'exception', NULL, 'menu.exception', NULL, 'warning', 1, '/exception/403', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117898, 0, '系统管理', NULL, 'menu.sysManage', NULL, 'tool', 1, '/sysManage', 0, 'RouteView', 0, NULL, NULL, 'Business', 900);
INSERT INTO `sys_menu` VALUES (417244902117899, 0, 'result', NULL, 'menu.result', NULL, 'check-circle-o', 1, '/result/success', 0, 'PageView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117900, 0, 'other', NULL, '其他组件', NULL, 'slack', 1, '/other/icon-selector', 0, 'PageView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117901, 417244902117902, 'search', NULL, 'menu.list.search-list', NULL, 'none', 1, '/list/search/article', 0, 'SearchLayout', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117902, 0, 'list', NULL, 'menu.list', NULL, 'table', 1, '/list/table-list', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117903, 0, 'profile', NULL, 'menu.profile', NULL, 'profile', 1, '/profile/basic', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117904, 417244902117900, 'list', NULL, '业务布局', NULL, 'none', 1, '/other/list/tree-list', 0, 'RouteView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117905, 417244902117898, 'tree-list', '/sysManage/tree-list', '树目录表格', NULL, 'none', 1, '', 0, 'TreeList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117906, 417244902117898, 'user-list', '/sysManage/user-list', '用户列表', NULL, 'none', 1, '', 0, 'UserList2', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117907, 417244902117898, 'role-list', '/sysManage/role-list', '角色列表', NULL, 'none', 1, '', 0, 'RoleList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117908, 417244902117898, 'permission-list', '/sysManage/permission-list', '权限列表', NULL, 'none', 1, '', 0, 'PermissionList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117909, 417244902114614, 'userList', '/userManage/userList', 'menu.userInfo.userList', NULL, 'none', 1, '', 0, 'UserList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117910, 417244902117904, 'edit-table', NULL, '内联编辑表格', NULL, 'none', 1, '', 0, 'TableInnerEditList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117911, 417244902117902, 'table-list', '/list/table-list/:pageNo([1-9]\\\\d*)?', 'menu.list.table-list', NULL, 'none', 1, '', 0, 'TableList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117912, 417244902117902, 'basic-list', NULL, 'menu.list.basic-list', NULL, 'none', 1, '', 0, 'StandardList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117913, 417244902117902, 'card', NULL, 'menu.list.card-list', NULL, 'none', 1, '', 0, 'CardList', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117914, 417244902117901, 'article', NULL, 'menu.list.search-list.articles', NULL, 'none', 1, '', 0, 'SearchArticles', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117915, 417244902117901, 'project', NULL, 'menu.list.search-list.projects', NULL, 'none', 1, '', 0, 'SearchProjects', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117916, 417244902117901, 'application', NULL, 'menu.list.search-list.applications', NULL, 'none', 1, '', 0, 'SearchApplications', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117917, 417244902117903, 'basic', NULL, 'menu.profile.basic', NULL, 'none', 1, '', 0, 'ProfileBasic', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117918, 417244902117903, 'advanced', NULL, 'menu.profile.advanced', NULL, 'none', 1, '', 0, 'ProfileAdvanced', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117919, 417244902117899, 'success', NULL, 'menu.result.success', NULL, 'none', 1, '', 0, 'ResultSuccess', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117920, 417244902117899, 'fail', NULL, 'menu.result.fail', NULL, 'none', 1, '', 0, 'ResultFail', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117921, 417244902117897, '403', NULL, 'menu.exception.not-permission', NULL, 'none', 1, '', 0, 'Exception403', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117922, 417244902117897, '404', NULL, 'menu.exception.not-find', NULL, 'none', 1, '', 0, 'Exception404', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117923, 417244902117896, 'center', NULL, 'menu.account.center', NULL, 'none', 1, '', 0, 'AccountCenter', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117924, 417244902117895, 'BasicSettings', '/account/settings/basic', 'account.settings.menuMap.basic', NULL, 'none', 1, '', 0, 'BasicSetting', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117925, 417244902117895, 'SecuritySettings', '/account/settings/security', 'account.settings.menuMap.security', NULL, 'none', 1, '', 0, 'SecuritySettings', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117926, 417244902117895, 'CustomSettings', '/account/settings/custom', 'account.settings.menuMap.custom', NULL, 'none', 1, '', 0, 'CustomSettings', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117927, 417244902117895, 'BindingSettings', '/account/settings/binding', 'account.settings.menuMap.binding', NULL, 'none', 1, '', 0, 'BindingSettings', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117928, 417244902117895, 'NotificationSettings', '/account/settings/notification', 'account.settings.menuMap.notification', NULL, 'none', 1, '', 0, 'NotificationSettings', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117967, 417244902117897, 'error', '', 'menu.exception.server-error', NULL, 'none', 1, '', 0, 'Exception500', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117976, 417244902117900, 'Icon选择', '', 'menu.other.IconSelector', NULL, 'none', 1, '', 0, 'IconSelectorView', 0, NULL, NULL, 'Business', 0);
INSERT INTO `sys_menu` VALUES (417244902117999, 417244902117898, '菜单列表', '/sysManage/menuList', 'menu.sysManage.menuList', NULL, 'none', 1, '', 0, 'MenuList', 0, NULL, NULL, 'Business', 50);
INSERT INTO `sys_menu` VALUES (421207777439813, 0, '123', '123', '123', 1, 'diff', 1, '123', NULL, 'RouteView', 0, 417244902117446, '2023-05-25 15:18:52.059000', 'Business', 50);
INSERT INTO `sys_menu` VALUES (421231510339653, 421207777439813, '1231121', '123', '123', 0, 'none', 1, NULL, NULL, '123', 0, 417244902117446, '2023-05-25 16:55:26.224440', 'Business', NULL);
INSERT INTO `sys_menu` VALUES (421231777194053, 421207777439813, 'asdas123', '13123', '123123', 0, 'none', 1, NULL, NULL, '123', 0, 417244902117446, '2023-05-25 16:56:31.374154', 'Business', NULL);
INSERT INTO `sys_menu` VALUES (421232401293381, 421207777439813, '123asdasd', '123123', '1231', 0, 'none', 1, NULL, NULL, '123', 0, 417244902117446, '2023-05-25 16:59:03.742138', 'Business', NULL);

SET FOREIGN_KEY_CHECKS = 1;
