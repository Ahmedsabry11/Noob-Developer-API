
using Microsoft.EntityFrameworkCore;
using DomainLayer.Data;
using DomainLayer.Entities;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using Microsoft.AspNetCore.Identity;
using WebTool.Services.Services;
using WebTool.Controllers;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace UnitTesting
{
    public class UnitTest1
    {

        
        //private readonly AuthenticateController ;
        public UnitTest1()
        {
            
        }
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void PassingTest()
        {


            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}

//-------------------------------- corner cases testcases ---------------------------------

//--------------------------------   invalid  testcases   ---------------------------------

//--------------------------------   sucessful testcases  ---------------------------------