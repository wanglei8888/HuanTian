#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.Inteface
 * 唯一标识：d205f0a1-2e57-498b-9fd7-6139b68bb14b
 * 文件名：IGenerateFiles
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/17 14:49:11
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

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 文件生成服务
    /// </summary>
    public interface IGenerateFilesService
    {
        /// <summary>
        /// 根据内容生成二维码图片
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        byte[] RenderQrCode(string value);

        /// <summary>
        /// 基于模板生成PDF文件
        /// </summary>
        /// <param name="templateAddress">模板地址</param>
        /// <param name="value">数据集model</param>
		/// <param name="setInfo">PDF 设置信息</param>
        /// <returns></returns>
        byte[] RenderTemplatePdf<TClass>(string templateAddress, TClass value, PdfSetting setInfo = default);

        /// <summary>
        /// 基于模板生成PDF文件,集合生成多个pdf(非表格)
        /// </summary>
        /// <param name="templateAddress">模板地址</param>
        /// <param name="value">数据集model</param>
		/// <param name="setInfo">PDF 设置信息</param>
        /// <returns></returns>
        byte[] RenderTemplatePdf<TClass>(string templateAddress, IEnumerable<TClass> value, PdfSetting setInfo = default);
        /// <summary>
        /// 基于模板生成Excel文件 集合生成列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="templateAddress"></param>
        /// <param name="entityList"></param>
        /// <returns></returns>
        byte[] RenderTemplateExcel<TEntity>(string templateAddress, IEnumerable<TEntity> entityList);
        /// <summary>
        /// 基于模板生成Excel文件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="templateAddress"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        byte[] RenderTemplateExcel<TEntity>(string templateAddress, TEntity entity);
    }
}
