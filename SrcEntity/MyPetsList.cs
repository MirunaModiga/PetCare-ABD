﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myPetCare.Properties;

namespace myPetCare
{
    public partial class MyPetsList : Form
    {
        string user;
        int userID = 0;
        PetCareEntities context = new PetCareEntities();

        public MyPetsList(string user)
        {
            InitializeComponent();
            this.user = user;
            label2.Text = "Welcome,\n"+user.ToString();
            userID = this.find_id();

            var count = (from pet_obj in context.Pets
                        where pet_obj.C_OwnerID == userID
                        select pet_obj).Count();

            label3.Text = "You have registered "+count.ToString()+" pets!";
        }

        private void MyPetsList_Load(object sender, EventArgs e)
        {
            populateItems();
        }

        private void populateItems()
        {
            var pets = from pet_obj in context.Pets
                         where pet_obj.C_OwnerID == userID
                         select pet_obj;

            ListPetItem[] listItems = new ListPetItem[pets.Count()];

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListPetItem();
                listItems[i].Icon = setIcon(pets.ToList()[i]);
                listItems[i].Width = flowLayoutPanel1.Width-25;
                listItems[i].IconBackground = Color.DarkSlateGray;
                listItems[i].PetName = pets.ToList()[i].C_petName;
                listItems[i].Description = Make_description(pets.ToList()[i]);

                if (flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                    flowLayoutPanel1.Controls.Add(listItems[i]);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void home_Click(object sender, EventArgs e)
        {
            HomePage home = new HomePage(this.user);
            home.Show();
            this.Close();
        }

        private int find_id()
        {
            int us = (from u in context.Users where u.C_Username == user select new { u.IDUser }).SingleOrDefault().IDUser;
            return us;
        }

        private string Make_description(Pet pets)
        {
            string age = CalculateYourAge(pets.C_birthdate);
            return "-> "+pets.C_petType + ", " + pets.C_sex + "\n-> Breed: " + pets.C_breed+"\n-> Age: " + age +"\n-> Color: " +pets.C_color;
        }

        private Image setIcon(Pet pets)
        {
            if(pets.C_photo!=null)
            {
                return Image.FromFile(pets.C_photo);
            }
            else if (pets.C_petType == "Dog")
            {
                return Resources.dog_icon;
            }
            else if(pets.C_petType == "Cat")
            {
                return Resources.cat_icon;
            }
            else if (pets.C_petType == "Bird")
            {
                return Resources.bird_icon;
            }
            else
            {
                return Resources.other_icon;
            }
        }

        string CalculateYourAge(DateTime Birthdate)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Birthdate).Ticks).Year - 1;
            DateTime PastYearDate = Birthdate.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            return String.Format("Age: {0} Year(s) {1} Month(s)",Years, Months);
        }

    }
}
