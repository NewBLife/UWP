// ***********************************************************************
// FileName:EfDbContext
// Description:
// Project:
// Author:NewBLife
// Created:2016/5/28 21:32:23
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************
using Microsoft.Data.Entity;

namespace SqliteEFCoreDemo.Models
{
    public class EfDbContext : DbContext
    {
        /// <summary>
        /// 注意：如果Student的Model里面没有设置表名将使用Students作为表名
        /// </summary>
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 配置数据库名
            optionsBuilder.UseSqlite("Filename=School.db");
        }
    }
}
