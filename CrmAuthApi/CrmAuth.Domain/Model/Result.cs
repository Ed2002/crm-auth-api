using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAuth.Domain.Model
{
    public class Result<T>
    {
        public ResultModel<T> CreateSucess(T model)
        {
            ResultModel<T> result = new()
            {
                Model = model,
                Menssages = new List<string>(),
                Success = true,
            };
            return result;
        }

        public ResultModel<T> CreateErro(string Menssage)
        {
            ResultModel<T> result = new()
            {
                Model = default(T),
                Menssages = new List<string>()
                {
                    Menssage
                },
                Success = false,
            };
            return result;
        }
    }
}
