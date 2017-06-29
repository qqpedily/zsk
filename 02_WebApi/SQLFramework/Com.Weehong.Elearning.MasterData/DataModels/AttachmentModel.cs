using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.DataModels
{
    /// <summary>
    /// 文件
    /// </summary>
    [Table("Comm_Attachment")]
    public class AttachmentModel
    {
        public AttachmentModel Clone()
        {
            return (AttachmentModel)this.MemberwiseClone();
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }


        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid? BusinessID { get; set; }

        /// <summary>
        /// 原图大小
        /// </summary>
        public int? FileLength { get; set; }

        /// <summary>
        /// 压缩图
        /// </summary>
        public byte[] CompreesImg { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; set; }
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FileFullPath { get; set; }
   
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        public int ValidStatus { get; set; }
    }
}
