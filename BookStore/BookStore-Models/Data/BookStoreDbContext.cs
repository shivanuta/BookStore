﻿using BookStore_Models.DBModels;

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server database
        //    options.UseSqlServer(Configuration.GetConnectionString("BookStore_APIContext"));
        //}

        public DbSet<Users> Users { get; set; }