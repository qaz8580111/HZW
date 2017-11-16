using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.BLL
{
    public class UserTypeBLL
    {
        private UserTypeDAL dal = new UserTypeDAL();

        public List<UserType> GetUserTypes()
        {
            return dal.GetUserTypes();
        }

        public Paging<List<UserType>> GetUserTypes(int start, int limit)
        {
            List<UserType> items = dal.GetUserTypes(start, limit);
            int total = dal.GetUserTypeCount();

            Paging<List<UserType>> paging = new Paging<List<UserType>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        public void AddUserType(UserType userType)
        {
            dal.AddUserType(userType);
        }

        public void EditUserType(UserType userType)
        {
            dal.EditUserType(userType);
        }

        public void DeleteUserType(int id)
        {
            dal.DeleteUserType(id);
        }
    }
}
