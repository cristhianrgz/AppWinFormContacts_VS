using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContactos
{
    class BusinessLogicLayer
    {
        private DataAccessLayer _dataAccessLayer;
        public BusinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }
        public Contact SaveContact(Contact c)
        {
            if (c.Id == 0)
                _dataAccessLayer.InsertContact(c);
            else
                _dataAccessLayer.UpdateContact(c);

           return c;
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccessLayer.GetContacts(searchText);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.DeleteContact(id);
        }
    }
}
