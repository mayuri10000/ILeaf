using ILeaf.Repository;
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
            var srv = ObjectFactory.GetInstance<IAppointmentService>();
            
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
