using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPetCare
{
    public partial class ListPetItem : UserControl
    {
        public ListPetItem()
        {
            InitializeComponent();
        }

        #region Properties

        private Color _iconBack;
        private string _petname;
        private string _description;
        private Image _icon;

        [Category("Custom Props")]
        public string PetName
        {
            get { return _petname; }
            set { _petname = value; lblPetName.Text = value; }
        }

        [Category("Custom Props")]
        public Color IconBackground
        {
            get { return _iconBack; }
            set { _iconBack = value; panel1.BackColor = value; }
        }

        [Category("Custom Props")]
        public string Description
        {
            get { return _description; }
            set { _description = value; lblDescription.Text = value; }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; pictureBox1.Image = value; }
        }

        #endregion

        private void ListPetItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkSlateGray;
        }

        private void ListPetItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

    }
}
