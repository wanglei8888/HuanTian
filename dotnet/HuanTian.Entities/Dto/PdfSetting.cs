#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Entities.Dto
 * 唯一标识：e3aa690f-9b23-4531-8760-47e4b4bf7d47
 * 文件名：PdfSetting
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/17 14:52:09
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Entities
{
    /// <summary>
    /// PDF 设置属性
    /// </summary>
    public class PdfSetting
    {
        public PdfSetting(int width, int height, PdfMargin pdfMargin)
        {
            Width = width;
            Height = height;
            Margin = pdfMargin;
        }
        public PdfSetting(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public PdfMargin Margin { get; set; }
    }
    /// <summary>
    /// PDF 外边距设置
    /// </summary>
    public class PdfMargin
    {
        public PdfMargin(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
