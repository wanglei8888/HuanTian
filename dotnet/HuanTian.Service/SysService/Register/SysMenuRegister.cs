#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.IRegister
 * 唯一标识：b3257fff-9527-40b6-b72c-c4d1f28b6aff
 * 文件名：SysMenuRegister
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/14 11:37:11
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using HuanTian.Entities;
using Mapster;


namespace HuanTian.Service
{
    /// <summary>
    /// SysMenuRegister 的摘要说明
    /// </summary>
    public class SysMenuRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SysMenuDO, SysMenuOutput>()
                .Map(dest => dest.Meta.Icon, src => src.Icon)
                .Map(dest => dest.Meta.Title, src => src.Title)
                .Map(dest => dest.Meta.Show, src => src.Show)
                .Map(dest => dest.Meta.HideChildren, src => src.HideChildren);
        }
    }
}