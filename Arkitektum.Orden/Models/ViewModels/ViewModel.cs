using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    
    /// <summary>
    /// Base class for mapping between view models and models.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class ViewModel<TModel, TViewModel>
    {
        public abstract TViewModel MapToViewModel(TModel input);

        public abstract TModel MapToModel(TViewModel input);
        
        public IEnumerable<TViewModel> MapToViewModel(IEnumerable<TModel> inputs)
        {
            var output = new List<TViewModel>();
            foreach(var input in inputs)
            {
                output.Add(MapToViewModel(input));
            }
            return output;
        }

        public IEnumerable<TModel> MapToModel(IEnumerable<TViewModel> inputs)
        {
            var output = new List<TModel>();
            foreach(var input in inputs)
            {
                output.Add(MapToModel(input));
            }
            return output;
        }
    }
}