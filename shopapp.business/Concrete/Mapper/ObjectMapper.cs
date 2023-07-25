using AutoMapper;

namespace shopapp.business.Concrete.Mapper
{
    internal class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<DTOMapper>(); });
            return config.CreateMapper();

        });

        public static IMapper Mapper => lazy.Value;
    }
}
