using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ControllerResponse<T>
    {
        public T ResponseData;
        public bool Success { get; set; }
    }
    public class RequestClass
    {
        public string Name { get; set; }
    }
    public class SomeController : BaseController
    {
        private IUserDatabase _userDatabase;
        public SomeController(IUserDatabase userDatabase)
        {
            _userDatabase = userDatabase;
        }
        
        public ControllerResponse<List<string>> GetAllUsers(RequestClass request)
        {
            var response = new ControllerResponse<List<string>>();
            response.Success = true;
            response.ResponseData = _userDatabase.GetAllUsers();
            return response;
        }


        public ControllerResponse<bool> InsertUser(RequestClass request)
        {
            var response = new ControllerResponse<bool>();
            response.Success = true;
            _userDatabase.AddUser(request.Name);
            return response;
        }




    }
}
