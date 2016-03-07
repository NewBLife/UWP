using MvvmDemo.Common;
using MvvmDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _userId;
        private string _userName;
        private DelegateCommand<string> _loginCommand;
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 登陆命令
        /// </summary>
        public DelegateCommand<string> LoginCommand
        {
            get
            {
                return _loginCommand
                    ??(_loginCommand=new DelegateCommand<string>(
                        s=>
                        {
                            UserName = new UserModel().GetUserName(s); 
                        },
                        s=>!string.IsNullOrEmpty(s)
                        ));
            }
        }
    }
}
