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
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;

        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailDialog();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDetailDialog()
        {
            ContactDetails cd = new ContactDetails();
            cd.ShowDialog(this);
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        public void PopulateContacts(string searchText = null) 
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }


        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString() == "Edit")
            {
                ContactDetails cd = new ContactDetails();
                cd.LoadContact(new Contact 
                { 
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString()

                });
                cd.ShowDialog(this);
            }
            else if(cell.Value.ToString() == "Delete")
            {
                int id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString());
                DeleteContact(id);
                PopulateContacts();
            }

        }
        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
