﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace KingOrder.CrossCutting.IoC.Helpers
{
    public class AssemblyHelper
    {
        #region constants

        private const string _assemblyType = "*.dll";

        #endregion

        #region pivate members

        private static AssemblyHelper _instance;

        private static List<Assembly> _assemblies;

        #endregion

        #region public methods implementations

        public static AssemblyHelper Instance()
        {
            if (_instance == null)
                _instance = new AssemblyHelper();

            return _instance;
        }

        public List<Assembly> GetAllAssemblies()
        {
            if (_assemblies != null)
                return _assemblies;

            _assemblies = new List<Assembly>();

            foreach (string assemblyPath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, _assemblyType, SearchOption.TopDirectoryOnly))
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);

                if (_assemblies.Find(a => a == assembly) != null)
                    continue;

                _assemblies.Add(assembly);
            }

            if (_assemblies.Any())
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name.Split('.').FirstOrDefault();
                _assemblies = _assemblies.Where(c => c.FullName.Contains(assemblyName)).ToList();
            }

            return _assemblies;
        }

        #endregion
    }
}
