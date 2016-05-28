// ***********************************************************************
// FileName:Course
// Description:
// Project:
// Author:NewBLife
// Created:2016/5/28 21:25:32
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqliteEFCoreDemo.Models
{
    /// <summary>
    /// 设置数据库表名
    /// </summary>
    [Table(name: "Course")]
    public class Course
    {
        [Required]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
