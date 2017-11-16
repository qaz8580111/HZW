using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.AddressBookBLLS
{
    public class AddressBookBLL
    {
        /// <summary>
        /// 获取分组下的联系人
        /// </summary>
        /// <param name="contactGroupId">所属通讯录组标识</param>
        /// <returns></returns>
        public static IQueryable<CONTACT> GetContactsByContactGroupId(decimal? contactGroupId
            , decimal userId)
        {
            PLEEntities db = new PLEEntities();
            var results =
                 from contacts in db.CONTACTS
                 from contactsgroups in db.CONTACTSGROUPS
                 where contacts.CONTACTGROUPID == contactGroupId
                 && contactsgroups.CREATEDUSERID == userId
                 && contactsgroups.CONTACTSGROUPID == contacts.CONTACTGROUPID
                 select contacts;
            return results;
        }
    }
}
