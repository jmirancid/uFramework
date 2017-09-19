using System;
using System.Configuration;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace uFramework.Repositories
{
    public class Factory
    {
        private static Lazy<UnityContainer> uContainer =
            new Lazy<UnityContainer>(() =>
            {
                return ConfigureContainer();
            });

        public static I Resolve<I>()
        {
            try
            {
                return uContainer.Value.Resolve<I>();
            }
            catch (Exception ex)
            {
                string message = string.Format("Error al intentar resolver el repositorio para la interfaz {0}", typeof(I).Name);

                StringBuilder detail = new StringBuilder();
                detail.AppendLine("Error al resolver el repositorio. Verifique lo siguiente:");
                detail.AppendLine("- Existencia del archivo de unity 'Unity.Repositories.config'.");
                detail.AppendLine(string.Format("- Existencia de la entrada correspondiente a la interfaz {0} en dicho archivo.", typeof(I).Name));
                detail.AppendLine("- Existencia del archivo binario de repositorio correspondiente 'Institucional.Repositories'.");

                throw new Exception(detail.ToString(), ex);
            }
        }

        private static UnityContainer ConfigureContainer()
        {
            var instance = new UnityContainer();

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(instance);

            return instance;
        }
    }
}
