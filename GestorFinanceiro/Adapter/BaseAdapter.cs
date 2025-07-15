using System.Runtime.CompilerServices;

namespace GestorFinanceiro.Mapper
{
    public class BaseAdapter<TDto,TModel>
    {


        public IEnumerable<TModel> Map (IEnumerable<TDto> dtos)
        {
            return dtos.Select(Map);
        }
        public TModel Map(TDto dto)
        {
            var model = (TModel)Activator.CreateInstance(typeof(TDto));
            var properties = GetProperties<TModel, TDto>();

            foreach (var propertyName in properties)
            {
                var value = dto.GetType().GetProperty(propertyName)?.GetValue(dto, null);
                model.GetType().GetProperty(propertyName)?.SetValue(model, value, null);
            }

            return dto;
        }

        public TDto Map(TModel model)
        {
            var dto = (TDto)Activator.CreateInstance(typeof(TDto));
            var properties = GetProperties<TDto, TModel>();

            foreach (var propertyName in properties)
            {
                var value = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
                dto.GetType().GetProperty(propertyName)?.SetValue(dto, value, null);
            }

            return dto;
        }

        private static List<string> GetProperties<TFrom, Tto>()
        {
            var from = typeof(TFrom).GetProperties();
            var to = typeof(Tto).GetProperties();
            
            var properties = new List<string>();

            foreach (var property in to) 
            {
                var fromProperty = from.FirstOrDefault(p => p.Name.Equals(property.Name) && p.CanWrite);
                if (fromProperty != null) properties.Add(property.Name);
            }
            return properties;
        }
    }
}
