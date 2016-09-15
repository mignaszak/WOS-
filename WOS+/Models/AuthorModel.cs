using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOS_.Models
{
    public class AuthorModel
    {
        string _Name;
        string _Surname;
        string _Organization;
        string _Id;

        public string Organization
        {
            get
            {
                return _Organization;
            }

            set
            {
                _Organization = value;
            }
        }

        public string Surname
        {
            get
            {
                return _Surname;
            }

            set
            {
                _Surname = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}, {2}", _Name, _Surname, _Organization);
            }
        }
    }
}