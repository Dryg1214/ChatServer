using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Model
{
    internal class UserModel
    {
        public string Username { get; set; }

        public ObservableCollection<MessageModel>? Messages { get; set; }

        public string? LastMessage //=> null ?? Messages.Last().Message;
        {
            get
            {
                if (Messages.Count <= null)
                    return Messages.Last().Message;
                else
                    return " ";
            }
            set
            {
                LastMessage = value;
            }
        }

    }
}
