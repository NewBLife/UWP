using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo.Models
{
    public class UserModel
    {
        public string GetUserName(string userid)
        {
            return string.Format("取得成功:{0}",userid);
        }
    }
}
