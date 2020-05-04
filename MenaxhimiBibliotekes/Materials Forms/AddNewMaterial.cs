﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MenaxhimiBibliotekes.BLL;
using MenaxhimiBibliotekes.BO;

namespace MenaxhimiBibliotekes.Materials_Forms
{
    public partial class AddNewMaterial : Form
    {
        //Global Variables and Instances
        Material material;
        MaterialBLL materialBLL;

        public AddNewMaterial()
        {
            InitializeComponent();
            
        }





        //Customized Methods
        private void MultipleAuthors(string myTextbox, string[] authorsContainer)
        {
            authorsContainer = myTextbox.Split('/');
        }

        private void DisabledByMaterialType (Control title, Control author, Control genre, Control language, Control isbn, Control location, Control publish_house, Control publish_date, Control publish_place, Control quantity, Control pages)
        {
            title.Enabled = false;
            author.Enabled = false;
            genre.Enabled = false;
            language.Enabled = false;
            isbn.Enabled = false;
            location.Enabled = false;
            publish_house.Enabled = false;
            publish_date.Enabled = false;
            publish_place.Enabled = false;
            quantity.Enabled = false;
            pages.Enabled = false;
        }

        private void EnabledByMaterialType(Control title, Control author, Control genre, Control language, Control isbn, Control location, Control publish_house, Control publish_date, Control publish_place, Control quantity, Control pages)
        {
            title.Enabled = true;
            author.Enabled = true;
            genre.Enabled = true;
            language.Enabled = true;
            isbn.Enabled = true;
            location.Enabled = true;
            publish_house.Enabled = true;
            publish_date.Enabled = true;
            publish_place.Enabled = true;
            quantity.Enabled = true;
            pages.Enabled = true;
        }







        //Events
        private void comboMaterialType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboMaterialType.SelectedItem == "Other")
            {
                InsertNewMaterialType materialtypeForm = new InsertNewMaterialType();
                materialtypeForm.ShowDialog();

                DisabledByMaterialType(txtTitle, txtAuthor, comboGenre, comboLanguage, txtISBN, comboMaterialLocation, txtPublishHouse, txtPublishDate, txtPublishPlace, txtQuantity, txtPages);

            }
            else if (comboMaterialType.SelectedItem == "Book")
            {
                EnabledByMaterialType(txtTitle, txtAuthor, comboGenre, comboLanguage, txtISBN, comboMaterialLocation, txtPublishHouse, txtPublishDate, txtPublishPlace, txtQuantity, txtPages);
            }
            else
            {
                EnabledByMaterialType(txtTitle, txtAuthor, comboGenre, comboLanguage, txtISBN, comboMaterialLocation, txtPublishHouse, txtPublishDate, txtPublishPlace, txtQuantity, txtPages);
                
                txtISBN.Enabled = false;
            }
        }

        private void comboGenre_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboGenre.SelectedItem == "Other")
            {
                InsertNewGenre genreForm = new InsertNewGenre();
                genreForm.ShowDialog();
            }
        }

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLanguage.SelectedItem == "Other")
            {
                InsertNewLanguage languageForm = new InsertNewLanguage();
                languageForm.ShowDialog();
            }
        }

        private void AddNewMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtTitle.Text != "" ||
                comboMaterialType.SelectedIndex != -1 ||
                txtAuthor.Text != "" ||
                comboGenre.SelectedIndex != -1 ||
                comboLanguage.SelectedIndex != -1 ||
                txtISBN.Text != "" ||
                txtPublishHouse.Text != "" ||
                txtPublishDate.Text != "" ||
                txtPublishPlace.Text != "" ||
                txtQuantity.Text != "" ||
                txtPages.Text != "")
            {
                DialogResult dialog = MessageBox.Show("You've made some changes in the form. Are you sure you want to close?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            try
            {
                material = new Material();
                materialBLL = new MaterialBLL();

                string authorTextbox = txtAuthor.Text;
                string[] myAuthors = authorTextbox.Split(' ');

                DateTime d = new DateTime();

                //material.Title = txtTitle.Text;
                //material._Author.AuthorName = myAuthors[0];
                //material._Author.AuthorLastName = myAuthors[1];
                //material._Genre._Genre = comboGenre.SelectedItem.ToString();
                //material._Language._Language = comboLanguage.SelectedItem.ToString();
                //material.ISBN = txtISBN.Text;
                                                                ////material._Shelf.Location = comboMaterialLocation.SelectedItem.ToString();
                //material._MaterialType._MaterialType = comboMaterialType.SelectedItem.ToString();
                //material._PublishHouse._PublishHouse = txtPublishHouse.Text;


                

                if (ValidateChildren(ValidationConstraints.Enabled))
                {

                    materialBLL.Add(material);
                    MessageBox.Show("The material is registered successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtTitle_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                txtTitle.Text = $"{lblTitle.Text} should not be empty";
                txtTitle.ForeColor = Color.DarkRed;
            }
        }

        private void txtTitle_Enter_1(object sender, EventArgs e)
        {
            if (txtTitle.Text == $"{lblTitle.Text} should not be empty")
            {
                txtTitle.Text = string.Empty;
                txtTitle.ForeColor = Color.Gray;
            }
        }

        private void txtAuthor_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                txtAuthor.Text = $"{lblAuthor.Text} should not be empty";
                txtAuthor.ForeColor = Color.DarkRed;
            }
        }

        private void txtAuthor_Enter(object sender, EventArgs e)
        {
            if (txtAuthor.Text == $"{lblAuthor.Text} should not be empty")
            {
                txtAuthor.Text = string.Empty;
                txtAuthor.ForeColor = Color.Gray;
            }
        }

        private void txtPublishHouse_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPublishHouse.Text))
            {
                txtPublishHouse.Text = $"{lblPublishHouse.Text} should not be empty";
                txtPublishHouse.ForeColor = Color.DarkRed;
            }
        }

        private void txtPublishHouse_Enter(object sender, EventArgs e)
        {
            if (txtPublishHouse.Text == $"{lblPublishHouse.Text} should not be empty")
            {
                txtPublishHouse.Text = string.Empty;
                txtPublishHouse.ForeColor = Color.Gray;
            }
        }
    }
}