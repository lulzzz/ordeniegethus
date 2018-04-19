using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models.ViewModels
{
    /// <summary>
    ///     Mapping utility for converting between domain object and view model
    /// </summary>
    /// <typeparam name="TInput">the input object, e.g. ApplicationUser</typeparam>
    /// <typeparam name="TOutput">the output object, e.g. UserViewModel</typeparam>
    public abstract class Mapper<TInput, TOutput>
    {
        public abstract IEnumerable<TOutput> MapToEnumerable(IEnumerable<TInput> inputs);

        public abstract TOutput Map(TInput input);

        public IEnumerable<TOutput> Map(IEnumerable<TInput> inputs)
        {
            var output = new List<TOutput>();
            foreach(var input in inputs)
            {
                output.Add(Map(input));
            }
            return output;
        }
    }
}