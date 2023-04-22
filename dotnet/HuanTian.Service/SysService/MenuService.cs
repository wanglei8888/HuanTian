﻿using HuanTian.Entities;
using HuanTian.EntityFrameworkCore;
using HuanTian.WebCore;
using Newtonsoft.Json;

namespace HuanTian.Service
{
    public class MenuService : IMneuService, IDynamicApiController, IScoped
    {
        private readonly EfSqlContext _mySqlContext;
        public MenuService(EfSqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public async Task<dynamic> GetUserMenu()
        {
            ////获取用户所包含的权限
            //var userRole = await _mySqlContext.SysUserRoleDO.Where(t => t.UserId == userId).ToListAsync();
            //var roleMneu = await _mySqlContext.SysMneuRoleDO.Where(t => userRole.Select(s => s.Id).Contains(t.RoleId)).ToListAsync();
            //var userMenu = await _mySqlContext.SysMenuDO.Where(t => roleMneu.Select(s => s.MenuId).Contains(t.Id)).ToListAsync();
            try
            {
                var jsonString = "[{\"name\":\"dashboard\",\"parentId\":0,\"id\":1,\"meta\":{\"title\":\"menu.dashboard\",\"icon\":\"dashboard\",\"show\":true},\"component\":\"RouteView\",\"redirect\":\"/dashboard/workplace\"},{\"name\":\"workplace\",\"parentId\":1,\"id\":7,\"meta\":{\"title\":\"menu.dashboard.monitor\",\"show\":true},\"component\":\"Workplace\"},{\"name\":\"monitor\",\"path\":\"https://www.baidu.com/\",\"parentId\":1,\"id\":3,\"meta\":{\"title\":\"menu.dashboard.workplace\",\"target\":\"_blank\",\"show\":true}},{\"name\":\"Analysis\",\"parentId\":1,\"id\":2,\"meta\":{\"title\":\"menu.dashboard.analysis\",\"show\":true},\"component\":\"Analysis\",\"path\":\"/dashboard/analysis\"},{\"name\":\"form\",\"parentId\":0,\"id\":10,\"meta\":{\"icon\":\"form\",\"title\":\"menu.form\"},\"redirect\":\"/form/base-form\",\"component\":\"RouteView\"},{\"name\":\"basic-form\",\"parentId\":10,\"id\":6,\"meta\":{\"title\":\"menu.form.basic-form\"},\"component\":\"BasicForm\"},{\"name\":\"step-form\",\"parentId\":10,\"id\":5,\"meta\":{\"title\":\"menu.form.step-form\"},\"component\":\"StepForm\"},{\"name\":\"advanced-form\",\"parentId\":10,\"id\":4,\"meta\":{\"title\":\"menu.form.advanced-form\"},\"component\":\"AdvanceForm\"},{\"name\":\"list\",\"parentId\":0,\"id\":10010,\"meta\":{\"icon\":\"table\",\"title\":\"menu.list\",\"show\":true},\"redirect\":\"/list/table-list\",\"component\":\"RouteView\"},{\"name\":\"table-list\",\"parentId\":10010,\"id\":10011,\"path\":\"/list/table-list/:pageNo([1-9]\\\\d*)?\",\"meta\":{\"title\":\"menu.list.table-list\",\"show\":true},\"component\":\"TableList\"},{\"name\":\"basic-list\",\"parentId\":10010,\"id\":10012,\"meta\":{\"title\":\"menu.list.basic-list\",\"show\":true},\"component\":\"StandardList\"},{\"name\":\"card\",\"parentId\":10010,\"id\":10013,\"meta\":{\"title\":\"menu.list.card-list\",\"show\":true},\"component\":\"CardList\"},{\"name\":\"search\",\"parentId\":10010,\"id\":10014,\"meta\":{\"title\":\"menu.list.search-list\",\"show\":true},\"redirect\":\"/list/search/article\",\"component\":\"SearchLayout\"},{\"name\":\"article\",\"parentId\":10014,\"id\":10015,\"meta\":{\"title\":\"menu.list.search-list.articles\",\"show\":true},\"component\":\"SearchArticles\"},{\"name\":\"project\",\"parentId\":10014,\"id\":10016,\"meta\":{\"title\":\"menu.list.search-list.projects\",\"show\":true},\"component\":\"SearchProjects\"},{\"name\":\"application\",\"parentId\":10014,\"id\":10017,\"meta\":{\"title\":\"menu.list.search-list.applications\",\"show\":true},\"component\":\"SearchApplications\"},{\"name\":\"profile\",\"parentId\":0,\"id\":10018,\"meta\":{\"title\":\"menu.profile\",\"icon\":\"profile\",\"show\":true},\"redirect\":\"/profile/basic\",\"component\":\"RouteView\"},{\"name\":\"basic\",\"parentId\":10018,\"id\":10019,\"meta\":{\"title\":\"menu.profile.basic\",\"show\":true},\"component\":\"ProfileBasic\"},{\"name\":\"advanced\",\"parentId\":10018,\"id\":10020,\"meta\":{\"title\":\"menu.profile.advanced\",\"show\":true},\"component\":\"ProfileAdvanced\"},{\"name\":\"result\",\"parentId\":0,\"id\":10021,\"meta\":{\"title\":\"menu.result\",\"icon\":\"check-circle-o\",\"show\":true},\"redirect\":\"/result/success\",\"component\":\"PageView\"},{\"name\":\"success\",\"parentId\":10021,\"id\":10022,\"meta\":{\"title\":\"menu.result.success\",\"hiddenHeaderContent\":true,\"show\":true},\"component\":\"ResultSuccess\"},{\"name\":\"fail\",\"parentId\":10021,\"id\":10023,\"meta\":{\"title\":\"menu.result.fail\",\"hiddenHeaderContent\":true,\"show\":true},\"component\":\"ResultFail\"},{\"name\":\"exception\",\"parentId\":0,\"id\":10024,\"meta\":{\"title\":\"menu.exception\",\"icon\":\"warning\",\"show\":true},\"redirect\":\"/exception/403\",\"component\":\"RouteView\"},{\"name\":\"403\",\"parentId\":10024,\"id\":10025,\"meta\":{\"title\":\"menu.exception.not-permission\",\"show\":true},\"component\":\"Exception403\"},{\"name\":\"404\",\"parentId\":10024,\"id\":10026,\"meta\":{\"title\":\"menu.exception.not-find\",\"show\":true},\"component\":\"Exception404\"},{\"name\":\"500\",\"parentId\":10024,\"id\":10027,\"meta\":{\"title\":\"menu.exception.server-error\",\"show\":true},\"component\":\"Exception500\"},{\"name\":\"account\",\"parentId\":0,\"id\":10028,\"meta\":{\"title\":\"menu.account\",\"icon\":\"user\",\"show\":true},\"redirect\":\"/account/center\",\"component\":\"RouteView\"},{\"name\":\"center\",\"parentId\":10028,\"id\":10029,\"meta\":{\"title\":\"menu.account.center\",\"show\":true},\"component\":\"AccountCenter\"},{\"name\":\"settings\",\"parentId\":10028,\"id\":10030,\"meta\":{\"title\":\"menu.account.settings\",\"hideHeader\":true,\"hideChildren\":true,\"show\":true},\"redirect\":\"/account/settings/basic\",\"component\":\"AccountSettings\"},{\"name\":\"BasicSettings\",\"path\":\"/account/settings/basic\",\"parentId\":10030,\"id\":10031,\"meta\":{\"title\":\"account.settings.menuMap.basic\",\"show\":false},\"component\":\"BasicSetting\"},{\"name\":\"SecuritySettings\",\"path\":\"/account/settings/security\",\"parentId\":10030,\"id\":10032,\"meta\":{\"title\":\"account.settings.menuMap.security\",\"show\":false},\"component\":\"SecuritySettings\"},{\"name\":\"CustomSettings\",\"path\":\"/account/settings/custom\",\"parentId\":10030,\"id\":10033,\"meta\":{\"title\":\"account.settings.menuMap.custom\",\"show\":false},\"component\":\"CustomSettings\"},{\"name\":\"BindingSettings\",\"path\":\"/account/settings/binding\",\"parentId\":10030,\"id\":10034,\"meta\":{\"title\":\"account.settings.menuMap.binding\",\"show\":false},\"component\":\"BindingSettings\"},{\"name\":\"NotificationSettings\",\"path\":\"/account/settings/notification\",\"parentId\":10030,\"id\":10034,\"meta\":{\"title\":\"account.settings.menuMap.notification\",\"show\":false},\"component\":\"NotificationSettings\"}]";
                var userInfo = JsonConvert.DeserializeObject<List<MenuItem>>(jsonString);

                return userInfo;
            }
            catch (Exception ex)
            {

                throw;
            }
           



        }
    }
}