using Bridge.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.NET
{
    public class Plugins : IPlugins
    {
        public static IPlugins GetPlugins(ITranslator translator)
        {            
            var path = System.IO.Path.GetDirectoryName(translator.BridgeLocation) + @"\plugins\";
            
            if (!System.IO.Directory.Exists(path)) 
            {
                return new Plugins() { plugins = new IPlugin[0] };
            }

            DirectoryCatalog dirCatalog = new DirectoryCatalog(path, "*.dll");            
            var catalog = new AggregateCatalog(dirCatalog);

            CompositionContainer container = new CompositionContainer(catalog);
            var plugins = new Plugins();
            container.ComposeParts(plugins);

            return plugins;
        }

        [ImportMany]
        private IEnumerable<IPlugin> plugins;

        public IEnumerable<IPlugin> Parts
        {
            get
            {
                return this.plugins;
            }
        }

        public void OnConfigRead(IAssemblyInfo config)
        {
            foreach (var plugin in this.Parts)
            {
                plugin.OnConfigRead(config);
            }
        }
        public void BeforeEmit(IEmitter emitter, ITranslator translator)
        {
            foreach (var plugin in this.Parts)
            {
                plugin.BeforeEmit(emitter, translator);
            }
        }

        public IEnumerable<string> GetConstructorInjectors(IConstructorBlock constructorBlock)
        {
            IEnumerable<string> result = new List<string>();
            foreach (var plugin in this.Parts)
            {
                result = result.Concat(plugin.GetConstructorInjectors(constructorBlock));
            }

            return result;
        }

        public bool HasConstructorInjectors(IConstructorBlock constructorBlock)
        {
            foreach (var plugin in this.Parts)
            {
                if (plugin.HasConstructorInjectors(constructorBlock))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
