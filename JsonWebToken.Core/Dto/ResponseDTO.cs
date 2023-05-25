using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.Dto
{
    public class ResponseDTO<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public static ResponseDTO<T> Succcess(T data,int StatusCode)
        {
            return new ResponseDTO<T> { Data = data, StatusCode = StatusCode };
        }
        public static ResponseDTO<T> Succcess(int StatusCode)
        {
            return new ResponseDTO<T> { StatusCode = StatusCode };
        }
        public static ResponseDTO<T> Fail(string Error, int StatusCode)
        {
            return new ResponseDTO<T> { Errors =new List<string>() { Error}, StatusCode = StatusCode };
        }

        public struct ResponseStruct<T>
        {
            public IActionResult ResponsesDTO(ResponseDTO<T> response)
            {
                if(response.StatusCode!=204)
                {
                    return new ObjectResult(response);
                }
                return null;
            }
        }
    }
}
