using DTSMCC_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTSMCC_WebApp.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {
        }

        // Atur connectionString

        // Memasukkan model yang digunakan untuk melakukan operasi CRUD / migrasi

        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Region> Regions { get; set; }

        // package manager (kayak cmd di vs): add-migration initial
        // abangnya pake pesan initial di commit
        // add-migration initial (kalau selain windows, pakai dotnet ef)
        // nanti terbuat code DDL
        // update-database (kalau selain windows, pakai dotnet ef)

        /*
         * Features:
         * - Login
         * - Register
         * - Forgot Password
         * - Change Password
         * - Lockout Account
         * - Password Hash
         * 
         * Tables:
         * - Role
         * - User
         * - UserRole
         * - Claim
         * - UserClaim
         */
    }
}
