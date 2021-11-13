using MobiQu.Services.Application.Common.Enums;
using MobiQu.Services.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Models.Responses
{
    public class ResponseModel<TModel> where TModel : class, new()
    {
        public DateTime ResponseDateTime { get; set; }

        public bool IsSuccessFull { get; set; }

        public HttpResponseType ResponseType { get; set; }

        public string ResponseMessage { get; set; }

        public TModel ResponseValue { get; set; }
        public ResponseModel(string responseMessage = null)
        {
            if(responseMessage is null)
            {
                responseMessage = "İşlem Başarıyla Gerçekleşti";
            }
        }
    }
}
