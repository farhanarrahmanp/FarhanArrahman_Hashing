using System;
using API.Repositories.Interface;
using API.Context;
using API.Models;
using System.Linq;
using System.Collections.Generic;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRepository : IAccountRepository
    {
        /*
         * Login
         * Register (tambah sendiri)
         * Change Password (tambah sendiri)
         * Forgot Password (tambah sendiri)
         */

        // requirement login -> email & password
        // response login -> id karyawan, fullname, email, role (ini gak masuk evaluasi (jwt -> json web token))

        // sekarang pakai viewmodels untuk return keluar

        MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ResponseLogin Login(Login login)
        {
            // userroles -> roles -> users -> employees
            Console.WriteLine("Masuk response login...");
            correctHash = BCrypt.Net.BCrypt.HashPassword(login.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x =>
                    x.User.Employee.Email.Equals(login.Email) &&
                    x.User.Password.Equals(BCrypt.Net.BCrypt.Verify(login.Password, correctHash)));
            Console.WriteLine("data myContext: - "+login.Email);
            Console.WriteLine("data myContext: - " + login.Password);
            Console.WriteLine("Berhasil myContextUserRoles!");
            if (data != null)
            {
                Console.WriteLine("data tidak null!");
                ResponseLogin responseLogin = new ResponseLogin()
                {
                    Id = data.User.Id,
                    FullName = data.User.Employee.FullName,
                    Email = data.User.Employee.Email,
                    Role = data.Role.Name
                };
                Console.WriteLine("datanya: - "+responseLogin.Id);
                Console.WriteLine("- " + responseLogin.FullName);
                Console.WriteLine("- " + responseLogin.Email);
                Console.WriteLine("- " + responseLogin.Role);
                return responseLogin;
            }
            return null;

            // setelah berjalan, bisa dilakukan di mvc.
        }

        public int Register(Register register)
        {
            // default role
            var role = myContext.Roles.Min(x => x.Name);
            var roleDefault = myContext.Roles.SingleOrDefault(x => x.Name.Equals(role)).Id;

            // is duplicate
            var checking = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email));
            if (checking != null)
            {
                return 0;
            }

            Employee employee = new Employee(register.FullName, register.Email);
            
            // insert employee to database
            myContext.Employees.Add(employee);
            var registeringEmployee = myContext.SaveChanges();

            // if inserted
            if (registeringEmployee > 0)
            {
                // assigning role
                var registeredEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;

                User user = new User()
                {
                    Id = registeredEmployee,
                    Password = BCrypt.Net.BCrypt.HashPassword(register.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
                };

                try
                {
                    myContext.Users.Add(user);
                    var registeringUser = myContext.SaveChanges();

                    if (registeringUser > 0)
                    {
                        UserRole userRole = new UserRole();
                        userRole.RoleId = roleDefault;
                        userRole.UserId = registeredEmployee;

                        // assigning role
                        myContext.UserRoles.Add(userRole);
                        var assigningRole = myContext.SaveChanges();
                        if (assigningRole > 0)
                        {
                            return assigningRole;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // if have a problem 
                    var dataEmployee = myContext.Employees.Find(registeredEmployee);
                    myContext.Employees.Remove(dataEmployee);
                    var deletingEmployee = myContext.SaveChanges();
                }
            }
            return 0;
        }

        public int ChangePassword(string email, string oldPassword, string newPassword)
        {
            var data = myContext.Users.Include(x => x.Employee).SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data == null)
            {
                return 0;
            }
            correctHash = BCrypt.Net.BCrypt.HashPassword(oldPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
            if (data.Password == correctHash)
            {
                data.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
            }
            myContext.Entry(data).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                return result;
            return 0;
        }

        public int ForgotPassword(string email)
        {
            var data = myContext.Users.Include(x => x.Employee).SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data == null)
            {
                return 0;
            }
            data.Password = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd)", BCrypt.Net.BCrypt.GenerateSalt(12));
            myContext.Entry(data).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                return result;
            return 0;
        }
    }
}
