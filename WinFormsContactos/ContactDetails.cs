using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContactos
{
    public partial class ContactDetails : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();

        }

        private void SaveContact()
        {
            Contact c = new Contact();
            c.FirstName = txtFirst.Text;
            c.LastName = txtLast.Text;
            c.Phone = txtPhone.Text;
            c.Address = txtAddress.Text;

            c.Id = _contact != null ? _contact.Id : 0;

            _businessLogicLayer.SaveContact(c);
        }

        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                clearForm();

                txtFirst.Text = contact.FirstName;
                txtLast.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;

            }
        }

        private void clearForm()
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
    }
}
