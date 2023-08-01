using AutoMapper;

namespace shopapp.web.Mapper
{
    internal class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<ModelMapper>(); });
            return config.CreateMapper();

        });

        public static IMapper Mapper => lazy.Value;
    }
}
