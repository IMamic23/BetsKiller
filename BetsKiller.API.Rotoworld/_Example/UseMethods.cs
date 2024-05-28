using BetsKiller.API.Rotoworld.Entities;
using BetsKiller.API.Rotoworld.Methods;
using System.Collections.Generic;

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
