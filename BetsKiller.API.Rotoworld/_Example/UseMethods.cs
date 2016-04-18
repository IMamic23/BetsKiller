using BetsKiller.API.Rotoworld.Entities;
using BetsKiller.API.Rotoworld.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Rotoworld._Example
{
    public class UseMethods
    {
        public static void Start()
        {
            MethodInjuries methodInjuries = new MethodInjuries();
            List<Injury> injuries = methodInjuries.Get();
        }
    }
}
