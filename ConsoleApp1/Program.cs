using ILeaf.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Initialize(InitializeStructureMap);
            //Console.WriteLine(ObjectFactory.WhatDoIHave());
            var srv = ObjectFactory.GetInstance<ISchoolInfoService>();
            srv.SaveObject(new ILeaf.Core.Models.SchoolInfo()
            {
                SchoolId = 10013,
                SchoolName = "北京邮电大学",
                Province = "北京"
            });

            Console.WriteLine(srv.GetObject(10013).SchoolName);

            Console.WriteLine(srv.GetAllSchoolsInProvince("北京")[0].SchoolName);

            Console.Read();
        }

        private static void InitializeStructureMap(IInitializationExpression x)
        {
            x.Scan(y =>
            {
                y.Assembly("ILeaf.Core");
                y.Assembly("ILeaf.Repository");
                y.Assembly("ILeaf.Service");
                //y.Assembly("Senparc.Models");
                y.WithDefaultConventions();
            });

        }
    }
}
