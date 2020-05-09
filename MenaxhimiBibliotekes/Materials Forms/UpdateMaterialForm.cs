﻿using MenaxhimiBibliotekes.BLL;
using MenaxhimiBibliotekes.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.Materials_Forms
{
    public partial class UpdateMaterialForm : Form
    {
        //Global Variables and Instances
        bool openFormG, openFormL, openFormSH, openFormMT;



        Material material;
        List<Material> materialList;

        MaterialBLL mbll;

        MaterialTypeBLL materialtypeBllList;
        List<MaterialType> materialtypeList;
        List<Material> list;


        GenreBLL genreBllList;
        List<Genre> genreList;

        LanguageBLL languageBllList;
        List<Language> languageList;

        ShelfBLL shelfBLLList;
        List<Shelf> shelfList;


        public UpdateMaterialForm()
        {
            InitializeComponent();

            openFormG = openFormL = openFormSH = openFormMT = false;


            material = new Material();
             mbll = new MaterialBLL();
            list = mbll.GetAll();


            //Combobox GENRE fill
            comboGenre.Items.Clear();
            genreBllList = new GenreBLL();
            Genre g = new Genre();
            genreList = genreBllList.GetAll();
            g = genreList[0];
            genreList[0] = new Genre() { GenreId = 0, _Genre = "Other" };
            genreList.Add(g);
            comboGenre.DataSource = genreList;
            comboGenre.DisplayMember = "_Genre";


            //Combobox MATERIAL TYPE fill
            comboMaterialType.Items.Clear();
            materialtypeBllList = new MaterialTypeBLL();
            MaterialType mt = new MaterialType();
            materialtypeList = materialtypeBllList.GetAll();
            mt = materialtypeList[0];
            materialtypeList[0] = new MaterialType() { MaterialTypeId = 0, _MaterialType = "Other" };
            materialtypeList.Add(mt);
            comboMaterialType.DataSource = materialtypeList;
            comboMaterialType.DisplayMember = "_MaterialType";


            //Combobox LANGUAGES fill
            comboLanguage.Items.Clear();
            languageBllList = new LanguageBLL();
            Language l = new Language();
            languageList = languageBllList.GetAll();
            l = languageList[0];
            languageList[0] = new Language() { LanguageId = 0, _Language = "Other" };
            comboLanguage.DataSource = languageList;
            comboLanguage.DisplayMember = "_Language";


            //Combobox MATERIAL LOCATION fill
            comboMaterialLocation.Items.Clear();
            shelfBLLList = new ShelfBLL();
            Shelf sh = new Shelf();
            shelfList = shelfBLLList.GetAll();
            sh = shelfList[0];
            shelfList[0] = new Shelf() { ShelfId = 0, Location = "Other" };
            comboMaterialLocation.DataSource = shelfList;
            comboMaterialLocation.DisplayMember = "Location";
        }










        //Customized Methods
        private void MultipleAuthors(string myTextbox, string[] authorsContainer)
        {
            authorsContainer = myTextbox.Split('/');
        }

        private void DisabledByMaterialType(Control title, Control author, Control genre, Control language, Control isbn, Control location, Control publish_house, Control publish_date, Control publish_place, Control quantity, Control pages)
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

        public Shelf getShelf()
        {
            return comboMaterialLocation.SelectedItem as Shelf;
        }

        public MaterialType getMaterialType()
        {
            return comboMaterialType.SelectedItem as MaterialType;
        }

        public Language getLanguange()
        {
            return comboLanguage.SelectedItem as Language;
        }

        public Genre getGenre()
        {
            return comboGenre.SelectedItem as Genre;
        }









        //Events
        private void comboGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Genre cg = comboGenre.SelectedItem as Genre;

            if (cg.GenreId == 0)
            {
                if (openFormG)
                {
                    InsertNewGenre genreForm = new InsertNewGenre();
                    genreForm.ShowDialog();
                }
                openFormG = true;

            }
        }

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language cl = comboLanguage.SelectedItem as Language;

            if (cl.LanguageId == 0)
            {
                if (openFormL)
                {
                    InsertNewLanguage languageForm = new InsertNewLanguage();
                    languageForm.ShowDialog();
                }
                openFormL = true;
            }
        }

        private void comboMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaterialType cmt = comboMaterialType.SelectedItem as MaterialType;

            if (cmt.MaterialTypeId == 0)
            {
                if (openFormMT)
                {
                    InsertNewMaterialType materialtypeForm = new InsertNewMaterialType();
                    materialtypeForm.ShowDialog();

                    DisabledByMaterialType(txtTitle, txtAuthor, comboGenre, comboLanguage, txtISBN, comboMaterialLocation, txtPublishHouse, txtPublishDate, txtPublishPlace, txtQuantity, txtPages);
                }
                openFormMT = true;
            }
            else
            {
                EnabledByMaterialType(txtTitle, txtAuthor, comboGenre, comboLanguage, txtISBN, comboMaterialLocation, txtPublishHouse, txtPublishDate, txtPublishPlace, txtQuantity, txtPages);
            }
        }

        private void comboMaterialLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Shelf cml = comboMaterialLocation.SelectedItem as Shelf;

            if (cml.ShelfId == 0)
            {
                if (openFormSH)
                {
                    MaterialLocation locationForm = new MaterialLocation();
                    locationForm.ShowDialog();
                }
                openFormSH = true;
            }
        }

        private void UpdateMaterialForm_FormClosing(object sender, FormClosingEventArgs e)
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

        




        private void btnUpdate_Click(object sender, EventArgs e)
        {



            //materialList = materialBLL.GetAll();


        }

        
        private void txtMaterialID_TextChanged(object sender, EventArgs e)
        {
            MaterialBLL mbll = new MaterialBLL();
            List<Material> list = mbll.GetAll();

            material = mbll.Get(Convert.ToInt32(txtMaterialID.Text));




        }
    }
}
