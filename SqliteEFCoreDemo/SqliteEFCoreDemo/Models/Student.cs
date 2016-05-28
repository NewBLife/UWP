// ***********************************************************************
// FileName:Student
// Description:
// Project:
// Author:NewBLife
// Created:2016/5/28 21:23:45
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqliteEFCoreDemo.Models
{
    /// <summary>
    /// 设置数据库表名
    /// </summary>
    [Table(name: "Student")]
    public class Student
    {
        [Required]
        public string ID { get; set; }

        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}
