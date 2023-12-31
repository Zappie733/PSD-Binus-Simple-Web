﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAAMEN.Model;
using RAAMEN.Controller;
using RAAMEN.Repository;

namespace RAAMEN.View.Staff
{
    public partial class Profile : System.Web.UI.Page
    {
        public User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //kenapa ini divalidasi lagi padahal di page master sudah? hal ini karena asp.net akan menjalankan kedua page master dan childnya dan jika pada kasus ini page profile ini mencoba mengakses value dari Session padahal bisa saja User nakal dan mencoba masuk ke page ini melalui ketik link yang dimana tentu saja Session belum tercreate dan akan bervalue null dan akan menyebabkan error. oleh karena itu untuk mencegah hal itu terjadi kita tetap harus validasi juga di page ini sehingga jika memang session blm terbuat ya berarti page ini tidak boleh diakses.
                if (Session["staff_session"] == null) 
                {
                    Response.Redirect("../Login.aspx");
                }

                string username = ((User)Session["staff_session"]).Username;
                string password = ((User)Session["staff_session"]).Password;

                user = UserRepository.getUser(username, password);

                idTextBox.Text = user.Id.ToString();
                roleTextBox.Text = UserRepository.getUserRole(user);
                usernameTextBox.Text = user.Username;
                emailTextBox.Text = user.Email;
                genderDropDownList.SelectedValue = user.Gender;
                //passwordTextBox.Text = user.Password;
            }
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            usernameTextBox.Enabled = true;
            emailTextBox.Enabled = true;
            genderDropDownList.Enabled = true;
            passwordTextBox.Enabled = true;
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string email = emailTextBox.Text;
            string gender = genderDropDownList.SelectedValue;
            string password = passwordTextBox.Text;
            string curUsername = ((User)Session["staff_session"]).Username;
            string curPassword = ((User)Session["staff_session"]).Password;

            string message = "";
            message = UserController.checkUpdate(username, email, gender, password, curUsername, curPassword);
            updateMessage.Text = message;

            if (message == "" || message.StartsWith("Your") || message.StartsWith("Nothing"))
            {
                usernameTextBox.Enabled = false;
                emailTextBox.Enabled = false;
                genderDropDownList.Enabled = false;
                passwordTextBox.Enabled = false;

                if (message.StartsWith("Your account(username"))
                {
                    //perbaharui session
                    Session.Clear();
                    //User user = UserFactory.createUserForSession(username, password);
                    User user = UserRepository.getUser(username, password);
                    Session["staff_session"] = user;


                    //perbaharui cookie
                    HttpCookie cookie = Request.Cookies.Get("user_cookie");
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                    }
                    HttpCookie newcookie = new HttpCookie("user_cookie");
                    newcookie.Values.Add("username", username);
                    newcookie.Values.Add("password", password);

                    newcookie.Expires = DateTime.Now.AddMinutes(30);

                    Response.Cookies.Add(newcookie);
                }
            }
        }
    }
}