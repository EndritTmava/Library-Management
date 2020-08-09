﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentValidation.Results;
using MenaxhimiBibliotekes.BLL;
using MenaxhimiBibliotekes.BLL.Validate;
using MenaxhimiBibliotekes.BO;


namespace MenaxhimiBibliotekes.Members_Forms
{
    public partial class AddMemberForm : Form
    {
        public AddMemberForm()
        {
            InitializeComponent();
        }

        Subscriber subscriber = new Subscriber();
        SubscriberBLL subscriberBLL = new SubscriberBLL();
        string subscriptionPlanVariable;

        Bill bill = new Bill();
        BillBLL billBLL = new BillBLL();

        Notification notification = new Notification();
        NotificationBLL notificationBLL = new NotificationBLL();

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                subscriber.Name = txtFirstName.Text;
                subscriber.LastName = txtLastName.Text;
                subscriber.Address = txtFullAddress.Text;

                subscriber.Birthday = dtPickerBirthdate.Value;

                subscriber.PersonalNo = txtPersonalNumber.Text;
                subscriber.Email = txtEmail.Text;
                subscriber.PhoneNo = txtPhoneNumber.Text;
                subscriber.InsDate = DateTime.Parse(txtFromDate.Text);
                subscriber.ExpirationDate = DateTime.Parse(txtTillDate.Text);

                subscriber.IsActive = true;

                subscriber.InsBy = FormLoggedUser.Id;

                if (radioMale.Checked) subscriber.Gender = 'M';
                else subscriber.Gender = 'F';

                SubscriberValidation subscriberValidator = new SubscriberValidation();
                subscriberValidator.subscriber = subscriber;
                subscriberValidator.ValidateSubscriber();
                ValidationResult results = subscriberValidator.Validate(subscriber);

                if (results.IsValid == false)
                {
                    foreach (ValidationFailure failure in results.Errors)
                    {
                        MessageBox.Show($"{failure.ErrorMessage}", "Error Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    subscriberBLL.Add(subscriber);
                    //MessageBox.Show("The subscriber is registered successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    subscriberBLL.SubscriberRegisterdEmail(subscriber);

                    #region BILL///////////////////////////////////////////////////////////////////////////////////////////
                    bill.BillingDate = DateTime.Now;
                    bill.RegistrationDate = subscriber.InsDate;
                    bill.ExpirationDate = subscriber.ExpirationDate;

                    if (subscriber.ExpirationDate.Month == subscriber.InsDate.AddMonths(1).Month)
                    {
                        bill.Price = 10;//10euro per Muaj
                    }

                    if (subscriber.ExpirationDate.Year == subscriber.InsDate.AddYears(1).Year)
                    {
                        bill.Price = 100;//100euro per Vit
                    }

                    bill.BillTypeId = 1;
                    bill.IsActive = true;
                    bill.InsBy = FormLoggedUser.Id;

                    BillValidation billValidator = new BillValidation();
                    billValidator.bill = bill;
                    billValidator.ValidateBill();
                    ValidationResult resultsBill = billValidator.Validate(bill);

                    if (resultsBill.IsValid == false)
                    {
                        foreach (ValidationFailure failure in resultsBill.Errors)
                        {
                            MessageBox.Show($"{failure.ErrorMessage}", "Error Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        bill.SubscriberId = subscriberBLL.MaxSubscriberId();
                        billBLL.Add(bill);
                        MessageBox.Show("The Subscriber is registered successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        notification.Message = $"Member {subscriber.Name} {subscriber.LastName} is registered from date {subscriber.InsDate} till {subscriber.ExpirationDate}";
                        notification.Date = DateTime.Now;
                        notification.Category = "Subscriber Notification";

                        notificationBLL.Add(notification);
                    }



                    #endregion
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbSubscriptionPlan(object sender, EventArgs e)
        {
            try
            {
                if (comboSubscriptionPlan.SelectedIndex == 0)
                {
                    txtFromDate.Text = DateTime.Now.ToShortDateString();

                    txtTillDate.Text = DateTime.Parse(txtFromDate.Text).AddYears(1).ToShortDateString();

                    subscriptionPlanVariable = "Yearly";
                }

                if (comboSubscriptionPlan.SelectedIndex == 1)
                {
                    txtFromDate.Text = DateTime.Now.ToShortDateString();

                    txtTillDate.Text = DateTime.Parse(txtFromDate.Text).AddMonths(1).ToShortDateString();

                    subscriptionPlanVariable = "Monthly";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {

            CookieSubscriber.ID = subscriberBLL.MaxSubscriberId();
            CookieSubscriber.FullName = subscriber.Name + " " + subscriber.LastName;
            CookieSubscriber.PersonalNumber = subscriber.PersonalNo;
            CookieSubscriber.SubscriptionPlan = subscriptionPlanVariable;
            CookieSubscriber.BillingDate = bill.BillingDate;
            CookieSubscriber.ExpirationDate = bill.ExpirationDate;
            CookieSubscriber.Price = bill.Price;
            CookieSubscriber.Cashier = FormLoggedUser.Name + "\t " + FormLoggedUser.LastName;

            BillingAddMember billingAddMember = new BillingAddMember();
            billingAddMember.ShowDialog();
        }
    }
}
    

