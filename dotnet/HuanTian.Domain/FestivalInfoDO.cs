using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HuanTian.Domain
{
    /// <summary>
    /// 节假日信息表
    /// </summary>
    [Table("festival_info")]
    [Description("节假日信息表")]

    public class FestivalInfoDO
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 节假日所在年份
        /// </summary>
        [Required]
        [Column("festival_year")]
        [Description("节假日所在年份")]
        public int FestivalYear { get; set; }
        /// <summary>
        /// 节假日日期
        /// </summary>
        [Required]
        [Column("festival_date")]
        [Description("节假日日期")]
        public DateTime FestivalDate { get; set; }
        /// <summary>
        /// 节假日日名称
        /// </summary>
        [Required]
        [Column("festival_name")]
        [MaxLength(50)]
        [Description("节假日日名称")]
        public string? FestivalName { get; set; }
        /// <summary>
        /// 节日状态(放假|上班)
        /// </summary>
        [Required]
        [Column("festival_status")]
        [MaxLength(10)]
        [Description("节日状态(放假|上班)")]
        public string? FestivalStatus { get; set; }
    }
}