
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.Common
{
    /// <summary>
    /// 模板字段
    /// </summary>
    public enum EnumTemplateField
    {
        /// <summary>
        /// 题名
        /// </summary>
        [Display(Name = "题名")]
        题名 = 11,
        /// <summary>
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        作者 = 13,
        /// <summary>
        /// 学科分类
        /// </summary>
        [Display(Name = "学科分类")]
        学科分类 = 21,
        /// <summary>
        /// 关键词
        /// </summary>
        [Display(Name = "关键词")]
        关键词 = 22,
        /// <summary>
        /// 英文摘要
        /// </summary>
        [Display(Name = "英文摘要")]
        英文摘要 = 25,
        /// <summary>
        /// 中文摘要
        /// </summary>
        [Display(Name = "中文摘要")]
        中文摘要 = 26,
        /// <summary>
        /// 版本
        /// </summary>
        [Display(Name = "版本")]
        版本 = 30,
        /// <summary>
        /// 合作状况
        /// </summary>
        [Display(Name = "合作状况")]
        合作状况 = 33,

        //[Display(Name = "项目资助者")]
        //项目资助者 = 34,

        /// <summary>
        /// 作者部门
        /// </summary>
        [Display(Name = "作者部门")]
        作者部门 = 39,
        /// <summary>
        /// 项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        项目名称 = 40,
        /// <summary>
        /// 出处(刊名)
        /// </summary>
        [Display(Name = "出处(刊名)")]
        出处刊名 = 52,
        /// <summary>
        /// 卷号
        /// </summary>
        [Display(Name = "卷号")]
        卷号 = 53,
        /// <summary>
        /// 期号
        /// </summary>
        [Display(Name = "期号")]
        期号 = 54,
        /// <summary>
        /// 页码
        /// </summary>
        [Display(Name = "页码")]
        页码 = 55,
        /// <summary>
        /// 收录类别
        /// </summary>
        [Display(Name = "收录类别")]
        收录类别 = 58,
        /// <summary>
        /// 发表日期
        /// </summary>
        [Display(Name = "发表日期")]
        发表日期 = 63,
        /// <summary>
        ///语种 
        /// </summary>
        [Display(Name = "语种")]
        语种 = 69,
        /// <summary>
        /// 项目编号
        /// </summary>
        [Display(Name = "项目编号")]
        项目编号 = 89,
        /// <summary>
        /// 产权排序
        /// </summary>
        [Display(Name = "产权排序")]
        产权排序 = 100,
        /// <summary>
        /// DOI标识
        /// </summary>
        [Display(Name = "DOI标识")]
        DOI标识 = 105,
        /// <summary>
        /// 作者单位
        /// </summary>
        [Display(Name = "作者单位")]
        作者单位 = 106,
        /// <summary>
        /// WOS记录号
        /// </summary>
        [Display(Name = "WOS记录号")]
        WOS记录号 = 108,
        /// <summary>
        /// 项目资助者
        /// </summary>
        [Display(Name = "项目资助者")]
        项目资助者 = 111,
        /// <summary>
        /// 类目[WOS]
        /// </summary>
        [Display(Name = "类目[WOS]")]
        类目WOS = 114,
        /// <summary>
        /// 研究领域[WOS]
        /// </summary>
        [Display(Name = "研究领域[WOS]")]
        研究领域WOS = 115,
        /// <summary>
        /// 关键词[WOS]
        /// </summary>
        [Display(Name = "关键词[WOS]")]
        关键词WOS = 116,
        /// <summary>
        /// 文章类型
        /// </summary>
        [Display(Name = "文章类型")]
        文章类型 = 121,
        /// <summary>
        /// 机构院系
        /// </summary>
        [Display(Name = "机构院系")]
        机构院系 = 999
    }
}

