using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightside.Common
{
	public class Kernel
	{
		private static StandardKernel kernel;

		public static void Initialize()
		{
			Dispose();
			kernel = new StandardKernel();
		}

		public static bool RequiresInitialization()
		{
			return kernel == null;
		}

		public static void Load(
				INinjectModule module)
		{
			var name = module.GetType().FullName;
			if (kernel.HasModule(name))
			{
				kernel.Unload(name);
			}
			kernel.Load(module);
		}

		public static object Get(
				Type t)
		{
			if (kernel == null)
				return null;
			return kernel.Get(t);
		}

		public static T Get<T>()
				where T : class
		{
			if (kernel == null)
				return null;

			return kernel.Get<T>();
		}

		public static void Dispose()
		{
			if (kernel != null)
			{
				kernel.Dispose();
				kernel = null;
			}
		}
	}
}