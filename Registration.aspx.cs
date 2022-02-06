using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions; // for Regular expression
using System.Drawing; // for change of color
using System.Configuration;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Assignment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string MYDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private int checkPassword(string password)
        {
            int score = 0;


            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[$&+,:;=?@#|'<>.^*()%!-]"))
            {
                score++;
            }
            return score;
        }

        protected void btn_reg_Click(object sender, EventArgs e)
        {
            int scores = checkPassword(tb_pwd.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }
            lbl_pwd.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwd.ForeColor = Color.Red;
                return;
            }
            lbl_pwd.ForeColor = Color.Green;

            //string pwd = get value from your Textbox
            string pwd = tb_pwd.Text.ToString().Trim(); ;
            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];
            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);
            SHA512Managed hashing = new SHA512Managed();
            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            finalHash = Convert.ToBase64String(hashWithSalt);
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            createAccount();

            Response.Redirect("Login.aspx", false);

        }
        protected void createAccount()
        {
            try
            {
                int m = photo.PostedFile.ContentLength;
                byte[] p = new byte[m];
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@fname, @lname, " +
                        "@creditcard, @email, @passwordhash, @dob, @photo, @passwordsalt, @IV, @Key)"))
                {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@fname", tb_firstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@lname", tb_lastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@creditcard", Convert.ToBase64String(encryptData(tb_creditCard.Text.Trim())));
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Parameters.AddWithValue("@email", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@passwordhash", finalHash);
                            cmd.Parameters.AddWithValue("@passwordsalt", salt);
                            cmd.Parameters.AddWithValue("@dob", tb_DOB.Text.Trim());
                            cmd.Parameters.AddWithValue("@photo", p);
                            
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

    }
}